using System.Numerics;

namespace Crystal.ImGui;

public readonly unsafe partial struct ImColorPtr
{
    public readonly ImColor* Handle;

    public ImColorPtr(ImColor* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImColorPtr(ImColor* handle) => new(handle);

    public static implicit operator ImColor*(ImColorPtr pointer) => pointer.Handle;

    public ref Vector4 Value => ref Handle->Value;

    public static ImColor HSV(float h, float s, float v, float a)
    {
        return ImGuiNative.ImColor_HSV(h, s, v, a);
    }

    public static ImColor HSV(float h, float s, float v)
    {
        return ImGuiNative.ImColor_HSV(h, s, v, 1f);
    }

    public static ImColorPtr Create()
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_Nil());
    }

    public static ImColorPtr Create(float r, float g, float b, float a)
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_Float(r, g, b, a));
    }

    public static ImColorPtr Create(float r, float g, float b)
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_Float(r, g, b, 1f));
    }

    public static ImColorPtr Create(Vector4 col)
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_Vec4(col));
    }

    public static ImColorPtr Create(int r, int g, int b, int a)
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_Int(r, g, b, a));
    }

    public static ImColorPtr Create(int r, int g, int b)
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_Int(r, g, b, 255));
    }

    public static ImColorPtr Create(uint rgba)
    {
        return new ImColorPtr(ImGuiNative.ImColor_ImColor_U32(rgba));
    }

    public void SetHSV(float h, float s, float v, float a)
    {
        ImGuiNative.ImColor_SetHSV(Handle, h, s, v, a);
    }

    public void SetHSV(float h, float s, float v)
    {
        ImGuiNative.ImColor_SetHSV(Handle, h, s, v, 1f);
    }

    public void Destroy()
    {
        ImGuiNative.ImColor_destroy(Handle);
    }
}

