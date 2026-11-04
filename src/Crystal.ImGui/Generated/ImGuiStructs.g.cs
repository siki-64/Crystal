using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Crystal.ImGui;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImColor
{
    public Vector4 Value;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawChannel
{
    public ImVector<ImDrawCmd> _CmdBuffer;
    public ImVector<ushort> _IdxBuffer;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawCmd
{
    public Vector4 ClipRect;
    public ImTextureRef TexRef;
    public uint VtxOffset;
    public uint IdxOffset;
    public uint ElemCount;
    public void* UserCallback;
    public void* UserCallbackData;
    public int UserCallbackDataSize;
    public int UserCallbackDataOffset;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawCmdHeader
{
    public Vector4 ClipRect;
    public ImTextureRef TexRef;
    public uint VtxOffset;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawData
{
    public bool Valid;
    public int FrameCount;
    public int TotalIdxCount;
    public int TotalVtxCount;
    public ImPtrVector<ImDrawList> CmdLists;
    public Vector2 DisplayPos;
    public Vector2 DisplaySize;
    public Vector2 FramebufferScale;
    public ImGuiViewport* OwnerViewport;
    public ImPtrVector<ImTextureData>* Textures;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawList
{
    public ImVector<ImDrawCmd> CmdBuffer;
    public ImVector<ushort> IdxBuffer;
    public ImVector<ImDrawVert> VtxBuffer;
    public ImDrawFlags Flags;
    public uint _VtxCurrentIdx;
    public ImDrawListSharedData* _Data;
    public ImDrawVert* _VtxWritePtr;
    public ushort* _IdxWritePtr;
    public ImVector<Vector2> _Path;
    public ImDrawCmdHeader _CmdHeader;
    public ImDrawListSplitter _Splitter;
    public ImVector<Vector4> _ClipRectStack;
    public ImVector<ImTextureRef> _TextureStack;
    public ImVector<byte> _CallbacksDataBuf;
    public ImSmallStackImDrawFlags3 _DrawFlagsStack;
    public float _FringeScale;
    public float _InvFringeScale;
    public bool _FringeScaleIsInteger;
    public byte* _OwnerName;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawListSplitter
{
    public int _Current;
    public int _Count;
    public ImVector<ImDrawChannel> _Channels;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImDrawVert
{
    public Vector2 pos;
    public Vector2 uv;
    public uint col;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFont
{
    public ImFontBaked* LastBaked;
    public ImFontAtlas* OwnerAtlas;
    public ImFontFlags Flags;
    public float CurrentRasterizerDensity;
    public uint FontId;
    public float LegacySize;
    public ImPtrVector<ImFontConfig> Sources;
    public ushort EllipsisChar;
    public ushort FallbackChar;
    public FixedArray1<byte> Used8kPagesMap;
    public bool EllipsisAutoBake;
    public ImGuiStorage RemapPairs;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFontAtlas
{
    public ImFontAtlasFlags Flags;
    public ImTextureFormat TexDesiredFormat;
    public int TexGlyphPadding;
    public int TexMinWidth;
    public int TexMinHeight;
    public int TexMaxWidth;
    public int TexMaxHeight;
    public void* UserData;
    public ImTextureRef TexRef;
    public ImTextureData* TexData;
    public ImPtrVector<ImTextureData> TexList;
    public bool Locked;
    public bool RendererHasTextures;
    public bool TexIsBuilt;
    public bool TexPixelsUseColors;
    public Vector2 TexUvScale;
    public Vector2 TexUvWhitePixel;
    public ImPtrVector<ImFont> Fonts;
    public ImVector<ImFontConfig> Sources;
    public int TexNextUniqueID;
    public int FontNextUniqueID;
    public ImPtrVector<ImDrawListSharedData> DrawListSharedDatas;
    public ImFontAtlasBuilder* Builder;
    public ImFontLoader* FontLoader;
    public byte* FontLoaderName;
    public void* FontLoaderData;
    public uint FontLoaderFlags;
    public int RefCount;
    public ImGuiContext* OwnerContext;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFontAtlasRect
{
    public ushort x;
    public ushort y;
    public ushort w;
    public ushort h;
    public Vector2 uv0;
    public Vector2 uv1;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFontBaked
{
    public ImVector<float> IndexAdvanceX;
    public float FallbackAdvanceX;
    public float Size;
    public float RasterizerDensity;
    public ImVector<ushort> IndexLookup;
    public ImVector<ImFontGlyph> Glyphs;
    public int FallbackGlyphIndex;
    public float Ascent;
    public float Descent;
    private uint _bitfield0;
    public uint MetricsTotalSurface
    {
        readonly get => (_bitfield0 >> 0) & 0x3FFFFFFu;
        set => _bitfield0 = (_bitfield0 & ~(0x3FFFFFFu << 0)) | ((value & 0x3FFFFFFu) << 0);
    }
    public uint WantDestroy
    {
        readonly get => (_bitfield0 >> 26) & 0x1u;
        set => _bitfield0 = (_bitfield0 & ~(0x1u << 26)) | ((value & 0x1u) << 26);
    }
    public uint LoadNoFallback
    {
        readonly get => (_bitfield0 >> 27) & 0x1u;
        set => _bitfield0 = (_bitfield0 & ~(0x1u << 27)) | ((value & 0x1u) << 27);
    }
    public uint LoadNoRenderOnLayout
    {
        readonly get => (_bitfield0 >> 28) & 0x1u;
        set => _bitfield0 = (_bitfield0 & ~(0x1u << 28)) | ((value & 0x1u) << 28);
    }
    public int LastUsedFrame;
    public uint BakedId;
    public ImFont* OwnerFont;
    public void* FontLoaderDatas;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFontConfig
{
    public FixedArray40<byte> Name;
    public void* FontData;
    public int FontDataSize;
    public bool FontDataOwnedByAtlas;
    public bool MergeMode;
    public bool PixelSnapH;
    public sbyte OversampleH;
    public sbyte OversampleV;
    public ushort EllipsisChar;
    public float SizePixels;
    public ushort* GlyphRanges;
    public ushort* GlyphExcludeRanges;
    public Vector2 GlyphOffset;
    public float GlyphMinAdvanceX;
    public float GlyphMaxAdvanceX;
    public float GlyphExtraAdvanceX;
    public uint FontNo;
    public uint FontLoaderFlags;
    public float RasterizerMultiply;
    public float RasterizerDensity;
    public float ExtraSizeScale;
    public ImFontFlags Flags;
    public ImFont* DstFont;
    public ImFontLoader* FontLoader;
    public void* FontLoaderData;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFontGlyph
{
    private uint _bitfield0;
    public uint Colored
    {
        readonly get => (_bitfield0 >> 0) & 0x1u;
        set => _bitfield0 = (_bitfield0 & ~(0x1u << 0)) | ((value & 0x1u) << 0);
    }
    public uint Visible
    {
        readonly get => (_bitfield0 >> 1) & 0x1u;
        set => _bitfield0 = (_bitfield0 & ~(0x1u << 1)) | ((value & 0x1u) << 1);
    }
    public uint SourceIdx
    {
        readonly get => (_bitfield0 >> 2) & 0xFu;
        set => _bitfield0 = (_bitfield0 & ~(0xFu << 2)) | ((value & 0xFu) << 2);
    }
    public uint Codepoint
    {
        readonly get => (_bitfield0 >> 6) & 0x3FFFFFFu;
        set => _bitfield0 = (_bitfield0 & ~(0x3FFFFFFu << 6)) | ((value & 0x3FFFFFFu) << 6);
    }
    public float AdvanceX;
    public float X0;
    public float Y0;
    public float X1;
    public float Y1;
    public float U0;
    public float V0;
    public float U1;
    public float V1;
    public int PackId;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImFontGlyphRangesBuilder
{
    public ImVector<uint> UsedChars;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiIO
{
    public ImGuiConfigFlags ConfigFlags;
    public ImGuiBackendFlags BackendFlags;
    public Vector2 DisplaySize;
    public Vector2 DisplayFramebufferScale;
    public float DeltaTime;
    public float IniSavingRate;
    public byte* IniFilename;
    public byte* LogFilename;
    public void* UserData;
    public ImFontAtlas* Fonts;
    public ImFont* FontDefault;
    public bool FontAllowUserScaling;
    public bool ConfigNavSwapGamepadButtons;
    public bool ConfigNavMoveSetMousePos;
    public bool ConfigNavCaptureKeyboard;
    public bool ConfigNavEscapeClearFocusItem;
    public bool ConfigNavEscapeClearFocusWindow;
    public bool ConfigNavCursorVisibleAuto;
    public bool ConfigNavCursorVisibleAlways;
    public bool ConfigDockingNoSplit;
    public bool ConfigDockingNoDockingOver;
    public bool ConfigDockingWithShift;
    public bool ConfigDockingAlwaysTabBar;
    public bool ConfigDockingTransparentPayload;
    public bool ConfigViewportsNoAutoMerge;
    public bool ConfigViewportsNoTaskBarIcon;
    public bool ConfigViewportsNoDecoration;
    public bool ConfigViewportsNoDefaultParent;
    public bool ConfigViewportsPlatformFocusSetsImGuiFocus;
    public bool ConfigDpiScaleFonts;
    public bool ConfigDpiScaleViewports;
    public bool ConfigMacOSXBehaviors;
    public bool ConfigInputTrickleEventQueue;
    public bool ConfigInputTextCursorBlink;
    public bool ConfigInputTextEnterKeepActive;
    public ImGuiColorEditFlags ConfigColorEditFlags;
    public bool ConfigDragClickToInputText;
    public bool ConfigWindowsResizeFromEdges;
    public bool ConfigWindowsMoveFromTitleBarOnly;
    public bool ConfigWindowsCopyContentsWithCtrlC;
    public bool ConfigScrollbarScrollByPage;
    public bool ConfigIniSettingsSaveLastUsedDate;
    public int ConfigIniSettingsAutoDiscardMonths;
    public bool ConfigDebugIniSettings;
    public bool MouseDrawCursor;
    public float ConfigMemoryCompactTimer;
    public float MouseDoubleClickTime;
    public float MouseDoubleClickMaxDist;
    public float MouseSingleClickDelay;
    public float MouseDragThreshold;
    public float KeyRepeatDelay;
    public float KeyRepeatRate;
    public bool ConfigErrorRecovery;
    public bool ConfigErrorRecoveryEnableAssert;
    public bool ConfigErrorRecoveryEnableDebugLog;
    public bool ConfigErrorRecoveryEnableTooltip;
    public bool ConfigDebugIsDebuggerPresent;
    public bool ConfigDebugHighlightIdConflicts;
    public bool ConfigDebugHighlightIdConflictsShowItemPicker;
    public bool ConfigDebugBeginReturnValueOnce;
    public bool ConfigDebugBeginReturnValueLoop;
    public bool ConfigDebugIgnoreFocusLoss;
    public bool ConfigDebugDrawListDefaultsToStrokeLegacy;
    public byte* BackendPlatformName;
    public byte* BackendRendererName;
    public void* BackendPlatformUserData;
    public void* BackendRendererUserData;
    public void* BackendLanguageUserData;
    public bool WantCaptureMouse;
    public bool WantCaptureKeyboard;
    public bool WantTextInput;
    public bool WantSetMousePos;
    public bool WantSaveIniSettings;
    public bool NavActive;
    public bool NavVisible;
    public float Framerate;
    public int MetricsRenderVertices;
    public int MetricsRenderIndices;
    public int MetricsRenderWindows;
    public int MetricsActiveWindows;
    public Vector2 MouseDelta;
    public ImGuiContext* Ctx;
    public Vector2 MousePos;
    public FixedArray5<bool> MouseDown;
    public float MouseWheel;
    public float MouseWheelH;
    public ImGuiMouseSource MouseSource;
    public uint MouseHoveredViewport;
    public bool KeyCtrl;
    public bool KeyShift;
    public bool KeyAlt;
    public bool KeySuper;
    public int KeyMods;
    public FixedArray155<ImGuiKeyData> KeysData;
    public bool WantCaptureMouseUnlessPopupClose;
    public Vector2 MousePosPrev;
    public FixedArray5<Vector2> MouseClickedPos;
    public FixedArray5<double> MouseClickedTime;
    public FixedArray5<bool> MouseClicked;
    public FixedArray5<bool> MouseDoubleClicked;
    public FixedArray5<ushort> MouseClickedCount;
    public FixedArray5<ushort> MouseClickedLastCount;
    public FixedArray5<bool> MouseReleased;
    public FixedArray5<double> MouseReleasedTime;
    public FixedArray5<bool> MouseDownOwned;
    public FixedArray5<bool> MouseDownOwnedUnlessPopupClose;
    public bool MouseWheelRequestAxisSwap;
    public bool MouseCtrlLeftAsRightClick;
    public FixedArray5<float> MouseDownDuration;
    public FixedArray5<float> MouseDownDurationPrev;
    public FixedArray5<Vector2> MouseDragMaxDistanceAbs;
    public FixedArray5<float> MouseDragMaxDistanceSqr;
    public float PenPressure;
    public bool AppFocusLost;
    public bool AppAcceptingEvents;
    public ushort InputQueueSurrogate;
    public ImVector<ushort> InputQueueCharacters;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiInputTextCallbackData
{
    public ImGuiContext* Ctx;
    public ImGuiInputTextFlags EventFlag;
    public ImGuiInputTextFlags Flags;
    public void* UserData;
    public uint ID;
    public ImGuiKey EventKey;
    public ushort EventChar;
    public bool EventActivated;
    public bool BufDirty;
    public byte* Buf;
    public int BufTextLen;
    public int BufSize;
    public int CursorPos;
    public int SelectionStart;
    public int SelectionEnd;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiKeyData
{
    public bool Down;
    public float DownDuration;
    public float DownDurationPrev;
    public float AnalogValue;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiListClipper
{
    public int DisplayStart;
    public int DisplayEnd;
    public int UserIndex;
    public int ItemsCount;
    public float ItemsHeight;
    public ImGuiListClipperFlags Flags;
    public double StartPosY;
    public double StartSeekOffsetY;
    public ImGuiContext* Ctx;
    public void* TempData;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiMultiSelectIO
{
    public ImVector<ImGuiSelectionRequest> Requests;
    public long RangeSrcItem;
    public long NavIdItem;
    public bool NavIdSelected;
    public bool RangeSrcReset;
    public int ItemsCount;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiOnceUponAFrame
{
    public int RefFrame;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiPayload
{
    public void* Data;
    public int DataSize;
    public uint SourceId;
    public uint SourceParentId;
    public int DataFrameCount;
    public FixedArray33<byte> DataType;
    public bool Preview;
    public bool Delivery;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiPlatformIO
{
    public void* Platform_GetClipboardTextFn;
    public void* Platform_SetClipboardTextFn;
    public void* Platform_ClipboardUserData;
    public void* Platform_OpenInShellFn;
    public void* Platform_OpenInShellUserData;
    public void* Platform_SetImeDataFn;
    public void* Platform_ImeUserData;
    public ushort Platform_LocaleDecimalPoint;
    public int Platform_SessionDate;
    public int Renderer_TextureMaxWidth;
    public int Renderer_TextureMaxHeight;
    public void* Renderer_RenderState;
    public void* DrawCallback_ResetRenderState;
    public void* DrawCallback_SetSamplerLinear;
    public void* DrawCallback_SetSamplerNearest;
    public void* DrawCallback_SetSamplerFromTex;
    public void* Platform_CreateWindow;
    public void* Platform_DestroyWindow;
    public void* Platform_ShowWindow;
    public void* Platform_SetWindowPos;
    public void* Platform_GetWindowPos;
    public void* Platform_SetWindowSize;
    public void* Platform_GetWindowSize;
    public void* Platform_GetWindowFramebufferScale;
    public void* Platform_SetWindowFocus;
    public void* Platform_GetWindowFocus;
    public void* Platform_GetWindowMinimized;
    public void* Platform_SetWindowTitle;
    public void* Platform_SetWindowAlpha;
    public void* Platform_UpdateWindow;
    public void* Platform_RenderWindow;
    public void* Platform_SwapBuffers;
    public void* Platform_GetWindowDpiScale;
    public void* Platform_OnChangedViewport;
    public void* Platform_GetWindowWorkAreaInsets;
    public void* Platform_CreateVkSurface;
    public void* Renderer_CreateWindow;
    public void* Renderer_DestroyWindow;
    public void* Renderer_SetWindowSize;
    public void* Renderer_RenderWindow;
    public void* Renderer_SwapBuffers;
    public ImVector<ImGuiPlatformMonitor> Monitors;
    public ImPtrVector<ImTextureData> Textures;
    public ImPtrVector<ImGuiViewport> Viewports;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiPlatformImeData
{
    public bool WantVisible;
    public bool WantTextInput;
    public Vector2 InputPos;
    public float InputLineHeight;
    public uint ViewportId;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiPlatformMonitor
{
    public Vector2 MainPos;
    public Vector2 MainSize;
    public Vector2 WorkPos;
    public Vector2 WorkSize;
    public float DpiScale;
    public void* PlatformHandle;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiSelectionBasicStorage
{
    public int Size;
    public bool PreserveOrder;
    public void* UserData;
    public void* AdapterIndexToStorageId;
    public int _SelectionOrder;
    public ImGuiStorage _Storage;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiSelectionExternalStorage
{
    public void* UserData;
    public void* AdapterSetItemSelected;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiSelectionRequest
{
    public ImGuiSelectionRequestType Type;
    public bool Selected;
    public sbyte RangeDirection;
    public long RangeFirstItem;
    public long RangeLastItem;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiSizeCallbackData
{
    public void* UserData;
    public Vector2 Pos;
    public Vector2 CurrentSize;
    public Vector2 DesiredSize;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiStorage
{
    public ImVector<ImGuiStoragePair> Data;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiStoragePair
{
    public uint key;
    private ImGuiStoragePairUnion0 _union0;
    [UnscopedRef] public ref int val_i => ref _union0.val_i;
    [UnscopedRef] public ref float val_f => ref _union0.val_f;
    [UnscopedRef] public ref void* val_p => ref _union0.val_p;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe struct ImGuiStoragePairUnion0
{
    [FieldOffset(0)] public int val_i;
    [FieldOffset(0)] public float val_f;
    [FieldOffset(0)] public void* val_p;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiStyle
{
    public float FontSizeBase;
    public float FontScaleMain;
    public float FontScaleDpi;
    public float Alpha;
    public float DisabledAlpha;
    public Vector2 WindowPadding;
    public float WindowRounding;
    public float WindowBorderSize;
    public float WindowBorderHoverPadding;
    public Vector2 WindowMinSize;
    public Vector2 WindowTitleAlign;
    public ImGuiDir WindowMenuButtonPosition;
    public float ChildRounding;
    public float ChildBorderSize;
    public float PopupRounding;
    public float PopupBorderSize;
    public Vector2 FramePadding;
    public float FrameRounding;
    public float FrameBorderSize;
    public Vector2 ItemSpacing;
    public Vector2 ItemInnerSpacing;
    public Vector2 CellPadding;
    public Vector2 TouchExtraPadding;
    public float IndentSpacing;
    public float ColumnsMinSpacing;
    public float ScrollbarSize;
    public float ScrollbarRounding;
    public float ScrollbarPadding;
    public float GrabMinSize;
    public float GrabRounding;
    public float LogSliderDeadzone;
    public float ImageRounding;
    public float ImageBorderSize;
    public float TabRounding;
    public float TabBorderSize;
    public float TabMinWidthBase;
    public float TabMinWidthShrink;
    public float TabCloseButtonMinWidthSelected;
    public float TabCloseButtonMinWidthUnselected;
    public float TabBarBorderSize;
    public float TabBarOverlineSize;
    public float TableAngledHeadersAngle;
    public Vector2 TableAngledHeadersTextAlign;
    public ImGuiTreeNodeFlags TreeLinesFlags;
    public float TreeLinesSize;
    public float TreeLinesRounding;
    public float MenuItemRounding;
    public float SelectableRounding;
    public float DragDropTargetRounding;
    public float DragDropTargetBorderSize;
    public float DragDropTargetPadding;
    public float ColorMarkerSize;
    public ImGuiDir ColorButtonPosition;
    public Vector2 ButtonTextAlign;
    public Vector2 SelectableTextAlign;
    public float InputTextCursorSize;
    public float SeparatorSize;
    public float SeparatorTextBorderSize;
    public Vector2 SeparatorTextAlign;
    public Vector2 SeparatorTextPadding;
    public Vector2 DisplayWindowPadding;
    public Vector2 DisplaySafeAreaPadding;
    public bool DockingNodeHasCloseButton;
    public float DockingSeparatorSize;
    public float MouseCursorScale;
    public bool AntiAliasedLines;
    public bool AntiAliasedLineEnds;
    public bool AntiAliasedFill;
    public float CurveTessellationMaxError;
    public float CircleTessellationMaxError;
    public FixedArray63<Vector4> Colors;
    public float HoverStationaryDelay;
    public float HoverDelayShort;
    public float HoverDelayNormal;
    public ImGuiHoveredFlags HoverFlagsForTooltipMouse;
    public ImGuiHoveredFlags HoverFlagsForTooltipNav;
    public float _MainScale;
    public float _NextFrameFontSizeBase;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiTableColumnSortSpecs
{
    public uint ColumnUserID;
    public short ColumnIndex;
    public short SortOrder;
    public ImGuiSortDirection SortDirection;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiTableSortSpecs
{
    public ImGuiTableColumnSortSpecs* Specs;
    public int SpecsCount;
    public bool SpecsDirty;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiTextBuffer
{
    public ImVector<byte> Buf;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiTextFilter
{
    public FixedArray256<byte> InputBuf;
    public int _CountExclude;
    public ImVector<ImGuiTextFilterItem> _Items;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiViewport
{
    public uint ID;
    public ImGuiViewportFlags Flags;
    public Vector2 Pos;
    public Vector2 Size;
    public Vector2 FramebufferScale;
    public Vector2 WorkPos;
    public Vector2 WorkSize;
    public float DpiScale;
    public uint ParentViewportId;
    public ImGuiViewport* ParentViewport;
    public ImDrawData* DrawData;
    public void* RendererUserData;
    public void* PlatformUserData;
    public void* PlatformIconData;
    public void* PlatformHandle;
    public void* PlatformHandleRaw;
    public bool PlatformWindowCreated;
    public bool PlatformRequestMove;
    public bool PlatformRequestResize;
    public bool PlatformRequestClose;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImGuiWindowClass
{
    public uint ClassId;
    public uint ParentViewportId;
    public uint FocusRouteParentWindowId;
    public ImGuiViewportFlags ViewportFlagsOverrideSet;
    public ImGuiViewportFlags ViewportFlagsOverrideClear;
    public ImGuiTabItemFlags TabItemFlagsOverrideSet;
    public ImGuiDockNodeFlags DockNodeFlagsOverrideSet;
    public bool DockingAlwaysTabBar;
    public bool DockingAllowUnclassed;
    public void* PlatformIconData;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImTextureData
{
    public int UniqueID;
    public ImTextureStatus Status;
    public void* BackendUserData;
    public void* QueueUserData;
    public ulong TexID;
    public ImTextureFormat Format;
    public int Width;
    public int Height;
    public int BytesPerPixel;
    public byte* Pixels;
    public ImTextureRect UsedRect;
    public ImTextureRect UpdateRect;
    public ImVector<ImTextureRect> Updates;
    public int UnusedFrames;
    public ushort RefCount;
    public bool UseColors;
    public bool WantDestroyNextFrame;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImTextureRect
{
    public ushort x;
    public ushort y;
    public ushort w;
    public ushort h;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct ImTextureRef
{
    public ImTextureData* _TexData;
    public ulong _TexID;
}

[StructLayout(LayoutKind.Sequential)]
public struct ImDrawListSharedData
{
}

[StructLayout(LayoutKind.Sequential)]
public struct ImFontAtlasBuilder
{
}

[StructLayout(LayoutKind.Sequential)]
public struct ImFontLoader
{
}

[StructLayout(LayoutKind.Sequential)]
public struct ImGuiContext
{
}

[StructLayout(LayoutKind.Sequential)]
public struct ImGuiTextFilterItem
{
}

[StructLayout(LayoutKind.Sequential)]
public struct ImSmallStackImDrawFlags3
{
    public FixedArray3<ImDrawFlags> LocalData;
    public ushort Size;
}

[InlineArray(1)]
public struct FixedArray1<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(3)]
public struct FixedArray3<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(5)]
public struct FixedArray5<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(33)]
public struct FixedArray33<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(40)]
public struct FixedArray40<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(63)]
public struct FixedArray63<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(155)]
public struct FixedArray155<T> where T : unmanaged
{
    private T _element;
}

[InlineArray(256)]
public struct FixedArray256<T> where T : unmanaged
{
    private T _element;
}
