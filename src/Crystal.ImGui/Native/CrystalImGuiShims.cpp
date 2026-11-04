#include "./imgui/imgui.h"
#include "./imgui/imgui_internal.h"
#include "cimgui.h"
#include <stddef.h>
#include <string.h>

CIMGUI_API void CRYSTAL_ImGuiTextBuffer_appendf(ImGuiTextBuffer* self,const char* text)
{
    ImGuiTextBuffer_appendf(self,"%s",text);
}

CIMGUI_API void CRYSTAL_igBulletText(const char* text)
{
    igBulletText("%s",text);
}

CIMGUI_API void CRYSTAL_igDebugLog(const char* text)
{
    igDebugLog("%s",text);
}

CIMGUI_API void CRYSTAL_igLabelText(const char* label,const char* text)
{
    igLabelText(label,"%s",text);
}

CIMGUI_API void CRYSTAL_igLogText(const char* text)
{
    igLogText("%s",text);
}

CIMGUI_API void CRYSTAL_igSetItemTooltip(const char* text)
{
    igSetItemTooltip("%s",text);
}

CIMGUI_API void CRYSTAL_igSetTooltip(const char* text)
{
    igSetTooltip("%s",text);
}

CIMGUI_API void CRYSTAL_igText(const char* text)
{
    igText("%s",text);
}

CIMGUI_API void CRYSTAL_igTextColored(const ImVec4_c col,const char* text)
{
    igTextColored(col,"%s",text);
}

CIMGUI_API void CRYSTAL_igTextDisabled(const char* text)
{
    igTextDisabled("%s",text);
}

CIMGUI_API void CRYSTAL_igTextWrapped(const char* text)
{
    igTextWrapped("%s",text);
}

CIMGUI_API bool CRYSTAL_igTreeNode_StrStr(const char* str_id,const char* text)
{
    return igTreeNode_StrStr(str_id,"%s",text);
}

CIMGUI_API bool CRYSTAL_igTreeNode_Ptr(const void* ptr_id,const char* text)
{
    return igTreeNode_Ptr(ptr_id,"%s",text);
}

CIMGUI_API bool CRYSTAL_igTreeNodeEx_StrStr(const char* str_id,ImGuiTreeNodeFlags flags,const char* text)
{
    return igTreeNodeEx_StrStr(str_id,flags,"%s",text);
}

CIMGUI_API bool CRYSTAL_igTreeNodeEx_Ptr(const void* ptr_id,ImGuiTreeNodeFlags flags,const char* text)
{
    return igTreeNodeEx_Ptr(ptr_id,flags,"%s",text);
}

CIMGUI_API int CRYSTAL_ImGuiStructSize(const char* struct_name)
{
    if (strcmp(struct_name, "ImColor") == 0) return (int)sizeof(ImColor);
    if (strcmp(struct_name, "ImDrawChannel") == 0) return (int)sizeof(ImDrawChannel);
    if (strcmp(struct_name, "ImDrawCmd") == 0) return (int)sizeof(ImDrawCmd);
    if (strcmp(struct_name, "ImDrawCmdHeader") == 0) return (int)sizeof(ImDrawCmdHeader);
    if (strcmp(struct_name, "ImDrawData") == 0) return (int)sizeof(ImDrawData);
    if (strcmp(struct_name, "ImDrawList") == 0) return (int)sizeof(ImDrawList);
    if (strcmp(struct_name, "ImDrawListSplitter") == 0) return (int)sizeof(ImDrawListSplitter);
    if (strcmp(struct_name, "ImDrawVert") == 0) return (int)sizeof(ImDrawVert);
    if (strcmp(struct_name, "ImFont") == 0) return (int)sizeof(ImFont);
    if (strcmp(struct_name, "ImFontAtlas") == 0) return (int)sizeof(ImFontAtlas);
    if (strcmp(struct_name, "ImFontAtlasRect") == 0) return (int)sizeof(ImFontAtlasRect);
    if (strcmp(struct_name, "ImFontBaked") == 0) return (int)sizeof(ImFontBaked);
    if (strcmp(struct_name, "ImFontConfig") == 0) return (int)sizeof(ImFontConfig);
    if (strcmp(struct_name, "ImFontGlyph") == 0) return (int)sizeof(ImFontGlyph);
    if (strcmp(struct_name, "ImFontGlyphRangesBuilder") == 0) return (int)sizeof(ImFontGlyphRangesBuilder);
    if (strcmp(struct_name, "ImGuiIO") == 0) return (int)sizeof(ImGuiIO);
    if (strcmp(struct_name, "ImGuiInputTextCallbackData") == 0) return (int)sizeof(ImGuiInputTextCallbackData);
    if (strcmp(struct_name, "ImGuiKeyData") == 0) return (int)sizeof(ImGuiKeyData);
    if (strcmp(struct_name, "ImGuiListClipper") == 0) return (int)sizeof(ImGuiListClipper);
    if (strcmp(struct_name, "ImGuiMultiSelectIO") == 0) return (int)sizeof(ImGuiMultiSelectIO);
    if (strcmp(struct_name, "ImGuiOnceUponAFrame") == 0) return (int)sizeof(ImGuiOnceUponAFrame);
    if (strcmp(struct_name, "ImGuiPayload") == 0) return (int)sizeof(ImGuiPayload);
    if (strcmp(struct_name, "ImGuiPlatformIO") == 0) return (int)sizeof(ImGuiPlatformIO);
    if (strcmp(struct_name, "ImGuiPlatformImeData") == 0) return (int)sizeof(ImGuiPlatformImeData);
    if (strcmp(struct_name, "ImGuiPlatformMonitor") == 0) return (int)sizeof(ImGuiPlatformMonitor);
    if (strcmp(struct_name, "ImGuiSelectionBasicStorage") == 0) return (int)sizeof(ImGuiSelectionBasicStorage);
    if (strcmp(struct_name, "ImGuiSelectionExternalStorage") == 0) return (int)sizeof(ImGuiSelectionExternalStorage);
    if (strcmp(struct_name, "ImGuiSelectionRequest") == 0) return (int)sizeof(ImGuiSelectionRequest);
    if (strcmp(struct_name, "ImGuiSizeCallbackData") == 0) return (int)sizeof(ImGuiSizeCallbackData);
    if (strcmp(struct_name, "ImGuiStorage") == 0) return (int)sizeof(ImGuiStorage);
    if (strcmp(struct_name, "ImGuiStoragePair") == 0) return (int)sizeof(ImGuiStoragePair);
    if (strcmp(struct_name, "ImGuiStyle") == 0) return (int)sizeof(ImGuiStyle);
    if (strcmp(struct_name, "ImGuiTableColumnSortSpecs") == 0) return (int)sizeof(ImGuiTableColumnSortSpecs);
    if (strcmp(struct_name, "ImGuiTableSortSpecs") == 0) return (int)sizeof(ImGuiTableSortSpecs);
    if (strcmp(struct_name, "ImGuiTextBuffer") == 0) return (int)sizeof(ImGuiTextBuffer);
    if (strcmp(struct_name, "ImGuiTextFilter") == 0) return (int)sizeof(ImGuiTextFilter);
    if (strcmp(struct_name, "ImGuiViewport") == 0) return (int)sizeof(ImGuiViewport);
    if (strcmp(struct_name, "ImGuiWindowClass") == 0) return (int)sizeof(ImGuiWindowClass);
    if (strcmp(struct_name, "ImTextureData") == 0) return (int)sizeof(ImTextureData);
    if (strcmp(struct_name, "ImTextureRect") == 0) return (int)sizeof(ImTextureRect);
    if (strcmp(struct_name, "ImTextureRef") == 0) return (int)sizeof(ImTextureRef);
    return -1;
}