public readonly unsafe partial struct ImDrawChannelPtr
{
    public readonly ImDrawChannel* Handle;

    public ImDrawChannelPtr(ImDrawChannel* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawChannelPtr(ImDrawChannel* handle) => new(handle);

    public static implicit operator ImDrawChannel*(ImDrawChannelPtr pointer) => pointer.Handle;

    public ref ImVector<ImDrawCmd> _CmdBuffer => ref Handle->_CmdBuffer;

    public ref ImVector<ushort> _IdxBuffer => ref Handle->_IdxBuffer;
}

public readonly unsafe partial struct ImDrawCmdPtr
{
    public readonly ImDrawCmd* Handle;

    public ImDrawCmdPtr(ImDrawCmd* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawCmdPtr(ImDrawCmd* handle) => new(handle);

    public static implicit operator ImDrawCmd*(ImDrawCmdPtr pointer) => pointer.Handle;

    public ref Vector4 ClipRect => ref Handle->ClipRect;

    public ref ImTextureRef TexRef => ref Handle->TexRef;

    public ref uint VtxOffset => ref Handle->VtxOffset;

    public ref uint IdxOffset => ref Handle->IdxOffset;

    public ref uint ElemCount => ref Handle->ElemCount;

    public ref void* UserCallback => ref Handle->UserCallback;

    public ref void* UserCallbackData => ref Handle->UserCallbackData;

    public ref int UserCallbackDataSize => ref Handle->UserCallbackDataSize;

    public ref int UserCallbackDataOffset => ref Handle->UserCallbackDataOffset;

    public ulong GetTexID()
    {
        return ImGuiNative.ImDrawCmd_GetTexID(Handle);
    }

    public static ImDrawCmdPtr Create()
    {
        return new ImDrawCmdPtr(ImGuiNative.ImDrawCmd_ImDrawCmd());
    }

    public void Destroy()
    {
        ImGuiNative.ImDrawCmd_destroy(Handle);
    }
}

public readonly unsafe partial struct ImDrawCmdHeaderPtr
{
    public readonly ImDrawCmdHeader* Handle;

    public ImDrawCmdHeaderPtr(ImDrawCmdHeader* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawCmdHeaderPtr(ImDrawCmdHeader* handle) => new(handle);

    public static implicit operator ImDrawCmdHeader*(ImDrawCmdHeaderPtr pointer) => pointer.Handle;

    public ref Vector4 ClipRect => ref Handle->ClipRect;

    public ref ImTextureRef TexRef => ref Handle->TexRef;

    public ref uint VtxOffset => ref Handle->VtxOffset;
}

public readonly unsafe partial struct ImDrawDataPtr
{
    public readonly ImDrawData* Handle;

    public ImDrawDataPtr(ImDrawData* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawDataPtr(ImDrawData* handle) => new(handle);

    public static implicit operator ImDrawData*(ImDrawDataPtr pointer) => pointer.Handle;

    public ref bool Valid => ref Handle->Valid;

    public ref int FrameCount => ref Handle->FrameCount;

    public ref int TotalIdxCount => ref Handle->TotalIdxCount;

    public ref int TotalVtxCount => ref Handle->TotalVtxCount;

    public ref ImPtrVector<ImDrawList> CmdLists => ref Handle->CmdLists;

    public ref Vector2 DisplayPos => ref Handle->DisplayPos;

    public ref Vector2 DisplaySize => ref Handle->DisplaySize;

    public ref Vector2 FramebufferScale => ref Handle->FramebufferScale;

    public ImGuiViewportPtr OwnerViewport
    {
        get => Handle->OwnerViewport;
        set => Handle->OwnerViewport = value;
    }

    public ref ImPtrVector<ImTextureData>* Textures => ref Handle->Textures;

    public void AddDrawList(ImDrawListPtr drawList)
    {
        ImGuiNative.ImDrawData_AddDrawList(Handle, drawList.Handle);
    }

    public void Clear()
    {
        ImGuiNative.ImDrawData_Clear(Handle);
    }

    public void DeIndexAllBuffers()
    {
        ImGuiNative.ImDrawData_DeIndexAllBuffers(Handle);
    }

    public static ImDrawDataPtr Create()
    {
        return new ImDrawDataPtr(ImGuiNative.ImDrawData_ImDrawData());
    }

    public void ScaleClipRects(Vector2 fbScale)
    {
        ImGuiNative.ImDrawData_ScaleClipRects(Handle, fbScale);
    }

    public void Destroy()
    {
        ImGuiNative.ImDrawData_destroy(Handle);
    }
}

public readonly unsafe partial struct ImDrawListPtr
{
    public readonly ImDrawList* Handle;

    public ImDrawListPtr(ImDrawList* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawListPtr(ImDrawList* handle) => new(handle);

    public static implicit operator ImDrawList*(ImDrawListPtr pointer) => pointer.Handle;

    public ref ImVector<ImDrawCmd> CmdBuffer => ref Handle->CmdBuffer;

    public ref ImVector<ushort> IdxBuffer => ref Handle->IdxBuffer;

    public ref ImVector<ImDrawVert> VtxBuffer => ref Handle->VtxBuffer;

    public ref ImDrawFlags Flags => ref Handle->Flags;

    public ref uint _VtxCurrentIdx => ref Handle->_VtxCurrentIdx;

    public ref ImDrawListSharedData* _Data => ref Handle->_Data;

    public ImDrawVertPtr _VtxWritePtr
    {
        get => Handle->_VtxWritePtr;
        set => Handle->_VtxWritePtr = value;
    }

    public ref ushort* _IdxWritePtr => ref Handle->_IdxWritePtr;

    public ref ImVector<Vector2> _Path => ref Handle->_Path;

    public ref ImDrawCmdHeader _CmdHeader => ref Handle->_CmdHeader;

    public ref ImDrawListSplitter _Splitter => ref Handle->_Splitter;

    public ref ImVector<Vector4> _ClipRectStack => ref Handle->_ClipRectStack;

    public ref ImVector<ImTextureRef> _TextureStack => ref Handle->_TextureStack;

    public ref ImVector<byte> _CallbacksDataBuf => ref Handle->_CallbacksDataBuf;

    public ref ImSmallStackImDrawFlags3 _DrawFlagsStack => ref Handle->_DrawFlagsStack;

    public ref float _FringeScale => ref Handle->_FringeScale;

    public ref float _InvFringeScale => ref Handle->_InvFringeScale;

    public ref bool _FringeScaleIsInteger => ref Handle->_FringeScaleIsInteger;

    public ref byte* _OwnerName => ref Handle->_OwnerName;

    public void AddBezierCubic(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness, int numSegments, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddBezierCubic(Handle, p1, p2, p3, p4, col, thickness, numSegments, flags);
    }

    public void AddBezierCubic(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness, int numSegments)
    {
        ImGuiNative.ImDrawList_AddBezierCubic(Handle, p1, p2, p3, p4, col, thickness, numSegments, (ImDrawFlags)(0));
    }

    public void AddBezierCubic(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddBezierCubic(Handle, p1, p2, p3, p4, col, thickness, 0, (ImDrawFlags)(0));
    }

    public void AddBezierQuadratic(Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness, int numSegments, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddBezierQuadratic(Handle, p1, p2, p3, col, thickness, numSegments, flags);
    }

    public void AddBezierQuadratic(Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness, int numSegments)
    {
        ImGuiNative.ImDrawList_AddBezierQuadratic(Handle, p1, p2, p3, col, thickness, numSegments, (ImDrawFlags)(0));
    }

    public void AddBezierQuadratic(Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddBezierQuadratic(Handle, p1, p2, p3, col, thickness, 0, (ImDrawFlags)(0));
    }

    public void AddCallback(void* callback, void* userdata, nuint userdataSize)
    {
        ImGuiNative.ImDrawList_AddCallback(Handle, callback, userdata, userdataSize);
    }

    public void AddCallback(void* callback, void* userdata)
    {
        ImGuiNative.ImDrawList_AddCallback(Handle, callback, userdata, (nuint)0);
    }

    public void AddCallback(void* callback)
    {
        ImGuiNative.ImDrawList_AddCallback(Handle, callback, null, (nuint)0);
    }

    public void AddCircle(Vector2 center, float radius, uint col, int numSegments, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddCircle(Handle, center, radius, col, numSegments, thickness, flags);
    }

    public void AddCircle(Vector2 center, float radius, uint col, int numSegments, float thickness)
    {
        ImGuiNative.ImDrawList_AddCircle(Handle, center, radius, col, numSegments, thickness, (ImDrawFlags)(0));
    }

    public void AddCircle(Vector2 center, float radius, uint col, int numSegments)
    {
        ImGuiNative.ImDrawList_AddCircle(Handle, center, radius, col, numSegments, 1f, (ImDrawFlags)(0));
    }

    public void AddCircle(Vector2 center, float radius, uint col)
    {
        ImGuiNative.ImDrawList_AddCircle(Handle, center, radius, col, 0, 1f, (ImDrawFlags)(0));
    }

    public void AddCircleFilled(Vector2 center, float radius, uint col, int numSegments)
    {
        ImGuiNative.ImDrawList_AddCircleFilled(Handle, center, radius, col, numSegments);
    }

    public void AddCircleFilled(Vector2 center, float radius, uint col)
    {
        ImGuiNative.ImDrawList_AddCircleFilled(Handle, center, radius, col, 0);
    }

    public void AddConcavePolyFilled(ReadOnlySpan<Vector2> points, uint col)
    {
        fixed (Vector2* pointsPointer = points)
        {
            ImGuiNative.ImDrawList_AddConcavePolyFilled(Handle, pointsPointer, points.Length, col);
        }
    }

    public void AddConvexPolyFilled(ReadOnlySpan<Vector2> points, uint col, ImDrawFlags flags)
    {
        fixed (Vector2* pointsPointer = points)
        {
            ImGuiNative.ImDrawList_AddConvexPolyFilled(Handle, pointsPointer, points.Length, col, flags);
        }
    }

    public void AddConvexPolyFilled(ReadOnlySpan<Vector2> points, uint col)
    {
        fixed (Vector2* pointsPointer = points)
        {
            ImGuiNative.ImDrawList_AddConvexPolyFilled(Handle, pointsPointer, points.Length, col, (ImDrawFlags)(0));
        }
    }

    public void AddDrawCmd()
    {
        ImGuiNative.ImDrawList_AddDrawCmd(Handle);
    }

    public void AddEllipse(Vector2 center, Vector2 radius, uint col, float rot, int numSegments, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddEllipse(Handle, center, radius, col, rot, numSegments, thickness, flags);
    }

    public void AddEllipse(Vector2 center, Vector2 radius, uint col, float rot, int numSegments, float thickness)
    {
        ImGuiNative.ImDrawList_AddEllipse(Handle, center, radius, col, rot, numSegments, thickness, (ImDrawFlags)(0));
    }

    public void AddEllipse(Vector2 center, Vector2 radius, uint col, float rot, int numSegments)
    {
        ImGuiNative.ImDrawList_AddEllipse(Handle, center, radius, col, rot, numSegments, 1f, (ImDrawFlags)(0));
    }

    public void AddEllipse(Vector2 center, Vector2 radius, uint col, float rot)
    {
        ImGuiNative.ImDrawList_AddEllipse(Handle, center, radius, col, rot, 0, 1f, (ImDrawFlags)(0));
    }

    public void AddEllipse(Vector2 center, Vector2 radius, uint col)
    {
        ImGuiNative.ImDrawList_AddEllipse(Handle, center, radius, col, 0f, 0, 1f, (ImDrawFlags)(0));
    }

    public void AddEllipseFilled(Vector2 center, Vector2 radius, uint col, float rot, int numSegments)
    {
        ImGuiNative.ImDrawList_AddEllipseFilled(Handle, center, radius, col, rot, numSegments);
    }

    public void AddEllipseFilled(Vector2 center, Vector2 radius, uint col, float rot)
    {
        ImGuiNative.ImDrawList_AddEllipseFilled(Handle, center, radius, col, rot, 0);
    }

    public void AddEllipseFilled(Vector2 center, Vector2 radius, uint col)
    {
        ImGuiNative.ImDrawList_AddEllipseFilled(Handle, center, radius, col, 0f, 0);
    }

    public void AddImage(ImTextureRef texRef, Vector2 pMin, Vector2 pMax, Vector2 uvMin, Vector2 uvMax, uint col)
    {
        ImGuiNative.ImDrawList_AddImage(Handle, texRef, pMin, pMax, uvMin, uvMax, col);
    }

    public void AddImage(ImTextureRef texRef, Vector2 pMin, Vector2 pMax, Vector2 uvMin, Vector2 uvMax)
    {
        ImGuiNative.ImDrawList_AddImage(Handle, texRef, pMin, pMax, uvMin, uvMax, 4294967295u);
    }

    public void AddImage(ImTextureRef texRef, Vector2 pMin, Vector2 pMax, Vector2 uvMin)
    {
        ImGuiNative.ImDrawList_AddImage(Handle, texRef, pMin, pMax, uvMin, new Vector2(1f, 1f), 4294967295u);
    }

    public void AddImage(ImTextureRef texRef, Vector2 pMin, Vector2 pMax)
    {
        ImGuiNative.ImDrawList_AddImage(Handle, texRef, pMin, pMax, new Vector2(0f, 0f), new Vector2(1f, 1f), 4294967295u);
    }

    public void AddImageQuad(ImTextureRef texRef, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4, uint col)
    {
        ImGuiNative.ImDrawList_AddImageQuad(Handle, texRef, p1, p2, p3, p4, uv1, uv2, uv3, uv4, col);
    }

    public void AddImageQuad(ImTextureRef texRef, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4)
    {
        ImGuiNative.ImDrawList_AddImageQuad(Handle, texRef, p1, p2, p3, p4, uv1, uv2, uv3, uv4, 4294967295u);
    }

    public void AddImageQuad(ImTextureRef texRef, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 uv1, Vector2 uv2, Vector2 uv3)
    {
        ImGuiNative.ImDrawList_AddImageQuad(Handle, texRef, p1, p2, p3, p4, uv1, uv2, uv3, new Vector2(0f, 1f), 4294967295u);
    }

    public void AddImageQuad(ImTextureRef texRef, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 uv1, Vector2 uv2)
    {
        ImGuiNative.ImDrawList_AddImageQuad(Handle, texRef, p1, p2, p3, p4, uv1, uv2, new Vector2(1f, 1f), new Vector2(0f, 1f), 4294967295u);
    }

    public void AddImageQuad(ImTextureRef texRef, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 uv1)
    {
        ImGuiNative.ImDrawList_AddImageQuad(Handle, texRef, p1, p2, p3, p4, uv1, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f), 4294967295u);
    }

    public void AddImageQuad(ImTextureRef texRef, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        ImGuiNative.ImDrawList_AddImageQuad(Handle, texRef, p1, p2, p3, p4, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f), 4294967295u);
    }

    public void AddImageRounded(ImTextureRef texRef, Vector2 pMin, Vector2 pMax, Vector2 uvMin, Vector2 uvMax, uint col, float rounding, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddImageRounded(Handle, texRef, pMin, pMax, uvMin, uvMax, col, rounding, flags);
    }

    public void AddImageRounded(ImTextureRef texRef, Vector2 pMin, Vector2 pMax, Vector2 uvMin, Vector2 uvMax, uint col, float rounding)
    {
        ImGuiNative.ImDrawList_AddImageRounded(Handle, texRef, pMin, pMax, uvMin, uvMax, col, rounding, (ImDrawFlags)(0));
    }

    public void AddLine(Vector2 p1, Vector2 p2, uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddLine(Handle, p1, p2, col, thickness, flags);
    }

    public void AddLine(Vector2 p1, Vector2 p2, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddLine(Handle, p1, p2, col, thickness, (ImDrawFlags)(0));
    }

    public void AddLine(Vector2 p1, Vector2 p2, uint col)
    {
        ImGuiNative.ImDrawList_AddLine(Handle, p1, p2, col, 1f, (ImDrawFlags)(0));
    }

    public void AddLineH(float minX, float maxX, float y, uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddLineH(Handle, minX, maxX, y, col, thickness, flags);
    }

    public void AddLineH(float minX, float maxX, float y, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddLineH(Handle, minX, maxX, y, col, thickness, (ImDrawFlags)(0));
    }

    public void AddLineH(float minX, float maxX, float y, uint col)
    {
        ImGuiNative.ImDrawList_AddLineH(Handle, minX, maxX, y, col, 1f, (ImDrawFlags)(0));
    }

    public void AddLineV(float x, float minY, float maxY, uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddLineV(Handle, x, minY, maxY, col, thickness, flags);
    }

    public void AddLineV(float x, float minY, float maxY, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddLineV(Handle, x, minY, maxY, col, thickness, (ImDrawFlags)(0));
    }

    public void AddLineV(float x, float minY, float maxY, uint col)
    {
        ImGuiNative.ImDrawList_AddLineV(Handle, x, minY, maxY, col, 1f, (ImDrawFlags)(0));
    }

    public void AddNgon(Vector2 center, float radius, uint col, int numSegments, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddNgon(Handle, center, radius, col, numSegments, thickness, flags);
    }

    public void AddNgon(Vector2 center, float radius, uint col, int numSegments, float thickness)
    {
        ImGuiNative.ImDrawList_AddNgon(Handle, center, radius, col, numSegments, thickness, (ImDrawFlags)(0));
    }

    public void AddNgon(Vector2 center, float radius, uint col, int numSegments)
    {
        ImGuiNative.ImDrawList_AddNgon(Handle, center, radius, col, numSegments, 1f, (ImDrawFlags)(0));
    }

    public void AddNgonFilled(Vector2 center, float radius, uint col, int numSegments)
    {
        ImGuiNative.ImDrawList_AddNgonFilled(Handle, center, radius, col, numSegments);
    }

    public void AddPolyline(ReadOnlySpan<Vector2> points, uint col, float thickness, ImDrawFlags flags)
    {
        fixed (Vector2* pointsPointer = points)
        {
            ImGuiNative.ImDrawList_AddPolyline(Handle, pointsPointer, points.Length, col, thickness, flags);
        }
    }

    public void AddPolyline(ReadOnlySpan<Vector2> points, uint col, float thickness)
    {
        fixed (Vector2* pointsPointer = points)
        {
            ImGuiNative.ImDrawList_AddPolyline(Handle, pointsPointer, points.Length, col, thickness, (ImDrawFlags)(0));
        }
    }

    public void AddQuad(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddQuad(Handle, p1, p2, p3, p4, col, thickness, flags);
    }

    public void AddQuad(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddQuad(Handle, p1, p2, p3, p4, col, thickness, (ImDrawFlags)(0));
    }

    public void AddQuad(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col)
    {
        ImGuiNative.ImDrawList_AddQuad(Handle, p1, p2, p3, p4, col, 1f, (ImDrawFlags)(0));
    }

    public void AddQuadFilled(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col)
    {
        ImGuiNative.ImDrawList_AddQuadFilled(Handle, p1, p2, p3, p4, col);
    }

    public void AddRect(Vector2 pMin, Vector2 pMax, uint col, float rounding, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddRect(Handle, pMin, pMax, col, rounding, thickness, flags);
    }

    public void AddRect(Vector2 pMin, Vector2 pMax, uint col, float rounding, float thickness)
    {
        ImGuiNative.ImDrawList_AddRect(Handle, pMin, pMax, col, rounding, thickness, (ImDrawFlags)(0));
    }

    public void AddRect(Vector2 pMin, Vector2 pMax, uint col, float rounding)
    {
        ImGuiNative.ImDrawList_AddRect(Handle, pMin, pMax, col, rounding, 1f, (ImDrawFlags)(0));
    }

    public void AddRect(Vector2 pMin, Vector2 pMax, uint col)
    {
        ImGuiNative.ImDrawList_AddRect(Handle, pMin, pMax, col, 0f, 1f, (ImDrawFlags)(0));
    }

    public void AddRectFilled(Vector2 pMin, Vector2 pMax, uint col, float rounding, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddRectFilled(Handle, pMin, pMax, col, rounding, flags);
    }

    public void AddRectFilled(Vector2 pMin, Vector2 pMax, uint col, float rounding)
    {
        ImGuiNative.ImDrawList_AddRectFilled(Handle, pMin, pMax, col, rounding, (ImDrawFlags)(0));
    }

    public void AddRectFilled(Vector2 pMin, Vector2 pMax, uint col)
    {
        ImGuiNative.ImDrawList_AddRectFilled(Handle, pMin, pMax, col, 0f, (ImDrawFlags)(0));
    }

    public void AddRectFilledMultiColor(Vector2 pMin, Vector2 pMax, uint colUprLeft, uint colUprRight, uint colBotRight, uint colBotLeft)
    {
        ImGuiNative.ImDrawList_AddRectFilledMultiColor(Handle, pMin, pMax, colUprLeft, colUprRight, colBotRight, colBotLeft);
    }

    public void AddText(Vector2 pos, uint col, string textBegin)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImDrawList_AddText_Vec2(Handle, pos, col, textBeginUtf8.Pointer, null);
    }

    public void AddText(ImFontPtr font, float fontSize, Vector2 pos, uint col, string textBegin, float wrapWidth, Vector4* cpuFineClipRect)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImDrawList_AddText_FontPtr(Handle, font.Handle, fontSize, pos, col, textBeginUtf8.Pointer, null, wrapWidth, cpuFineClipRect);
    }

    public void AddText(ImFontPtr font, float fontSize, Vector2 pos, uint col, string textBegin, float wrapWidth)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImDrawList_AddText_FontPtr(Handle, font.Handle, fontSize, pos, col, textBeginUtf8.Pointer, null, wrapWidth, null);
    }

    public void AddText(ImFontPtr font, float fontSize, Vector2 pos, uint col, string textBegin)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImDrawList_AddText_FontPtr(Handle, font.Handle, fontSize, pos, col, textBeginUtf8.Pointer, null, 0f, null);
    }

    public void AddTriangle(Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_AddTriangle(Handle, p1, p2, p3, col, thickness, flags);
    }

    public void AddTriangle(Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness)
    {
        ImGuiNative.ImDrawList_AddTriangle(Handle, p1, p2, p3, col, thickness, (ImDrawFlags)(0));
    }

    public void AddTriangle(Vector2 p1, Vector2 p2, Vector2 p3, uint col)
    {
        ImGuiNative.ImDrawList_AddTriangle(Handle, p1, p2, p3, col, 1f, (ImDrawFlags)(0));
    }

    public void AddTriangleFilled(Vector2 p1, Vector2 p2, Vector2 p3, uint col)
    {
        ImGuiNative.ImDrawList_AddTriangleFilled(Handle, p1, p2, p3, col);
    }

    public void ChannelsMerge()
    {
        ImGuiNative.ImDrawList_ChannelsMerge(Handle);
    }

    public void ChannelsSetCurrent(int n)
    {
        ImGuiNative.ImDrawList_ChannelsSetCurrent(Handle, n);
    }

    public void ChannelsSplit(int count)
    {
        ImGuiNative.ImDrawList_ChannelsSplit(Handle, count);
    }

    public ImDrawListPtr CloneOutput()
    {
        return new ImDrawListPtr(ImGuiNative.ImDrawList_CloneOutput(Handle));
    }

    public Vector2 GetClipRectMax()
    {
        return ImGuiNative.ImDrawList_GetClipRectMax(Handle);
    }

    public Vector2 GetClipRectMin()
    {
        return ImGuiNative.ImDrawList_GetClipRectMin(Handle);
    }

    public static ImDrawListPtr Create(ImDrawListSharedData* sharedData)
    {
        return new ImDrawListPtr(ImGuiNative.ImDrawList_ImDrawList(sharedData));
    }

    public void PathArcTo(Vector2 center, float radius, float aMin, float aMax, int numSegments)
    {
        ImGuiNative.ImDrawList_PathArcTo(Handle, center, radius, aMin, aMax, numSegments);
    }

    public void PathArcTo(Vector2 center, float radius, float aMin, float aMax)
    {
        ImGuiNative.ImDrawList_PathArcTo(Handle, center, radius, aMin, aMax, 0);
    }

    public void PathArcToFast(Vector2 center, float radius, int aMinOf12, int aMaxOf12)
    {
        ImGuiNative.ImDrawList_PathArcToFast(Handle, center, radius, aMinOf12, aMaxOf12);
    }

    public void PathBezierCubicCurveTo(Vector2 p2, Vector2 p3, Vector2 p4, int numSegments)
    {
        ImGuiNative.ImDrawList_PathBezierCubicCurveTo(Handle, p2, p3, p4, numSegments);
    }

    public void PathBezierCubicCurveTo(Vector2 p2, Vector2 p3, Vector2 p4)
    {
        ImGuiNative.ImDrawList_PathBezierCubicCurveTo(Handle, p2, p3, p4, 0);
    }

    public void PathBezierQuadraticCurveTo(Vector2 p2, Vector2 p3, int numSegments)
    {
        ImGuiNative.ImDrawList_PathBezierQuadraticCurveTo(Handle, p2, p3, numSegments);
    }

    public void PathBezierQuadraticCurveTo(Vector2 p2, Vector2 p3)
    {
        ImGuiNative.ImDrawList_PathBezierQuadraticCurveTo(Handle, p2, p3, 0);
    }

    public void PathClear()
    {
        ImGuiNative.ImDrawList_PathClear(Handle);
    }

    public void PathEllipticalArcTo(Vector2 center, Vector2 radius, float rot, float aMin, float aMax, int numSegments)
    {
        ImGuiNative.ImDrawList_PathEllipticalArcTo(Handle, center, radius, rot, aMin, aMax, numSegments);
    }

    public void PathEllipticalArcTo(Vector2 center, Vector2 radius, float rot, float aMin, float aMax)
    {
        ImGuiNative.ImDrawList_PathEllipticalArcTo(Handle, center, radius, rot, aMin, aMax, 0);
    }

    public void PathFillConcave(uint col)
    {
        ImGuiNative.ImDrawList_PathFillConcave(Handle, col);
    }

    public void PathFillConvex(uint col, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_PathFillConvex(Handle, col, flags);
    }

    public void PathFillConvex(uint col)
    {
        ImGuiNative.ImDrawList_PathFillConvex(Handle, col, (ImDrawFlags)(0));
    }

    public void PathLineTo(Vector2 pos)
    {
        ImGuiNative.ImDrawList_PathLineTo(Handle, pos);
    }

    public void PathLineToMergeDuplicate(Vector2 pos)
    {
        ImGuiNative.ImDrawList_PathLineToMergeDuplicate(Handle, pos);
    }

    public void PathRect(Vector2 rectMin, Vector2 rectMax, float rounding, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_PathRect(Handle, rectMin, rectMax, rounding, flags);
    }

    public void PathRect(Vector2 rectMin, Vector2 rectMax, float rounding)
    {
        ImGuiNative.ImDrawList_PathRect(Handle, rectMin, rectMax, rounding, (ImDrawFlags)(0));
    }

    public void PathRect(Vector2 rectMin, Vector2 rectMax)
    {
        ImGuiNative.ImDrawList_PathRect(Handle, rectMin, rectMax, 0f, (ImDrawFlags)(0));
    }

    public void PathStroke(uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList_PathStroke(Handle, col, thickness, flags);
    }

    public void PathStroke(uint col, float thickness)
    {
        ImGuiNative.ImDrawList_PathStroke(Handle, col, thickness, (ImDrawFlags)(0));
    }

    public void PathStroke(uint col)
    {
        ImGuiNative.ImDrawList_PathStroke(Handle, col, 1f, (ImDrawFlags)(0));
    }

    public void PopClipRect()
    {
        ImGuiNative.ImDrawList_PopClipRect(Handle);
    }

    public void PopDrawFlag()
    {
        ImGuiNative.ImDrawList_PopDrawFlag(Handle);
    }

    public void PopTexture()
    {
        ImGuiNative.ImDrawList_PopTexture(Handle);
    }

    public void PrimQuadUV(Vector2 a, Vector2 b, Vector2 c, Vector2 d, Vector2 uvA, Vector2 uvB, Vector2 uvC, Vector2 uvD, uint col)
    {
        ImGuiNative.ImDrawList_PrimQuadUV(Handle, a, b, c, d, uvA, uvB, uvC, uvD, col);
    }

    public void PrimRect(Vector2 a, Vector2 b, uint col)
    {
        ImGuiNative.ImDrawList_PrimRect(Handle, a, b, col);
    }

    public void PrimRectUV(Vector2 a, Vector2 b, Vector2 uvA, Vector2 uvB, uint col)
    {
        ImGuiNative.ImDrawList_PrimRectUV(Handle, a, b, uvA, uvB, col);
    }

    public void PrimReserve(int idxCount, int vtxCount)
    {
        ImGuiNative.ImDrawList_PrimReserve(Handle, idxCount, vtxCount);
    }

    public void PrimUnreserve(int idxCount, int vtxCount)
    {
        ImGuiNative.ImDrawList_PrimUnreserve(Handle, idxCount, vtxCount);
    }

    public void PrimVtx(Vector2 pos, Vector2 uv, uint col)
    {
        ImGuiNative.ImDrawList_PrimVtx(Handle, pos, uv, col);
    }

    public void PrimWriteIdx(ushort idx)
    {
        ImGuiNative.ImDrawList_PrimWriteIdx(Handle, idx);
    }

    public void PrimWriteVtx(Vector2 pos, Vector2 uv, uint col)
    {
        ImGuiNative.ImDrawList_PrimWriteVtx(Handle, pos, uv, col);
    }

    public void PushClipRect(Vector2 clipRectMin, Vector2 clipRectMax, bool intersectWithCurrentClipRect)
    {
        ImGuiNative.ImDrawList_PushClipRect(Handle, clipRectMin, clipRectMax, intersectWithCurrentClipRect);
    }

    public void PushClipRect(Vector2 clipRectMin, Vector2 clipRectMax)
    {
        ImGuiNative.ImDrawList_PushClipRect(Handle, clipRectMin, clipRectMax, false);
    }

    public void PushClipRectFullScreen()
    {
        ImGuiNative.ImDrawList_PushClipRectFullScreen(Handle);
    }

    public void PushDrawFlag(ImDrawFlags flags, bool enabled)
    {
        ImGuiNative.ImDrawList_PushDrawFlag(Handle, flags, enabled);
    }

    public void PushTexture(ImTextureRef texRef)
    {
        ImGuiNative.ImDrawList_PushTexture(Handle, texRef);
    }

    public void _AddLine(Vector2 p1, Vector2 p2, uint col, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList__AddLine(Handle, p1, p2, col, thickness, flags);
    }

    public void _AddPolyline(ReadOnlySpan<Vector2> points, uint col, float thickness, ImDrawFlags flags, float maxInnerOffset)
    {
        fixed (Vector2* pointsPointer = points)
        {
            ImGuiNative.ImDrawList__AddPolyline(Handle, pointsPointer, points.Length, col, thickness, flags, maxInnerOffset);
        }
    }

    public void _AddRectBaked(Vector2 pMin, Vector2 pMax, uint col, float r, float t, Vector4 texUvs, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList__AddRectBaked(Handle, pMin, pMax, col, r, t, texUvs, flags);
    }

    public void _AddRectFilledBaked(Vector2 pMin, Vector2 pMax, uint col, float r, Vector4 texUvs, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList__AddRectFilledBaked(Handle, pMin, pMax, col, r, texUvs, flags);
    }

    public void _AddRectTinyRounding(Vector2 pMin, Vector2 pMax, uint col, float rounding, float thickness, ImDrawFlags flags)
    {
        ImGuiNative.ImDrawList__AddRectTinyRounding(Handle, pMin, pMax, col, rounding, thickness, flags);
    }

    public int _CalcCircleAutoSegmentCount(float radius)
    {
        return ImGuiNative.ImDrawList__CalcCircleAutoSegmentCount(Handle, radius);
    }

    public float _CalculateCenterBiasedOffset(float thickness)
    {
        return ImGuiNative.ImDrawList__CalculateCenterBiasedOffset(Handle, thickness);
    }

    public void _ClearFreeMemory()
    {
        ImGuiNative.ImDrawList__ClearFreeMemory(Handle);
    }

    public void _OnChangedClipRect()
    {
        ImGuiNative.ImDrawList__OnChangedClipRect(Handle);
    }

    public void _OnChangedTexture()
    {
        ImGuiNative.ImDrawList__OnChangedTexture(Handle);
    }

    public void _OnChangedVtxOffset()
    {
        ImGuiNative.ImDrawList__OnChangedVtxOffset(Handle);
    }

    public void _PathArcToFastEx(Vector2 center, float radius, int aMinSample, int aMaxSample, int aStep)
    {
        ImGuiNative.ImDrawList__PathArcToFastEx(Handle, center, radius, aMinSample, aMaxSample, aStep);
    }

    public void _PathArcToN(Vector2 center, float radius, float aMin, float aMax, int numSegments)
    {
        ImGuiNative.ImDrawList__PathArcToN(Handle, center, radius, aMin, aMax, numSegments);
    }

    public void _PopUnusedDrawCmd()
    {
        ImGuiNative.ImDrawList__PopUnusedDrawCmd(Handle);
    }

    public void _ResetForNewFrame()
    {
        ImGuiNative.ImDrawList__ResetForNewFrame(Handle);
    }

    public void _SelectLineTexture(float screenThickness, ref Vector2 outUv0, ref Vector2 outUv1, ref float outFringe, ImDrawFlags flags)
    {
        fixed (Vector2* outUv0Pointer = &outUv0)
        fixed (Vector2* outUv1Pointer = &outUv1)
        fixed (float* outFringePointer = &outFringe)
        {
            ImGuiNative.ImDrawList__SelectLineTexture(Handle, screenThickness, outUv0Pointer, outUv1Pointer, outFringePointer, flags);
        }
    }

    public void _SetDrawListSharedData(ImDrawListSharedData* data)
    {
        ImGuiNative.ImDrawList__SetDrawListSharedData(Handle, data);
    }

    public void _SetPixelDensity(float pixelDensity)
    {
        ImGuiNative.ImDrawList__SetPixelDensity(Handle, pixelDensity);
    }

    public void _SetTexture(ImTextureRef texRef)
    {
        ImGuiNative.ImDrawList__SetTexture(Handle, texRef);
    }

    public void _TryMergeDrawCmds()
    {
        ImGuiNative.ImDrawList__TryMergeDrawCmds(Handle);
    }

    public void Destroy()
    {
        ImGuiNative.ImDrawList_destroy(Handle);
    }
}

