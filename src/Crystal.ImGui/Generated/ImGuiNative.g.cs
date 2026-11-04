using System.Numerics;
using System.Runtime.InteropServices;

namespace Crystal.ImGui;

public static unsafe partial class ImGuiNative
{
    [LibraryImport(LibraryName)]
    public static partial ImColor ImColor_HSV(float h, float s, float v, float a);

    [LibraryImport(LibraryName)]
    public static partial ImColor* ImColor_ImColor_Nil();

    [LibraryImport(LibraryName)]
    public static partial ImColor* ImColor_ImColor_Float(float r, float g, float b, float a);

    [LibraryImport(LibraryName)]
    public static partial ImColor* ImColor_ImColor_Vec4(Vector4 col);

    [LibraryImport(LibraryName)]
    public static partial ImColor* ImColor_ImColor_Int(int r, int g, int b, int a);

    [LibraryImport(LibraryName)]
    public static partial ImColor* ImColor_ImColor_U32(uint rgba);

    [LibraryImport(LibraryName)]
    public static partial void ImColor_SetHSV(ImColor* self, float h, float s, float v, float a);

    [LibraryImport(LibraryName)]
    public static partial void ImColor_destroy(ImColor* self);

    [LibraryImport(LibraryName)]
    public static partial ulong ImDrawCmd_GetTexID(ImDrawCmd* self);

    [LibraryImport(LibraryName)]
    public static partial ImDrawCmd* ImDrawCmd_ImDrawCmd();

