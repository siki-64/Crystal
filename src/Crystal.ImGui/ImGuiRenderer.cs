using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Crystal.Graphics;
using Crystal.Input;

namespace Crystal.ImGui;

public sealed unsafe class ImGuiRenderer : IDisposable
{
    private const string ShaderResourceName = "Shaders/ImGui.crystalshader";
    public const nint ResetRenderStateCallback = -8;

    private static readonly BlendState ImGuiBlend = new(new RenderTargetBlendDescription
    {
        BlendEnable = true,
        SourceBlend = Blend.SourceAlpha,
        DestinationBlend = Blend.InverseSourceAlpha,
        SourceBlendAlpha = Blend.One,
        DestinationBlendAlpha = Blend.InverseSourceAlpha,
    });

    private static readonly VertexLayout DrawVertexLayout = new(
        new VertexElement("POSITION", 0, VertexElementFormat.Vector2, 0),
        new VertexElement("TEXCOORD", 0, VertexElementFormat.Vector2, 8),
        new VertexElement("COLOR", 0, VertexElementFormat.Color, 16));

    private static readonly (Keys Key, ImGuiKey ImGuiKey)[] KeyMap = BuildKeyMap();

    private readonly Game _game;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly ShaderLibrary _shaderLibrary;
    private readonly GraphicsPipeline _pipeline;
    private readonly ShaderParameter _projection;
    private readonly ShaderParameter _texture;
    private readonly ShaderParameter _sampler;
    private readonly SamplerState _linearClamp;
    private readonly ImGuiContext* _context;
    private readonly Dictionary<ulong, Texture2D> _textures = [];
    private readonly HashSet<ulong> _ownedTextures = [];
    private ulong _nextTextureId = 1;
    private byte[] _uploadScratch = [];
    private int _previousScrollWheelValue;
    private int _previousHorizontalScrollWheelValue;
    private bool _isTextInputRequested;
    private byte* _iniFilePath;
    private bool _isDisposed;

    public ImGuiRenderer(Game game)
        : this(game, game.GraphicsDevice.BackBufferFormat)
    {
    }

    public ImGuiRenderer(Game game, SurfaceFormat renderTargetFormat, SurfaceFormat? depthStencilFormat = null, int sampleCount = 1)
    {
        ArgumentNullException.ThrowIfNull(game);
        _game = game;
        _graphicsDevice = game.GraphicsDevice;

        using (var stream = typeof(ImGuiRenderer).Assembly.GetManifestResourceStream(ShaderResourceName)
            ?? throw new InvalidOperationException($"The embedded shader '{ShaderResourceName}' is missing."))
        {
            _shaderLibrary = new ShaderLibrary(_graphicsDevice, stream, "ImGui");
        }
        var program = _shaderLibrary.GetProgram("ImGui");
        _projection = program.Parameters["ProjectionMatrix"]!;
        _texture = program.Parameters["FontTexture"]!;
        _sampler = program.Parameters["FontSampler"]!;
        _linearClamp = _graphicsDevice.GetSamplerState(SamplerDescription.LinearClamp);
        _pipeline = new GraphicsPipeline(_graphicsDevice, new GraphicsPipelineDescription
        {
            Program = program,
            BlendState = ImGuiBlend,
            RasterizerState = RasterizerState.CullNone,
            DepthStencilState = DepthStencilState.None,
            VertexLayout = DrawVertexLayout,
            RenderTargetFormats = [renderTargetFormat],
            DepthStencilFormat = depthStencilFormat,
            SampleCount = sampleCount,
            Name = "ImGui",
        });

        _context = ImGui.CreateContext();
        ImGui.SetCurrentContext(_context);
        var io = ImGui.GetIO();
        io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset | ImGuiBackendFlags.RendererHasTextures;
        io.BackendRendererName = ImGuiUtf8.Literal("Crystal.ImGui\0"u8);
        io.IniFilename = null;
        io.Fonts.AddFontDefault();

        game.Window.TextInput += OnTextInput;
    }

    public ImGuiContext* Context => _context;

    public bool WantCaptureMouse
    {
        get
        {
            ImGui.SetCurrentContext(_context);
            return ImGui.GetIO().WantCaptureMouse;
        }
    }