public readonly unsafe partial struct ImDrawListSplitterPtr
{
    public readonly ImDrawListSplitter* Handle;

    public ImDrawListSplitterPtr(ImDrawListSplitter* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawListSplitterPtr(ImDrawListSplitter* handle) => new(handle);

    public static implicit operator ImDrawListSplitter*(ImDrawListSplitterPtr pointer) => pointer.Handle;

    public ref int _Current => ref Handle->_Current;

    public ref int _Count => ref Handle->_Count;

    public ref ImVector<ImDrawChannel> _Channels => ref Handle->_Channels;

    public void Clear()
    {
        ImGuiNative.ImDrawListSplitter_Clear(Handle);
    }

    public void ClearFreeMemory()
    {
        ImGuiNative.ImDrawListSplitter_ClearFreeMemory(Handle);
    }

    public static ImDrawListSplitterPtr Create()
    {
        return new ImDrawListSplitterPtr(ImGuiNative.ImDrawListSplitter_ImDrawListSplitter());
    }

    public void Merge(ImDrawListPtr drawList)
    {
        ImGuiNative.ImDrawListSplitter_Merge(Handle, drawList.Handle);
    }

    public void SetCurrentChannel(ImDrawListPtr drawList, int channelIdx)
    {
        ImGuiNative.ImDrawListSplitter_SetCurrentChannel(Handle, drawList.Handle, channelIdx);
    }

    public void Split(ImDrawListPtr drawList, int count)
    {
        ImGuiNative.ImDrawListSplitter_Split(Handle, drawList.Handle, count);
    }

    public void Destroy()
    {
        ImGuiNative.ImDrawListSplitter_destroy(Handle);
    }
}

public readonly unsafe partial struct ImDrawVertPtr
{
    public readonly ImDrawVert* Handle;

    public ImDrawVertPtr(ImDrawVert* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImDrawVertPtr(ImDrawVert* handle) => new(handle);

    public static implicit operator ImDrawVert*(ImDrawVertPtr pointer) => pointer.Handle;

    public ref Vector2 pos => ref Handle->pos;

    public ref Vector2 uv => ref Handle->uv;

    public ref uint col => ref Handle->col;
}

public readonly unsafe partial struct ImFontPtr
{
    public readonly ImFont* Handle;

    public ImFontPtr(ImFont* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontPtr(ImFont* handle) => new(handle);

    public static implicit operator ImFont*(ImFontPtr pointer) => pointer.Handle;

    public ImFontBakedPtr LastBaked
    {
        get => Handle->LastBaked;
        set => Handle->LastBaked = value;
    }

    public ImFontAtlasPtr OwnerAtlas
    {
        get => Handle->OwnerAtlas;
        set => Handle->OwnerAtlas = value;
    }

    public ref ImFontFlags Flags => ref Handle->Flags;

    public ref float CurrentRasterizerDensity => ref Handle->CurrentRasterizerDensity;

    public ref uint FontId => ref Handle->FontId;

    public ref float LegacySize => ref Handle->LegacySize;

    public ref ImPtrVector<ImFontConfig> Sources => ref Handle->Sources;

    public ref ushort EllipsisChar => ref Handle->EllipsisChar;

    public ref ushort FallbackChar => ref Handle->FallbackChar;

    public ref FixedArray1<byte> Used8kPagesMap => ref Handle->Used8kPagesMap;

    public ref bool EllipsisAutoBake => ref Handle->EllipsisAutoBake;

    public ref ImGuiStorage RemapPairs => ref Handle->RemapPairs;

    public void AddRemapCodepoint(ushort fromCodepoint, ushort toCodepoint)
    {
        ImGuiNative.ImFont_AddRemapCodepoint(Handle, fromCodepoint, toCodepoint);
    }

    public void AddRemapCodepointToGlyphIndex(ushort fromCodepoint, int fontSrcIdx, int glyphIdx)
    {
        ImGuiNative.ImFont_AddRemapCodepointToGlyphIndex(Handle, fromCodepoint, fontSrcIdx, glyphIdx);
    }

    public Vector2 CalcTextSizeA(float size, float maxWidth, float wrapWidth, string textBegin, byte** outRemaining)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImFont_CalcTextSizeA(Handle, size, maxWidth, wrapWidth, textBeginUtf8.Pointer, null, outRemaining);
    }

    public Vector2 CalcTextSizeA(float size, float maxWidth, float wrapWidth, string textBegin)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImFont_CalcTextSizeA(Handle, size, maxWidth, wrapWidth, textBeginUtf8.Pointer, null, null);
    }

    public string CalcWordWrapPosition(float size, string text, float wrapWidth)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiUtf8.ToString(ImGuiNative.ImFont_CalcWordWrapPosition(Handle, size, textUtf8.Pointer, null, wrapWidth));
    }

    public void ClearOutputData()
    {
        ImGuiNative.ImFont_ClearOutputData(Handle);
    }

    public string GetDebugName()
    {
        return ImGuiUtf8.ToString(ImGuiNative.ImFont_GetDebugName(Handle));
    }

    public ImFontBakedPtr GetFontBaked(float fontSize, float density)
    {
        return new ImFontBakedPtr(ImGuiNative.ImFont_GetFontBaked(Handle, fontSize, density));
    }

    public ImFontBakedPtr GetFontBaked(float fontSize)
    {
        return new ImFontBakedPtr(ImGuiNative.ImFont_GetFontBaked(Handle, fontSize, -1f));
    }

    public static ImFontPtr Create()
    {
        return new ImFontPtr(ImGuiNative.ImFont_ImFont());
    }

    public bool IsGlyphInFont(ushort c)
    {
        return ImGuiNative.ImFont_IsGlyphInFont(Handle, c);
    }

    public bool IsGlyphRangeUnused(uint cBegin, uint cLast)
    {
        return ImGuiNative.ImFont_IsGlyphRangeUnused(Handle, cBegin, cLast);
    }

    public bool IsLoaded()
    {
        return ImGuiNative.ImFont_IsLoaded(Handle);
    }

    public void RenderChar(ImDrawListPtr drawList, float size, Vector2 pos, uint col, ushort c, Vector4* cpuFineClip)
    {
        ImGuiNative.ImFont_RenderChar(Handle, drawList.Handle, size, pos, col, c, cpuFineClip);
    }

    public void RenderChar(ImDrawListPtr drawList, float size, Vector2 pos, uint col, ushort c)
    {
        ImGuiNative.ImFont_RenderChar(Handle, drawList.Handle, size, pos, col, c, null);
    }

    public void RenderText(ImDrawListPtr drawList, float size, Vector2 pos, uint col, Vector4 clipRect, string textBegin, float wrapWidth, int flags)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImFont_RenderText(Handle, drawList.Handle, size, pos, col, clipRect, textBeginUtf8.Pointer, null, wrapWidth, flags);
    }

    public void RenderText(ImDrawListPtr drawList, float size, Vector2 pos, uint col, Vector4 clipRect, string textBegin, float wrapWidth)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImFont_RenderText(Handle, drawList.Handle, size, pos, col, clipRect, textBeginUtf8.Pointer, null, wrapWidth, 0);
    }

    public void RenderText(ImDrawListPtr drawList, float size, Vector2 pos, uint col, Vector4 clipRect, string textBegin)
    {
        using var textBeginUtf8 = new ImGuiUtf8String(textBegin, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImFont_RenderText(Handle, drawList.Handle, size, pos, col, clipRect, textBeginUtf8.Pointer, null, 0f, 0);
    }

    public void Destroy()
    {
        ImGuiNative.ImFont_destroy(Handle);
    }
}