    [LibraryImport(LibraryName)]
    public static partial void ImDrawCmd_destroy(ImDrawCmd* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawData_AddDrawList(ImDrawData* self, ImDrawList* draw_list);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawData_Clear(ImDrawData* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawData_DeIndexAllBuffers(ImDrawData* self);

    [LibraryImport(LibraryName)]
    public static partial ImDrawData* ImDrawData_ImDrawData();

    [LibraryImport(LibraryName)]
    public static partial void ImDrawData_ScaleClipRects(ImDrawData* self, Vector2 fb_scale);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawData_destroy(ImDrawData* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawListSplitter_Clear(ImDrawListSplitter* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawListSplitter_ClearFreeMemory(ImDrawListSplitter* self);

    [LibraryImport(LibraryName)]
    public static partial ImDrawListSplitter* ImDrawListSplitter_ImDrawListSplitter();

    [LibraryImport(LibraryName)]
    public static partial void ImDrawListSplitter_Merge(ImDrawListSplitter* self, ImDrawList* draw_list);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawListSplitter_SetCurrentChannel(ImDrawListSplitter* self, ImDrawList* draw_list, int channel_idx);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawListSplitter_Split(ImDrawListSplitter* self, ImDrawList* draw_list, int count);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawListSplitter_destroy(ImDrawListSplitter* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddBezierCubic(ImDrawList* self, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness, int num_segments, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddBezierQuadratic(ImDrawList* self, Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness, int num_segments, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddCallback(ImDrawList* self, void* callback, void* userdata, nuint userdata_size);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddCircle(ImDrawList* self, Vector2 center, float radius, uint col, int num_segments, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddCircleFilled(ImDrawList* self, Vector2 center, float radius, uint col, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddConcavePolyFilled(ImDrawList* self, Vector2* points, int num_points, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddConvexPolyFilled(ImDrawList* self, Vector2* points, int num_points, uint col, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddDrawCmd(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddEllipse(ImDrawList* self, Vector2 center, Vector2 radius, uint col, float rot, int num_segments, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddEllipseFilled(ImDrawList* self, Vector2 center, Vector2 radius, uint col, float rot, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddImage(ImDrawList* self, ImTextureRef tex_ref, Vector2 p_min, Vector2 p_max, Vector2 uv_min, Vector2 uv_max, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddImageQuad(ImDrawList* self, ImTextureRef tex_ref, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddImageRounded(ImDrawList* self, ImTextureRef tex_ref, Vector2 p_min, Vector2 p_max, Vector2 uv_min, Vector2 uv_max, uint col, float rounding, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddLine(ImDrawList* self, Vector2 p1, Vector2 p2, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddLineH(ImDrawList* self, float min_x, float max_x, float y, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddLineV(ImDrawList* self, float x, float min_y, float max_y, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddNgon(ImDrawList* self, Vector2 center, float radius, uint col, int num_segments, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddNgonFilled(ImDrawList* self, Vector2 center, float radius, uint col, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddPolyline(ImDrawList* self, Vector2* points, int num_points, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddQuad(ImDrawList* self, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddQuadFilled(ImDrawList* self, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddRect(ImDrawList* self, Vector2 p_min, Vector2 p_max, uint col, float rounding, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddRectFilled(ImDrawList* self, Vector2 p_min, Vector2 p_max, uint col, float rounding, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddRectFilledMultiColor(ImDrawList* self, Vector2 p_min, Vector2 p_max, uint col_upr_left, uint col_upr_right, uint col_bot_right, uint col_bot_left);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddText_Vec2(ImDrawList* self, Vector2 pos, uint col, byte* text_begin, byte* text_end);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddText_FontPtr(ImDrawList* self, ImFont* font, float font_size, Vector2 pos, uint col, byte* text_begin, byte* text_end, float wrap_width, Vector4* cpu_fine_clip_rect);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddTriangle(ImDrawList* self, Vector2 p1, Vector2 p2, Vector2 p3, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_AddTriangleFilled(ImDrawList* self, Vector2 p1, Vector2 p2, Vector2 p3, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_ChannelsMerge(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_ChannelsSetCurrent(ImDrawList* self, int n);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_ChannelsSplit(ImDrawList* self, int count);

    [LibraryImport(LibraryName)]
    public static partial ImDrawList* ImDrawList_CloneOutput(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial Vector2 ImDrawList_GetClipRectMax(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial Vector2 ImDrawList_GetClipRectMin(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial ImDrawList* ImDrawList_ImDrawList(ImDrawListSharedData* shared_data);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathArcTo(ImDrawList* self, Vector2 center, float radius, float a_min, float a_max, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathArcToFast(ImDrawList* self, Vector2 center, float radius, int a_min_of_12, int a_max_of_12);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathBezierCubicCurveTo(ImDrawList* self, Vector2 p2, Vector2 p3, Vector2 p4, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathBezierQuadraticCurveTo(ImDrawList* self, Vector2 p2, Vector2 p3, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathClear(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathEllipticalArcTo(ImDrawList* self, Vector2 center, Vector2 radius, float rot, float a_min, float a_max, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathFillConcave(ImDrawList* self, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathFillConvex(ImDrawList* self, uint col, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathLineTo(ImDrawList* self, Vector2 pos);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathLineToMergeDuplicate(ImDrawList* self, Vector2 pos);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathRect(ImDrawList* self, Vector2 rect_min, Vector2 rect_max, float rounding, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PathStroke(ImDrawList* self, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PopClipRect(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PopDrawFlag(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PopTexture(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimQuadUV(ImDrawList* self, Vector2 a, Vector2 b, Vector2 c, Vector2 d, Vector2 uv_a, Vector2 uv_b, Vector2 uv_c, Vector2 uv_d, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimRect(ImDrawList* self, Vector2 a, Vector2 b, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimRectUV(ImDrawList* self, Vector2 a, Vector2 b, Vector2 uv_a, Vector2 uv_b, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimReserve(ImDrawList* self, int idx_count, int vtx_count);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimUnreserve(ImDrawList* self, int idx_count, int vtx_count);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimVtx(ImDrawList* self, Vector2 pos, Vector2 uv, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimWriteIdx(ImDrawList* self, ushort idx);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PrimWriteVtx(ImDrawList* self, Vector2 pos, Vector2 uv, uint col);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PushClipRect(ImDrawList* self, Vector2 clip_rect_min, Vector2 clip_rect_max, [MarshalAs(UnmanagedType.U1)] bool intersect_with_current_clip_rect);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PushClipRectFullScreen(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PushDrawFlag(ImDrawList* self, ImDrawFlags flags, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_PushTexture(ImDrawList* self, ImTextureRef tex_ref);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__AddLine(ImDrawList* self, Vector2 p1, Vector2 p2, uint col, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__AddPolyline(ImDrawList* self, Vector2* points, int num_points, uint col, float thickness, ImDrawFlags flags, float max_inner_offset);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__AddRectBaked(ImDrawList* self, Vector2 p_min, Vector2 p_max, uint col, float r, float t, Vector4 tex_uvs, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__AddRectFilledBaked(ImDrawList* self, Vector2 p_min, Vector2 p_max, uint col, float r, Vector4 tex_uvs, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__AddRectTinyRounding(ImDrawList* self, Vector2 p_min, Vector2 p_max, uint col, float rounding, float thickness, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial int ImDrawList__CalcCircleAutoSegmentCount(ImDrawList* self, float radius);

    [LibraryImport(LibraryName)]
    public static partial float ImDrawList__CalculateCenterBiasedOffset(ImDrawList* self, float thickness);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__ClearFreeMemory(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__OnChangedClipRect(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__OnChangedTexture(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__OnChangedVtxOffset(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__PathArcToFastEx(ImDrawList* self, Vector2 center, float radius, int a_min_sample, int a_max_sample, int a_step);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__PathArcToN(ImDrawList* self, Vector2 center, float radius, float a_min, float a_max, int num_segments);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__PopUnusedDrawCmd(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__ResetForNewFrame(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__SelectLineTexture(ImDrawList* self, float screen_thickness, Vector2* out_uv0, Vector2* out_uv1, float* out_fringe, ImDrawFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__SetDrawListSharedData(ImDrawList* self, ImDrawListSharedData* data);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__SetPixelDensity(ImDrawList* self, float pixel_density);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__SetTexture(ImDrawList* self, ImTextureRef tex_ref);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList__TryMergeDrawCmds(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial void ImDrawList_destroy(ImDrawList* self);

    [LibraryImport(LibraryName)]
    public static partial ImFontAtlasRect* ImFontAtlasRect_ImFontAtlasRect();

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlasRect_destroy(ImFontAtlasRect* self);

    [LibraryImport(LibraryName)]
    public static partial int ImFontAtlas_AddCustomRect(ImFontAtlas* self, int width, int height, ImFontAtlasRect* out_r);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFont(ImFontAtlas* self, ImFontConfig* font_cfg);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontDefault(ImFontAtlas* self, ImFontConfig* font_cfg);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontDefaultBitmap(ImFontAtlas* self, ImFontConfig* font_cfg);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontDefaultVector(ImFontAtlas* self, ImFontConfig* font_cfg);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontFromFileTTF(ImFontAtlas* self, byte* filename, float size_pixels, ImFontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(ImFontAtlas* self, byte* compressed_font_data_base85, float size_pixels, ImFontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontFromMemoryCompressedTTF(ImFontAtlas* self, void* compressed_font_data, int compressed_font_data_size, float size_pixels, ImFontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFontAtlas_AddFontFromMemoryTTF(ImFontAtlas* self, void* font_data, int font_data_size, float size_pixels, ImFontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_Clear(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_ClearFonts(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_ClearInputData(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_ClearTexData(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_CompactCache(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImFontAtlas_GetCustomRect(ImFontAtlas* self, int id, ImFontAtlasRect* out_r);

    [LibraryImport(LibraryName)]
    public static partial ushort* ImFontAtlas_GetGlyphRangesDefault(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    public static partial ImFontAtlas* ImFontAtlas_ImFontAtlas();

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_RemoveCustomRect(ImFontAtlas* self, int id);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_RemoveFont(ImFontAtlas* self, ImFont* font);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_SetFontLoader(ImFontAtlas* self, ImFontLoader* font_loader);

    [LibraryImport(LibraryName)]
    public static partial void ImFontAtlas_destroy(ImFontAtlas* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFontBaked_ClearOutputData(ImFontBaked* self);

    [LibraryImport(LibraryName)]
    public static partial ImFontGlyph* ImFontBaked_FindGlyph(ImFontBaked* self, ushort c);

    [LibraryImport(LibraryName)]
    public static partial ImFontGlyph* ImFontBaked_FindGlyphNoFallback(ImFontBaked* self, ushort c);

    [LibraryImport(LibraryName)]
    public static partial float ImFontBaked_GetCharAdvance(ImFontBaked* self, ushort c);

    [LibraryImport(LibraryName)]
    public static partial ImFontBaked* ImFontBaked_ImFontBaked();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImFontBaked_IsGlyphLoaded(ImFontBaked* self, ushort c);

    [LibraryImport(LibraryName)]
    public static partial void ImFontBaked_destroy(ImFontBaked* self);

    [LibraryImport(LibraryName)]
    public static partial ImFontConfig* ImFontConfig_ImFontConfig();

    [LibraryImport(LibraryName)]
    public static partial void ImFontConfig_destroy(ImFontConfig* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_AddChar(ImFontGlyphRangesBuilder* self, ushort c);

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_AddRanges(ImFontGlyphRangesBuilder* self, ushort* ranges);

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_AddText(ImFontGlyphRangesBuilder* self, byte* text, byte* text_end);

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_BuildRanges(ImFontGlyphRangesBuilder* self, ImVector<ushort>* out_ranges);

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_Clear(ImFontGlyphRangesBuilder* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImFontGlyphRangesBuilder_GetBit(ImFontGlyphRangesBuilder* self, nuint n);

    [LibraryImport(LibraryName)]
    public static partial ImFontGlyphRangesBuilder* ImFontGlyphRangesBuilder_ImFontGlyphRangesBuilder();

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_SetBit(ImFontGlyphRangesBuilder* self, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyphRangesBuilder_destroy(ImFontGlyphRangesBuilder* self);

    [LibraryImport(LibraryName)]
    public static partial ImFontGlyph* ImFontGlyph_ImFontGlyph();

    [LibraryImport(LibraryName)]
    public static partial void ImFontGlyph_destroy(ImFontGlyph* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFont_AddRemapCodepoint(ImFont* self, ushort from_codepoint, ushort to_codepoint);

    [LibraryImport(LibraryName)]
    public static partial void ImFont_AddRemapCodepointToGlyphIndex(ImFont* self, ushort from_codepoint, int font_src_idx, int glyph_idx);

    [LibraryImport(LibraryName)]
    public static partial Vector2 ImFont_CalcTextSizeA(ImFont* self, float size, float max_width, float wrap_width, byte* text_begin, byte* text_end, byte** out_remaining);

    [LibraryImport(LibraryName)]
    public static partial byte* ImFont_CalcWordWrapPosition(ImFont* self, float size, byte* text, byte* text_end, float wrap_width);

    [LibraryImport(LibraryName)]
    public static partial void ImFont_ClearOutputData(ImFont* self);

    [LibraryImport(LibraryName)]
    public static partial byte* ImFont_GetDebugName(ImFont* self);

    [LibraryImport(LibraryName)]
    public static partial ImFontBaked* ImFont_GetFontBaked(ImFont* self, float font_size, float density);

    [LibraryImport(LibraryName)]
    public static partial ImFont* ImFont_ImFont();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImFont_IsGlyphInFont(ImFont* self, ushort c);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImFont_IsGlyphRangeUnused(ImFont* self, uint c_begin, uint c_last);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImFont_IsLoaded(ImFont* self);

    [LibraryImport(LibraryName)]
    public static partial void ImFont_RenderChar(ImFont* self, ImDrawList* draw_list, float size, Vector2 pos, uint col, ushort c, Vector4* cpu_fine_clip);

    [LibraryImport(LibraryName)]
    public static partial void ImFont_RenderText(ImFont* self, ImDrawList* draw_list, float size, Vector2 pos, uint col, Vector4 clip_rect, byte* text_begin, byte* text_end, float wrap_width, int flags);

    [LibraryImport(LibraryName)]
    public static partial void ImFont_destroy(ImFont* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddFocusEvent(ImGuiIO* self, [MarshalAs(UnmanagedType.U1)] bool focused);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddInputCharacter(ImGuiIO* self, uint c);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddInputCharacterUTF16(ImGuiIO* self, ushort c);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddInputCharactersUTF8(ImGuiIO* self, byte* str);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddKeyAnalogEvent(ImGuiIO* self, ImGuiKey key, [MarshalAs(UnmanagedType.U1)] bool down, float v);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddKeyEvent(ImGuiIO* self, ImGuiKey key, [MarshalAs(UnmanagedType.U1)] bool down);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddMouseButtonEvent(ImGuiIO* self, int button, [MarshalAs(UnmanagedType.U1)] bool down);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddMousePosEvent(ImGuiIO* self, float x, float y);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddMouseSourceEvent(ImGuiIO* self, ImGuiMouseSource source);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddMouseViewportEvent(ImGuiIO* self, uint id);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_AddMouseWheelEvent(ImGuiIO* self, float wheel_x, float wheel_y);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_ClearEventsQueue(ImGuiIO* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_ClearInputKeys(ImGuiIO* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_ClearInputMouse(ImGuiIO* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiIO* ImGuiIO_ImGuiIO();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_SetAppAcceptingEvents(ImGuiIO* self, [MarshalAs(UnmanagedType.U1)] bool accepting_events);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_SetKeyEventNativeData(ImGuiIO* self, ImGuiKey key, int native_keycode, int native_scancode, int native_legacy_index);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiIO_destroy(ImGuiIO* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiInputTextCallbackData_ClearSelection(ImGuiInputTextCallbackData* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiInputTextCallbackData_DeleteChars(ImGuiInputTextCallbackData* self, int pos, int bytes_count);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiInputTextCallbackData_HasSelection(ImGuiInputTextCallbackData* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiInputTextCallbackData* ImGuiInputTextCallbackData_ImGuiInputTextCallbackData();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiInputTextCallbackData_InsertChars(ImGuiInputTextCallbackData* self, int pos, byte* text, byte* text_end);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiInputTextCallbackData_SelectAll(ImGuiInputTextCallbackData* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiInputTextCallbackData_SetSelection(ImGuiInputTextCallbackData* self, int s, int e);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiInputTextCallbackData_destroy(ImGuiInputTextCallbackData* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiListClipper_Begin(ImGuiListClipper* self, int items_count, float items_height);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiListClipper_End(ImGuiListClipper* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiListClipper* ImGuiListClipper_ImGuiListClipper();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiListClipper_IncludeItemByIndex(ImGuiListClipper* self, int item_index);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiListClipper_IncludeItemsByIndex(ImGuiListClipper* self, int item_begin, int item_end);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiListClipper_SeekCursorForItem(ImGuiListClipper* self, int item_index);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiListClipper_Step(ImGuiListClipper* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiListClipper_destroy(ImGuiListClipper* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiOnceUponAFrame* ImGuiOnceUponAFrame_ImGuiOnceUponAFrame();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiOnceUponAFrame_destroy(ImGuiOnceUponAFrame* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPayload_Clear(ImGuiPayload* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiPayload* ImGuiPayload_ImGuiPayload();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiPayload_IsDataType(ImGuiPayload* self, byte* type);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiPayload_IsDelivery(ImGuiPayload* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiPayload_IsPreview(ImGuiPayload* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPayload_destroy(ImGuiPayload* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPlatformIO_ClearPlatformHandlers(ImGuiPlatformIO* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPlatformIO_ClearRendererHandlers(ImGuiPlatformIO* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiPlatformIO* ImGuiPlatformIO_ImGuiPlatformIO();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPlatformIO_destroy(ImGuiPlatformIO* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiPlatformImeData* ImGuiPlatformImeData_ImGuiPlatformImeData();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPlatformImeData_destroy(ImGuiPlatformImeData* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiPlatformMonitor* ImGuiPlatformMonitor_ImGuiPlatformMonitor();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiPlatformMonitor_destroy(ImGuiPlatformMonitor* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionBasicStorage_ApplyRequests(ImGuiSelectionBasicStorage* self, ImGuiMultiSelectIO* ms_io);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionBasicStorage_Clear(ImGuiSelectionBasicStorage* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiSelectionBasicStorage_Contains(ImGuiSelectionBasicStorage* self, uint id);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiSelectionBasicStorage_GetNextSelectedItem(ImGuiSelectionBasicStorage* self, void** opaque_it, uint* out_id);

    [LibraryImport(LibraryName)]
    public static partial uint ImGuiSelectionBasicStorage_GetStorageIdFromIndex(ImGuiSelectionBasicStorage* self, int idx);

    [LibraryImport(LibraryName)]
    public static partial ImGuiSelectionBasicStorage* ImGuiSelectionBasicStorage_ImGuiSelectionBasicStorage();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionBasicStorage_SetItemSelected(ImGuiSelectionBasicStorage* self, uint id, [MarshalAs(UnmanagedType.U1)] bool selected);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionBasicStorage_Swap(ImGuiSelectionBasicStorage* self, ImGuiSelectionBasicStorage* r);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionBasicStorage_destroy(ImGuiSelectionBasicStorage* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionExternalStorage_ApplyRequests(ImGuiSelectionExternalStorage* self, ImGuiMultiSelectIO* ms_io);

    [LibraryImport(LibraryName)]
    public static partial ImGuiSelectionExternalStorage* ImGuiSelectionExternalStorage_ImGuiSelectionExternalStorage();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiSelectionExternalStorage_destroy(ImGuiSelectionExternalStorage* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiStoragePair* ImGuiStoragePair_ImGuiStoragePair_Int(uint _key, int _val);

    [LibraryImport(LibraryName)]
    public static partial ImGuiStoragePair* ImGuiStoragePair_ImGuiStoragePair_Float(uint _key, float _val);

    [LibraryImport(LibraryName)]
    public static partial ImGuiStoragePair* ImGuiStoragePair_ImGuiStoragePair_Ptr(uint _key, void* _val);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStoragePair_destroy(ImGuiStoragePair* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_BuildSortByKey(ImGuiStorage* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_Clear(ImGuiStorage* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiStorage_GetBool(ImGuiStorage* self, uint key, [MarshalAs(UnmanagedType.U1)] bool default_val);

    [LibraryImport(LibraryName)]
    public static partial bool* ImGuiStorage_GetBoolRef(ImGuiStorage* self, uint key, [MarshalAs(UnmanagedType.U1)] bool default_val);

    [LibraryImport(LibraryName)]
    public static partial float ImGuiStorage_GetFloat(ImGuiStorage* self, uint key, float default_val);

    [LibraryImport(LibraryName)]
    public static partial float* ImGuiStorage_GetFloatRef(ImGuiStorage* self, uint key, float default_val);

    [LibraryImport(LibraryName)]
    public static partial int ImGuiStorage_GetInt(ImGuiStorage* self, uint key, int default_val);

    [LibraryImport(LibraryName)]
    public static partial int* ImGuiStorage_GetIntRef(ImGuiStorage* self, uint key, int default_val);

    [LibraryImport(LibraryName)]
    public static partial void* ImGuiStorage_GetVoidPtr(ImGuiStorage* self, uint key);

    [LibraryImport(LibraryName)]
    public static partial void** ImGuiStorage_GetVoidPtrRef(ImGuiStorage* self, uint key, void* default_val);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_SetAllInt(ImGuiStorage* self, int val);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_SetBool(ImGuiStorage* self, uint key, [MarshalAs(UnmanagedType.U1)] bool val);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_SetFloat(ImGuiStorage* self, uint key, float val);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_SetInt(ImGuiStorage* self, uint key, int val);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStorage_SetVoidPtr(ImGuiStorage* self, uint key, void* val);

    [LibraryImport(LibraryName)]
    public static partial ImGuiStyle* ImGuiStyle_ImGuiStyle();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStyle_ScaleAllSizes(ImGuiStyle* self, float scale_factor);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiStyle_destroy(ImGuiStyle* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiTableColumnSortSpecs* ImGuiTableColumnSortSpecs_ImGuiTableColumnSortSpecs();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTableColumnSortSpecs_destroy(ImGuiTableColumnSortSpecs* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiTableSortSpecs* ImGuiTableSortSpecs_ImGuiTableSortSpecs();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTableSortSpecs_destroy(ImGuiTableSortSpecs* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiTextBuffer* ImGuiTextBuffer_ImGuiTextBuffer();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextBuffer_append(ImGuiTextBuffer* self, byte* str, byte* str_end);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_ImGuiTextBuffer_appendf(ImGuiTextBuffer* self, byte* text);

    [LibraryImport(LibraryName)]
    public static partial byte* ImGuiTextBuffer_begin(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    public static partial byte* ImGuiTextBuffer_c_str(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextBuffer_clear(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextBuffer_destroy(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiTextBuffer_empty(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    public static partial byte* ImGuiTextBuffer_end(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextBuffer_reserve(ImGuiTextBuffer* self, int capacity);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextBuffer_resize(ImGuiTextBuffer* self, int size);

    [LibraryImport(LibraryName)]
    public static partial int ImGuiTextBuffer_size(ImGuiTextBuffer* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextFilter_Build(ImGuiTextFilter* self);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextFilter_Clear(ImGuiTextFilter* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiTextFilter_Draw(ImGuiTextFilter* self, byte* label);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiTextFilter_DrawWithHint(ImGuiTextFilter* self, byte* label, byte* hint);

    [LibraryImport(LibraryName)]
    public static partial ImGuiTextFilter* ImGuiTextFilter_ImGuiTextFilter(byte* default_filter);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiTextFilter_IsActive(ImGuiTextFilter* self);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ImGuiTextFilter_PassFilter(ImGuiTextFilter* self, byte* text, byte* text_end);

    [LibraryImport(LibraryName)]
    public static partial void ImGuiTextFilter_destroy(ImGuiTextFilter* self);

    [LibraryImport(LibraryName)]
    public static partial Vector2 ImGuiViewport_GetCenter(ImGuiViewport* self);

    [LibraryImport(LibraryName)]
    public static partial byte* ImGuiViewport_GetDebugName(ImGuiViewport* self);

    [LibraryImport(LibraryName)]
    public static partial Vector2 ImGuiViewport_GetWorkCenter(ImGuiViewport* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiViewport* ImGuiViewport_ImGuiViewport();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiViewport_destroy(ImGuiViewport* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiWindowClass* ImGuiWindowClass_ImGuiWindowClass();

    [LibraryImport(LibraryName)]
    public static partial void ImGuiWindowClass_destroy(ImGuiWindowClass* self);

    [LibraryImport(LibraryName)]
    public static partial void ImTextureData_Create(ImTextureData* self, ImTextureFormat format, int w, int h);

    [LibraryImport(LibraryName)]
    public static partial void ImTextureData_DestroyPixels(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial int ImTextureData_GetPitch(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial void* ImTextureData_GetPixels(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial void* ImTextureData_GetPixelsAt(ImTextureData* self, int x, int y);

    [LibraryImport(LibraryName)]
    public static partial int ImTextureData_GetSizeInBytes(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial ulong ImTextureData_GetTexID(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial ImTextureRef ImTextureData_GetTexRef(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial ImTextureData* ImTextureData_ImTextureData();

    [LibraryImport(LibraryName)]
    public static partial void ImTextureData_SetStatus(ImTextureData* self, ImTextureStatus status);

    [LibraryImport(LibraryName)]
    public static partial void ImTextureData_SetTexID(ImTextureData* self, ulong tex_id);

    [LibraryImport(LibraryName)]
    public static partial void ImTextureData_destroy(ImTextureData* self);

    [LibraryImport(LibraryName)]
    public static partial ulong ImTextureRef_GetTexID(ImTextureRef* self);

    [LibraryImport(LibraryName)]
    public static partial ImTextureRef* ImTextureRef_ImTextureRef_Nil();

    [LibraryImport(LibraryName)]
    public static partial ImTextureRef* ImTextureRef_ImTextureRef_TextureID(ulong tex_id);

    [LibraryImport(LibraryName)]
    public static partial void ImTextureRef_destroy(ImTextureRef* self);

    [LibraryImport(LibraryName)]
    public static partial Vector2* ImVec2_ImVec2_Nil();

    [LibraryImport(LibraryName)]
    public static partial Vector2* ImVec2_ImVec2_Float(float _x, float _y);

    [LibraryImport(LibraryName)]
    public static partial void ImVec2_destroy(Vector2* self);

    [LibraryImport(LibraryName)]
    public static partial Vector4* ImVec4_ImVec4_Nil();

    [LibraryImport(LibraryName)]
    public static partial Vector4* ImVec4_ImVec4_Float(float _x, float _y, float _z, float _w);

    [LibraryImport(LibraryName)]
    public static partial void ImVec4_destroy(Vector4* self);

    [LibraryImport(LibraryName)]
    public static partial ImGuiPayload* igAcceptDragDropPayload(byte* type, ImGuiDragDropFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igAlignTextToFramePadding();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igArrowButton(byte* str_id, ImGuiDir dir);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBegin(byte* name, bool* p_open, ImGuiWindowFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginChild_Str(byte* str_id, Vector2 size, ImGuiChildFlags child_flags, ImGuiWindowFlags window_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginChild_ID(uint id, Vector2 size, ImGuiChildFlags child_flags, ImGuiWindowFlags window_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginCombo(byte* label, byte* preview_value, ImGuiComboFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igBeginDisabled([MarshalAs(UnmanagedType.U1)] bool disabled);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginDragDropSource(ImGuiDragDropFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginDragDropTarget();

    [LibraryImport(LibraryName)]
    public static partial void igBeginGroup();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginItemTooltip();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginListBox(byte* label, Vector2 size);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginMainMenuBar();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginMenu(byte* label, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginMenuBar();

    [LibraryImport(LibraryName)]
    public static partial ImGuiMultiSelectIO* igBeginMultiSelect(ImGuiMultiSelectFlags flags, int selection_size, int items_count);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginPopup(byte* str_id, ImGuiWindowFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginPopupContextItem(byte* str_id, ImGuiPopupFlags popup_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginPopupContextVoid(byte* str_id, ImGuiPopupFlags popup_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginPopupContextWindow(byte* str_id, ImGuiPopupFlags popup_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginPopupModal(byte* name, bool* p_open, ImGuiWindowFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginTabBar(byte* str_id, ImGuiTabBarFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginTabItem(byte* label, bool* p_open, ImGuiTabItemFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginTable(byte* str_id, int columns, ImGuiTableFlags flags, Vector2 outer_size, float inner_width);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igBeginTooltip();

    [LibraryImport(LibraryName)]
    public static partial void igBullet();

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igBulletText(byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igButton(byte* label, Vector2 size);

    [LibraryImport(LibraryName)]
    public static partial float igCalcItemWidth();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igCalcTextSize(byte* text, byte* text_end, [MarshalAs(UnmanagedType.U1)] bool hide_text_after_double_hash, float wrap_width);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCheckbox(byte* label, bool* v);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCheckboxFlags_IntPtr(byte* label, int* flags, int flags_value);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCheckboxFlags_UintPtr(byte* label, uint* flags, uint flags_value);

    [LibraryImport(LibraryName)]
    public static partial void igCloseCurrentPopup();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCollapsingHeader_TreeNodeFlags(byte* label, ImGuiTreeNodeFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCollapsingHeader_BoolPtr(byte* label, bool* p_visible, ImGuiTreeNodeFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igColorButton(byte* desc_id, Vector4 col, ImGuiColorEditFlags flags, Vector2 size);

    [LibraryImport(LibraryName)]
    public static partial uint igColorConvertFloat4ToU32(Vector4 @in);

    [LibraryImport(LibraryName)]
    public static partial void igColorConvertHSVtoRGB(float h, float s, float v, float* out_r, float* out_g, float* out_b);

    [LibraryImport(LibraryName)]
    public static partial void igColorConvertRGBtoHSV(float r, float g, float b, float* out_h, float* out_s, float* out_v);

    [LibraryImport(LibraryName)]
    public static partial Vector4 igColorConvertU32ToFloat4(uint @in);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igColorEdit3(byte* label, float* col, ImGuiColorEditFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igColorEdit4(byte* label, float* col, ImGuiColorEditFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igColorPicker3(byte* label, float* col, ImGuiColorEditFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igColorPicker4(byte* label, float* col, ImGuiColorEditFlags flags, float* ref_col);

    [LibraryImport(LibraryName)]
    public static partial void igColumns(int count, byte* id, [MarshalAs(UnmanagedType.U1)] bool borders);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCombo_Str_arr(byte* label, int* current_item, byte** items, int items_count, int popup_max_height_in_items);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCombo_Str(byte* label, int* current_item, byte* items_separated_by_zeros, int popup_max_height_in_items);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igCombo_FnStrPtr(byte* label, int* current_item, void* getter, void* user_data, int items_count, int popup_max_height_in_items);

    [LibraryImport(LibraryName)]
    public static partial ImGuiContext* igCreateContext(ImFontAtlas* shared_font_atlas);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDebugCheckVersionAndDataLayout(byte* version_str, nuint sz_io, nuint sz_style, nuint sz_vec2, nuint sz_vec4, nuint sz_drawvert, nuint sz_drawidx);

    [LibraryImport(LibraryName)]
    public static partial void igDebugFlashStyleColor(ImGuiCol idx);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igDebugLog(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void igDebugStartItemPicker();

    [LibraryImport(LibraryName)]
    public static partial void igDebugTextEncoding(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void igDestroyContext(ImGuiContext* ctx);

    [LibraryImport(LibraryName)]
    public static partial void igDestroyPlatformWindows();

    [LibraryImport(LibraryName)]
    public static partial uint igDockSpace(uint dockspace_id, Vector2 size, ImGuiDockNodeFlags flags, ImGuiWindowClass* window_class);

    [LibraryImport(LibraryName)]
    public static partial uint igDockSpaceOverViewport(uint dockspace_id, ImGuiViewport* viewport, ImGuiDockNodeFlags flags, ImGuiWindowClass* window_class);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragFloat(byte* label, float* v, float v_speed, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragFloat2(byte* label, float* v, float v_speed, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragFloat3(byte* label, float* v, float v_speed, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragFloat4(byte* label, float* v, float v_speed, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragFloatRange2(byte* label, float* v_current_min, float* v_current_max, float v_speed, float v_min, float v_max, byte* format, byte* format_max, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragInt(byte* label, int* v, float v_speed, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragInt2(byte* label, int* v, float v_speed, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragInt3(byte* label, int* v, float v_speed, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragInt4(byte* label, int* v, float v_speed, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragIntRange2(byte* label, int* v_current_min, int* v_current_max, float v_speed, int v_min, int v_max, byte* format, byte* format_max, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragScalar(byte* label, ImGuiDataType data_type, void* p_data, float v_speed, void* p_min, void* p_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igDragScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components, float v_speed, void* p_min, void* p_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igDummy(Vector2 size);

    [LibraryImport(LibraryName)]
    public static partial void igEnd();

    [LibraryImport(LibraryName)]
    public static partial void igEndChild();

    [LibraryImport(LibraryName)]
    public static partial void igEndCombo();

    [LibraryImport(LibraryName)]
    public static partial void igEndDisabled();

    [LibraryImport(LibraryName)]
    public static partial void igEndDragDropSource();

    [LibraryImport(LibraryName)]
    public static partial void igEndDragDropTarget();

    [LibraryImport(LibraryName)]
    public static partial void igEndFrame();

    [LibraryImport(LibraryName)]
    public static partial void igEndGroup();

    [LibraryImport(LibraryName)]
    public static partial void igEndListBox();

    [LibraryImport(LibraryName)]
    public static partial void igEndMainMenuBar();

    [LibraryImport(LibraryName)]
    public static partial void igEndMenu();

    [LibraryImport(LibraryName)]
    public static partial void igEndMenuBar();

    [LibraryImport(LibraryName)]
    public static partial ImGuiMultiSelectIO* igEndMultiSelect();

    [LibraryImport(LibraryName)]
    public static partial void igEndPopup();

    [LibraryImport(LibraryName)]
    public static partial void igEndTabBar();

    [LibraryImport(LibraryName)]
    public static partial void igEndTabItem();

    [LibraryImport(LibraryName)]
    public static partial void igEndTable();

    [LibraryImport(LibraryName)]
    public static partial void igEndTooltip();

    [LibraryImport(LibraryName)]
    public static partial ImGuiViewport* igFindViewportByID(uint viewport_id);

    [LibraryImport(LibraryName)]
    public static partial ImGuiViewport* igFindViewportByPlatformHandle(void* platform_handle);

    [LibraryImport(LibraryName)]
    public static partial void igGetAllocatorFunctions(void** p_alloc_func, void** p_free_func, void** p_user_data);

    [LibraryImport(LibraryName)]
    public static partial ImDrawList* igGetBackgroundDrawList(ImGuiViewport* viewport);

    [LibraryImport(LibraryName)]
    public static partial byte* igGetClipboardText();

    [LibraryImport(LibraryName)]
    public static partial uint igGetColorU32_Col(ImGuiCol idx, float alpha_mul);

    [LibraryImport(LibraryName)]
    public static partial uint igGetColorU32_Vec4(Vector4 col);

    [LibraryImport(LibraryName)]
    public static partial uint igGetColorU32_U32(uint col, float alpha_mul);

    [LibraryImport(LibraryName)]
    public static partial int igGetColumnIndex();

    [LibraryImport(LibraryName)]
    public static partial float igGetColumnOffset(int column_index);

    [LibraryImport(LibraryName)]
    public static partial float igGetColumnWidth(int column_index);

    [LibraryImport(LibraryName)]
    public static partial int igGetColumnsCount();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetContentRegionAvail();

    [LibraryImport(LibraryName)]
    public static partial ImGuiContext* igGetCurrentContext();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetCursorPos();

    [LibraryImport(LibraryName)]
    public static partial float igGetCursorPosX();

    [LibraryImport(LibraryName)]
    public static partial float igGetCursorPosY();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetCursorScreenPos();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetCursorStartPos();

    [LibraryImport(LibraryName)]
    public static partial ImGuiPayload* igGetDragDropPayload();

    [LibraryImport(LibraryName)]
    public static partial ImDrawData* igGetDrawData();

    [LibraryImport(LibraryName)]
    public static partial ImDrawListSharedData* igGetDrawListSharedData();

    [LibraryImport(LibraryName)]
    public static partial ImFont* igGetFont();

    [LibraryImport(LibraryName)]
    public static partial ImFontBaked* igGetFontBaked();

    [LibraryImport(LibraryName)]
    public static partial float igGetFontSize();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetFontTexUvWhitePixel();

    [LibraryImport(LibraryName)]
    public static partial ImDrawList* igGetForegroundDrawList(ImGuiViewport* viewport);

    [LibraryImport(LibraryName)]
    public static partial int igGetFrameCount();

    [LibraryImport(LibraryName)]
    public static partial float igGetFrameHeight();

    [LibraryImport(LibraryName)]
    public static partial float igGetFrameHeightWithSpacing();

    [LibraryImport(LibraryName)]
    public static partial uint igGetID_Str(byte* str_id);

    [LibraryImport(LibraryName)]
    public static partial uint igGetID_StrStr(byte* str_id_begin, byte* str_id_end);

    [LibraryImport(LibraryName)]
    public static partial uint igGetID_Ptr(void* ptr_id);

    [LibraryImport(LibraryName)]
    public static partial uint igGetID_Int(int int_id);

    [LibraryImport(LibraryName)]
    public static partial ImGuiIO* igGetIO();

    [LibraryImport(LibraryName)]
    public static partial int igGetItemClickedCountWithSingleClickDelay(ImGuiMouseButton mouse_button, float delay);

    [LibraryImport(LibraryName)]
    public static partial ImGuiItemFlags igGetItemFlags();

    [LibraryImport(LibraryName)]
    public static partial uint igGetItemID();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetItemRectMax();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetItemRectMin();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetItemRectSize();

    [LibraryImport(LibraryName)]
    public static partial byte* igGetKeyName(ImGuiKey key);

    [LibraryImport(LibraryName)]
    public static partial int igGetKeyPressedAmount(ImGuiKey key, float repeat_delay, float rate);

    [LibraryImport(LibraryName)]
    public static partial ImGuiViewport* igGetMainViewport();

    [LibraryImport(LibraryName)]
    public static partial int igGetMouseClickedCount(ImGuiMouseButton button);

    [LibraryImport(LibraryName)]
    public static partial ImGuiMouseCursor igGetMouseCursor();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetMouseDragDelta(ImGuiMouseButton button, float lock_threshold);

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetMousePos();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetMousePosOnOpeningCurrentPopup();

    [LibraryImport(LibraryName)]
    public static partial ImGuiPlatformIO* igGetPlatformIO();

    [LibraryImport(LibraryName)]
    public static partial float igGetScrollMaxX();

    [LibraryImport(LibraryName)]
    public static partial float igGetScrollMaxY();

    [LibraryImport(LibraryName)]
    public static partial float igGetScrollX();

    [LibraryImport(LibraryName)]
    public static partial float igGetScrollY();

    [LibraryImport(LibraryName)]
    public static partial ImGuiStorage* igGetStateStorage();

    [LibraryImport(LibraryName)]
    public static partial ImGuiStyle* igGetStyle();

    [LibraryImport(LibraryName)]
    public static partial byte* igGetStyleColorName(ImGuiCol idx);

    [LibraryImport(LibraryName)]
    public static partial Vector4* igGetStyleColorVec4(ImGuiCol idx);

    [LibraryImport(LibraryName)]
    public static partial float igGetTextLineHeight();

    [LibraryImport(LibraryName)]
    public static partial float igGetTextLineHeightWithSpacing();

    [LibraryImport(LibraryName)]
    public static partial double igGetTime();

    [LibraryImport(LibraryName)]
    public static partial float igGetTreeNodeToLabelSpacing();

    [LibraryImport(LibraryName)]
    public static partial byte* igGetVersion();

    [LibraryImport(LibraryName)]
    public static partial uint igGetWindowDockID();

    [LibraryImport(LibraryName)]
    public static partial float igGetWindowDpiScale();

    [LibraryImport(LibraryName)]
    public static partial ImDrawList* igGetWindowDrawList();

    [LibraryImport(LibraryName)]
    public static partial float igGetWindowHeight();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetWindowPos();

    [LibraryImport(LibraryName)]
    public static partial Vector2 igGetWindowSize();

    [LibraryImport(LibraryName)]
    public static partial ImGuiViewport* igGetWindowViewport();

    [LibraryImport(LibraryName)]
    public static partial float igGetWindowWidth();

    [LibraryImport(LibraryName)]
    public static partial void igImage(ImTextureRef tex_ref, Vector2 image_size, Vector2 uv0, Vector2 uv1);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igImageButton(byte* str_id, ImTextureRef tex_ref, Vector2 image_size, Vector2 uv0, Vector2 uv1, Vector4 bg_col, Vector4 tint_col);

    [LibraryImport(LibraryName)]
    public static partial void igImageWithBg(ImTextureRef tex_ref, Vector2 image_size, Vector2 uv0, Vector2 uv1, Vector4 bg_col, Vector4 tint_col);

    [LibraryImport(LibraryName)]
    public static partial void igIndent(float indent_w);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputDouble(byte* label, double* v, double step, double step_fast, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputFloat(byte* label, float* v, float step, float step_fast, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputFloat2(byte* label, float* v, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputFloat3(byte* label, float* v, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputFloat4(byte* label, float* v, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputInt(byte* label, int* v, int step, int step_fast, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputInt2(byte* label, int* v, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputInt3(byte* label, int* v, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputInt4(byte* label, int* v, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputScalar(byte* label, ImGuiDataType data_type, void* p_data, void* p_step, void* p_step_fast, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_step, void* p_step_fast, byte* format, ImGuiInputTextFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputText(byte* label, byte* buf, nuint buf_size, ImGuiInputTextFlags flags, void* callback, void* user_data);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputTextMultiline(byte* label, byte* buf, nuint buf_size, Vector2 size, ImGuiInputTextFlags flags, void* callback, void* user_data);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInputTextWithHint(byte* label, byte* hint, byte* buf, nuint buf_size, ImGuiInputTextFlags flags, void* callback, void* user_data);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igInvisibleButton(byte* str_id, Vector2 size, ImGuiButtonFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsAnyItemActive();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsAnyItemFocused();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsAnyItemHovered();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsAnyMouseDown();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemActivated();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemActive();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemClicked(ImGuiMouseButton mouse_button);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemDeactivated();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemDeactivatedAfterEdit();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemEdited();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemFocused();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemHovered(ImGuiHoveredFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemToggledOpen();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemToggledSelection();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsItemVisible();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsKeyChordPressed(int key_chord);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsKeyDown(ImGuiKey key);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsKeyPressed(ImGuiKey key, [MarshalAs(UnmanagedType.U1)] bool repeat);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsKeyReleased(ImGuiKey key);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseClicked(ImGuiMouseButton button, [MarshalAs(UnmanagedType.U1)] bool repeat);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseDoubleClicked(ImGuiMouseButton button);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseDown(ImGuiMouseButton button);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseDragging(ImGuiMouseButton button, float lock_threshold);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseHoveringRect(Vector2 r_min, Vector2 r_max, [MarshalAs(UnmanagedType.U1)] bool clip);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMousePosValid(Vector2* mouse_pos);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseReleased(ImGuiMouseButton button);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsMouseReleasedWithDelay(ImGuiMouseButton button, float delay);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsPopupOpen(byte* str_id, ImGuiPopupFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsRectVisible_Nil(Vector2 size);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsRectVisible_Vec2(Vector2 rect_min, Vector2 rect_max);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsWindowAppearing();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsWindowCollapsed();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsWindowDocked();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsWindowFocused(ImGuiFocusedFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igIsWindowHovered(ImGuiHoveredFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igLabelText(byte* label, byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igListBox_Str_arr(byte* label, int* current_item, byte** items, int items_count, int height_in_items);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igListBox_FnStrPtr(byte* label, int* current_item, void* getter, void* user_data, int items_count, int height_in_items);

    [LibraryImport(LibraryName)]
    public static partial void igLoadIniSettingsFromDisk(byte* ini_filename);

    [LibraryImport(LibraryName)]
    public static partial void igLoadIniSettingsFromMemory(byte* ini_data, nuint ini_size);

    [LibraryImport(LibraryName)]
    public static partial void igLogButtons();

    [LibraryImport(LibraryName)]
    public static partial void igLogFinish();

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igLogText(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void igLogToClipboard(int auto_open_depth);

    [LibraryImport(LibraryName)]
    public static partial void igLogToFile(int auto_open_depth, byte* filename);

    [LibraryImport(LibraryName)]
    public static partial void igLogToTTY(int auto_open_depth);

    [LibraryImport(LibraryName)]
    public static partial void* igMemAlloc(nuint size);

    [LibraryImport(LibraryName)]
    public static partial void igMemFree(void* ptr);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igMenuItem_Bool(byte* label, byte* shortcut, [MarshalAs(UnmanagedType.U1)] bool selected, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igMenuItem_BoolPtr(byte* label, byte* shortcut, bool* p_selected, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibraryName)]
    public static partial void igNewFrame();

    [LibraryImport(LibraryName)]
    public static partial void igNewLine();

    [LibraryImport(LibraryName)]
    public static partial void igNextColumn();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igOpenPopup_Str(byte* str_id, ImGuiPopupFlags popup_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igOpenPopup_ID(uint id, ImGuiPopupFlags popup_flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igOpenPopupOnItemClick(byte* str_id, ImGuiPopupFlags popup_flags);

    [LibraryImport(LibraryName)]
    public static partial void igPlotHistogram_FloatPtr(byte* label, float* values, int values_count, int values_offset, byte* overlay_text, float scale_min, float scale_max, Vector2 graph_size, int stride);

    [LibraryImport(LibraryName)]
    public static partial void igPlotHistogram_FnFloatPtr(byte* label, void* values_getter, void* data, int values_count, int values_offset, byte* overlay_text, float scale_min, float scale_max, Vector2 graph_size);

    [LibraryImport(LibraryName)]
    public static partial void igPlotLines_FloatPtr(byte* label, float* values, int values_count, int values_offset, byte* overlay_text, float scale_min, float scale_max, Vector2 graph_size, int stride);

    [LibraryImport(LibraryName)]
    public static partial void igPlotLines_FnFloatPtr(byte* label, void* values_getter, void* data, int values_count, int values_offset, byte* overlay_text, float scale_min, float scale_max, Vector2 graph_size);

    [LibraryImport(LibraryName)]
    public static partial void igPopClipRect();

    [LibraryImport(LibraryName)]
    public static partial void igPopFont();

    [LibraryImport(LibraryName)]
    public static partial void igPopID();

    [LibraryImport(LibraryName)]
    public static partial void igPopItemFlag();

    [LibraryImport(LibraryName)]
    public static partial void igPopItemWidth();

    [LibraryImport(LibraryName)]
    public static partial void igPopStyleColor(int count);

    [LibraryImport(LibraryName)]
    public static partial void igPopStyleVar(int count);

    [LibraryImport(LibraryName)]
    public static partial void igPopTextWrapPos();

    [LibraryImport(LibraryName)]
    public static partial void igProgressBar(float fraction, Vector2 size_arg, byte* overlay);

    [LibraryImport(LibraryName)]
    public static partial void igPushClipRect(Vector2 clip_rect_min, Vector2 clip_rect_max, [MarshalAs(UnmanagedType.U1)] bool intersect_with_current_clip_rect);

    [LibraryImport(LibraryName)]
    public static partial void igPushFont(ImFont* font, float font_size_base_unscaled);

    [LibraryImport(LibraryName)]
    public static partial void igPushID_Str(byte* str_id);

    [LibraryImport(LibraryName)]
    public static partial void igPushID_StrStr(byte* str_id_begin, byte* str_id_end);

    [LibraryImport(LibraryName)]
    public static partial void igPushID_Ptr(void* ptr_id);

    [LibraryImport(LibraryName)]
    public static partial void igPushID_Int(int int_id);

    [LibraryImport(LibraryName)]
    public static partial void igPushItemFlag(ImGuiItemFlags option, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibraryName)]
    public static partial void igPushItemWidth(float item_width);

    [LibraryImport(LibraryName)]
    public static partial void igPushStyleColor_U32(ImGuiCol idx, uint col);

    [LibraryImport(LibraryName)]
    public static partial void igPushStyleColor_Vec4(ImGuiCol idx, Vector4 col);

    [LibraryImport(LibraryName)]
    public static partial void igPushStyleVar_Float(ImGuiStyleVar idx, float val);

    [LibraryImport(LibraryName)]
    public static partial void igPushStyleVar_Vec2(ImGuiStyleVar idx, Vector2 val);

    [LibraryImport(LibraryName)]
    public static partial void igPushStyleVarX(ImGuiStyleVar idx, float val_x);

    [LibraryImport(LibraryName)]
    public static partial void igPushStyleVarY(ImGuiStyleVar idx, float val_y);

    [LibraryImport(LibraryName)]
    public static partial void igPushTextWrapPos(float wrap_local_pos_x);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igRadioButton_Bool(byte* label, [MarshalAs(UnmanagedType.U1)] bool active);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igRadioButton_IntPtr(byte* label, int* v, int v_button);

    [LibraryImport(LibraryName)]
    public static partial void igRender();

    [LibraryImport(LibraryName)]
    public static partial void igRenderPlatformWindowsDefault(void* platform_render_arg, void* renderer_render_arg);

    [LibraryImport(LibraryName)]
    public static partial void igResetMouseDragDelta(ImGuiMouseButton button);

    [LibraryImport(LibraryName)]
    public static partial void igSameLine(float offset_from_start_x, float spacing);

    [LibraryImport(LibraryName)]
    public static partial void igSaveIniSettingsToDisk(byte* ini_filename);

    [LibraryImport(LibraryName)]
    public static partial byte* igSaveIniSettingsToMemory(nuint* out_ini_size);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSelectable_Bool(byte* label, [MarshalAs(UnmanagedType.U1)] bool selected, ImGuiSelectableFlags flags, Vector2 size);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSelectable_BoolPtr(byte* label, bool* p_selected, ImGuiSelectableFlags flags, Vector2 size);

    [LibraryImport(LibraryName)]
    public static partial void igSeparator();

    [LibraryImport(LibraryName)]
    public static partial void igSeparatorText(byte* label);

    [LibraryImport(LibraryName)]
    public static partial void igSetAllocatorFunctions(void* alloc_func, void* free_func, void* user_data);

    [LibraryImport(LibraryName)]
    public static partial void igSetClipboardText(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void igSetColumnOffset(int column_index, float offset_x);

    [LibraryImport(LibraryName)]
    public static partial void igSetColumnWidth(int column_index, float width);

    [LibraryImport(LibraryName)]
    public static partial void igSetCurrentContext(ImGuiContext* ctx);

    [LibraryImport(LibraryName)]
    public static partial void igSetCursorPos(Vector2 local_pos);

    [LibraryImport(LibraryName)]
    public static partial void igSetCursorPosX(float local_x);

    [LibraryImport(LibraryName)]
    public static partial void igSetCursorPosY(float local_y);

    [LibraryImport(LibraryName)]
    public static partial void igSetCursorScreenPos(Vector2 pos);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSetDragDropPayload(byte* type, void* data, nuint sz, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetItemDefaultFocus();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSetItemKeyOwner(ImGuiKey key);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igSetItemTooltip(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void igSetKeyboardFocusHere(int offset);

    [LibraryImport(LibraryName)]
    public static partial void igSetMouseCursor(ImGuiMouseCursor cursor_type);

    [LibraryImport(LibraryName)]
    public static partial void igSetNavCursorVisible([MarshalAs(UnmanagedType.U1)] bool visible);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextFrameWantCaptureKeyboard([MarshalAs(UnmanagedType.U1)] bool want_capture_keyboard);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextFrameWantCaptureMouse([MarshalAs(UnmanagedType.U1)] bool want_capture_mouse);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextItemAllowOverlap();

    [LibraryImport(LibraryName)]
    public static partial void igSetNextItemOpen([MarshalAs(UnmanagedType.U1)] bool is_open, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextItemSelectionUserData(long selection_user_data);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextItemShortcut(int key_chord, ImGuiInputFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextItemStorageID(uint storage_id);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextItemWidth(float item_width);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowBgAlpha(float alpha);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowClass(ImGuiWindowClass* window_class);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowCollapsed([MarshalAs(UnmanagedType.U1)] bool collapsed, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowContentSize(Vector2 size);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowDockID(uint dock_id, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowFocus();

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowPos(Vector2 pos, ImGuiCond cond, Vector2 pivot);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowScroll(Vector2 scroll);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowSize(Vector2 size, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowSizeConstraints(Vector2 size_min, Vector2 size_max, void* custom_callback, void* custom_callback_data);

    [LibraryImport(LibraryName)]
    public static partial void igSetNextWindowViewport(uint viewport_id);

    [LibraryImport(LibraryName)]
    public static partial void igSetScrollFromPosX(float local_x, float center_x_ratio);

    [LibraryImport(LibraryName)]
    public static partial void igSetScrollFromPosY(float local_y, float center_y_ratio);

    [LibraryImport(LibraryName)]
    public static partial void igSetScrollHereX(float center_x_ratio);

    [LibraryImport(LibraryName)]
    public static partial void igSetScrollHereY(float center_y_ratio);

    [LibraryImport(LibraryName)]
    public static partial void igSetScrollX(float scroll_x);

    [LibraryImport(LibraryName)]
    public static partial void igSetScrollY(float scroll_y);

    [LibraryImport(LibraryName)]
    public static partial void igSetStateStorage(ImGuiStorage* storage);

    [LibraryImport(LibraryName)]
    public static partial void igSetTabItemClosed(byte* tab_or_docked_window_label);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igSetTooltip(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowCollapsed_Bool([MarshalAs(UnmanagedType.U1)] bool collapsed, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowCollapsed_Str(byte* name, [MarshalAs(UnmanagedType.U1)] bool collapsed, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowFocus_Nil();

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowFocus_Str(byte* name);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowPos_Vec2(Vector2 pos, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowPos_Str(byte* name, Vector2 pos, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowSize_Vec2(Vector2 size, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    public static partial void igSetWindowSize_Str(byte* name, Vector2 size, ImGuiCond cond);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igShortcut(int key_chord, ImGuiInputFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igShowAboutWindow(bool* p_open);

    [LibraryImport(LibraryName)]
    public static partial void igShowDebugLogWindow(bool* p_open);

    [LibraryImport(LibraryName)]
    public static partial void igShowDemoWindow(bool* p_open);

    [LibraryImport(LibraryName)]
    public static partial void igShowFontSelector(byte* label);

    [LibraryImport(LibraryName)]
    public static partial void igShowIDStackToolWindow(bool* p_open);

    [LibraryImport(LibraryName)]
    public static partial void igShowMetricsWindow(bool* p_open);

    [LibraryImport(LibraryName)]
    public static partial void igShowStyleEditor(ImGuiStyle* @ref);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igShowStyleSelector(byte* label);

    [LibraryImport(LibraryName)]
    public static partial void igShowUserGuide();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderAngle(byte* label, float* v_rad, float v_degrees_min, float v_degrees_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderFloat(byte* label, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderFloat2(byte* label, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderFloat3(byte* label, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderFloat4(byte* label, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderInt(byte* label, int* v, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderInt2(byte* label, int* v, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderInt3(byte* label, int* v, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderInt4(byte* label, int* v, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderScalar(byte* label, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSliderScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_min, void* p_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igSmallButton(byte* label);

    [LibraryImport(LibraryName)]
    public static partial void igSpacing();

    [LibraryImport(LibraryName)]
    public static partial void igStyleColorsClassic(ImGuiStyle* dst);

    [LibraryImport(LibraryName)]
    public static partial void igStyleColorsDark(ImGuiStyle* dst);

    [LibraryImport(LibraryName)]
    public static partial void igStyleColorsLight(ImGuiStyle* dst);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTabItemButton(byte* label, ImGuiTabItemFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igTableAngledHeadersRow();

    [LibraryImport(LibraryName)]
    public static partial int igTableGetColumnCount();

    [LibraryImport(LibraryName)]
    public static partial ImGuiTableColumnFlags igTableGetColumnFlags(int column_n);

    [LibraryImport(LibraryName)]
    public static partial int igTableGetColumnIndex();

    [LibraryImport(LibraryName)]
    public static partial byte* igTableGetColumnName(int column_n);

    [LibraryImport(LibraryName)]
    public static partial int igTableGetHoveredColumn();

    [LibraryImport(LibraryName)]
    public static partial int igTableGetRowIndex();

    [LibraryImport(LibraryName)]
    public static partial ImGuiTableSortSpecs* igTableGetSortSpecs();

    [LibraryImport(LibraryName)]
    public static partial void igTableHeader(byte* label);

    [LibraryImport(LibraryName)]
    public static partial void igTableHeadersRow();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTableNextColumn();

    [LibraryImport(LibraryName)]
    public static partial void igTableNextRow(ImGuiTableRowFlags row_flags, float min_row_height);

    [LibraryImport(LibraryName)]
    public static partial void igTableSetBgColor(ImGuiTableBgTarget target, uint color, int column_n);

    [LibraryImport(LibraryName)]
    public static partial void igTableSetColumnEnabled(int column_n, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTableSetColumnIndex(int column_n);

    [LibraryImport(LibraryName)]
    public static partial void igTableSetupColumn(byte* label, ImGuiTableColumnFlags flags, float init_width_or_weight, uint user_data);

    [LibraryImport(LibraryName)]
    public static partial void igTableSetupScrollFreeze(int cols, int rows);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igText(byte* text);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igTextColored(Vector4 col, byte* text);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igTextDisabled(byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTextLink(byte* label);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTextLinkOpenURL(byte* label, byte* url);

    [LibraryImport(LibraryName)]
    public static partial void igTextUnformatted(byte* text, byte* text_end);

    [LibraryImport(LibraryName)]
    public static partial void CRYSTAL_igTextWrapped(byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTreeNode_Str(byte* label);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool CRYSTAL_igTreeNode_StrStr(byte* str_id, byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool CRYSTAL_igTreeNode_Ptr(void* ptr_id, byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTreeNodeEx_Str(byte* label, ImGuiTreeNodeFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool CRYSTAL_igTreeNodeEx_StrStr(byte* str_id, ImGuiTreeNodeFlags flags, byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool CRYSTAL_igTreeNodeEx_Ptr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* text);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igTreeNodeGetOpen(uint storage_id);

    [LibraryImport(LibraryName)]
    public static partial void igTreePop();

    [LibraryImport(LibraryName)]
    public static partial void igTreePush_Str(byte* str_id);

    [LibraryImport(LibraryName)]
    public static partial void igTreePush_Ptr(void* ptr_id);

    [LibraryImport(LibraryName)]
    public static partial void igUnindent(float indent_w);

    [LibraryImport(LibraryName)]
    public static partial void igUpdatePlatformWindows();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igVSliderFloat(byte* label, Vector2 size, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igVSliderInt(byte* label, Vector2 size, int* v, int v_min, int v_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool igVSliderScalar(byte* label, Vector2 size, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max, byte* format, ImGuiSliderFlags flags);

    [LibraryImport(LibraryName)]
    public static partial void igValue_Bool(byte* prefix, [MarshalAs(UnmanagedType.U1)] bool b);

    [LibraryImport(LibraryName)]
    public static partial void igValue_Int(byte* prefix, int v);

    [LibraryImport(LibraryName)]
    public static partial void igValue_Uint(byte* prefix, uint v);

    [LibraryImport(LibraryName)]
    public static partial void igValue_Float(byte* prefix, float v, byte* float_format);
}