    public bool WantCaptureKeyboard
    {
        get
        {
            ImGui.SetCurrentContext(_context);
            return ImGui.GetIO().WantCaptureKeyboard;
        }
    }

    public bool WantTextInput
    {
        get
        {
            ImGui.SetCurrentContext(_context);
            return ImGui.GetIO().WantTextInput;
        }
    }

    public bool DockingEnabled
    {
        get
        {
            ImGui.SetCurrentContext(_context);
            return (ImGui.GetIO().ConfigFlags & ImGuiConfigFlags.DockingEnable) != 0;
        }
        set
        {
            ImGui.SetCurrentContext(_context);
            var io = ImGui.GetIO();
            if (value) io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            else io.ConfigFlags &= ~ImGuiConfigFlags.DockingEnable;
        }
    }

    public string? IniFilePath
    {
        get
        {
            ImGui.SetCurrentContext(_context);
            return ImGuiUtf8.ToString(ImGui.GetIO().IniFilename);
        }
        set
        {
            ImGui.SetCurrentContext(_context);
            var previous = _iniFilePath;
            _iniFilePath = null;
            if (value != null)
            {
                var byteCount = Encoding.UTF8.GetByteCount(value);
                _iniFilePath = (byte*)NativeMemory.Alloc((nuint)byteCount + 1);
                Encoding.UTF8.GetBytes(value, new Span<byte>(_iniFilePath, byteCount));
                _iniFilePath[byteCount] = 0;
            }
            ImGui.GetIO().IniFilename = _iniFilePath;
            if (previous != null) NativeMemory.Free(previous);
        }
    }

    public ImTextureRef BindTexture(Texture2D texture)
    {
        ArgumentNullException.ThrowIfNull(texture);
        var id = _nextTextureId++;
        _textures[id] = texture;
        return new ImTextureRef { _TexID = id };
    }

    public void UnbindTexture(ImTextureRef textureRef)
    {
        if (!_ownedTextures.Contains(textureRef._TexID)) _textures.Remove(textureRef._TexID);
    }