public readonly unsafe partial struct ImFontAtlasPtr
{
    public readonly ImFontAtlas* Handle;

    public ImFontAtlasPtr(ImFontAtlas* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontAtlasPtr(ImFontAtlas* handle) => new(handle);

    public static implicit operator ImFontAtlas*(ImFontAtlasPtr pointer) => pointer.Handle;

    public ref ImFontAtlasFlags Flags => ref Handle->Flags;

    public ref ImTextureFormat TexDesiredFormat => ref Handle->TexDesiredFormat;

    public ref int TexGlyphPadding => ref Handle->TexGlyphPadding;

    public ref int TexMinWidth => ref Handle->TexMinWidth;

    public ref int TexMinHeight => ref Handle->TexMinHeight;

    public ref int TexMaxWidth => ref Handle->TexMaxWidth;

    public ref int TexMaxHeight => ref Handle->TexMaxHeight;

    public ref void* UserData => ref Handle->UserData;

    public ref ImTextureRef TexRef => ref Handle->TexRef;

    public ImTextureDataPtr TexData
    {
        get => Handle->TexData;
        set => Handle->TexData = value;
    }

    public ref ImPtrVector<ImTextureData> TexList => ref Handle->TexList;

    public ref bool Locked => ref Handle->Locked;

    public ref bool RendererHasTextures => ref Handle->RendererHasTextures;

    public ref bool TexIsBuilt => ref Handle->TexIsBuilt;

    public ref bool TexPixelsUseColors => ref Handle->TexPixelsUseColors;

    public ref Vector2 TexUvScale => ref Handle->TexUvScale;

    public ref Vector2 TexUvWhitePixel => ref Handle->TexUvWhitePixel;

    public ref ImPtrVector<ImFont> Fonts => ref Handle->Fonts;

    public ref ImVector<ImFontConfig> Sources => ref Handle->Sources;

    public ref int TexNextUniqueID => ref Handle->TexNextUniqueID;

    public ref int FontNextUniqueID => ref Handle->FontNextUniqueID;

    public ref ImPtrVector<ImDrawListSharedData> DrawListSharedDatas => ref Handle->DrawListSharedDatas;

    public ref ImFontAtlasBuilder* Builder => ref Handle->Builder;

    public ref ImFontLoader* FontLoader => ref Handle->FontLoader;

    public ref byte* FontLoaderName => ref Handle->FontLoaderName;

    public ref void* FontLoaderData => ref Handle->FontLoaderData;

    public ref uint FontLoaderFlags => ref Handle->FontLoaderFlags;

    public ref int RefCount => ref Handle->RefCount;

    public ref ImGuiContext* OwnerContext => ref Handle->OwnerContext;

    public int AddCustomRect(int width, int height, ImFontAtlasRectPtr outR)
    {
        return ImGuiNative.ImFontAtlas_AddCustomRect(Handle, width, height, outR.Handle);
    }

    public int AddCustomRect(int width, int height)
    {
        return ImGuiNative.ImFontAtlas_AddCustomRect(Handle, width, height, null);
    }

    public ImFontPtr AddFont(ImFontConfigPtr fontCfg)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFont(Handle, fontCfg.Handle));
    }

    public ImFontPtr AddFontDefault(ImFontConfigPtr fontCfg)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontDefault(Handle, fontCfg.Handle));
    }

    public ImFontPtr AddFontDefault()
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontDefault(Handle, null));
    }

    public ImFontPtr AddFontDefaultBitmap(ImFontConfigPtr fontCfg)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontDefaultBitmap(Handle, fontCfg.Handle));
    }

    public ImFontPtr AddFontDefaultBitmap()
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontDefaultBitmap(Handle, null));
    }

    public ImFontPtr AddFontDefaultVector(ImFontConfigPtr fontCfg)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontDefaultVector(Handle, fontCfg.Handle));
    }

    public ImFontPtr AddFontDefaultVector()
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontDefaultVector(Handle, null));
    }

    public ImFontPtr AddFontFromFileTTF(string filename, float sizePixels, ImFontConfigPtr fontCfg, ushort* glyphRanges)
    {
        using var filenameUtf8 = new ImGuiUtf8String(filename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromFileTTF(Handle, filenameUtf8.Pointer, sizePixels, fontCfg.Handle, glyphRanges));
    }

    public ImFontPtr AddFontFromFileTTF(string filename, float sizePixels, ImFontConfigPtr fontCfg)
    {
        using var filenameUtf8 = new ImGuiUtf8String(filename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromFileTTF(Handle, filenameUtf8.Pointer, sizePixels, fontCfg.Handle, null));
    }

    public ImFontPtr AddFontFromFileTTF(string filename, float sizePixels)
    {
        using var filenameUtf8 = new ImGuiUtf8String(filename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromFileTTF(Handle, filenameUtf8.Pointer, sizePixels, null, null));
    }

    public ImFontPtr AddFontFromFileTTF(string filename)
    {
        using var filenameUtf8 = new ImGuiUtf8String(filename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromFileTTF(Handle, filenameUtf8.Pointer, 0f, null, null));
    }

    public ImFontPtr AddFontFromMemoryCompressedBase85TTF(string compressedFontDataBase85, float sizePixels, ImFontConfigPtr fontCfg, ushort* glyphRanges)
    {
        using var compressedFontDataBase85Utf8 = new ImGuiUtf8String(compressedFontDataBase85, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(Handle, compressedFontDataBase85Utf8.Pointer, sizePixels, fontCfg.Handle, glyphRanges));
    }

    public ImFontPtr AddFontFromMemoryCompressedBase85TTF(string compressedFontDataBase85, float sizePixels, ImFontConfigPtr fontCfg)
    {
        using var compressedFontDataBase85Utf8 = new ImGuiUtf8String(compressedFontDataBase85, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(Handle, compressedFontDataBase85Utf8.Pointer, sizePixels, fontCfg.Handle, null));
    }

    public ImFontPtr AddFontFromMemoryCompressedBase85TTF(string compressedFontDataBase85, float sizePixels)
    {
        using var compressedFontDataBase85Utf8 = new ImGuiUtf8String(compressedFontDataBase85, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(Handle, compressedFontDataBase85Utf8.Pointer, sizePixels, null, null));
    }

    public ImFontPtr AddFontFromMemoryCompressedBase85TTF(string compressedFontDataBase85)
    {
        using var compressedFontDataBase85Utf8 = new ImGuiUtf8String(compressedFontDataBase85, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(Handle, compressedFontDataBase85Utf8.Pointer, 0f, null, null));
    }

    public ImFontPtr AddFontFromMemoryCompressedTTF(void* compressedFontData, int compressedFontDataSize, float sizePixels, ImFontConfigPtr fontCfg, ushort* glyphRanges)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedTTF(Handle, compressedFontData, compressedFontDataSize, sizePixels, fontCfg.Handle, glyphRanges));
    }

    public ImFontPtr AddFontFromMemoryCompressedTTF(void* compressedFontData, int compressedFontDataSize, float sizePixels, ImFontConfigPtr fontCfg)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedTTF(Handle, compressedFontData, compressedFontDataSize, sizePixels, fontCfg.Handle, null));
    }

    public ImFontPtr AddFontFromMemoryCompressedTTF(void* compressedFontData, int compressedFontDataSize, float sizePixels)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedTTF(Handle, compressedFontData, compressedFontDataSize, sizePixels, null, null));
    }

    public ImFontPtr AddFontFromMemoryCompressedTTF(void* compressedFontData, int compressedFontDataSize)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryCompressedTTF(Handle, compressedFontData, compressedFontDataSize, 0f, null, null));
    }

    public ImFontPtr AddFontFromMemoryTTF(void* fontData, int fontDataSize, float sizePixels, ImFontConfigPtr fontCfg, ushort* glyphRanges)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryTTF(Handle, fontData, fontDataSize, sizePixels, fontCfg.Handle, glyphRanges));
    }

    public ImFontPtr AddFontFromMemoryTTF(void* fontData, int fontDataSize, float sizePixels, ImFontConfigPtr fontCfg)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryTTF(Handle, fontData, fontDataSize, sizePixels, fontCfg.Handle, null));
    }

    public ImFontPtr AddFontFromMemoryTTF(void* fontData, int fontDataSize, float sizePixels)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryTTF(Handle, fontData, fontDataSize, sizePixels, null, null));
    }

    public ImFontPtr AddFontFromMemoryTTF(void* fontData, int fontDataSize)
    {
        return new ImFontPtr(ImGuiNative.ImFontAtlas_AddFontFromMemoryTTF(Handle, fontData, fontDataSize, 0f, null, null));
    }

    public void Clear()
    {
        ImGuiNative.ImFontAtlas_Clear(Handle);
    }

    public void ClearFonts()
    {
        ImGuiNative.ImFontAtlas_ClearFonts(Handle);
    }

    public void ClearInputData()
    {
        ImGuiNative.ImFontAtlas_ClearInputData(Handle);
    }

    public void ClearTexData()
    {
        ImGuiNative.ImFontAtlas_ClearTexData(Handle);
    }

    public void CompactCache()
    {
        ImGuiNative.ImFontAtlas_CompactCache(Handle);
    }

    public bool GetCustomRect(int id, ImFontAtlasRectPtr outR)
    {
        return ImGuiNative.ImFontAtlas_GetCustomRect(Handle, id, outR.Handle);
    }

    public ushort* GetGlyphRangesDefault()
    {
        return ImGuiNative.ImFontAtlas_GetGlyphRangesDefault(Handle);
    }

    public static ImFontAtlasPtr Create()
    {
        return new ImFontAtlasPtr(ImGuiNative.ImFontAtlas_ImFontAtlas());
    }

    public void RemoveCustomRect(int id)
    {
        ImGuiNative.ImFontAtlas_RemoveCustomRect(Handle, id);
    }

    public void RemoveFont(ImFontPtr font)
    {
        ImGuiNative.ImFontAtlas_RemoveFont(Handle, font.Handle);
    }

    public void SetFontLoader(ImFontLoader* fontLoader)
    {
        ImGuiNative.ImFontAtlas_SetFontLoader(Handle, fontLoader);
    }

    public void Destroy()
    {
        ImGuiNative.ImFontAtlas_destroy(Handle);
    }
}