CIMGUI_API int CRYSTAL_ImGuiFieldOffset(const char* struct_name, const char* field_name)
{
    if (strcmp(struct_name, "ImColor") == 0)
    {
        if (strcmp(field_name, "Value") == 0) return (int)offsetof(ImColor, Value);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawChannel") == 0)
    {
        if (strcmp(field_name, "_CmdBuffer") == 0) return (int)offsetof(ImDrawChannel, _CmdBuffer);
        if (strcmp(field_name, "_IdxBuffer") == 0) return (int)offsetof(ImDrawChannel, _IdxBuffer);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawCmd") == 0)
    {
        if (strcmp(field_name, "ClipRect") == 0) return (int)offsetof(ImDrawCmd, ClipRect);
        if (strcmp(field_name, "TexRef") == 0) return (int)offsetof(ImDrawCmd, TexRef);
        if (strcmp(field_name, "VtxOffset") == 0) return (int)offsetof(ImDrawCmd, VtxOffset);
        if (strcmp(field_name, "IdxOffset") == 0) return (int)offsetof(ImDrawCmd, IdxOffset);
        if (strcmp(field_name, "ElemCount") == 0) return (int)offsetof(ImDrawCmd, ElemCount);
        if (strcmp(field_name, "UserCallback") == 0) return (int)offsetof(ImDrawCmd, UserCallback);
        if (strcmp(field_name, "UserCallbackData") == 0) return (int)offsetof(ImDrawCmd, UserCallbackData);
        if (strcmp(field_name, "UserCallbackDataSize") == 0) return (int)offsetof(ImDrawCmd, UserCallbackDataSize);
        if (strcmp(field_name, "UserCallbackDataOffset") == 0) return (int)offsetof(ImDrawCmd, UserCallbackDataOffset);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawCmdHeader") == 0)
    {
        if (strcmp(field_name, "ClipRect") == 0) return (int)offsetof(ImDrawCmdHeader, ClipRect);
        if (strcmp(field_name, "TexRef") == 0) return (int)offsetof(ImDrawCmdHeader, TexRef);
        if (strcmp(field_name, "VtxOffset") == 0) return (int)offsetof(ImDrawCmdHeader, VtxOffset);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawData") == 0)
    {
        if (strcmp(field_name, "Valid") == 0) return (int)offsetof(ImDrawData, Valid);
        if (strcmp(field_name, "FrameCount") == 0) return (int)offsetof(ImDrawData, FrameCount);
        if (strcmp(field_name, "TotalIdxCount") == 0) return (int)offsetof(ImDrawData, TotalIdxCount);
        if (strcmp(field_name, "TotalVtxCount") == 0) return (int)offsetof(ImDrawData, TotalVtxCount);
        if (strcmp(field_name, "CmdLists") == 0) return (int)offsetof(ImDrawData, CmdLists);
        if (strcmp(field_name, "DisplayPos") == 0) return (int)offsetof(ImDrawData, DisplayPos);
        if (strcmp(field_name, "DisplaySize") == 0) return (int)offsetof(ImDrawData, DisplaySize);
        if (strcmp(field_name, "FramebufferScale") == 0) return (int)offsetof(ImDrawData, FramebufferScale);
        if (strcmp(field_name, "OwnerViewport") == 0) return (int)offsetof(ImDrawData, OwnerViewport);
        if (strcmp(field_name, "Textures") == 0) return (int)offsetof(ImDrawData, Textures);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawList") == 0)
    {
        if (strcmp(field_name, "CmdBuffer") == 0) return (int)offsetof(ImDrawList, CmdBuffer);
        if (strcmp(field_name, "IdxBuffer") == 0) return (int)offsetof(ImDrawList, IdxBuffer);
        if (strcmp(field_name, "VtxBuffer") == 0) return (int)offsetof(ImDrawList, VtxBuffer);
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImDrawList, Flags);
        if (strcmp(field_name, "_VtxCurrentIdx") == 0) return (int)offsetof(ImDrawList, _VtxCurrentIdx);
        if (strcmp(field_name, "_Data") == 0) return (int)offsetof(ImDrawList, _Data);
        if (strcmp(field_name, "_VtxWritePtr") == 0) return (int)offsetof(ImDrawList, _VtxWritePtr);
        if (strcmp(field_name, "_IdxWritePtr") == 0) return (int)offsetof(ImDrawList, _IdxWritePtr);
        if (strcmp(field_name, "_Path") == 0) return (int)offsetof(ImDrawList, _Path);
        if (strcmp(field_name, "_CmdHeader") == 0) return (int)offsetof(ImDrawList, _CmdHeader);
        if (strcmp(field_name, "_Splitter") == 0) return (int)offsetof(ImDrawList, _Splitter);
        if (strcmp(field_name, "_ClipRectStack") == 0) return (int)offsetof(ImDrawList, _ClipRectStack);
        if (strcmp(field_name, "_TextureStack") == 0) return (int)offsetof(ImDrawList, _TextureStack);
        if (strcmp(field_name, "_CallbacksDataBuf") == 0) return (int)offsetof(ImDrawList, _CallbacksDataBuf);
        if (strcmp(field_name, "_DrawFlagsStack") == 0) return (int)offsetof(ImDrawList, _DrawFlagsStack);
        if (strcmp(field_name, "_FringeScale") == 0) return (int)offsetof(ImDrawList, _FringeScale);
        if (strcmp(field_name, "_InvFringeScale") == 0) return (int)offsetof(ImDrawList, _InvFringeScale);
        if (strcmp(field_name, "_FringeScaleIsInteger") == 0) return (int)offsetof(ImDrawList, _FringeScaleIsInteger);
        if (strcmp(field_name, "_OwnerName") == 0) return (int)offsetof(ImDrawList, _OwnerName);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawListSplitter") == 0)
    {
        if (strcmp(field_name, "_Current") == 0) return (int)offsetof(ImDrawListSplitter, _Current);
        if (strcmp(field_name, "_Count") == 0) return (int)offsetof(ImDrawListSplitter, _Count);
        if (strcmp(field_name, "_Channels") == 0) return (int)offsetof(ImDrawListSplitter, _Channels);
        return -1;
    }
    if (strcmp(struct_name, "ImDrawVert") == 0)
    {
        if (strcmp(field_name, "pos") == 0) return (int)offsetof(ImDrawVert, pos);
        if (strcmp(field_name, "uv") == 0) return (int)offsetof(ImDrawVert, uv);
        if (strcmp(field_name, "col") == 0) return (int)offsetof(ImDrawVert, col);
        return -1;
    }
    if (strcmp(struct_name, "ImFont") == 0)
    {
        if (strcmp(field_name, "LastBaked") == 0) return (int)offsetof(ImFont, LastBaked);
        if (strcmp(field_name, "OwnerAtlas") == 0) return (int)offsetof(ImFont, OwnerAtlas);
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImFont, Flags);
        if (strcmp(field_name, "CurrentRasterizerDensity") == 0) return (int)offsetof(ImFont, CurrentRasterizerDensity);
        if (strcmp(field_name, "FontId") == 0) return (int)offsetof(ImFont, FontId);
        if (strcmp(field_name, "LegacySize") == 0) return (int)offsetof(ImFont, LegacySize);
        if (strcmp(field_name, "Sources") == 0) return (int)offsetof(ImFont, Sources);
        if (strcmp(field_name, "EllipsisChar") == 0) return (int)offsetof(ImFont, EllipsisChar);
        if (strcmp(field_name, "FallbackChar") == 0) return (int)offsetof(ImFont, FallbackChar);
        if (strcmp(field_name, "Used8kPagesMap") == 0) return (int)offsetof(ImFont, Used8kPagesMap);
        if (strcmp(field_name, "EllipsisAutoBake") == 0) return (int)offsetof(ImFont, EllipsisAutoBake);
        if (strcmp(field_name, "RemapPairs") == 0) return (int)offsetof(ImFont, RemapPairs);
        return -1;
    }
    if (strcmp(struct_name, "ImFontAtlas") == 0)
    {
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImFontAtlas, Flags);
        if (strcmp(field_name, "TexDesiredFormat") == 0) return (int)offsetof(ImFontAtlas, TexDesiredFormat);
        if (strcmp(field_name, "TexGlyphPadding") == 0) return (int)offsetof(ImFontAtlas, TexGlyphPadding);
        if (strcmp(field_name, "TexMinWidth") == 0) return (int)offsetof(ImFontAtlas, TexMinWidth);
        if (strcmp(field_name, "TexMinHeight") == 0) return (int)offsetof(ImFontAtlas, TexMinHeight);
        if (strcmp(field_name, "TexMaxWidth") == 0) return (int)offsetof(ImFontAtlas, TexMaxWidth);
        if (strcmp(field_name, "TexMaxHeight") == 0) return (int)offsetof(ImFontAtlas, TexMaxHeight);
        if (strcmp(field_name, "UserData") == 0) return (int)offsetof(ImFontAtlas, UserData);
        if (strcmp(field_name, "TexRef") == 0) return (int)offsetof(ImFontAtlas, TexRef);
        if (strcmp(field_name, "TexData") == 0) return (int)offsetof(ImFontAtlas, TexData);
        if (strcmp(field_name, "TexList") == 0) return (int)offsetof(ImFontAtlas, TexList);
        if (strcmp(field_name, "Locked") == 0) return (int)offsetof(ImFontAtlas, Locked);
        if (strcmp(field_name, "RendererHasTextures") == 0) return (int)offsetof(ImFontAtlas, RendererHasTextures);
        if (strcmp(field_name, "TexIsBuilt") == 0) return (int)offsetof(ImFontAtlas, TexIsBuilt);
        if (strcmp(field_name, "TexPixelsUseColors") == 0) return (int)offsetof(ImFontAtlas, TexPixelsUseColors);
        if (strcmp(field_name, "TexUvScale") == 0) return (int)offsetof(ImFontAtlas, TexUvScale);
        if (strcmp(field_name, "TexUvWhitePixel") == 0) return (int)offsetof(ImFontAtlas, TexUvWhitePixel);
        if (strcmp(field_name, "Fonts") == 0) return (int)offsetof(ImFontAtlas, Fonts);
        if (strcmp(field_name, "Sources") == 0) return (int)offsetof(ImFontAtlas, Sources);
        if (strcmp(field_name, "TexNextUniqueID") == 0) return (int)offsetof(ImFontAtlas, TexNextUniqueID);
        if (strcmp(field_name, "FontNextUniqueID") == 0) return (int)offsetof(ImFontAtlas, FontNextUniqueID);
        if (strcmp(field_name, "DrawListSharedDatas") == 0) return (int)offsetof(ImFontAtlas, DrawListSharedDatas);
        if (strcmp(field_name, "Builder") == 0) return (int)offsetof(ImFontAtlas, Builder);
        if (strcmp(field_name, "FontLoader") == 0) return (int)offsetof(ImFontAtlas, FontLoader);
        if (strcmp(field_name, "FontLoaderName") == 0) return (int)offsetof(ImFontAtlas, FontLoaderName);
        if (strcmp(field_name, "FontLoaderData") == 0) return (int)offsetof(ImFontAtlas, FontLoaderData);
        if (strcmp(field_name, "FontLoaderFlags") == 0) return (int)offsetof(ImFontAtlas, FontLoaderFlags);
        if (strcmp(field_name, "RefCount") == 0) return (int)offsetof(ImFontAtlas, RefCount);
        if (strcmp(field_name, "OwnerContext") == 0) return (int)offsetof(ImFontAtlas, OwnerContext);
        return -1;
    }
    if (strcmp(struct_name, "ImFontAtlasRect") == 0)
    {
        if (strcmp(field_name, "x") == 0) return (int)offsetof(ImFontAtlasRect, x);
        if (strcmp(field_name, "y") == 0) return (int)offsetof(ImFontAtlasRect, y);
        if (strcmp(field_name, "w") == 0) return (int)offsetof(ImFontAtlasRect, w);
        if (strcmp(field_name, "h") == 0) return (int)offsetof(ImFontAtlasRect, h);
        if (strcmp(field_name, "uv0") == 0) return (int)offsetof(ImFontAtlasRect, uv0);
        if (strcmp(field_name, "uv1") == 0) return (int)offsetof(ImFontAtlasRect, uv1);
        return -1;
    }
    if (strcmp(struct_name, "ImFontBaked") == 0)
    {
        if (strcmp(field_name, "IndexAdvanceX") == 0) return (int)offsetof(ImFontBaked, IndexAdvanceX);
        if (strcmp(field_name, "FallbackAdvanceX") == 0) return (int)offsetof(ImFontBaked, FallbackAdvanceX);
        if (strcmp(field_name, "Size") == 0) return (int)offsetof(ImFontBaked, Size);
        if (strcmp(field_name, "RasterizerDensity") == 0) return (int)offsetof(ImFontBaked, RasterizerDensity);
        if (strcmp(field_name, "IndexLookup") == 0) return (int)offsetof(ImFontBaked, IndexLookup);
        if (strcmp(field_name, "Glyphs") == 0) return (int)offsetof(ImFontBaked, Glyphs);
        if (strcmp(field_name, "FallbackGlyphIndex") == 0) return (int)offsetof(ImFontBaked, FallbackGlyphIndex);
        if (strcmp(field_name, "Ascent") == 0) return (int)offsetof(ImFontBaked, Ascent);
        if (strcmp(field_name, "Descent") == 0) return (int)offsetof(ImFontBaked, Descent);
        if (strcmp(field_name, "LastUsedFrame") == 0) return (int)offsetof(ImFontBaked, LastUsedFrame);
        if (strcmp(field_name, "BakedId") == 0) return (int)offsetof(ImFontBaked, BakedId);
        if (strcmp(field_name, "OwnerFont") == 0) return (int)offsetof(ImFontBaked, OwnerFont);
        if (strcmp(field_name, "FontLoaderDatas") == 0) return (int)offsetof(ImFontBaked, FontLoaderDatas);
        return -1;
    }
    if (strcmp(struct_name, "ImFontConfig") == 0)
    {
        if (strcmp(field_name, "Name") == 0) return (int)offsetof(ImFontConfig, Name);
        if (strcmp(field_name, "FontData") == 0) return (int)offsetof(ImFontConfig, FontData);
        if (strcmp(field_name, "FontDataSize") == 0) return (int)offsetof(ImFontConfig, FontDataSize);
        if (strcmp(field_name, "FontDataOwnedByAtlas") == 0) return (int)offsetof(ImFontConfig, FontDataOwnedByAtlas);
        if (strcmp(field_name, "MergeMode") == 0) return (int)offsetof(ImFontConfig, MergeMode);
        if (strcmp(field_name, "PixelSnapH") == 0) return (int)offsetof(ImFontConfig, PixelSnapH);
        if (strcmp(field_name, "OversampleH") == 0) return (int)offsetof(ImFontConfig, OversampleH);
        if (strcmp(field_name, "OversampleV") == 0) return (int)offsetof(ImFontConfig, OversampleV);
        if (strcmp(field_name, "EllipsisChar") == 0) return (int)offsetof(ImFontConfig, EllipsisChar);
        if (strcmp(field_name, "SizePixels") == 0) return (int)offsetof(ImFontConfig, SizePixels);
        if (strcmp(field_name, "GlyphRanges") == 0) return (int)offsetof(ImFontConfig, GlyphRanges);
        if (strcmp(field_name, "GlyphExcludeRanges") == 0) return (int)offsetof(ImFontConfig, GlyphExcludeRanges);
        if (strcmp(field_name, "GlyphOffset") == 0) return (int)offsetof(ImFontConfig, GlyphOffset);
        if (strcmp(field_name, "GlyphMinAdvanceX") == 0) return (int)offsetof(ImFontConfig, GlyphMinAdvanceX);
        if (strcmp(field_name, "GlyphMaxAdvanceX") == 0) return (int)offsetof(ImFontConfig, GlyphMaxAdvanceX);
        if (strcmp(field_name, "GlyphExtraAdvanceX") == 0) return (int)offsetof(ImFontConfig, GlyphExtraAdvanceX);
        if (strcmp(field_name, "FontNo") == 0) return (int)offsetof(ImFontConfig, FontNo);
        if (strcmp(field_name, "FontLoaderFlags") == 0) return (int)offsetof(ImFontConfig, FontLoaderFlags);
        if (strcmp(field_name, "RasterizerMultiply") == 0) return (int)offsetof(ImFontConfig, RasterizerMultiply);
        if (strcmp(field_name, "RasterizerDensity") == 0) return (int)offsetof(ImFontConfig, RasterizerDensity);
        if (strcmp(field_name, "ExtraSizeScale") == 0) return (int)offsetof(ImFontConfig, ExtraSizeScale);
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImFontConfig, Flags);
        if (strcmp(field_name, "DstFont") == 0) return (int)offsetof(ImFontConfig, DstFont);
        if (strcmp(field_name, "FontLoader") == 0) return (int)offsetof(ImFontConfig, FontLoader);
        if (strcmp(field_name, "FontLoaderData") == 0) return (int)offsetof(ImFontConfig, FontLoaderData);
        return -1;
    }
    if (strcmp(struct_name, "ImFontGlyph") == 0)
    {
        if (strcmp(field_name, "AdvanceX") == 0) return (int)offsetof(ImFontGlyph, AdvanceX);
        if (strcmp(field_name, "X0") == 0) return (int)offsetof(ImFontGlyph, X0);
        if (strcmp(field_name, "Y0") == 0) return (int)offsetof(ImFontGlyph, Y0);
        if (strcmp(field_name, "X1") == 0) return (int)offsetof(ImFontGlyph, X1);
        if (strcmp(field_name, "Y1") == 0) return (int)offsetof(ImFontGlyph, Y1);
        if (strcmp(field_name, "U0") == 0) return (int)offsetof(ImFontGlyph, U0);
        if (strcmp(field_name, "V0") == 0) return (int)offsetof(ImFontGlyph, V0);
        if (strcmp(field_name, "U1") == 0) return (int)offsetof(ImFontGlyph, U1);
        if (strcmp(field_name, "V1") == 0) return (int)offsetof(ImFontGlyph, V1);
        if (strcmp(field_name, "PackId") == 0) return (int)offsetof(ImFontGlyph, PackId);
        return -1;
    }
    if (strcmp(struct_name, "ImFontGlyphRangesBuilder") == 0)
    {
        if (strcmp(field_name, "UsedChars") == 0) return (int)offsetof(ImFontGlyphRangesBuilder, UsedChars);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiIO") == 0)
    {
        if (strcmp(field_name, "ConfigFlags") == 0) return (int)offsetof(ImGuiIO, ConfigFlags);
        if (strcmp(field_name, "BackendFlags") == 0) return (int)offsetof(ImGuiIO, BackendFlags);
        if (strcmp(field_name, "DisplaySize") == 0) return (int)offsetof(ImGuiIO, DisplaySize);
        if (strcmp(field_name, "DisplayFramebufferScale") == 0) return (int)offsetof(ImGuiIO, DisplayFramebufferScale);
        if (strcmp(field_name, "DeltaTime") == 0) return (int)offsetof(ImGuiIO, DeltaTime);
        if (strcmp(field_name, "IniSavingRate") == 0) return (int)offsetof(ImGuiIO, IniSavingRate);
        if (strcmp(field_name, "IniFilename") == 0) return (int)offsetof(ImGuiIO, IniFilename);
        if (strcmp(field_name, "LogFilename") == 0) return (int)offsetof(ImGuiIO, LogFilename);
        if (strcmp(field_name, "UserData") == 0) return (int)offsetof(ImGuiIO, UserData);
        if (strcmp(field_name, "Fonts") == 0) return (int)offsetof(ImGuiIO, Fonts);
        if (strcmp(field_name, "FontDefault") == 0) return (int)offsetof(ImGuiIO, FontDefault);
        if (strcmp(field_name, "FontAllowUserScaling") == 0) return (int)offsetof(ImGuiIO, FontAllowUserScaling);
        if (strcmp(field_name, "ConfigNavSwapGamepadButtons") == 0) return (int)offsetof(ImGuiIO, ConfigNavSwapGamepadButtons);
        if (strcmp(field_name, "ConfigNavMoveSetMousePos") == 0) return (int)offsetof(ImGuiIO, ConfigNavMoveSetMousePos);
        if (strcmp(field_name, "ConfigNavCaptureKeyboard") == 0) return (int)offsetof(ImGuiIO, ConfigNavCaptureKeyboard);
        if (strcmp(field_name, "ConfigNavEscapeClearFocusItem") == 0) return (int)offsetof(ImGuiIO, ConfigNavEscapeClearFocusItem);
        if (strcmp(field_name, "ConfigNavEscapeClearFocusWindow") == 0) return (int)offsetof(ImGuiIO, ConfigNavEscapeClearFocusWindow);
        if (strcmp(field_name, "ConfigNavCursorVisibleAuto") == 0) return (int)offsetof(ImGuiIO, ConfigNavCursorVisibleAuto);
        if (strcmp(field_name, "ConfigNavCursorVisibleAlways") == 0) return (int)offsetof(ImGuiIO, ConfigNavCursorVisibleAlways);
        if (strcmp(field_name, "ConfigDockingNoSplit") == 0) return (int)offsetof(ImGuiIO, ConfigDockingNoSplit);
        if (strcmp(field_name, "ConfigDockingNoDockingOver") == 0) return (int)offsetof(ImGuiIO, ConfigDockingNoDockingOver);
        if (strcmp(field_name, "ConfigDockingWithShift") == 0) return (int)offsetof(ImGuiIO, ConfigDockingWithShift);
        if (strcmp(field_name, "ConfigDockingAlwaysTabBar") == 0) return (int)offsetof(ImGuiIO, ConfigDockingAlwaysTabBar);
        if (strcmp(field_name, "ConfigDockingTransparentPayload") == 0) return (int)offsetof(ImGuiIO, ConfigDockingTransparentPayload);
        if (strcmp(field_name, "ConfigViewportsNoAutoMerge") == 0) return (int)offsetof(ImGuiIO, ConfigViewportsNoAutoMerge);
        if (strcmp(field_name, "ConfigViewportsNoTaskBarIcon") == 0) return (int)offsetof(ImGuiIO, ConfigViewportsNoTaskBarIcon);
        if (strcmp(field_name, "ConfigViewportsNoDecoration") == 0) return (int)offsetof(ImGuiIO, ConfigViewportsNoDecoration);
        if (strcmp(field_name, "ConfigViewportsNoDefaultParent") == 0) return (int)offsetof(ImGuiIO, ConfigViewportsNoDefaultParent);
        if (strcmp(field_name, "ConfigViewportsPlatformFocusSetsImGuiFocus") == 0) return (int)offsetof(ImGuiIO, ConfigViewportsPlatformFocusSetsImGuiFocus);
        if (strcmp(field_name, "ConfigDpiScaleFonts") == 0) return (int)offsetof(ImGuiIO, ConfigDpiScaleFonts);
        if (strcmp(field_name, "ConfigDpiScaleViewports") == 0) return (int)offsetof(ImGuiIO, ConfigDpiScaleViewports);
        if (strcmp(field_name, "ConfigMacOSXBehaviors") == 0) return (int)offsetof(ImGuiIO, ConfigMacOSXBehaviors);
        if (strcmp(field_name, "ConfigInputTrickleEventQueue") == 0) return (int)offsetof(ImGuiIO, ConfigInputTrickleEventQueue);
        if (strcmp(field_name, "ConfigInputTextCursorBlink") == 0) return (int)offsetof(ImGuiIO, ConfigInputTextCursorBlink);
        if (strcmp(field_name, "ConfigInputTextEnterKeepActive") == 0) return (int)offsetof(ImGuiIO, ConfigInputTextEnterKeepActive);
        if (strcmp(field_name, "ConfigColorEditFlags") == 0) return (int)offsetof(ImGuiIO, ConfigColorEditFlags);
        if (strcmp(field_name, "ConfigDragClickToInputText") == 0) return (int)offsetof(ImGuiIO, ConfigDragClickToInputText);
        if (strcmp(field_name, "ConfigWindowsResizeFromEdges") == 0) return (int)offsetof(ImGuiIO, ConfigWindowsResizeFromEdges);
        if (strcmp(field_name, "ConfigWindowsMoveFromTitleBarOnly") == 0) return (int)offsetof(ImGuiIO, ConfigWindowsMoveFromTitleBarOnly);
        if (strcmp(field_name, "ConfigWindowsCopyContentsWithCtrlC") == 0) return (int)offsetof(ImGuiIO, ConfigWindowsCopyContentsWithCtrlC);
        if (strcmp(field_name, "ConfigScrollbarScrollByPage") == 0) return (int)offsetof(ImGuiIO, ConfigScrollbarScrollByPage);
        if (strcmp(field_name, "ConfigIniSettingsSaveLastUsedDate") == 0) return (int)offsetof(ImGuiIO, ConfigIniSettingsSaveLastUsedDate);
        if (strcmp(field_name, "ConfigIniSettingsAutoDiscardMonths") == 0) return (int)offsetof(ImGuiIO, ConfigIniSettingsAutoDiscardMonths);
        if (strcmp(field_name, "ConfigDebugIniSettings") == 0) return (int)offsetof(ImGuiIO, ConfigDebugIniSettings);
        if (strcmp(field_name, "MouseDrawCursor") == 0) return (int)offsetof(ImGuiIO, MouseDrawCursor);
        if (strcmp(field_name, "ConfigMemoryCompactTimer") == 0) return (int)offsetof(ImGuiIO, ConfigMemoryCompactTimer);
        if (strcmp(field_name, "MouseDoubleClickTime") == 0) return (int)offsetof(ImGuiIO, MouseDoubleClickTime);
        if (strcmp(field_name, "MouseDoubleClickMaxDist") == 0) return (int)offsetof(ImGuiIO, MouseDoubleClickMaxDist);
        if (strcmp(field_name, "MouseSingleClickDelay") == 0) return (int)offsetof(ImGuiIO, MouseSingleClickDelay);
        if (strcmp(field_name, "MouseDragThreshold") == 0) return (int)offsetof(ImGuiIO, MouseDragThreshold);
        if (strcmp(field_name, "KeyRepeatDelay") == 0) return (int)offsetof(ImGuiIO, KeyRepeatDelay);
        if (strcmp(field_name, "KeyRepeatRate") == 0) return (int)offsetof(ImGuiIO, KeyRepeatRate);
        if (strcmp(field_name, "ConfigErrorRecovery") == 0) return (int)offsetof(ImGuiIO, ConfigErrorRecovery);
        if (strcmp(field_name, "ConfigErrorRecoveryEnableAssert") == 0) return (int)offsetof(ImGuiIO, ConfigErrorRecoveryEnableAssert);
        if (strcmp(field_name, "ConfigErrorRecoveryEnableDebugLog") == 0) return (int)offsetof(ImGuiIO, ConfigErrorRecoveryEnableDebugLog);
        if (strcmp(field_name, "ConfigErrorRecoveryEnableTooltip") == 0) return (int)offsetof(ImGuiIO, ConfigErrorRecoveryEnableTooltip);
        if (strcmp(field_name, "ConfigDebugIsDebuggerPresent") == 0) return (int)offsetof(ImGuiIO, ConfigDebugIsDebuggerPresent);
        if (strcmp(field_name, "ConfigDebugHighlightIdConflicts") == 0) return (int)offsetof(ImGuiIO, ConfigDebugHighlightIdConflicts);
        if (strcmp(field_name, "ConfigDebugHighlightIdConflictsShowItemPicker") == 0) return (int)offsetof(ImGuiIO, ConfigDebugHighlightIdConflictsShowItemPicker);
        if (strcmp(field_name, "ConfigDebugBeginReturnValueOnce") == 0) return (int)offsetof(ImGuiIO, ConfigDebugBeginReturnValueOnce);
        if (strcmp(field_name, "ConfigDebugBeginReturnValueLoop") == 0) return (int)offsetof(ImGuiIO, ConfigDebugBeginReturnValueLoop);
        if (strcmp(field_name, "ConfigDebugIgnoreFocusLoss") == 0) return (int)offsetof(ImGuiIO, ConfigDebugIgnoreFocusLoss);
        if (strcmp(field_name, "ConfigDebugDrawListDefaultsToStrokeLegacy") == 0) return (int)offsetof(ImGuiIO, ConfigDebugDrawListDefaultsToStrokeLegacy);
        if (strcmp(field_name, "BackendPlatformName") == 0) return (int)offsetof(ImGuiIO, BackendPlatformName);
        if (strcmp(field_name, "BackendRendererName") == 0) return (int)offsetof(ImGuiIO, BackendRendererName);
        if (strcmp(field_name, "BackendPlatformUserData") == 0) return (int)offsetof(ImGuiIO, BackendPlatformUserData);
        if (strcmp(field_name, "BackendRendererUserData") == 0) return (int)offsetof(ImGuiIO, BackendRendererUserData);
        if (strcmp(field_name, "BackendLanguageUserData") == 0) return (int)offsetof(ImGuiIO, BackendLanguageUserData);
        if (strcmp(field_name, "WantCaptureMouse") == 0) return (int)offsetof(ImGuiIO, WantCaptureMouse);
        if (strcmp(field_name, "WantCaptureKeyboard") == 0) return (int)offsetof(ImGuiIO, WantCaptureKeyboard);
        if (strcmp(field_name, "WantTextInput") == 0) return (int)offsetof(ImGuiIO, WantTextInput);
        if (strcmp(field_name, "WantSetMousePos") == 0) return (int)offsetof(ImGuiIO, WantSetMousePos);
        if (strcmp(field_name, "WantSaveIniSettings") == 0) return (int)offsetof(ImGuiIO, WantSaveIniSettings);
        if (strcmp(field_name, "NavActive") == 0) return (int)offsetof(ImGuiIO, NavActive);
        if (strcmp(field_name, "NavVisible") == 0) return (int)offsetof(ImGuiIO, NavVisible);
        if (strcmp(field_name, "Framerate") == 0) return (int)offsetof(ImGuiIO, Framerate);
        if (strcmp(field_name, "MetricsRenderVertices") == 0) return (int)offsetof(ImGuiIO, MetricsRenderVertices);
        if (strcmp(field_name, "MetricsRenderIndices") == 0) return (int)offsetof(ImGuiIO, MetricsRenderIndices);
        if (strcmp(field_name, "MetricsRenderWindows") == 0) return (int)offsetof(ImGuiIO, MetricsRenderWindows);
        if (strcmp(field_name, "MetricsActiveWindows") == 0) return (int)offsetof(ImGuiIO, MetricsActiveWindows);
        if (strcmp(field_name, "MouseDelta") == 0) return (int)offsetof(ImGuiIO, MouseDelta);
        if (strcmp(field_name, "Ctx") == 0) return (int)offsetof(ImGuiIO, Ctx);
        if (strcmp(field_name, "MousePos") == 0) return (int)offsetof(ImGuiIO, MousePos);
        if (strcmp(field_name, "MouseDown") == 0) return (int)offsetof(ImGuiIO, MouseDown);
        if (strcmp(field_name, "MouseWheel") == 0) return (int)offsetof(ImGuiIO, MouseWheel);
        if (strcmp(field_name, "MouseWheelH") == 0) return (int)offsetof(ImGuiIO, MouseWheelH);
        if (strcmp(field_name, "MouseSource") == 0) return (int)offsetof(ImGuiIO, MouseSource);
        if (strcmp(field_name, "MouseHoveredViewport") == 0) return (int)offsetof(ImGuiIO, MouseHoveredViewport);
        if (strcmp(field_name, "KeyCtrl") == 0) return (int)offsetof(ImGuiIO, KeyCtrl);
        if (strcmp(field_name, "KeyShift") == 0) return (int)offsetof(ImGuiIO, KeyShift);
        if (strcmp(field_name, "KeyAlt") == 0) return (int)offsetof(ImGuiIO, KeyAlt);
        if (strcmp(field_name, "KeySuper") == 0) return (int)offsetof(ImGuiIO, KeySuper);
        if (strcmp(field_name, "KeyMods") == 0) return (int)offsetof(ImGuiIO, KeyMods);
        if (strcmp(field_name, "KeysData") == 0) return (int)offsetof(ImGuiIO, KeysData);
        if (strcmp(field_name, "WantCaptureMouseUnlessPopupClose") == 0) return (int)offsetof(ImGuiIO, WantCaptureMouseUnlessPopupClose);
        if (strcmp(field_name, "MousePosPrev") == 0) return (int)offsetof(ImGuiIO, MousePosPrev);
        if (strcmp(field_name, "MouseClickedPos") == 0) return (int)offsetof(ImGuiIO, MouseClickedPos);
        if (strcmp(field_name, "MouseClickedTime") == 0) return (int)offsetof(ImGuiIO, MouseClickedTime);
        if (strcmp(field_name, "MouseClicked") == 0) return (int)offsetof(ImGuiIO, MouseClicked);
        if (strcmp(field_name, "MouseDoubleClicked") == 0) return (int)offsetof(ImGuiIO, MouseDoubleClicked);
        if (strcmp(field_name, "MouseClickedCount") == 0) return (int)offsetof(ImGuiIO, MouseClickedCount);
        if (strcmp(field_name, "MouseClickedLastCount") == 0) return (int)offsetof(ImGuiIO, MouseClickedLastCount);
        if (strcmp(field_name, "MouseReleased") == 0) return (int)offsetof(ImGuiIO, MouseReleased);
        if (strcmp(field_name, "MouseReleasedTime") == 0) return (int)offsetof(ImGuiIO, MouseReleasedTime);
        if (strcmp(field_name, "MouseDownOwned") == 0) return (int)offsetof(ImGuiIO, MouseDownOwned);
        if (strcmp(field_name, "MouseDownOwnedUnlessPopupClose") == 0) return (int)offsetof(ImGuiIO, MouseDownOwnedUnlessPopupClose);
        if (strcmp(field_name, "MouseWheelRequestAxisSwap") == 0) return (int)offsetof(ImGuiIO, MouseWheelRequestAxisSwap);
        if (strcmp(field_name, "MouseCtrlLeftAsRightClick") == 0) return (int)offsetof(ImGuiIO, MouseCtrlLeftAsRightClick);
        if (strcmp(field_name, "MouseDownDuration") == 0) return (int)offsetof(ImGuiIO, MouseDownDuration);
        if (strcmp(field_name, "MouseDownDurationPrev") == 0) return (int)offsetof(ImGuiIO, MouseDownDurationPrev);
        if (strcmp(field_name, "MouseDragMaxDistanceAbs") == 0) return (int)offsetof(ImGuiIO, MouseDragMaxDistanceAbs);
        if (strcmp(field_name, "MouseDragMaxDistanceSqr") == 0) return (int)offsetof(ImGuiIO, MouseDragMaxDistanceSqr);
        if (strcmp(field_name, "PenPressure") == 0) return (int)offsetof(ImGuiIO, PenPressure);
        if (strcmp(field_name, "AppFocusLost") == 0) return (int)offsetof(ImGuiIO, AppFocusLost);
        if (strcmp(field_name, "AppAcceptingEvents") == 0) return (int)offsetof(ImGuiIO, AppAcceptingEvents);
        if (strcmp(field_name, "InputQueueSurrogate") == 0) return (int)offsetof(ImGuiIO, InputQueueSurrogate);
        if (strcmp(field_name, "InputQueueCharacters") == 0) return (int)offsetof(ImGuiIO, InputQueueCharacters);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiInputTextCallbackData") == 0)
    {
        if (strcmp(field_name, "Ctx") == 0) return (int)offsetof(ImGuiInputTextCallbackData, Ctx);
        if (strcmp(field_name, "EventFlag") == 0) return (int)offsetof(ImGuiInputTextCallbackData, EventFlag);
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImGuiInputTextCallbackData, Flags);
        if (strcmp(field_name, "UserData") == 0) return (int)offsetof(ImGuiInputTextCallbackData, UserData);
        if (strcmp(field_name, "ID") == 0) return (int)offsetof(ImGuiInputTextCallbackData, ID);
        if (strcmp(field_name, "EventKey") == 0) return (int)offsetof(ImGuiInputTextCallbackData, EventKey);
        if (strcmp(field_name, "EventChar") == 0) return (int)offsetof(ImGuiInputTextCallbackData, EventChar);
        if (strcmp(field_name, "EventActivated") == 0) return (int)offsetof(ImGuiInputTextCallbackData, EventActivated);
        if (strcmp(field_name, "BufDirty") == 0) return (int)offsetof(ImGuiInputTextCallbackData, BufDirty);
        if (strcmp(field_name, "Buf") == 0) return (int)offsetof(ImGuiInputTextCallbackData, Buf);
        if (strcmp(field_name, "BufTextLen") == 0) return (int)offsetof(ImGuiInputTextCallbackData, BufTextLen);
        if (strcmp(field_name, "BufSize") == 0) return (int)offsetof(ImGuiInputTextCallbackData, BufSize);
        if (strcmp(field_name, "CursorPos") == 0) return (int)offsetof(ImGuiInputTextCallbackData, CursorPos);
        if (strcmp(field_name, "SelectionStart") == 0) return (int)offsetof(ImGuiInputTextCallbackData, SelectionStart);
        if (strcmp(field_name, "SelectionEnd") == 0) return (int)offsetof(ImGuiInputTextCallbackData, SelectionEnd);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiKeyData") == 0)
    {
        if (strcmp(field_name, "Down") == 0) return (int)offsetof(ImGuiKeyData, Down);
        if (strcmp(field_name, "DownDuration") == 0) return (int)offsetof(ImGuiKeyData, DownDuration);
        if (strcmp(field_name, "DownDurationPrev") == 0) return (int)offsetof(ImGuiKeyData, DownDurationPrev);
        if (strcmp(field_name, "AnalogValue") == 0) return (int)offsetof(ImGuiKeyData, AnalogValue);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiListClipper") == 0)
    {
        if (strcmp(field_name, "DisplayStart") == 0) return (int)offsetof(ImGuiListClipper, DisplayStart);
        if (strcmp(field_name, "DisplayEnd") == 0) return (int)offsetof(ImGuiListClipper, DisplayEnd);
        if (strcmp(field_name, "UserIndex") == 0) return (int)offsetof(ImGuiListClipper, UserIndex);
        if (strcmp(field_name, "ItemsCount") == 0) return (int)offsetof(ImGuiListClipper, ItemsCount);
        if (strcmp(field_name, "ItemsHeight") == 0) return (int)offsetof(ImGuiListClipper, ItemsHeight);
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImGuiListClipper, Flags);
        if (strcmp(field_name, "StartPosY") == 0) return (int)offsetof(ImGuiListClipper, StartPosY);
        if (strcmp(field_name, "StartSeekOffsetY") == 0) return (int)offsetof(ImGuiListClipper, StartSeekOffsetY);
        if (strcmp(field_name, "Ctx") == 0) return (int)offsetof(ImGuiListClipper, Ctx);
        if (strcmp(field_name, "TempData") == 0) return (int)offsetof(ImGuiListClipper, TempData);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiMultiSelectIO") == 0)
    {
        if (strcmp(field_name, "Requests") == 0) return (int)offsetof(ImGuiMultiSelectIO, Requests);
        if (strcmp(field_name, "RangeSrcItem") == 0) return (int)offsetof(ImGuiMultiSelectIO, RangeSrcItem);
        if (strcmp(field_name, "NavIdItem") == 0) return (int)offsetof(ImGuiMultiSelectIO, NavIdItem);
        if (strcmp(field_name, "NavIdSelected") == 0) return (int)offsetof(ImGuiMultiSelectIO, NavIdSelected);
        if (strcmp(field_name, "RangeSrcReset") == 0) return (int)offsetof(ImGuiMultiSelectIO, RangeSrcReset);
        if (strcmp(field_name, "ItemsCount") == 0) return (int)offsetof(ImGuiMultiSelectIO, ItemsCount);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiOnceUponAFrame") == 0)
    {
        if (strcmp(field_name, "RefFrame") == 0) return (int)offsetof(ImGuiOnceUponAFrame, RefFrame);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiPayload") == 0)
    {
        if (strcmp(field_name, "Data") == 0) return (int)offsetof(ImGuiPayload, Data);
        if (strcmp(field_name, "DataSize") == 0) return (int)offsetof(ImGuiPayload, DataSize);
        if (strcmp(field_name, "SourceId") == 0) return (int)offsetof(ImGuiPayload, SourceId);
        if (strcmp(field_name, "SourceParentId") == 0) return (int)offsetof(ImGuiPayload, SourceParentId);
        if (strcmp(field_name, "DataFrameCount") == 0) return (int)offsetof(ImGuiPayload, DataFrameCount);
        if (strcmp(field_name, "DataType") == 0) return (int)offsetof(ImGuiPayload, DataType);
        if (strcmp(field_name, "Preview") == 0) return (int)offsetof(ImGuiPayload, Preview);
        if (strcmp(field_name, "Delivery") == 0) return (int)offsetof(ImGuiPayload, Delivery);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiPlatformIO") == 0)
    {
        if (strcmp(field_name, "Platform_GetClipboardTextFn") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetClipboardTextFn);
        if (strcmp(field_name, "Platform_SetClipboardTextFn") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetClipboardTextFn);
        if (strcmp(field_name, "Platform_ClipboardUserData") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_ClipboardUserData);
        if (strcmp(field_name, "Platform_OpenInShellFn") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_OpenInShellFn);
        if (strcmp(field_name, "Platform_OpenInShellUserData") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_OpenInShellUserData);
        if (strcmp(field_name, "Platform_SetImeDataFn") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetImeDataFn);
        if (strcmp(field_name, "Platform_ImeUserData") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_ImeUserData);
        if (strcmp(field_name, "Platform_LocaleDecimalPoint") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_LocaleDecimalPoint);
        if (strcmp(field_name, "Platform_SessionDate") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SessionDate);
        if (strcmp(field_name, "Renderer_TextureMaxWidth") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_TextureMaxWidth);
        if (strcmp(field_name, "Renderer_TextureMaxHeight") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_TextureMaxHeight);
        if (strcmp(field_name, "Renderer_RenderState") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_RenderState);
        if (strcmp(field_name, "DrawCallback_ResetRenderState") == 0) return (int)offsetof(ImGuiPlatformIO, DrawCallback_ResetRenderState);
        if (strcmp(field_name, "DrawCallback_SetSamplerLinear") == 0) return (int)offsetof(ImGuiPlatformIO, DrawCallback_SetSamplerLinear);
        if (strcmp(field_name, "DrawCallback_SetSamplerNearest") == 0) return (int)offsetof(ImGuiPlatformIO, DrawCallback_SetSamplerNearest);
        if (strcmp(field_name, "DrawCallback_SetSamplerFromTex") == 0) return (int)offsetof(ImGuiPlatformIO, DrawCallback_SetSamplerFromTex);
        if (strcmp(field_name, "Platform_CreateWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_CreateWindow);
        if (strcmp(field_name, "Platform_DestroyWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_DestroyWindow);
        if (strcmp(field_name, "Platform_ShowWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_ShowWindow);
        if (strcmp(field_name, "Platform_SetWindowPos") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetWindowPos);
        if (strcmp(field_name, "Platform_GetWindowPos") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowPos);
        if (strcmp(field_name, "Platform_SetWindowSize") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetWindowSize);
        if (strcmp(field_name, "Platform_GetWindowSize") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowSize);
        if (strcmp(field_name, "Platform_GetWindowFramebufferScale") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowFramebufferScale);
        if (strcmp(field_name, "Platform_SetWindowFocus") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetWindowFocus);
        if (strcmp(field_name, "Platform_GetWindowFocus") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowFocus);
        if (strcmp(field_name, "Platform_GetWindowMinimized") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowMinimized);
        if (strcmp(field_name, "Platform_SetWindowTitle") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetWindowTitle);
        if (strcmp(field_name, "Platform_SetWindowAlpha") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SetWindowAlpha);
        if (strcmp(field_name, "Platform_UpdateWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_UpdateWindow);
        if (strcmp(field_name, "Platform_RenderWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_RenderWindow);
        if (strcmp(field_name, "Platform_SwapBuffers") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_SwapBuffers);
        if (strcmp(field_name, "Platform_GetWindowDpiScale") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowDpiScale);
        if (strcmp(field_name, "Platform_OnChangedViewport") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_OnChangedViewport);
        if (strcmp(field_name, "Platform_GetWindowWorkAreaInsets") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_GetWindowWorkAreaInsets);
        if (strcmp(field_name, "Platform_CreateVkSurface") == 0) return (int)offsetof(ImGuiPlatformIO, Platform_CreateVkSurface);
        if (strcmp(field_name, "Renderer_CreateWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_CreateWindow);
        if (strcmp(field_name, "Renderer_DestroyWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_DestroyWindow);
        if (strcmp(field_name, "Renderer_SetWindowSize") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_SetWindowSize);
        if (strcmp(field_name, "Renderer_RenderWindow") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_RenderWindow);
        if (strcmp(field_name, "Renderer_SwapBuffers") == 0) return (int)offsetof(ImGuiPlatformIO, Renderer_SwapBuffers);
        if (strcmp(field_name, "Monitors") == 0) return (int)offsetof(ImGuiPlatformIO, Monitors);
        if (strcmp(field_name, "Textures") == 0) return (int)offsetof(ImGuiPlatformIO, Textures);
        if (strcmp(field_name, "Viewports") == 0) return (int)offsetof(ImGuiPlatformIO, Viewports);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiPlatformImeData") == 0)
    {
        if (strcmp(field_name, "WantVisible") == 0) return (int)offsetof(ImGuiPlatformImeData, WantVisible);
        if (strcmp(field_name, "WantTextInput") == 0) return (int)offsetof(ImGuiPlatformImeData, WantTextInput);
        if (strcmp(field_name, "InputPos") == 0) return (int)offsetof(ImGuiPlatformImeData, InputPos);
        if (strcmp(field_name, "InputLineHeight") == 0) return (int)offsetof(ImGuiPlatformImeData, InputLineHeight);
        if (strcmp(field_name, "ViewportId") == 0) return (int)offsetof(ImGuiPlatformImeData, ViewportId);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiPlatformMonitor") == 0)
    {
        if (strcmp(field_name, "MainPos") == 0) return (int)offsetof(ImGuiPlatformMonitor, MainPos);
        if (strcmp(field_name, "MainSize") == 0) return (int)offsetof(ImGuiPlatformMonitor, MainSize);
        if (strcmp(field_name, "WorkPos") == 0) return (int)offsetof(ImGuiPlatformMonitor, WorkPos);
        if (strcmp(field_name, "WorkSize") == 0) return (int)offsetof(ImGuiPlatformMonitor, WorkSize);
        if (strcmp(field_name, "DpiScale") == 0) return (int)offsetof(ImGuiPlatformMonitor, DpiScale);
        if (strcmp(field_name, "PlatformHandle") == 0) return (int)offsetof(ImGuiPlatformMonitor, PlatformHandle);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiSelectionBasicStorage") == 0)
    {
        if (strcmp(field_name, "Size") == 0) return (int)offsetof(ImGuiSelectionBasicStorage, Size);
        if (strcmp(field_name, "PreserveOrder") == 0) return (int)offsetof(ImGuiSelectionBasicStorage, PreserveOrder);
        if (strcmp(field_name, "UserData") == 0) return (int)offsetof(ImGuiSelectionBasicStorage, UserData);
        if (strcmp(field_name, "AdapterIndexToStorageId") == 0) return (int)offsetof(ImGuiSelectionBasicStorage, AdapterIndexToStorageId);
        if (strcmp(field_name, "_SelectionOrder") == 0) return (int)offsetof(ImGuiSelectionBasicStorage, _SelectionOrder);
        if (strcmp(field_name, "_Storage") == 0) return (int)offsetof(ImGuiSelectionBasicStorage, _Storage);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiSelectionExternalStorage") == 0)
    {
        if (strcmp(field_name, "UserData") == 0) return (int)offsetof(ImGuiSelectionExternalStorage, UserData);
        if (strcmp(field_name, "AdapterSetItemSelected") == 0) return (int)offsetof(ImGuiSelectionExternalStorage, AdapterSetItemSelected);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiSelectionRequest") == 0)
    {
        if (strcmp(field_name, "Type") == 0) return (int)offsetof(ImGuiSelectionRequest, Type);
        if (strcmp(field_name, "Selected") == 0) return (int)offsetof(ImGuiSelectionRequest, Selected);
        if (strcmp(field_name, "RangeDirection") == 0) return (int)offsetof(ImGuiSelectionRequest, RangeDirection);
        if (strcmp(field_name, "RangeFirstItem") == 0) return (int)offsetof(ImGuiSelectionRequest, RangeFirstItem);
        if (strcmp(field_name, "RangeLastItem") == 0) return (int)offsetof(ImGuiSelectionRequest, RangeLastItem);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiSizeCallbackData") == 0)
    {
        if (strcmp(field_name, "UserData") == 0) return (int)offsetof(ImGuiSizeCallbackData, UserData);
        if (strcmp(field_name, "Pos") == 0) return (int)offsetof(ImGuiSizeCallbackData, Pos);
        if (strcmp(field_name, "CurrentSize") == 0) return (int)offsetof(ImGuiSizeCallbackData, CurrentSize);
        if (strcmp(field_name, "DesiredSize") == 0) return (int)offsetof(ImGuiSizeCallbackData, DesiredSize);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiStorage") == 0)
    {
        if (strcmp(field_name, "Data") == 0) return (int)offsetof(ImGuiStorage, Data);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiStoragePair") == 0)
    {
        if (strcmp(field_name, "key") == 0) return (int)offsetof(ImGuiStoragePair, key);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiStyle") == 0)
    {
        if (strcmp(field_name, "FontSizeBase") == 0) return (int)offsetof(ImGuiStyle, FontSizeBase);
        if (strcmp(field_name, "FontScaleMain") == 0) return (int)offsetof(ImGuiStyle, FontScaleMain);
        if (strcmp(field_name, "FontScaleDpi") == 0) return (int)offsetof(ImGuiStyle, FontScaleDpi);
        if (strcmp(field_name, "Alpha") == 0) return (int)offsetof(ImGuiStyle, Alpha);
        if (strcmp(field_name, "DisabledAlpha") == 0) return (int)offsetof(ImGuiStyle, DisabledAlpha);
        if (strcmp(field_name, "WindowPadding") == 0) return (int)offsetof(ImGuiStyle, WindowPadding);
        if (strcmp(field_name, "WindowRounding") == 0) return (int)offsetof(ImGuiStyle, WindowRounding);
        if (strcmp(field_name, "WindowBorderSize") == 0) return (int)offsetof(ImGuiStyle, WindowBorderSize);
        if (strcmp(field_name, "WindowBorderHoverPadding") == 0) return (int)offsetof(ImGuiStyle, WindowBorderHoverPadding);
        if (strcmp(field_name, "WindowMinSize") == 0) return (int)offsetof(ImGuiStyle, WindowMinSize);
        if (strcmp(field_name, "WindowTitleAlign") == 0) return (int)offsetof(ImGuiStyle, WindowTitleAlign);
        if (strcmp(field_name, "WindowMenuButtonPosition") == 0) return (int)offsetof(ImGuiStyle, WindowMenuButtonPosition);
        if (strcmp(field_name, "ChildRounding") == 0) return (int)offsetof(ImGuiStyle, ChildRounding);
        if (strcmp(field_name, "ChildBorderSize") == 0) return (int)offsetof(ImGuiStyle, ChildBorderSize);
        if (strcmp(field_name, "PopupRounding") == 0) return (int)offsetof(ImGuiStyle, PopupRounding);
        if (strcmp(field_name, "PopupBorderSize") == 0) return (int)offsetof(ImGuiStyle, PopupBorderSize);
        if (strcmp(field_name, "FramePadding") == 0) return (int)offsetof(ImGuiStyle, FramePadding);
        if (strcmp(field_name, "FrameRounding") == 0) return (int)offsetof(ImGuiStyle, FrameRounding);
        if (strcmp(field_name, "FrameBorderSize") == 0) return (int)offsetof(ImGuiStyle, FrameBorderSize);
        if (strcmp(field_name, "ItemSpacing") == 0) return (int)offsetof(ImGuiStyle, ItemSpacing);
        if (strcmp(field_name, "ItemInnerSpacing") == 0) return (int)offsetof(ImGuiStyle, ItemInnerSpacing);
        if (strcmp(field_name, "CellPadding") == 0) return (int)offsetof(ImGuiStyle, CellPadding);
        if (strcmp(field_name, "TouchExtraPadding") == 0) return (int)offsetof(ImGuiStyle, TouchExtraPadding);
        if (strcmp(field_name, "IndentSpacing") == 0) return (int)offsetof(ImGuiStyle, IndentSpacing);
        if (strcmp(field_name, "ColumnsMinSpacing") == 0) return (int)offsetof(ImGuiStyle, ColumnsMinSpacing);
        if (strcmp(field_name, "ScrollbarSize") == 0) return (int)offsetof(ImGuiStyle, ScrollbarSize);
        if (strcmp(field_name, "ScrollbarRounding") == 0) return (int)offsetof(ImGuiStyle, ScrollbarRounding);
        if (strcmp(field_name, "ScrollbarPadding") == 0) return (int)offsetof(ImGuiStyle, ScrollbarPadding);
        if (strcmp(field_name, "GrabMinSize") == 0) return (int)offsetof(ImGuiStyle, GrabMinSize);
        if (strcmp(field_name, "GrabRounding") == 0) return (int)offsetof(ImGuiStyle, GrabRounding);
        if (strcmp(field_name, "LogSliderDeadzone") == 0) return (int)offsetof(ImGuiStyle, LogSliderDeadzone);
        if (strcmp(field_name, "ImageRounding") == 0) return (int)offsetof(ImGuiStyle, ImageRounding);
        if (strcmp(field_name, "ImageBorderSize") == 0) return (int)offsetof(ImGuiStyle, ImageBorderSize);
        if (strcmp(field_name, "TabRounding") == 0) return (int)offsetof(ImGuiStyle, TabRounding);
        if (strcmp(field_name, "TabBorderSize") == 0) return (int)offsetof(ImGuiStyle, TabBorderSize);
        if (strcmp(field_name, "TabMinWidthBase") == 0) return (int)offsetof(ImGuiStyle, TabMinWidthBase);
        if (strcmp(field_name, "TabMinWidthShrink") == 0) return (int)offsetof(ImGuiStyle, TabMinWidthShrink);
        if (strcmp(field_name, "TabCloseButtonMinWidthSelected") == 0) return (int)offsetof(ImGuiStyle, TabCloseButtonMinWidthSelected);
        if (strcmp(field_name, "TabCloseButtonMinWidthUnselected") == 0) return (int)offsetof(ImGuiStyle, TabCloseButtonMinWidthUnselected);
        if (strcmp(field_name, "TabBarBorderSize") == 0) return (int)offsetof(ImGuiStyle, TabBarBorderSize);
        if (strcmp(field_name, "TabBarOverlineSize") == 0) return (int)offsetof(ImGuiStyle, TabBarOverlineSize);
        if (strcmp(field_name, "TableAngledHeadersAngle") == 0) return (int)offsetof(ImGuiStyle, TableAngledHeadersAngle);
        if (strcmp(field_name, "TableAngledHeadersTextAlign") == 0) return (int)offsetof(ImGuiStyle, TableAngledHeadersTextAlign);
        if (strcmp(field_name, "TreeLinesFlags") == 0) return (int)offsetof(ImGuiStyle, TreeLinesFlags);
        if (strcmp(field_name, "TreeLinesSize") == 0) return (int)offsetof(ImGuiStyle, TreeLinesSize);
        if (strcmp(field_name, "TreeLinesRounding") == 0) return (int)offsetof(ImGuiStyle, TreeLinesRounding);
        if (strcmp(field_name, "MenuItemRounding") == 0) return (int)offsetof(ImGuiStyle, MenuItemRounding);
        if (strcmp(field_name, "SelectableRounding") == 0) return (int)offsetof(ImGuiStyle, SelectableRounding);
        if (strcmp(field_name, "DragDropTargetRounding") == 0) return (int)offsetof(ImGuiStyle, DragDropTargetRounding);
        if (strcmp(field_name, "DragDropTargetBorderSize") == 0) return (int)offsetof(ImGuiStyle, DragDropTargetBorderSize);
        if (strcmp(field_name, "DragDropTargetPadding") == 0) return (int)offsetof(ImGuiStyle, DragDropTargetPadding);
        if (strcmp(field_name, "ColorMarkerSize") == 0) return (int)offsetof(ImGuiStyle, ColorMarkerSize);
        if (strcmp(field_name, "ColorButtonPosition") == 0) return (int)offsetof(ImGuiStyle, ColorButtonPosition);
        if (strcmp(field_name, "ButtonTextAlign") == 0) return (int)offsetof(ImGuiStyle, ButtonTextAlign);
        if (strcmp(field_name, "SelectableTextAlign") == 0) return (int)offsetof(ImGuiStyle, SelectableTextAlign);
        if (strcmp(field_name, "InputTextCursorSize") == 0) return (int)offsetof(ImGuiStyle, InputTextCursorSize);
        if (strcmp(field_name, "SeparatorSize") == 0) return (int)offsetof(ImGuiStyle, SeparatorSize);
        if (strcmp(field_name, "SeparatorTextBorderSize") == 0) return (int)offsetof(ImGuiStyle, SeparatorTextBorderSize);
        if (strcmp(field_name, "SeparatorTextAlign") == 0) return (int)offsetof(ImGuiStyle, SeparatorTextAlign);
        if (strcmp(field_name, "SeparatorTextPadding") == 0) return (int)offsetof(ImGuiStyle, SeparatorTextPadding);
        if (strcmp(field_name, "DisplayWindowPadding") == 0) return (int)offsetof(ImGuiStyle, DisplayWindowPadding);
        if (strcmp(field_name, "DisplaySafeAreaPadding") == 0) return (int)offsetof(ImGuiStyle, DisplaySafeAreaPadding);
        if (strcmp(field_name, "DockingNodeHasCloseButton") == 0) return (int)offsetof(ImGuiStyle, DockingNodeHasCloseButton);
        if (strcmp(field_name, "DockingSeparatorSize") == 0) return (int)offsetof(ImGuiStyle, DockingSeparatorSize);
        if (strcmp(field_name, "MouseCursorScale") == 0) return (int)offsetof(ImGuiStyle, MouseCursorScale);
        if (strcmp(field_name, "AntiAliasedLines") == 0) return (int)offsetof(ImGuiStyle, AntiAliasedLines);
        if (strcmp(field_name, "AntiAliasedLineEnds") == 0) return (int)offsetof(ImGuiStyle, AntiAliasedLineEnds);
        if (strcmp(field_name, "AntiAliasedFill") == 0) return (int)offsetof(ImGuiStyle, AntiAliasedFill);
        if (strcmp(field_name, "CurveTessellationMaxError") == 0) return (int)offsetof(ImGuiStyle, CurveTessellationMaxError);
        if (strcmp(field_name, "CircleTessellationMaxError") == 0) return (int)offsetof(ImGuiStyle, CircleTessellationMaxError);
        if (strcmp(field_name, "Colors") == 0) return (int)offsetof(ImGuiStyle, Colors);
        if (strcmp(field_name, "HoverStationaryDelay") == 0) return (int)offsetof(ImGuiStyle, HoverStationaryDelay);
        if (strcmp(field_name, "HoverDelayShort") == 0) return (int)offsetof(ImGuiStyle, HoverDelayShort);
        if (strcmp(field_name, "HoverDelayNormal") == 0) return (int)offsetof(ImGuiStyle, HoverDelayNormal);
        if (strcmp(field_name, "HoverFlagsForTooltipMouse") == 0) return (int)offsetof(ImGuiStyle, HoverFlagsForTooltipMouse);
        if (strcmp(field_name, "HoverFlagsForTooltipNav") == 0) return (int)offsetof(ImGuiStyle, HoverFlagsForTooltipNav);
        if (strcmp(field_name, "_MainScale") == 0) return (int)offsetof(ImGuiStyle, _MainScale);
        if (strcmp(field_name, "_NextFrameFontSizeBase") == 0) return (int)offsetof(ImGuiStyle, _NextFrameFontSizeBase);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiTableColumnSortSpecs") == 0)
    {
        if (strcmp(field_name, "ColumnUserID") == 0) return (int)offsetof(ImGuiTableColumnSortSpecs, ColumnUserID);
        if (strcmp(field_name, "ColumnIndex") == 0) return (int)offsetof(ImGuiTableColumnSortSpecs, ColumnIndex);
        if (strcmp(field_name, "SortOrder") == 0) return (int)offsetof(ImGuiTableColumnSortSpecs, SortOrder);
        if (strcmp(field_name, "SortDirection") == 0) return (int)offsetof(ImGuiTableColumnSortSpecs, SortDirection);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiTableSortSpecs") == 0)
    {
        if (strcmp(field_name, "Specs") == 0) return (int)offsetof(ImGuiTableSortSpecs, Specs);
        if (strcmp(field_name, "SpecsCount") == 0) return (int)offsetof(ImGuiTableSortSpecs, SpecsCount);
        if (strcmp(field_name, "SpecsDirty") == 0) return (int)offsetof(ImGuiTableSortSpecs, SpecsDirty);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiTextBuffer") == 0)
    {
        if (strcmp(field_name, "Buf") == 0) return (int)offsetof(ImGuiTextBuffer, Buf);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiTextFilter") == 0)
    {
        if (strcmp(field_name, "InputBuf") == 0) return (int)offsetof(ImGuiTextFilter, InputBuf);
        if (strcmp(field_name, "_CountExclude") == 0) return (int)offsetof(ImGuiTextFilter, _CountExclude);
        if (strcmp(field_name, "_Items") == 0) return (int)offsetof(ImGuiTextFilter, _Items);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiViewport") == 0)
    {
        if (strcmp(field_name, "ID") == 0) return (int)offsetof(ImGuiViewport, ID);
        if (strcmp(field_name, "Flags") == 0) return (int)offsetof(ImGuiViewport, Flags);
        if (strcmp(field_name, "Pos") == 0) return (int)offsetof(ImGuiViewport, Pos);
        if (strcmp(field_name, "Size") == 0) return (int)offsetof(ImGuiViewport, Size);
        if (strcmp(field_name, "FramebufferScale") == 0) return (int)offsetof(ImGuiViewport, FramebufferScale);
        if (strcmp(field_name, "WorkPos") == 0) return (int)offsetof(ImGuiViewport, WorkPos);
        if (strcmp(field_name, "WorkSize") == 0) return (int)offsetof(ImGuiViewport, WorkSize);
        if (strcmp(field_name, "DpiScale") == 0) return (int)offsetof(ImGuiViewport, DpiScale);
        if (strcmp(field_name, "ParentViewportId") == 0) return (int)offsetof(ImGuiViewport, ParentViewportId);
        if (strcmp(field_name, "ParentViewport") == 0) return (int)offsetof(ImGuiViewport, ParentViewport);
        if (strcmp(field_name, "DrawData") == 0) return (int)offsetof(ImGuiViewport, DrawData);
        if (strcmp(field_name, "RendererUserData") == 0) return (int)offsetof(ImGuiViewport, RendererUserData);
        if (strcmp(field_name, "PlatformUserData") == 0) return (int)offsetof(ImGuiViewport, PlatformUserData);
        if (strcmp(field_name, "PlatformIconData") == 0) return (int)offsetof(ImGuiViewport, PlatformIconData);
        if (strcmp(field_name, "PlatformHandle") == 0) return (int)offsetof(ImGuiViewport, PlatformHandle);
        if (strcmp(field_name, "PlatformHandleRaw") == 0) return (int)offsetof(ImGuiViewport, PlatformHandleRaw);
        if (strcmp(field_name, "PlatformWindowCreated") == 0) return (int)offsetof(ImGuiViewport, PlatformWindowCreated);
        if (strcmp(field_name, "PlatformRequestMove") == 0) return (int)offsetof(ImGuiViewport, PlatformRequestMove);
        if (strcmp(field_name, "PlatformRequestResize") == 0) return (int)offsetof(ImGuiViewport, PlatformRequestResize);
        if (strcmp(field_name, "PlatformRequestClose") == 0) return (int)offsetof(ImGuiViewport, PlatformRequestClose);
        return -1;
    }
    if (strcmp(struct_name, "ImGuiWindowClass") == 0)
    {
        if (strcmp(field_name, "ClassId") == 0) return (int)offsetof(ImGuiWindowClass, ClassId);
        if (strcmp(field_name, "ParentViewportId") == 0) return (int)offsetof(ImGuiWindowClass, ParentViewportId);
        if (strcmp(field_name, "FocusRouteParentWindowId") == 0) return (int)offsetof(ImGuiWindowClass, FocusRouteParentWindowId);
        if (strcmp(field_name, "ViewportFlagsOverrideSet") == 0) return (int)offsetof(ImGuiWindowClass, ViewportFlagsOverrideSet);
        if (strcmp(field_name, "ViewportFlagsOverrideClear") == 0) return (int)offsetof(ImGuiWindowClass, ViewportFlagsOverrideClear);
        if (strcmp(field_name, "TabItemFlagsOverrideSet") == 0) return (int)offsetof(ImGuiWindowClass, TabItemFlagsOverrideSet);
        if (strcmp(field_name, "DockNodeFlagsOverrideSet") == 0) return (int)offsetof(ImGuiWindowClass, DockNodeFlagsOverrideSet);
        if (strcmp(field_name, "DockingAlwaysTabBar") == 0) return (int)offsetof(ImGuiWindowClass, DockingAlwaysTabBar);
        if (strcmp(field_name, "DockingAllowUnclassed") == 0) return (int)offsetof(ImGuiWindowClass, DockingAllowUnclassed);
        if (strcmp(field_name, "PlatformIconData") == 0) return (int)offsetof(ImGuiWindowClass, PlatformIconData);
        return -1;
    }
    if (strcmp(struct_name, "ImTextureData") == 0)
    {
        if (strcmp(field_name, "UniqueID") == 0) return (int)offsetof(ImTextureData, UniqueID);
        if (strcmp(field_name, "Status") == 0) return (int)offsetof(ImTextureData, Status);
        if (strcmp(field_name, "BackendUserData") == 0) return (int)offsetof(ImTextureData, BackendUserData);
        if (strcmp(field_name, "QueueUserData") == 0) return (int)offsetof(ImTextureData, QueueUserData);
        if (strcmp(field_name, "TexID") == 0) return (int)offsetof(ImTextureData, TexID);
        if (strcmp(field_name, "Format") == 0) return (int)offsetof(ImTextureData, Format);
        if (strcmp(field_name, "Width") == 0) return (int)offsetof(ImTextureData, Width);
        if (strcmp(field_name, "Height") == 0) return (int)offsetof(ImTextureData, Height);
        if (strcmp(field_name, "BytesPerPixel") == 0) return (int)offsetof(ImTextureData, BytesPerPixel);
        if (strcmp(field_name, "Pixels") == 0) return (int)offsetof(ImTextureData, Pixels);
        if (strcmp(field_name, "UsedRect") == 0) return (int)offsetof(ImTextureData, UsedRect);
        if (strcmp(field_name, "UpdateRect") == 0) return (int)offsetof(ImTextureData, UpdateRect);
        if (strcmp(field_name, "Updates") == 0) return (int)offsetof(ImTextureData, Updates);
        if (strcmp(field_name, "UnusedFrames") == 0) return (int)offsetof(ImTextureData, UnusedFrames);
        if (strcmp(field_name, "RefCount") == 0) return (int)offsetof(ImTextureData, RefCount);
        if (strcmp(field_name, "UseColors") == 0) return (int)offsetof(ImTextureData, UseColors);
        if (strcmp(field_name, "WantDestroyNextFrame") == 0) return (int)offsetof(ImTextureData, WantDestroyNextFrame);
        return -1;
    }
    if (strcmp(struct_name, "ImTextureRect") == 0)
    {
        if (strcmp(field_name, "x") == 0) return (int)offsetof(ImTextureRect, x);
        if (strcmp(field_name, "y") == 0) return (int)offsetof(ImTextureRect, y);
        if (strcmp(field_name, "w") == 0) return (int)offsetof(ImTextureRect, w);
        if (strcmp(field_name, "h") == 0) return (int)offsetof(ImTextureRect, h);
        return -1;
    }
    if (strcmp(struct_name, "ImTextureRef") == 0)
    {
        if (strcmp(field_name, "_TexData") == 0) return (int)offsetof(ImTextureRef, _TexData);
        if (strcmp(field_name, "_TexID") == 0) return (int)offsetof(ImTextureRef, _TexID);
        return -1;
    }
    return -1;
}