    public void BeginFrame(GameTime gameTime)
    {
        ArgumentNullException.ThrowIfNull(gameTime);
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        ImGui.SetCurrentContext(_context);
        var io = ImGui.GetIO();
        io.DisplaySize = new Vector2(_graphicsDevice.BackBufferWidth, _graphicsDevice.BackBufferHeight);
        io.DisplayFramebufferScale = Vector2.One;
        var elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        io.DeltaTime = elapsedSeconds > 0f ? elapsedSeconds : 1f / 60f;

        var mouse = Mouse.GetState();
        io.AddFocusEvent(_game.IsActive);
        io.AddMousePosEvent(mouse.X, mouse.Y);
        io.AddMouseButtonEvent(0, mouse.LeftButton == ButtonState.Pressed);
        io.AddMouseButtonEvent(1, mouse.RightButton == ButtonState.Pressed);
        io.AddMouseButtonEvent(2, mouse.MiddleButton == ButtonState.Pressed);
        io.AddMouseButtonEvent(3, mouse.XButton1 == ButtonState.Pressed);
        io.AddMouseButtonEvent(4, mouse.XButton2 == ButtonState.Pressed);
        var wheelX = mouse.HorizontalScrollWheelValue - _previousHorizontalScrollWheelValue;
        var wheelY = mouse.ScrollWheelValue - _previousScrollWheelValue;
        if (wheelX != 0 || wheelY != 0) io.AddMouseWheelEvent(wheelX / (float)Mouse.WheelDelta, wheelY / (float)Mouse.WheelDelta);
        _previousScrollWheelValue = mouse.ScrollWheelValue;
        _previousHorizontalScrollWheelValue = mouse.HorizontalScrollWheelValue;

        var keyboard = Keyboard.GetState();
        io.AddKeyEvent(ImGuiKey.ModCtrl, keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl));
        io.AddKeyEvent(ImGuiKey.ModShift, keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift));
        io.AddKeyEvent(ImGuiKey.ModAlt, keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt));
        io.AddKeyEvent(ImGuiKey.ModSuper, keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows));
        foreach (var (key, imGuiKey) in KeyMap) io.AddKeyEvent(imGuiKey, keyboard.IsKeyDown(key));

        ImGui.NewFrame();
    }

    public void EndFrame()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        ImGui.SetCurrentContext(_context);
        ImGui.Render();
        UpdateTextInput(ImGui.GetIO().WantTextInput);
        var drawData = ImGui.GetDrawData();
        UpdateTextures(drawData);
        Draw(drawData);
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;

        _game.Window.TextInput -= OnTextInput;
        UpdateTextInput(false);
        ImGui.SetCurrentContext(_context);
        var platformTextures = ImGui.GetPlatformIO().Textures;
        for (var index = 0; index < platformTextures.Size; index++)
        {
            ImTextureDataPtr texture = platformTextures[index];
            if (texture.RefCount == 1) DestroyTexture(texture);
        }
        ImGui.DestroyContext(_context);
        if (_iniFilePath != null)
        {
            NativeMemory.Free(_iniFilePath);
            _iniFilePath = null;
        }

        foreach (var id in _ownedTextures) _textures[id].Dispose();
        _ownedTextures.Clear();
        _textures.Clear();
        _pipeline.Dispose();
        _shaderLibrary.Dispose();
    }

    private void OnTextInput(object? sender, TextInputEventArgs input)
    {
        if (char.IsControl(input.Character)) return;
        ImGui.SetCurrentContext(_context);
        ImGui.GetIO().AddInputCharacterUTF16(input.Character);
    }

    private void UpdateTextInput(bool wantTextInput)
    {
        if (wantTextInput == _isTextInputRequested) return;
        _isTextInputRequested = wantTextInput;
        _game.Window.IsTextInputEnabled = wantTextInput;
    }

    private void UpdateTextures(ImDrawDataPtr drawData)
    {
        if (drawData.Textures == null) return;
        for (var index = 0; index < drawData.Textures->Size; index++)
        {
            ImTextureDataPtr texture = (*drawData.Textures)[index];
            switch (texture.Status)
            {
                case ImTextureStatus.WantCreate:
                    CreateTexture(texture);
                    break;
                case ImTextureStatus.WantUpdates:
                    UpdateTexture(texture);
                    break;
                case ImTextureStatus.WantDestroy when texture.UnusedFrames > 0:
                    DestroyTexture(texture);
                    break;
            }
        }
    }

    private void CreateTexture(ImTextureDataPtr texture)
    {
        var created = new Texture2D(_graphicsDevice, new TextureDescription { Width = texture.Width, Height = texture.Height, Name = "ImGuiTexture" });
        var id = _nextTextureId++;
        _textures[id] = created;
        _ownedTextures.Add(id);
        UploadRegion(texture, created, 0, 0, texture.Width, texture.Height);
        texture.SetTexID(id);
        texture.SetStatus(ImTextureStatus.OK);
    }

    private void UpdateTexture(ImTextureDataPtr texture)
    {
        if (!_textures.TryGetValue(texture.GetTexID(), out var target))
        {
            CreateTexture(texture);
            return;
        }

        var updates = texture.Updates;
        if (updates.Size == 0)
        {
            var region = texture.UpdateRect;
            UploadRegion(texture, target, region.x, region.y, region.w, region.h);
        }
        for (var index = 0; index < updates.Size; index++)
        {
            var region = updates[index];
            UploadRegion(texture, target, region.x, region.y, region.w, region.h);
        }
        texture.SetStatus(ImTextureStatus.OK);
    }

    private void DestroyTexture(ImTextureDataPtr texture)
    {
        var id = texture.GetTexID();
        if (_ownedTextures.Remove(id) && _textures.Remove(id, out var owned)) owned.Dispose();
        texture.SetTexID(0);
        texture.SetStatus(ImTextureStatus.Destroyed);
    }

    private void UploadRegion(ImTextureDataPtr texture, Texture2D target, int x, int y, int width, int height)
    {
        if (width <= 0 || height <= 0) return;

        var byteCount = width * height * 4;
        if (_uploadScratch.Length < byteCount) _uploadScratch = new byte[byteCount];

        var bytesPerPixel = texture.BytesPerPixel;
        var sourceStride = texture.Width * bytesPerPixel;
        var destination = 0;
        for (var row = 0; row < height; row++)
        {
            var source = texture.Pixels + (y + row) * sourceStride + x * bytesPerPixel;
            if (texture.Format == ImTextureFormat.RGBA32)
            {
                new ReadOnlySpan<byte>(source, width * 4).CopyTo(_uploadScratch.AsSpan(destination));
                destination += width * 4;
                continue;
            }
            for (var column = 0; column < width; column++)
            {
                _uploadScratch[destination++] = 255;
                _uploadScratch[destination++] = 255;
                _uploadScratch[destination++] = 255;
                _uploadScratch[destination++] = source[column];
            }
        }

        target.SetData(new ReadOnlySpan<byte>(_uploadScratch, 0, byteCount), new Rectangle(x, y, width, height));
    }

    private void Draw(ImDrawDataPtr drawData)
    {
        if (drawData.CmdLists.Size == 0 || drawData.TotalVtxCount == 0) return;

        var displayPosition = drawData.DisplayPos;
        var displaySize = drawData.DisplaySize;
        var framebufferScale = drawData.FramebufferScale;
        var framebufferWidth = (int)(displaySize.X * framebufferScale.X);
        var framebufferHeight = (int)(displaySize.Y * framebufferScale.Y);
        if (framebufferWidth <= 0 || framebufferHeight <= 0) return;

        var viewport = _graphicsDevice.Viewport;
        var scissorRectangle = _graphicsDevice.ScissorRectangle;
        SetRenderState(drawData, framebufferWidth, framebufferHeight);

        for (var listIndex = 0; listIndex < drawData.CmdLists.Size; listIndex++)
        {
            var drawList = drawData.CmdLists[listIndex];
            if (drawList->VtxBuffer.Size == 0 || drawList->IdxBuffer.Size == 0) continue;
            _graphicsDevice.SetVertexData<ImDrawVert>(drawList->VtxBuffer.AsSpan());
            _graphicsDevice.SetIndexData((ReadOnlySpan<ushort>)drawList->IdxBuffer.AsSpan());

            for (var commandIndex = 0; commandIndex < drawList->CmdBuffer.Size; commandIndex++)
            {
                ref var command = ref drawList->CmdBuffer[commandIndex];
                if (command.UserCallback != null)
                {
                    if ((nint)command.UserCallback == ResetRenderStateCallback) SetRenderState(drawData, framebufferWidth, framebufferHeight);
                    else ((delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void>)command.UserCallback)(drawList, (ImDrawCmd*)Unsafe.AsPointer(ref command));
                    continue;
                }
                if (command.ElemCount == 0) continue;

                var textureId = command.TexRef._TexData != null ? command.TexRef._TexData->TexID : command.TexRef._TexID;
                if (!_textures.TryGetValue(textureId, out var texture)) continue;

                var clip = command.ClipRect;
                var left = (int)MathF.Max(0f, (clip.X - displayPosition.X) * framebufferScale.X);
                var top = (int)MathF.Max(0f, (clip.Y - displayPosition.Y) * framebufferScale.Y);
                var right = (int)MathF.Min(framebufferWidth, (clip.Z - displayPosition.X) * framebufferScale.X);
                var bottom = (int)MathF.Min(framebufferHeight, (clip.W - displayPosition.Y) * framebufferScale.Y);
                if (right <= left || bottom <= top) continue;

                _graphicsDevice.ScissorRectangle = new Rectangle(left, top, right - left, bottom - top);
                _texture.SetValue(texture);
                _graphicsDevice.DrawIndexed((int)command.ElemCount, (int)command.IdxOffset, (int)command.VtxOffset);
            }
        }

        _texture.SetValue((Texture?)null);
        _graphicsDevice.Viewport = viewport;
        _graphicsDevice.ScissorRectangle = scissorRectangle;
    }

    private void SetRenderState(ImDrawDataPtr drawData, int framebufferWidth, int framebufferHeight)
    {
        var displayPosition = drawData.DisplayPos;
        var displaySize = drawData.DisplaySize;
        _graphicsDevice.Viewport = new Viewport(0, 0, framebufferWidth, framebufferHeight);
        _graphicsDevice.SetPipeline(_pipeline);
        _projection.SetValue(Matrix4x4.CreateOrthographicOffCenter(
            displayPosition.X, displayPosition.X + displaySize.X, displayPosition.Y + displaySize.Y, displayPosition.Y, -1f, 1f));
        _sampler.SetValue(_linearClamp);
    }

    private static (Keys, ImGuiKey)[] BuildKeyMap()
    {
        var map = new List<(Keys, ImGuiKey)>
        {
            (Keys.Tab, ImGuiKey.Tab), (Keys.Left, ImGuiKey.LeftArrow), (Keys.Right, ImGuiKey.RightArrow),
            (Keys.Up, ImGuiKey.UpArrow), (Keys.Down, ImGuiKey.DownArrow), (Keys.PageUp, ImGuiKey.PageUp),
            (Keys.PageDown, ImGuiKey.PageDown), (Keys.Home, ImGuiKey.Home), (Keys.End, ImGuiKey.End),
            (Keys.Insert, ImGuiKey.Insert), (Keys.Delete, ImGuiKey.Delete), (Keys.Back, ImGuiKey.Backspace),
            (Keys.Space, ImGuiKey.Space), (Keys.Enter, ImGuiKey.Enter), (Keys.Escape, ImGuiKey.Escape),
            (Keys.LeftControl, ImGuiKey.LeftCtrl), (Keys.LeftShift, ImGuiKey.LeftShift), (Keys.LeftAlt, ImGuiKey.LeftAlt),
            (Keys.LeftWindows, ImGuiKey.LeftSuper), (Keys.RightControl, ImGuiKey.RightCtrl), (Keys.RightShift, ImGuiKey.RightShift),
            (Keys.RightAlt, ImGuiKey.RightAlt), (Keys.RightWindows, ImGuiKey.RightSuper), (Keys.Apps, ImGuiKey.Menu),
            (Keys.OemQuotes, ImGuiKey.Apostrophe), (Keys.OemComma, ImGuiKey.Comma), (Keys.OemMinus, ImGuiKey.Minus),
            (Keys.OemPeriod, ImGuiKey.Period), (Keys.OemQuestion, ImGuiKey.Slash), (Keys.OemSemicolon, ImGuiKey.Semicolon),
            (Keys.OemPlus, ImGuiKey.Equal), (Keys.OemOpenBrackets, ImGuiKey.LeftBracket), (Keys.OemPipe, ImGuiKey.Backslash),
            (Keys.OemCloseBrackets, ImGuiKey.RightBracket), (Keys.OemTilde, ImGuiKey.GraveAccent), (Keys.OemBackslash, ImGuiKey.Oem102),
            (Keys.CapsLock, ImGuiKey.CapsLock), (Keys.Scroll, ImGuiKey.ScrollLock), (Keys.NumLock, ImGuiKey.NumLock),
            (Keys.PrintScreen, ImGuiKey.PrintScreen), (Keys.Pause, ImGuiKey.Pause),
            (Keys.Decimal, ImGuiKey.KeypadDecimal), (Keys.Divide, ImGuiKey.KeypadDivide), (Keys.Multiply, ImGuiKey.KeypadMultiply),
            (Keys.Subtract, ImGuiKey.KeypadSubtract), (Keys.Add, ImGuiKey.KeypadAdd), (Keys.NumPadEnter, ImGuiKey.KeypadEnter),
            (Keys.D0, ImGuiKey.D0), (Keys.NumPad0, ImGuiKey.Keypad0),
        };

        for (var offset = 0; offset < 9; offset++)
        {
            map.Add((Keys.D1 + offset, ImGuiKey.D1 + offset));
            map.Add((Keys.NumPad1 + offset, ImGuiKey.Keypad1 + offset));
        }
        for (var offset = 0; offset < 26; offset++) map.Add((Keys.A + offset, ImGuiKey.A + offset));
        for (var offset = 0; offset < 12; offset++)
        {
            map.Add((Keys.F1 + offset, ImGuiKey.F1 + offset));
            map.Add((Keys.F13 + offset, ImGuiKey.F13 + offset));
        }
        return [.. map];
    }
}