public readonly unsafe partial struct ImFontAtlasRectPtr
{
    public readonly ImFontAtlasRect* Handle;

    public ImFontAtlasRectPtr(ImFontAtlasRect* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontAtlasRectPtr(ImFontAtlasRect* handle) => new(handle);

    public static implicit operator ImFontAtlasRect*(ImFontAtlasRectPtr pointer) => pointer.Handle;

    public ref ushort x => ref Handle->x;

    public ref ushort y => ref Handle->y;

    public ref ushort w => ref Handle->w;

    public ref ushort h => ref Handle->h;

    public ref Vector2 uv0 => ref Handle->uv0;

    public ref Vector2 uv1 => ref Handle->uv1;

    public static ImFontAtlasRectPtr Create()
    {
        return new ImFontAtlasRectPtr(ImGuiNative.ImFontAtlasRect_ImFontAtlasRect());
    }

    public void Destroy()
    {
        ImGuiNative.ImFontAtlasRect_destroy(Handle);
    }
}

public readonly unsafe partial struct ImFontBakedPtr
{
    public readonly ImFontBaked* Handle;

    public ImFontBakedPtr(ImFontBaked* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontBakedPtr(ImFontBaked* handle) => new(handle);

    public static implicit operator ImFontBaked*(ImFontBakedPtr pointer) => pointer.Handle;

    public ref ImVector<float> IndexAdvanceX => ref Handle->IndexAdvanceX;

    public ref float FallbackAdvanceX => ref Handle->FallbackAdvanceX;

    public ref float Size => ref Handle->Size;

    public ref float RasterizerDensity => ref Handle->RasterizerDensity;

    public ref ImVector<ushort> IndexLookup => ref Handle->IndexLookup;

    public ref ImVector<ImFontGlyph> Glyphs => ref Handle->Glyphs;

    public ref int FallbackGlyphIndex => ref Handle->FallbackGlyphIndex;

    public ref float Ascent => ref Handle->Ascent;

    public ref float Descent => ref Handle->Descent;

    public uint MetricsTotalSurface
    {
        get => Handle->MetricsTotalSurface;
        set => Handle->MetricsTotalSurface = value;
    }

    public uint WantDestroy
    {
        get => Handle->WantDestroy;
        set => Handle->WantDestroy = value;
    }

    public uint LoadNoFallback
    {
        get => Handle->LoadNoFallback;
        set => Handle->LoadNoFallback = value;
    }

    public uint LoadNoRenderOnLayout
    {
        get => Handle->LoadNoRenderOnLayout;
        set => Handle->LoadNoRenderOnLayout = value;
    }

    public ref int LastUsedFrame => ref Handle->LastUsedFrame;

    public ref uint BakedId => ref Handle->BakedId;

    public ImFontPtr OwnerFont
    {
        get => Handle->OwnerFont;
        set => Handle->OwnerFont = value;
    }

    public ref void* FontLoaderDatas => ref Handle->FontLoaderDatas;

    public void ClearOutputData()
    {
        ImGuiNative.ImFontBaked_ClearOutputData(Handle);
    }

    public ImFontGlyphPtr FindGlyph(ushort c)
    {
        return new ImFontGlyphPtr(ImGuiNative.ImFontBaked_FindGlyph(Handle, c));
    }

    public ImFontGlyphPtr FindGlyphNoFallback(ushort c)
    {
        return new ImFontGlyphPtr(ImGuiNative.ImFontBaked_FindGlyphNoFallback(Handle, c));
    }

    public float GetCharAdvance(ushort c)
    {
        return ImGuiNative.ImFontBaked_GetCharAdvance(Handle, c);
    }

    public static ImFontBakedPtr Create()
    {
        return new ImFontBakedPtr(ImGuiNative.ImFontBaked_ImFontBaked());
    }

    public bool IsGlyphLoaded(ushort c)
    {
        return ImGuiNative.ImFontBaked_IsGlyphLoaded(Handle, c);
    }

    public void Destroy()
    {
        ImGuiNative.ImFontBaked_destroy(Handle);
    }
}

public readonly unsafe partial struct ImFontConfigPtr
{
    public readonly ImFontConfig* Handle;

    public ImFontConfigPtr(ImFontConfig* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontConfigPtr(ImFontConfig* handle) => new(handle);

    public static implicit operator ImFontConfig*(ImFontConfigPtr pointer) => pointer.Handle;

    public ref FixedArray40<byte> Name => ref Handle->Name;

    public ref void* FontData => ref Handle->FontData;

    public ref int FontDataSize => ref Handle->FontDataSize;

    public ref bool FontDataOwnedByAtlas => ref Handle->FontDataOwnedByAtlas;

    public ref bool MergeMode => ref Handle->MergeMode;

    public ref bool PixelSnapH => ref Handle->PixelSnapH;

    public ref sbyte OversampleH => ref Handle->OversampleH;

    public ref sbyte OversampleV => ref Handle->OversampleV;

    public ref ushort EllipsisChar => ref Handle->EllipsisChar;

    public ref float SizePixels => ref Handle->SizePixels;

    public ref ushort* GlyphRanges => ref Handle->GlyphRanges;

    public ref ushort* GlyphExcludeRanges => ref Handle->GlyphExcludeRanges;

    public ref Vector2 GlyphOffset => ref Handle->GlyphOffset;

    public ref float GlyphMinAdvanceX => ref Handle->GlyphMinAdvanceX;

    public ref float GlyphMaxAdvanceX => ref Handle->GlyphMaxAdvanceX;

    public ref float GlyphExtraAdvanceX => ref Handle->GlyphExtraAdvanceX;

    public ref uint FontNo => ref Handle->FontNo;

    public ref uint FontLoaderFlags => ref Handle->FontLoaderFlags;

    public ref float RasterizerMultiply => ref Handle->RasterizerMultiply;

    public ref float RasterizerDensity => ref Handle->RasterizerDensity;

    public ref float ExtraSizeScale => ref Handle->ExtraSizeScale;

    public ref ImFontFlags Flags => ref Handle->Flags;

    public ImFontPtr DstFont
    {
        get => Handle->DstFont;
        set => Handle->DstFont = value;
    }

    public ref ImFontLoader* FontLoader => ref Handle->FontLoader;

    public ref void* FontLoaderData => ref Handle->FontLoaderData;

    public static ImFontConfigPtr Create()
    {
        return new ImFontConfigPtr(ImGuiNative.ImFontConfig_ImFontConfig());
    }

    public void Destroy()
    {
        ImGuiNative.ImFontConfig_destroy(Handle);
    }
}

public readonly unsafe partial struct ImFontGlyphPtr
{
    public readonly ImFontGlyph* Handle;

    public ImFontGlyphPtr(ImFontGlyph* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontGlyphPtr(ImFontGlyph* handle) => new(handle);

    public static implicit operator ImFontGlyph*(ImFontGlyphPtr pointer) => pointer.Handle;

    public uint Colored
    {
        get => Handle->Colored;
        set => Handle->Colored = value;
    }

    public uint Visible
    {
        get => Handle->Visible;
        set => Handle->Visible = value;
    }

    public uint SourceIdx
    {
        get => Handle->SourceIdx;
        set => Handle->SourceIdx = value;
    }

    public uint Codepoint
    {
        get => Handle->Codepoint;
        set => Handle->Codepoint = value;
    }

    public ref float AdvanceX => ref Handle->AdvanceX;

    public ref float X0 => ref Handle->X0;

    public ref float Y0 => ref Handle->Y0;

    public ref float X1 => ref Handle->X1;

    public ref float Y1 => ref Handle->Y1;

    public ref float U0 => ref Handle->U0;

    public ref float V0 => ref Handle->V0;

    public ref float U1 => ref Handle->U1;

    public ref float V1 => ref Handle->V1;

    public ref int PackId => ref Handle->PackId;

    public static ImFontGlyphPtr Create()
    {
        return new ImFontGlyphPtr(ImGuiNative.ImFontGlyph_ImFontGlyph());
    }

    public void Destroy()
    {
        ImGuiNative.ImFontGlyph_destroy(Handle);
    }
}

public readonly unsafe partial struct ImFontGlyphRangesBuilderPtr
{
    public readonly ImFontGlyphRangesBuilder* Handle;

    public ImFontGlyphRangesBuilderPtr(ImFontGlyphRangesBuilder* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImFontGlyphRangesBuilderPtr(ImFontGlyphRangesBuilder* handle) => new(handle);

    public static implicit operator ImFontGlyphRangesBuilder*(ImFontGlyphRangesBuilderPtr pointer) => pointer.Handle;

    public ref ImVector<uint> UsedChars => ref Handle->UsedChars;

    public void AddChar(ushort c)
    {
        ImGuiNative.ImFontGlyphRangesBuilder_AddChar(Handle, c);
    }

    public void AddRanges(ushort* ranges)
    {
        ImGuiNative.ImFontGlyphRangesBuilder_AddRanges(Handle, ranges);
    }

    public void AddText(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImFontGlyphRangesBuilder_AddText(Handle, textUtf8.Pointer, null);
    }

    public void BuildRanges(ImVector<ushort>* outRanges)
    {
        ImGuiNative.ImFontGlyphRangesBuilder_BuildRanges(Handle, outRanges);
    }

    public void Clear()
    {
        ImGuiNative.ImFontGlyphRangesBuilder_Clear(Handle);
    }

    public bool GetBit(nuint n)
    {
        return ImGuiNative.ImFontGlyphRangesBuilder_GetBit(Handle, n);
    }

    public static ImFontGlyphRangesBuilderPtr Create()
    {
        return new ImFontGlyphRangesBuilderPtr(ImGuiNative.ImFontGlyphRangesBuilder_ImFontGlyphRangesBuilder());
    }

    public void SetBit(nuint n)
    {
        ImGuiNative.ImFontGlyphRangesBuilder_SetBit(Handle, n);
    }

    public void Destroy()
    {
        ImGuiNative.ImFontGlyphRangesBuilder_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiIOPtr
{
    public readonly ImGuiIO* Handle;

    public ImGuiIOPtr(ImGuiIO* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiIOPtr(ImGuiIO* handle) => new(handle);

    public static implicit operator ImGuiIO*(ImGuiIOPtr pointer) => pointer.Handle;

    public ref ImGuiConfigFlags ConfigFlags => ref Handle->ConfigFlags;

    public ref ImGuiBackendFlags BackendFlags => ref Handle->BackendFlags;

    public ref Vector2 DisplaySize => ref Handle->DisplaySize;

    public ref Vector2 DisplayFramebufferScale => ref Handle->DisplayFramebufferScale;

    public ref float DeltaTime => ref Handle->DeltaTime;

    public ref float IniSavingRate => ref Handle->IniSavingRate;

    public ref byte* IniFilename => ref Handle->IniFilename;

    public ref byte* LogFilename => ref Handle->LogFilename;

    public ref void* UserData => ref Handle->UserData;

    public ImFontAtlasPtr Fonts
    {
        get => Handle->Fonts;
        set => Handle->Fonts = value;
    }

    public ImFontPtr FontDefault
    {
        get => Handle->FontDefault;
        set => Handle->FontDefault = value;
    }

    public ref bool FontAllowUserScaling => ref Handle->FontAllowUserScaling;

    public ref bool ConfigNavSwapGamepadButtons => ref Handle->ConfigNavSwapGamepadButtons;

    public ref bool ConfigNavMoveSetMousePos => ref Handle->ConfigNavMoveSetMousePos;

    public ref bool ConfigNavCaptureKeyboard => ref Handle->ConfigNavCaptureKeyboard;

    public ref bool ConfigNavEscapeClearFocusItem => ref Handle->ConfigNavEscapeClearFocusItem;

    public ref bool ConfigNavEscapeClearFocusWindow => ref Handle->ConfigNavEscapeClearFocusWindow;

    public ref bool ConfigNavCursorVisibleAuto => ref Handle->ConfigNavCursorVisibleAuto;

    public ref bool ConfigNavCursorVisibleAlways => ref Handle->ConfigNavCursorVisibleAlways;

    public ref bool ConfigDockingNoSplit => ref Handle->ConfigDockingNoSplit;

    public ref bool ConfigDockingNoDockingOver => ref Handle->ConfigDockingNoDockingOver;

    public ref bool ConfigDockingWithShift => ref Handle->ConfigDockingWithShift;

    public ref bool ConfigDockingAlwaysTabBar => ref Handle->ConfigDockingAlwaysTabBar;

    public ref bool ConfigDockingTransparentPayload => ref Handle->ConfigDockingTransparentPayload;

    public ref bool ConfigViewportsNoAutoMerge => ref Handle->ConfigViewportsNoAutoMerge;

    public ref bool ConfigViewportsNoTaskBarIcon => ref Handle->ConfigViewportsNoTaskBarIcon;

    public ref bool ConfigViewportsNoDecoration => ref Handle->ConfigViewportsNoDecoration;

    public ref bool ConfigViewportsNoDefaultParent => ref Handle->ConfigViewportsNoDefaultParent;

    public ref bool ConfigViewportsPlatformFocusSetsImGuiFocus => ref Handle->ConfigViewportsPlatformFocusSetsImGuiFocus;

    public ref bool ConfigDpiScaleFonts => ref Handle->ConfigDpiScaleFonts;

    public ref bool ConfigDpiScaleViewports => ref Handle->ConfigDpiScaleViewports;

    public ref bool ConfigMacOSXBehaviors => ref Handle->ConfigMacOSXBehaviors;

    public ref bool ConfigInputTrickleEventQueue => ref Handle->ConfigInputTrickleEventQueue;

    public ref bool ConfigInputTextCursorBlink => ref Handle->ConfigInputTextCursorBlink;

    public ref bool ConfigInputTextEnterKeepActive => ref Handle->ConfigInputTextEnterKeepActive;

    public ref ImGuiColorEditFlags ConfigColorEditFlags => ref Handle->ConfigColorEditFlags;

    public ref bool ConfigDragClickToInputText => ref Handle->ConfigDragClickToInputText;

    public ref bool ConfigWindowsResizeFromEdges => ref Handle->ConfigWindowsResizeFromEdges;

    public ref bool ConfigWindowsMoveFromTitleBarOnly => ref Handle->ConfigWindowsMoveFromTitleBarOnly;

    public ref bool ConfigWindowsCopyContentsWithCtrlC => ref Handle->ConfigWindowsCopyContentsWithCtrlC;

    public ref bool ConfigScrollbarScrollByPage => ref Handle->ConfigScrollbarScrollByPage;

    public ref bool ConfigIniSettingsSaveLastUsedDate => ref Handle->ConfigIniSettingsSaveLastUsedDate;

    public ref int ConfigIniSettingsAutoDiscardMonths => ref Handle->ConfigIniSettingsAutoDiscardMonths;

    public ref bool ConfigDebugIniSettings => ref Handle->ConfigDebugIniSettings;

    public ref bool MouseDrawCursor => ref Handle->MouseDrawCursor;

    public ref float ConfigMemoryCompactTimer => ref Handle->ConfigMemoryCompactTimer;

    public ref float MouseDoubleClickTime => ref Handle->MouseDoubleClickTime;

    public ref float MouseDoubleClickMaxDist => ref Handle->MouseDoubleClickMaxDist;

    public ref float MouseSingleClickDelay => ref Handle->MouseSingleClickDelay;

    public ref float MouseDragThreshold => ref Handle->MouseDragThreshold;

    public ref float KeyRepeatDelay => ref Handle->KeyRepeatDelay;

    public ref float KeyRepeatRate => ref Handle->KeyRepeatRate;

    public ref bool ConfigErrorRecovery => ref Handle->ConfigErrorRecovery;

    public ref bool ConfigErrorRecoveryEnableAssert => ref Handle->ConfigErrorRecoveryEnableAssert;

    public ref bool ConfigErrorRecoveryEnableDebugLog => ref Handle->ConfigErrorRecoveryEnableDebugLog;

    public ref bool ConfigErrorRecoveryEnableTooltip => ref Handle->ConfigErrorRecoveryEnableTooltip;

    public ref bool ConfigDebugIsDebuggerPresent => ref Handle->ConfigDebugIsDebuggerPresent;

    public ref bool ConfigDebugHighlightIdConflicts => ref Handle->ConfigDebugHighlightIdConflicts;

    public ref bool ConfigDebugHighlightIdConflictsShowItemPicker => ref Handle->ConfigDebugHighlightIdConflictsShowItemPicker;

    public ref bool ConfigDebugBeginReturnValueOnce => ref Handle->ConfigDebugBeginReturnValueOnce;

    public ref bool ConfigDebugBeginReturnValueLoop => ref Handle->ConfigDebugBeginReturnValueLoop;

    public ref bool ConfigDebugIgnoreFocusLoss => ref Handle->ConfigDebugIgnoreFocusLoss;

    public ref bool ConfigDebugDrawListDefaultsToStrokeLegacy => ref Handle->ConfigDebugDrawListDefaultsToStrokeLegacy;

    public ref byte* BackendPlatformName => ref Handle->BackendPlatformName;

    public ref byte* BackendRendererName => ref Handle->BackendRendererName;

    public ref void* BackendPlatformUserData => ref Handle->BackendPlatformUserData;

    public ref void* BackendRendererUserData => ref Handle->BackendRendererUserData;

    public ref void* BackendLanguageUserData => ref Handle->BackendLanguageUserData;

    public ref bool WantCaptureMouse => ref Handle->WantCaptureMouse;

    public ref bool WantCaptureKeyboard => ref Handle->WantCaptureKeyboard;

    public ref bool WantTextInput => ref Handle->WantTextInput;

    public ref bool WantSetMousePos => ref Handle->WantSetMousePos;

    public ref bool WantSaveIniSettings => ref Handle->WantSaveIniSettings;

    public ref bool NavActive => ref Handle->NavActive;

    public ref bool NavVisible => ref Handle->NavVisible;

    public ref float Framerate => ref Handle->Framerate;

    public ref int MetricsRenderVertices => ref Handle->MetricsRenderVertices;

    public ref int MetricsRenderIndices => ref Handle->MetricsRenderIndices;

    public ref int MetricsRenderWindows => ref Handle->MetricsRenderWindows;

    public ref int MetricsActiveWindows => ref Handle->MetricsActiveWindows;

    public ref Vector2 MouseDelta => ref Handle->MouseDelta;

    public ref ImGuiContext* Ctx => ref Handle->Ctx;

    public ref Vector2 MousePos => ref Handle->MousePos;

    public ref FixedArray5<bool> MouseDown => ref Handle->MouseDown;

    public ref float MouseWheel => ref Handle->MouseWheel;

    public ref float MouseWheelH => ref Handle->MouseWheelH;

    public ref ImGuiMouseSource MouseSource => ref Handle->MouseSource;

    public ref uint MouseHoveredViewport => ref Handle->MouseHoveredViewport;

    public ref bool KeyCtrl => ref Handle->KeyCtrl;

    public ref bool KeyShift => ref Handle->KeyShift;

    public ref bool KeyAlt => ref Handle->KeyAlt;

    public ref bool KeySuper => ref Handle->KeySuper;

    public ref int KeyMods => ref Handle->KeyMods;

    public ref FixedArray155<ImGuiKeyData> KeysData => ref Handle->KeysData;

    public ref bool WantCaptureMouseUnlessPopupClose => ref Handle->WantCaptureMouseUnlessPopupClose;

    public ref Vector2 MousePosPrev => ref Handle->MousePosPrev;

    public ref FixedArray5<Vector2> MouseClickedPos => ref Handle->MouseClickedPos;

    public ref FixedArray5<double> MouseClickedTime => ref Handle->MouseClickedTime;

    public ref FixedArray5<bool> MouseClicked => ref Handle->MouseClicked;

    public ref FixedArray5<bool> MouseDoubleClicked => ref Handle->MouseDoubleClicked;

    public ref FixedArray5<ushort> MouseClickedCount => ref Handle->MouseClickedCount;

    public ref FixedArray5<ushort> MouseClickedLastCount => ref Handle->MouseClickedLastCount;

    public ref FixedArray5<bool> MouseReleased => ref Handle->MouseReleased;

    public ref FixedArray5<double> MouseReleasedTime => ref Handle->MouseReleasedTime;

    public ref FixedArray5<bool> MouseDownOwned => ref Handle->MouseDownOwned;

    public ref FixedArray5<bool> MouseDownOwnedUnlessPopupClose => ref Handle->MouseDownOwnedUnlessPopupClose;

    public ref bool MouseWheelRequestAxisSwap => ref Handle->MouseWheelRequestAxisSwap;

    public ref bool MouseCtrlLeftAsRightClick => ref Handle->MouseCtrlLeftAsRightClick;

    public ref FixedArray5<float> MouseDownDuration => ref Handle->MouseDownDuration;

    public ref FixedArray5<float> MouseDownDurationPrev => ref Handle->MouseDownDurationPrev;

    public ref FixedArray5<Vector2> MouseDragMaxDistanceAbs => ref Handle->MouseDragMaxDistanceAbs;

    public ref FixedArray5<float> MouseDragMaxDistanceSqr => ref Handle->MouseDragMaxDistanceSqr;

    public ref float PenPressure => ref Handle->PenPressure;

    public ref bool AppFocusLost => ref Handle->AppFocusLost;

    public ref bool AppAcceptingEvents => ref Handle->AppAcceptingEvents;

    public ref ushort InputQueueSurrogate => ref Handle->InputQueueSurrogate;

    public ref ImVector<ushort> InputQueueCharacters => ref Handle->InputQueueCharacters;

    public void AddFocusEvent(bool focused)
    {
        ImGuiNative.ImGuiIO_AddFocusEvent(Handle, focused);
    }

    public void AddInputCharacter(uint c)
    {
        ImGuiNative.ImGuiIO_AddInputCharacter(Handle, c);
    }

    public void AddInputCharacterUTF16(ushort c)
    {
        ImGuiNative.ImGuiIO_AddInputCharacterUTF16(Handle, c);
    }

    public void AddInputCharactersUTF8(string str)
    {
        using var strUtf8 = new ImGuiUtf8String(str, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImGuiIO_AddInputCharactersUTF8(Handle, strUtf8.Pointer);
    }

    public void AddKeyAnalogEvent(ImGuiKey key, bool down, float v)
    {
        ImGuiNative.ImGuiIO_AddKeyAnalogEvent(Handle, key, down, v);
    }

    public void AddKeyEvent(ImGuiKey key, bool down)
    {
        ImGuiNative.ImGuiIO_AddKeyEvent(Handle, key, down);
    }

    public void AddMouseButtonEvent(int button, bool down)
    {
        ImGuiNative.ImGuiIO_AddMouseButtonEvent(Handle, button, down);
    }

    public void AddMousePosEvent(float x, float y)
    {
        ImGuiNative.ImGuiIO_AddMousePosEvent(Handle, x, y);
    }

    public void AddMouseSourceEvent(ImGuiMouseSource source)
    {
        ImGuiNative.ImGuiIO_AddMouseSourceEvent(Handle, source);
    }

    public void AddMouseViewportEvent(uint id)
    {
        ImGuiNative.ImGuiIO_AddMouseViewportEvent(Handle, id);
    }

    public void AddMouseWheelEvent(float wheelX, float wheelY)
    {
        ImGuiNative.ImGuiIO_AddMouseWheelEvent(Handle, wheelX, wheelY);
    }

    public void ClearEventsQueue()
    {
        ImGuiNative.ImGuiIO_ClearEventsQueue(Handle);
    }

    public void ClearInputKeys()
    {
        ImGuiNative.ImGuiIO_ClearInputKeys(Handle);
    }

    public void ClearInputMouse()
    {
        ImGuiNative.ImGuiIO_ClearInputMouse(Handle);
    }

    public static ImGuiIOPtr Create()
    {
        return new ImGuiIOPtr(ImGuiNative.ImGuiIO_ImGuiIO());
    }

    public void SetAppAcceptingEvents(bool acceptingEvents)
    {
        ImGuiNative.ImGuiIO_SetAppAcceptingEvents(Handle, acceptingEvents);
    }

    public void SetKeyEventNativeData(ImGuiKey key, int nativeKeycode, int nativeScancode, int nativeLegacyIndex)
    {
        ImGuiNative.ImGuiIO_SetKeyEventNativeData(Handle, key, nativeKeycode, nativeScancode, nativeLegacyIndex);
    }

    public void SetKeyEventNativeData(ImGuiKey key, int nativeKeycode, int nativeScancode)
    {
        ImGuiNative.ImGuiIO_SetKeyEventNativeData(Handle, key, nativeKeycode, nativeScancode, -1);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiIO_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiInputTextCallbackDataPtr
{
    public readonly ImGuiInputTextCallbackData* Handle;

    public ImGuiInputTextCallbackDataPtr(ImGuiInputTextCallbackData* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiInputTextCallbackDataPtr(ImGuiInputTextCallbackData* handle) => new(handle);

    public static implicit operator ImGuiInputTextCallbackData*(ImGuiInputTextCallbackDataPtr pointer) => pointer.Handle;

    public ref ImGuiContext* Ctx => ref Handle->Ctx;

    public ref ImGuiInputTextFlags EventFlag => ref Handle->EventFlag;

    public ref ImGuiInputTextFlags Flags => ref Handle->Flags;

    public ref void* UserData => ref Handle->UserData;

    public ref uint ID => ref Handle->ID;

    public ref ImGuiKey EventKey => ref Handle->EventKey;

    public ref ushort EventChar => ref Handle->EventChar;

    public ref bool EventActivated => ref Handle->EventActivated;

    public ref bool BufDirty => ref Handle->BufDirty;

    public ref byte* Buf => ref Handle->Buf;

    public ref int BufTextLen => ref Handle->BufTextLen;

    public ref int BufSize => ref Handle->BufSize;

    public ref int CursorPos => ref Handle->CursorPos;

    public ref int SelectionStart => ref Handle->SelectionStart;

    public ref int SelectionEnd => ref Handle->SelectionEnd;

    public void ClearSelection()
    {
        ImGuiNative.ImGuiInputTextCallbackData_ClearSelection(Handle);
    }

    public void DeleteChars(int pos, int bytesCount)
    {
        ImGuiNative.ImGuiInputTextCallbackData_DeleteChars(Handle, pos, bytesCount);
    }

    public bool HasSelection()
    {
        return ImGuiNative.ImGuiInputTextCallbackData_HasSelection(Handle);
    }

    public static ImGuiInputTextCallbackDataPtr Create()
    {
        return new ImGuiInputTextCallbackDataPtr(ImGuiNative.ImGuiInputTextCallbackData_ImGuiInputTextCallbackData());
    }

    public void InsertChars(int pos, string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImGuiInputTextCallbackData_InsertChars(Handle, pos, textUtf8.Pointer, null);
    }

    public void SelectAll()
    {
        ImGuiNative.ImGuiInputTextCallbackData_SelectAll(Handle);
    }

    public void SetSelection(int s, int e)
    {
        ImGuiNative.ImGuiInputTextCallbackData_SetSelection(Handle, s, e);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiInputTextCallbackData_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiKeyDataPtr
{
    public readonly ImGuiKeyData* Handle;

    public ImGuiKeyDataPtr(ImGuiKeyData* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiKeyDataPtr(ImGuiKeyData* handle) => new(handle);

    public static implicit operator ImGuiKeyData*(ImGuiKeyDataPtr pointer) => pointer.Handle;

    public ref bool Down => ref Handle->Down;

    public ref float DownDuration => ref Handle->DownDuration;

    public ref float DownDurationPrev => ref Handle->DownDurationPrev;

    public ref float AnalogValue => ref Handle->AnalogValue;
}

public readonly unsafe partial struct ImGuiListClipperPtr
{
    public readonly ImGuiListClipper* Handle;

    public ImGuiListClipperPtr(ImGuiListClipper* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiListClipperPtr(ImGuiListClipper* handle) => new(handle);

    public static implicit operator ImGuiListClipper*(ImGuiListClipperPtr pointer) => pointer.Handle;

    public ref int DisplayStart => ref Handle->DisplayStart;

    public ref int DisplayEnd => ref Handle->DisplayEnd;

    public ref int UserIndex => ref Handle->UserIndex;

    public ref int ItemsCount => ref Handle->ItemsCount;

    public ref float ItemsHeight => ref Handle->ItemsHeight;

    public ref ImGuiListClipperFlags Flags => ref Handle->Flags;

    public ref double StartPosY => ref Handle->StartPosY;

    public ref double StartSeekOffsetY => ref Handle->StartSeekOffsetY;

    public ref ImGuiContext* Ctx => ref Handle->Ctx;

    public ref void* TempData => ref Handle->TempData;

    public void Begin(int itemsCount, float itemsHeight)
    {
        ImGuiNative.ImGuiListClipper_Begin(Handle, itemsCount, itemsHeight);
    }

    public void Begin(int itemsCount)
    {
        ImGuiNative.ImGuiListClipper_Begin(Handle, itemsCount, -1f);
    }

    public void End()
    {
        ImGuiNative.ImGuiListClipper_End(Handle);
    }

    public static ImGuiListClipperPtr Create()
    {
        return new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
    }

    public void IncludeItemByIndex(int itemIndex)
    {
        ImGuiNative.ImGuiListClipper_IncludeItemByIndex(Handle, itemIndex);
    }

    public void IncludeItemsByIndex(int itemBegin, int itemEnd)
    {
        ImGuiNative.ImGuiListClipper_IncludeItemsByIndex(Handle, itemBegin, itemEnd);
    }

    public void SeekCursorForItem(int itemIndex)
    {
        ImGuiNative.ImGuiListClipper_SeekCursorForItem(Handle, itemIndex);
    }

    public bool Step()
    {
        return ImGuiNative.ImGuiListClipper_Step(Handle);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiListClipper_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiMultiSelectIOPtr
{
    public readonly ImGuiMultiSelectIO* Handle;

    public ImGuiMultiSelectIOPtr(ImGuiMultiSelectIO* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiMultiSelectIOPtr(ImGuiMultiSelectIO* handle) => new(handle);

    public static implicit operator ImGuiMultiSelectIO*(ImGuiMultiSelectIOPtr pointer) => pointer.Handle;

    public ref ImVector<ImGuiSelectionRequest> Requests => ref Handle->Requests;

    public ref long RangeSrcItem => ref Handle->RangeSrcItem;

    public ref long NavIdItem => ref Handle->NavIdItem;

    public ref bool NavIdSelected => ref Handle->NavIdSelected;

    public ref bool RangeSrcReset => ref Handle->RangeSrcReset;

    public ref int ItemsCount => ref Handle->ItemsCount;
}

public readonly unsafe partial struct ImGuiOnceUponAFramePtr
{
    public readonly ImGuiOnceUponAFrame* Handle;

    public ImGuiOnceUponAFramePtr(ImGuiOnceUponAFrame* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiOnceUponAFramePtr(ImGuiOnceUponAFrame* handle) => new(handle);

    public static implicit operator ImGuiOnceUponAFrame*(ImGuiOnceUponAFramePtr pointer) => pointer.Handle;

    public ref int RefFrame => ref Handle->RefFrame;

    public static ImGuiOnceUponAFramePtr Create()
    {
        return new ImGuiOnceUponAFramePtr(ImGuiNative.ImGuiOnceUponAFrame_ImGuiOnceUponAFrame());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiOnceUponAFrame_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiPayloadPtr
{
    public readonly ImGuiPayload* Handle;

    public ImGuiPayloadPtr(ImGuiPayload* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiPayloadPtr(ImGuiPayload* handle) => new(handle);

    public static implicit operator ImGuiPayload*(ImGuiPayloadPtr pointer) => pointer.Handle;

    public ref void* Data => ref Handle->Data;

    public ref int DataSize => ref Handle->DataSize;

    public ref uint SourceId => ref Handle->SourceId;

    public ref uint SourceParentId => ref Handle->SourceParentId;

    public ref int DataFrameCount => ref Handle->DataFrameCount;

    public ref FixedArray33<byte> DataType => ref Handle->DataType;

    public ref bool Preview => ref Handle->Preview;

    public ref bool Delivery => ref Handle->Delivery;

    public void Clear()
    {
        ImGuiNative.ImGuiPayload_Clear(Handle);
    }

    public static ImGuiPayloadPtr Create()
    {
        return new ImGuiPayloadPtr(ImGuiNative.ImGuiPayload_ImGuiPayload());
    }

    public bool IsDataType(string type)
    {
        using var typeUtf8 = new ImGuiUtf8String(type, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImGuiPayload_IsDataType(Handle, typeUtf8.Pointer);
    }

    public bool IsDelivery()
    {
        return ImGuiNative.ImGuiPayload_IsDelivery(Handle);
    }

    public bool IsPreview()
    {
        return ImGuiNative.ImGuiPayload_IsPreview(Handle);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiPayload_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiPlatformIOPtr
{
    public readonly ImGuiPlatformIO* Handle;

    public ImGuiPlatformIOPtr(ImGuiPlatformIO* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiPlatformIOPtr(ImGuiPlatformIO* handle) => new(handle);

    public static implicit operator ImGuiPlatformIO*(ImGuiPlatformIOPtr pointer) => pointer.Handle;

    public ref void* Platform_GetClipboardTextFn => ref Handle->Platform_GetClipboardTextFn;

    public ref void* Platform_SetClipboardTextFn => ref Handle->Platform_SetClipboardTextFn;

    public ref void* Platform_ClipboardUserData => ref Handle->Platform_ClipboardUserData;

    public ref void* Platform_OpenInShellFn => ref Handle->Platform_OpenInShellFn;

    public ref void* Platform_OpenInShellUserData => ref Handle->Platform_OpenInShellUserData;

    public ref void* Platform_SetImeDataFn => ref Handle->Platform_SetImeDataFn;

    public ref void* Platform_ImeUserData => ref Handle->Platform_ImeUserData;

    public ref ushort Platform_LocaleDecimalPoint => ref Handle->Platform_LocaleDecimalPoint;

    public ref int Platform_SessionDate => ref Handle->Platform_SessionDate;

    public ref int Renderer_TextureMaxWidth => ref Handle->Renderer_TextureMaxWidth;

    public ref int Renderer_TextureMaxHeight => ref Handle->Renderer_TextureMaxHeight;

    public ref void* Renderer_RenderState => ref Handle->Renderer_RenderState;

    public ref void* DrawCallback_ResetRenderState => ref Handle->DrawCallback_ResetRenderState;

    public ref void* DrawCallback_SetSamplerLinear => ref Handle->DrawCallback_SetSamplerLinear;

    public ref void* DrawCallback_SetSamplerNearest => ref Handle->DrawCallback_SetSamplerNearest;

    public ref void* DrawCallback_SetSamplerFromTex => ref Handle->DrawCallback_SetSamplerFromTex;

    public ref void* Platform_CreateWindow => ref Handle->Platform_CreateWindow;

    public ref void* Platform_DestroyWindow => ref Handle->Platform_DestroyWindow;

    public ref void* Platform_ShowWindow => ref Handle->Platform_ShowWindow;

    public ref void* Platform_SetWindowPos => ref Handle->Platform_SetWindowPos;

    public ref void* Platform_GetWindowPos => ref Handle->Platform_GetWindowPos;

    public ref void* Platform_SetWindowSize => ref Handle->Platform_SetWindowSize;

    public ref void* Platform_GetWindowSize => ref Handle->Platform_GetWindowSize;

    public ref void* Platform_GetWindowFramebufferScale => ref Handle->Platform_GetWindowFramebufferScale;

    public ref void* Platform_SetWindowFocus => ref Handle->Platform_SetWindowFocus;

    public ref void* Platform_GetWindowFocus => ref Handle->Platform_GetWindowFocus;

    public ref void* Platform_GetWindowMinimized => ref Handle->Platform_GetWindowMinimized;

    public ref void* Platform_SetWindowTitle => ref Handle->Platform_SetWindowTitle;

    public ref void* Platform_SetWindowAlpha => ref Handle->Platform_SetWindowAlpha;

    public ref void* Platform_UpdateWindow => ref Handle->Platform_UpdateWindow;

    public ref void* Platform_RenderWindow => ref Handle->Platform_RenderWindow;

    public ref void* Platform_SwapBuffers => ref Handle->Platform_SwapBuffers;

    public ref void* Platform_GetWindowDpiScale => ref Handle->Platform_GetWindowDpiScale;

    public ref void* Platform_OnChangedViewport => ref Handle->Platform_OnChangedViewport;

    public ref void* Platform_GetWindowWorkAreaInsets => ref Handle->Platform_GetWindowWorkAreaInsets;

    public ref void* Platform_CreateVkSurface => ref Handle->Platform_CreateVkSurface;

    public ref void* Renderer_CreateWindow => ref Handle->Renderer_CreateWindow;

    public ref void* Renderer_DestroyWindow => ref Handle->Renderer_DestroyWindow;

    public ref void* Renderer_SetWindowSize => ref Handle->Renderer_SetWindowSize;

    public ref void* Renderer_RenderWindow => ref Handle->Renderer_RenderWindow;

    public ref void* Renderer_SwapBuffers => ref Handle->Renderer_SwapBuffers;

    public ref ImVector<ImGuiPlatformMonitor> Monitors => ref Handle->Monitors;

    public ref ImPtrVector<ImTextureData> Textures => ref Handle->Textures;

    public ref ImPtrVector<ImGuiViewport> Viewports => ref Handle->Viewports;

    public void ClearPlatformHandlers()
    {
        ImGuiNative.ImGuiPlatformIO_ClearPlatformHandlers(Handle);
    }

    public void ClearRendererHandlers()
    {
        ImGuiNative.ImGuiPlatformIO_ClearRendererHandlers(Handle);
    }

    public static ImGuiPlatformIOPtr Create()
    {
        return new ImGuiPlatformIOPtr(ImGuiNative.ImGuiPlatformIO_ImGuiPlatformIO());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiPlatformIO_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiPlatformImeDataPtr
{
    public readonly ImGuiPlatformImeData* Handle;

    public ImGuiPlatformImeDataPtr(ImGuiPlatformImeData* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiPlatformImeDataPtr(ImGuiPlatformImeData* handle) => new(handle);

    public static implicit operator ImGuiPlatformImeData*(ImGuiPlatformImeDataPtr pointer) => pointer.Handle;

    public ref bool WantVisible => ref Handle->WantVisible;

    public ref bool WantTextInput => ref Handle->WantTextInput;

    public ref Vector2 InputPos => ref Handle->InputPos;

    public ref float InputLineHeight => ref Handle->InputLineHeight;

    public ref uint ViewportId => ref Handle->ViewportId;

    public static ImGuiPlatformImeDataPtr Create()
    {
        return new ImGuiPlatformImeDataPtr(ImGuiNative.ImGuiPlatformImeData_ImGuiPlatformImeData());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiPlatformImeData_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiPlatformMonitorPtr
{
    public readonly ImGuiPlatformMonitor* Handle;

    public ImGuiPlatformMonitorPtr(ImGuiPlatformMonitor* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiPlatformMonitorPtr(ImGuiPlatformMonitor* handle) => new(handle);

    public static implicit operator ImGuiPlatformMonitor*(ImGuiPlatformMonitorPtr pointer) => pointer.Handle;

    public ref Vector2 MainPos => ref Handle->MainPos;

    public ref Vector2 MainSize => ref Handle->MainSize;

    public ref Vector2 WorkPos => ref Handle->WorkPos;

    public ref Vector2 WorkSize => ref Handle->WorkSize;

    public ref float DpiScale => ref Handle->DpiScale;

    public ref void* PlatformHandle => ref Handle->PlatformHandle;

    public static ImGuiPlatformMonitorPtr Create()
    {
        return new ImGuiPlatformMonitorPtr(ImGuiNative.ImGuiPlatformMonitor_ImGuiPlatformMonitor());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiPlatformMonitor_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiSelectionBasicStoragePtr
{
    public readonly ImGuiSelectionBasicStorage* Handle;

    public ImGuiSelectionBasicStoragePtr(ImGuiSelectionBasicStorage* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiSelectionBasicStoragePtr(ImGuiSelectionBasicStorage* handle) => new(handle);

    public static implicit operator ImGuiSelectionBasicStorage*(ImGuiSelectionBasicStoragePtr pointer) => pointer.Handle;

    public ref int Size => ref Handle->Size;

    public ref bool PreserveOrder => ref Handle->PreserveOrder;

    public ref void* UserData => ref Handle->UserData;

    public ref void* AdapterIndexToStorageId => ref Handle->AdapterIndexToStorageId;

    public ref int _SelectionOrder => ref Handle->_SelectionOrder;

    public ref ImGuiStorage _Storage => ref Handle->_Storage;

    public void ApplyRequests(ImGuiMultiSelectIOPtr msIo)
    {
        ImGuiNative.ImGuiSelectionBasicStorage_ApplyRequests(Handle, msIo.Handle);
    }

    public void Clear()
    {
        ImGuiNative.ImGuiSelectionBasicStorage_Clear(Handle);
    }

    public bool Contains(uint id)
    {
        return ImGuiNative.ImGuiSelectionBasicStorage_Contains(Handle, id);
    }

    public bool GetNextSelectedItem(void** opaqueIt, ref uint outId)
    {
        fixed (uint* outIdPointer = &outId)
        {
            return ImGuiNative.ImGuiSelectionBasicStorage_GetNextSelectedItem(Handle, opaqueIt, outIdPointer);
        }
    }

    public uint GetStorageIdFromIndex(int idx)
    {
        return ImGuiNative.ImGuiSelectionBasicStorage_GetStorageIdFromIndex(Handle, idx);
    }

    public static ImGuiSelectionBasicStoragePtr Create()
    {
        return new ImGuiSelectionBasicStoragePtr(ImGuiNative.ImGuiSelectionBasicStorage_ImGuiSelectionBasicStorage());
    }

    public void SetItemSelected(uint id, bool selected)
    {
        ImGuiNative.ImGuiSelectionBasicStorage_SetItemSelected(Handle, id, selected);
    }

    public void Swap(ImGuiSelectionBasicStoragePtr r)
    {
        ImGuiNative.ImGuiSelectionBasicStorage_Swap(Handle, r.Handle);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiSelectionBasicStorage_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiSelectionExternalStoragePtr
{
    public readonly ImGuiSelectionExternalStorage* Handle;

    public ImGuiSelectionExternalStoragePtr(ImGuiSelectionExternalStorage* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiSelectionExternalStoragePtr(ImGuiSelectionExternalStorage* handle) => new(handle);

    public static implicit operator ImGuiSelectionExternalStorage*(ImGuiSelectionExternalStoragePtr pointer) => pointer.Handle;

    public ref void* UserData => ref Handle->UserData;

    public ref void* AdapterSetItemSelected => ref Handle->AdapterSetItemSelected;

    public void ApplyRequests(ImGuiMultiSelectIOPtr msIo)
    {
        ImGuiNative.ImGuiSelectionExternalStorage_ApplyRequests(Handle, msIo.Handle);
    }

    public static ImGuiSelectionExternalStoragePtr Create()
    {
        return new ImGuiSelectionExternalStoragePtr(ImGuiNative.ImGuiSelectionExternalStorage_ImGuiSelectionExternalStorage());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiSelectionExternalStorage_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiSelectionRequestPtr
{
    public readonly ImGuiSelectionRequest* Handle;

    public ImGuiSelectionRequestPtr(ImGuiSelectionRequest* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiSelectionRequestPtr(ImGuiSelectionRequest* handle) => new(handle);

    public static implicit operator ImGuiSelectionRequest*(ImGuiSelectionRequestPtr pointer) => pointer.Handle;

    public ref ImGuiSelectionRequestType Type => ref Handle->Type;

    public ref bool Selected => ref Handle->Selected;

    public ref sbyte RangeDirection => ref Handle->RangeDirection;

    public ref long RangeFirstItem => ref Handle->RangeFirstItem;

    public ref long RangeLastItem => ref Handle->RangeLastItem;
}

public readonly unsafe partial struct ImGuiSizeCallbackDataPtr
{
    public readonly ImGuiSizeCallbackData* Handle;

    public ImGuiSizeCallbackDataPtr(ImGuiSizeCallbackData* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiSizeCallbackDataPtr(ImGuiSizeCallbackData* handle) => new(handle);

    public static implicit operator ImGuiSizeCallbackData*(ImGuiSizeCallbackDataPtr pointer) => pointer.Handle;

    public ref void* UserData => ref Handle->UserData;

    public ref Vector2 Pos => ref Handle->Pos;

    public ref Vector2 CurrentSize => ref Handle->CurrentSize;

    public ref Vector2 DesiredSize => ref Handle->DesiredSize;
}

public readonly unsafe partial struct ImGuiStoragePtr
{
    public readonly ImGuiStorage* Handle;

    public ImGuiStoragePtr(ImGuiStorage* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiStoragePtr(ImGuiStorage* handle) => new(handle);

    public static implicit operator ImGuiStorage*(ImGuiStoragePtr pointer) => pointer.Handle;

    public ref ImVector<ImGuiStoragePair> Data => ref Handle->Data;

    public void BuildSortByKey()
    {
        ImGuiNative.ImGuiStorage_BuildSortByKey(Handle);
    }

    public void Clear()
    {
        ImGuiNative.ImGuiStorage_Clear(Handle);
    }

    public bool GetBool(uint key, bool defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetBool(Handle, key, defaultVal);
    }

    public bool GetBool(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetBool(Handle, key, false);
    }

    public bool* GetBoolRef(uint key, bool defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetBoolRef(Handle, key, defaultVal);
    }

    public bool* GetBoolRef(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetBoolRef(Handle, key, false);
    }

    public float GetFloat(uint key, float defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetFloat(Handle, key, defaultVal);
    }

    public float GetFloat(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetFloat(Handle, key, 0f);
    }

    public float* GetFloatRef(uint key, float defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetFloatRef(Handle, key, defaultVal);
    }

    public float* GetFloatRef(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetFloatRef(Handle, key, 0f);
    }

    public int GetInt(uint key, int defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetInt(Handle, key, defaultVal);
    }

    public int GetInt(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetInt(Handle, key, 0);
    }

    public int* GetIntRef(uint key, int defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetIntRef(Handle, key, defaultVal);
    }

    public int* GetIntRef(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetIntRef(Handle, key, 0);
    }

    public void* GetVoidPtr(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetVoidPtr(Handle, key);
    }

    public void** GetVoidPtrRef(uint key, void* defaultVal)
    {
        return ImGuiNative.ImGuiStorage_GetVoidPtrRef(Handle, key, defaultVal);
    }

    public void** GetVoidPtrRef(uint key)
    {
        return ImGuiNative.ImGuiStorage_GetVoidPtrRef(Handle, key, null);
    }

    public void SetAllInt(int val)
    {
        ImGuiNative.ImGuiStorage_SetAllInt(Handle, val);
    }

    public void SetBool(uint key, bool val)
    {
        ImGuiNative.ImGuiStorage_SetBool(Handle, key, val);
    }

    public void SetFloat(uint key, float val)
    {
        ImGuiNative.ImGuiStorage_SetFloat(Handle, key, val);
    }

    public void SetInt(uint key, int val)
    {
        ImGuiNative.ImGuiStorage_SetInt(Handle, key, val);
    }

    public void SetVoidPtr(uint key, void* val)
    {
        ImGuiNative.ImGuiStorage_SetVoidPtr(Handle, key, val);
    }
}

public readonly unsafe partial struct ImGuiStoragePairPtr
{
    public readonly ImGuiStoragePair* Handle;

    public ImGuiStoragePairPtr(ImGuiStoragePair* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiStoragePairPtr(ImGuiStoragePair* handle) => new(handle);

    public static implicit operator ImGuiStoragePair*(ImGuiStoragePairPtr pointer) => pointer.Handle;

    public ref uint key => ref Handle->key;

    public ref int val_i => ref Handle->val_i;

    public ref float val_f => ref Handle->val_f;

    public ref void* val_p => ref Handle->val_p;

    public static ImGuiStoragePairPtr Create(uint key, int val)
    {
        return new ImGuiStoragePairPtr(ImGuiNative.ImGuiStoragePair_ImGuiStoragePair_Int(key, val));
    }

    public static ImGuiStoragePairPtr Create(uint key, float val)
    {
        return new ImGuiStoragePairPtr(ImGuiNative.ImGuiStoragePair_ImGuiStoragePair_Float(key, val));
    }

    public static ImGuiStoragePairPtr Create(uint key, void* val)
    {
        return new ImGuiStoragePairPtr(ImGuiNative.ImGuiStoragePair_ImGuiStoragePair_Ptr(key, val));
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiStoragePair_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiStylePtr
{
    public readonly ImGuiStyle* Handle;

    public ImGuiStylePtr(ImGuiStyle* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiStylePtr(ImGuiStyle* handle) => new(handle);

    public static implicit operator ImGuiStyle*(ImGuiStylePtr pointer) => pointer.Handle;

    public ref float FontSizeBase => ref Handle->FontSizeBase;

    public ref float FontScaleMain => ref Handle->FontScaleMain;

    public ref float FontScaleDpi => ref Handle->FontScaleDpi;

    public ref float Alpha => ref Handle->Alpha;

    public ref float DisabledAlpha => ref Handle->DisabledAlpha;

    public ref Vector2 WindowPadding => ref Handle->WindowPadding;

    public ref float WindowRounding => ref Handle->WindowRounding;

    public ref float WindowBorderSize => ref Handle->WindowBorderSize;

    public ref float WindowBorderHoverPadding => ref Handle->WindowBorderHoverPadding;

    public ref Vector2 WindowMinSize => ref Handle->WindowMinSize;

    public ref Vector2 WindowTitleAlign => ref Handle->WindowTitleAlign;

    public ref ImGuiDir WindowMenuButtonPosition => ref Handle->WindowMenuButtonPosition;

    public ref float ChildRounding => ref Handle->ChildRounding;

    public ref float ChildBorderSize => ref Handle->ChildBorderSize;

    public ref float PopupRounding => ref Handle->PopupRounding;

    public ref float PopupBorderSize => ref Handle->PopupBorderSize;

    public ref Vector2 FramePadding => ref Handle->FramePadding;

    public ref float FrameRounding => ref Handle->FrameRounding;

    public ref float FrameBorderSize => ref Handle->FrameBorderSize;

    public ref Vector2 ItemSpacing => ref Handle->ItemSpacing;

    public ref Vector2 ItemInnerSpacing => ref Handle->ItemInnerSpacing;

    public ref Vector2 CellPadding => ref Handle->CellPadding;

    public ref Vector2 TouchExtraPadding => ref Handle->TouchExtraPadding;

    public ref float IndentSpacing => ref Handle->IndentSpacing;

    public ref float ColumnsMinSpacing => ref Handle->ColumnsMinSpacing;

    public ref float ScrollbarSize => ref Handle->ScrollbarSize;

    public ref float ScrollbarRounding => ref Handle->ScrollbarRounding;

    public ref float ScrollbarPadding => ref Handle->ScrollbarPadding;

    public ref float GrabMinSize => ref Handle->GrabMinSize;

    public ref float GrabRounding => ref Handle->GrabRounding;

    public ref float LogSliderDeadzone => ref Handle->LogSliderDeadzone;

    public ref float ImageRounding => ref Handle->ImageRounding;

    public ref float ImageBorderSize => ref Handle->ImageBorderSize;

    public ref float TabRounding => ref Handle->TabRounding;

    public ref float TabBorderSize => ref Handle->TabBorderSize;

    public ref float TabMinWidthBase => ref Handle->TabMinWidthBase;

    public ref float TabMinWidthShrink => ref Handle->TabMinWidthShrink;

    public ref float TabCloseButtonMinWidthSelected => ref Handle->TabCloseButtonMinWidthSelected;

    public ref float TabCloseButtonMinWidthUnselected => ref Handle->TabCloseButtonMinWidthUnselected;

    public ref float TabBarBorderSize => ref Handle->TabBarBorderSize;

    public ref float TabBarOverlineSize => ref Handle->TabBarOverlineSize;

    public ref float TableAngledHeadersAngle => ref Handle->TableAngledHeadersAngle;

    public ref Vector2 TableAngledHeadersTextAlign => ref Handle->TableAngledHeadersTextAlign;

    public ref ImGuiTreeNodeFlags TreeLinesFlags => ref Handle->TreeLinesFlags;

    public ref float TreeLinesSize => ref Handle->TreeLinesSize;

    public ref float TreeLinesRounding => ref Handle->TreeLinesRounding;

    public ref float MenuItemRounding => ref Handle->MenuItemRounding;

    public ref float SelectableRounding => ref Handle->SelectableRounding;

    public ref float DragDropTargetRounding => ref Handle->DragDropTargetRounding;

    public ref float DragDropTargetBorderSize => ref Handle->DragDropTargetBorderSize;

    public ref float DragDropTargetPadding => ref Handle->DragDropTargetPadding;

    public ref float ColorMarkerSize => ref Handle->ColorMarkerSize;

    public ref ImGuiDir ColorButtonPosition => ref Handle->ColorButtonPosition;

    public ref Vector2 ButtonTextAlign => ref Handle->ButtonTextAlign;

    public ref Vector2 SelectableTextAlign => ref Handle->SelectableTextAlign;

    public ref float InputTextCursorSize => ref Handle->InputTextCursorSize;

    public ref float SeparatorSize => ref Handle->SeparatorSize;

    public ref float SeparatorTextBorderSize => ref Handle->SeparatorTextBorderSize;

    public ref Vector2 SeparatorTextAlign => ref Handle->SeparatorTextAlign;

    public ref Vector2 SeparatorTextPadding => ref Handle->SeparatorTextPadding;

    public ref Vector2 DisplayWindowPadding => ref Handle->DisplayWindowPadding;

    public ref Vector2 DisplaySafeAreaPadding => ref Handle->DisplaySafeAreaPadding;

    public ref bool DockingNodeHasCloseButton => ref Handle->DockingNodeHasCloseButton;

    public ref float DockingSeparatorSize => ref Handle->DockingSeparatorSize;

    public ref float MouseCursorScale => ref Handle->MouseCursorScale;

    public ref bool AntiAliasedLines => ref Handle->AntiAliasedLines;

    public ref bool AntiAliasedLineEnds => ref Handle->AntiAliasedLineEnds;

    public ref bool AntiAliasedFill => ref Handle->AntiAliasedFill;

    public ref float CurveTessellationMaxError => ref Handle->CurveTessellationMaxError;

    public ref float CircleTessellationMaxError => ref Handle->CircleTessellationMaxError;

    public ref FixedArray63<Vector4> Colors => ref Handle->Colors;

    public ref float HoverStationaryDelay => ref Handle->HoverStationaryDelay;

    public ref float HoverDelayShort => ref Handle->HoverDelayShort;

    public ref float HoverDelayNormal => ref Handle->HoverDelayNormal;

    public ref ImGuiHoveredFlags HoverFlagsForTooltipMouse => ref Handle->HoverFlagsForTooltipMouse;

    public ref ImGuiHoveredFlags HoverFlagsForTooltipNav => ref Handle->HoverFlagsForTooltipNav;

    public ref float _MainScale => ref Handle->_MainScale;

    public ref float _NextFrameFontSizeBase => ref Handle->_NextFrameFontSizeBase;

    public static ImGuiStylePtr Create()
    {
        return new ImGuiStylePtr(ImGuiNative.ImGuiStyle_ImGuiStyle());
    }

    public void ScaleAllSizes(float scaleFactor)
    {
        ImGuiNative.ImGuiStyle_ScaleAllSizes(Handle, scaleFactor);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiStyle_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiTableColumnSortSpecsPtr
{
    public readonly ImGuiTableColumnSortSpecs* Handle;

    public ImGuiTableColumnSortSpecsPtr(ImGuiTableColumnSortSpecs* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiTableColumnSortSpecsPtr(ImGuiTableColumnSortSpecs* handle) => new(handle);

    public static implicit operator ImGuiTableColumnSortSpecs*(ImGuiTableColumnSortSpecsPtr pointer) => pointer.Handle;

    public ref uint ColumnUserID => ref Handle->ColumnUserID;

    public ref short ColumnIndex => ref Handle->ColumnIndex;

    public ref short SortOrder => ref Handle->SortOrder;

    public ref ImGuiSortDirection SortDirection => ref Handle->SortDirection;

    public static ImGuiTableColumnSortSpecsPtr Create()
    {
        return new ImGuiTableColumnSortSpecsPtr(ImGuiNative.ImGuiTableColumnSortSpecs_ImGuiTableColumnSortSpecs());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiTableColumnSortSpecs_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiTableSortSpecsPtr
{
    public readonly ImGuiTableSortSpecs* Handle;

    public ImGuiTableSortSpecsPtr(ImGuiTableSortSpecs* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiTableSortSpecsPtr(ImGuiTableSortSpecs* handle) => new(handle);

    public static implicit operator ImGuiTableSortSpecs*(ImGuiTableSortSpecsPtr pointer) => pointer.Handle;

    public ImGuiTableColumnSortSpecsPtr Specs
    {
        get => Handle->Specs;
        set => Handle->Specs = value;
    }

    public ref int SpecsCount => ref Handle->SpecsCount;

    public ref bool SpecsDirty => ref Handle->SpecsDirty;

    public static ImGuiTableSortSpecsPtr Create()
    {
        return new ImGuiTableSortSpecsPtr(ImGuiNative.ImGuiTableSortSpecs_ImGuiTableSortSpecs());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiTableSortSpecs_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiTextBufferPtr
{
    public readonly ImGuiTextBuffer* Handle;

    public ImGuiTextBufferPtr(ImGuiTextBuffer* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiTextBufferPtr(ImGuiTextBuffer* handle) => new(handle);

    public static implicit operator ImGuiTextBuffer*(ImGuiTextBufferPtr pointer) => pointer.Handle;

    public ref ImVector<byte> Buf => ref Handle->Buf;

    public static ImGuiTextBufferPtr Create()
    {
        return new ImGuiTextBufferPtr(ImGuiNative.ImGuiTextBuffer_ImGuiTextBuffer());
    }

    public void Append(string str)
    {
        using var strUtf8 = new ImGuiUtf8String(str, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.ImGuiTextBuffer_append(Handle, strUtf8.Pointer, null);
    }

    public void Appendf(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_ImGuiTextBuffer_appendf(Handle, textUtf8.Pointer);
    }

    public string Begin()
    {
        return ImGuiUtf8.ToString(ImGuiNative.ImGuiTextBuffer_begin(Handle));
    }

    public string CStr()
    {
        return ImGuiUtf8.ToString(ImGuiNative.ImGuiTextBuffer_c_str(Handle));
    }

    public void Clear()
    {
        ImGuiNative.ImGuiTextBuffer_clear(Handle);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiTextBuffer_destroy(Handle);
    }

    public bool Empty()
    {
        return ImGuiNative.ImGuiTextBuffer_empty(Handle);
    }

    public string End()
    {
        return ImGuiUtf8.ToString(ImGuiNative.ImGuiTextBuffer_end(Handle));
    }

    public void Reserve(int capacity)
    {
        ImGuiNative.ImGuiTextBuffer_reserve(Handle, capacity);
    }

    public void Resize(int size)
    {
        ImGuiNative.ImGuiTextBuffer_resize(Handle, size);
    }

    public int Size()
    {
        return ImGuiNative.ImGuiTextBuffer_size(Handle);
    }
}

public readonly unsafe partial struct ImGuiTextFilterPtr
{
    public readonly ImGuiTextFilter* Handle;

    public ImGuiTextFilterPtr(ImGuiTextFilter* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiTextFilterPtr(ImGuiTextFilter* handle) => new(handle);

    public static implicit operator ImGuiTextFilter*(ImGuiTextFilterPtr pointer) => pointer.Handle;

    public ref FixedArray256<byte> InputBuf => ref Handle->InputBuf;

    public ref int _CountExclude => ref Handle->_CountExclude;

    public ref ImVector<ImGuiTextFilterItem> _Items => ref Handle->_Items;

    public void Build()
    {
        ImGuiNative.ImGuiTextFilter_Build(Handle);
    }

    public void Clear()
    {
        ImGuiNative.ImGuiTextFilter_Clear(Handle);
    }

    public bool Draw(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImGuiTextFilter_Draw(Handle, labelUtf8.Pointer);
    }

    public bool Draw()
    {
        return ImGuiNative.ImGuiTextFilter_Draw(Handle, ImGuiUtf8.Literal("Filter\0"u8));
    }

    public bool DrawWithHint(string label, string hint)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var hintUtf8 = new ImGuiUtf8String(hint, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImGuiTextFilter_DrawWithHint(Handle, labelUtf8.Pointer, hintUtf8.Pointer);
    }

    public bool DrawWithHint(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImGuiTextFilter_DrawWithHint(Handle, labelUtf8.Pointer, ImGuiUtf8.Literal("incl -excl\0"u8));
    }

    public bool DrawWithHint()
    {
        return ImGuiNative.ImGuiTextFilter_DrawWithHint(Handle, ImGuiUtf8.Literal("Filter\0"u8), ImGuiUtf8.Literal("incl -excl\0"u8));
    }

    public static ImGuiTextFilterPtr Create(string defaultFilter)
    {
        using var defaultFilterUtf8 = new ImGuiUtf8String(defaultFilter, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImGuiTextFilterPtr(ImGuiNative.ImGuiTextFilter_ImGuiTextFilter(defaultFilterUtf8.Pointer));
    }

    public static ImGuiTextFilterPtr Create()
    {
        return new ImGuiTextFilterPtr(ImGuiNative.ImGuiTextFilter_ImGuiTextFilter(ImGuiUtf8.Literal("\0"u8)));
    }

    public bool IsActive()
    {
        return ImGuiNative.ImGuiTextFilter_IsActive(Handle);
    }

    public bool PassFilter(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.ImGuiTextFilter_PassFilter(Handle, textUtf8.Pointer, null);
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiTextFilter_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiViewportPtr
{
    public readonly ImGuiViewport* Handle;

    public ImGuiViewportPtr(ImGuiViewport* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiViewportPtr(ImGuiViewport* handle) => new(handle);

    public static implicit operator ImGuiViewport*(ImGuiViewportPtr pointer) => pointer.Handle;

    public ref uint ID => ref Handle->ID;

    public ref ImGuiViewportFlags Flags => ref Handle->Flags;

    public ref Vector2 Pos => ref Handle->Pos;

    public ref Vector2 Size => ref Handle->Size;

    public ref Vector2 FramebufferScale => ref Handle->FramebufferScale;

    public ref Vector2 WorkPos => ref Handle->WorkPos;

    public ref Vector2 WorkSize => ref Handle->WorkSize;

    public ref float DpiScale => ref Handle->DpiScale;

    public ref uint ParentViewportId => ref Handle->ParentViewportId;

    public ImGuiViewportPtr ParentViewport
    {
        get => Handle->ParentViewport;
        set => Handle->ParentViewport = value;
    }

    public ImDrawDataPtr DrawData
    {
        get => Handle->DrawData;
        set => Handle->DrawData = value;
    }

    public ref void* RendererUserData => ref Handle->RendererUserData;

    public ref void* PlatformUserData => ref Handle->PlatformUserData;

    public ref void* PlatformIconData => ref Handle->PlatformIconData;

    public ref void* PlatformHandle => ref Handle->PlatformHandle;

    public ref void* PlatformHandleRaw => ref Handle->PlatformHandleRaw;

    public ref bool PlatformWindowCreated => ref Handle->PlatformWindowCreated;

    public ref bool PlatformRequestMove => ref Handle->PlatformRequestMove;

    public ref bool PlatformRequestResize => ref Handle->PlatformRequestResize;

    public ref bool PlatformRequestClose => ref Handle->PlatformRequestClose;

    public Vector2 GetCenter()
    {
        return ImGuiNative.ImGuiViewport_GetCenter(Handle);
    }

    public string GetDebugName()
    {
        return ImGuiUtf8.ToString(ImGuiNative.ImGuiViewport_GetDebugName(Handle));
    }

    public Vector2 GetWorkCenter()
    {
        return ImGuiNative.ImGuiViewport_GetWorkCenter(Handle);
    }

    public static ImGuiViewportPtr Create()
    {
        return new ImGuiViewportPtr(ImGuiNative.ImGuiViewport_ImGuiViewport());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiViewport_destroy(Handle);
    }
}

public readonly unsafe partial struct ImGuiWindowClassPtr
{
    public readonly ImGuiWindowClass* Handle;

    public ImGuiWindowClassPtr(ImGuiWindowClass* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImGuiWindowClassPtr(ImGuiWindowClass* handle) => new(handle);

    public static implicit operator ImGuiWindowClass*(ImGuiWindowClassPtr pointer) => pointer.Handle;

    public ref uint ClassId => ref Handle->ClassId;

    public ref uint ParentViewportId => ref Handle->ParentViewportId;

    public ref uint FocusRouteParentWindowId => ref Handle->FocusRouteParentWindowId;

    public ref ImGuiViewportFlags ViewportFlagsOverrideSet => ref Handle->ViewportFlagsOverrideSet;

    public ref ImGuiViewportFlags ViewportFlagsOverrideClear => ref Handle->ViewportFlagsOverrideClear;

    public ref ImGuiTabItemFlags TabItemFlagsOverrideSet => ref Handle->TabItemFlagsOverrideSet;

    public ref ImGuiDockNodeFlags DockNodeFlagsOverrideSet => ref Handle->DockNodeFlagsOverrideSet;

    public ref bool DockingAlwaysTabBar => ref Handle->DockingAlwaysTabBar;

    public ref bool DockingAllowUnclassed => ref Handle->DockingAllowUnclassed;

    public ref void* PlatformIconData => ref Handle->PlatformIconData;

    public static ImGuiWindowClassPtr Create()
    {
        return new ImGuiWindowClassPtr(ImGuiNative.ImGuiWindowClass_ImGuiWindowClass());
    }

    public void Destroy()
    {
        ImGuiNative.ImGuiWindowClass_destroy(Handle);
    }
}

public readonly unsafe partial struct ImTextureDataPtr
{
    public readonly ImTextureData* Handle;

    public ImTextureDataPtr(ImTextureData* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImTextureDataPtr(ImTextureData* handle) => new(handle);

    public static implicit operator ImTextureData*(ImTextureDataPtr pointer) => pointer.Handle;

    public ref int UniqueID => ref Handle->UniqueID;

    public ref ImTextureStatus Status => ref Handle->Status;

    public ref void* BackendUserData => ref Handle->BackendUserData;

    public ref void* QueueUserData => ref Handle->QueueUserData;

    public ref ulong TexID => ref Handle->TexID;

    public ref ImTextureFormat Format => ref Handle->Format;

    public ref int Width => ref Handle->Width;

    public ref int Height => ref Handle->Height;

    public ref int BytesPerPixel => ref Handle->BytesPerPixel;

    public ref byte* Pixels => ref Handle->Pixels;

    public ref ImTextureRect UsedRect => ref Handle->UsedRect;

    public ref ImTextureRect UpdateRect => ref Handle->UpdateRect;

    public ref ImVector<ImTextureRect> Updates => ref Handle->Updates;

    public ref int UnusedFrames => ref Handle->UnusedFrames;

    public ref ushort RefCount => ref Handle->RefCount;

    public ref bool UseColors => ref Handle->UseColors;

    public ref bool WantDestroyNextFrame => ref Handle->WantDestroyNextFrame;

    public void Create(ImTextureFormat format, int w, int h)
    {
        ImGuiNative.ImTextureData_Create(Handle, format, w, h);
    }

    public void DestroyPixels()
    {
        ImGuiNative.ImTextureData_DestroyPixels(Handle);
    }

    public int GetPitch()
    {
        return ImGuiNative.ImTextureData_GetPitch(Handle);
    }

    public void* GetPixels()
    {
        return ImGuiNative.ImTextureData_GetPixels(Handle);
    }

    public void* GetPixelsAt(int x, int y)
    {
        return ImGuiNative.ImTextureData_GetPixelsAt(Handle, x, y);
    }

    public int GetSizeInBytes()
    {
        return ImGuiNative.ImTextureData_GetSizeInBytes(Handle);
    }

    public ulong GetTexID()
    {
        return ImGuiNative.ImTextureData_GetTexID(Handle);
    }

    public ImTextureRef GetTexRef()
    {
        return ImGuiNative.ImTextureData_GetTexRef(Handle);
    }

    public static ImTextureDataPtr Create()
    {
        return new ImTextureDataPtr(ImGuiNative.ImTextureData_ImTextureData());
    }

    public void SetStatus(ImTextureStatus status)
    {
        ImGuiNative.ImTextureData_SetStatus(Handle, status);
    }

    public void SetTexID(ulong texId)
    {
        ImGuiNative.ImTextureData_SetTexID(Handle, texId);
    }

    public void Destroy()
    {
        ImGuiNative.ImTextureData_destroy(Handle);
    }
}

public readonly unsafe partial struct ImTextureRectPtr
{
    public readonly ImTextureRect* Handle;

    public ImTextureRectPtr(ImTextureRect* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImTextureRectPtr(ImTextureRect* handle) => new(handle);

    public static implicit operator ImTextureRect*(ImTextureRectPtr pointer) => pointer.Handle;

    public ref ushort x => ref Handle->x;

    public ref ushort y => ref Handle->y;

    public ref ushort w => ref Handle->w;

    public ref ushort h => ref Handle->h;
}

public readonly unsafe partial struct ImTextureRefPtr
{
    public readonly ImTextureRef* Handle;

    public ImTextureRefPtr(ImTextureRef* handle) => Handle = handle;

    public bool IsNull => Handle == null;

    public static implicit operator ImTextureRefPtr(ImTextureRef* handle) => new(handle);

    public static implicit operator ImTextureRef*(ImTextureRefPtr pointer) => pointer.Handle;

    public ImTextureDataPtr _TexData
    {
        get => Handle->_TexData;
        set => Handle->_TexData = value;
    }

    public ref ulong _TexID => ref Handle->_TexID;

    public ulong GetTexID()
    {
        return ImGuiNative.ImTextureRef_GetTexID(Handle);
    }

    public static ImTextureRefPtr Create()
    {
        return new ImTextureRefPtr(ImGuiNative.ImTextureRef_ImTextureRef_Nil());
    }

    public static ImTextureRefPtr Create(ulong texId)
    {
        return new ImTextureRefPtr(ImGuiNative.ImTextureRef_ImTextureRef_TextureID(texId));
    }

    public void Destroy()
    {
        ImGuiNative.ImTextureRef_destroy(Handle);
    }
}
