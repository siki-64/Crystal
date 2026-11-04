using System.Runtime.InteropServices;
using Crystal.ImGui;

namespace Crystal.Tests;

[TestClass]
public sealed unsafe partial class ImGuiStructLayoutTests
{
    [LibraryImport(ImGuiNative.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial int CRYSTAL_ImGuiStructSize(string structName);

    [LibraryImport(ImGuiNative.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial int CRYSTAL_ImGuiFieldOffset(string structName, string fieldName);

    [TestMethod]
    public void ImColorMatchesNativeLayout()
    {
        ImColor value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImColor"), sizeof(ImColor), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImColor", "Value"), (int)((byte*)&value.Value - start), "Value");
    }

    [TestMethod]
    public void ImDrawChannelMatchesNativeLayout()
    {
        ImDrawChannel value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawChannel"), sizeof(ImDrawChannel), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawChannel", "_CmdBuffer"), (int)((byte*)&value._CmdBuffer - start), "_CmdBuffer");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawChannel", "_IdxBuffer"), (int)((byte*)&value._IdxBuffer - start), "_IdxBuffer");
    }

    [TestMethod]
    public void ImDrawCmdMatchesNativeLayout()
    {
        ImDrawCmd value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawCmd"), sizeof(ImDrawCmd), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "ClipRect"), (int)((byte*)&value.ClipRect - start), "ClipRect");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "TexRef"), (int)((byte*)&value.TexRef - start), "TexRef");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "VtxOffset"), (int)((byte*)&value.VtxOffset - start), "VtxOffset");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "IdxOffset"), (int)((byte*)&value.IdxOffset - start), "IdxOffset");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "ElemCount"), (int)((byte*)&value.ElemCount - start), "ElemCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "UserCallback"), (int)((byte*)&value.UserCallback - start), "UserCallback");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "UserCallbackData"), (int)((byte*)&value.UserCallbackData - start), "UserCallbackData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "UserCallbackDataSize"), (int)((byte*)&value.UserCallbackDataSize - start), "UserCallbackDataSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmd", "UserCallbackDataOffset"), (int)((byte*)&value.UserCallbackDataOffset - start), "UserCallbackDataOffset");
    }

    [TestMethod]
    public void ImDrawCmdHeaderMatchesNativeLayout()
    {
        ImDrawCmdHeader value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawCmdHeader"), sizeof(ImDrawCmdHeader), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmdHeader", "ClipRect"), (int)((byte*)&value.ClipRect - start), "ClipRect");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmdHeader", "TexRef"), (int)((byte*)&value.TexRef - start), "TexRef");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawCmdHeader", "VtxOffset"), (int)((byte*)&value.VtxOffset - start), "VtxOffset");
    }

    [TestMethod]
    public void ImDrawDataMatchesNativeLayout()
    {
        ImDrawData value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawData"), sizeof(ImDrawData), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "Valid"), (int)((byte*)&value.Valid - start), "Valid");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "FrameCount"), (int)((byte*)&value.FrameCount - start), "FrameCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "TotalIdxCount"), (int)((byte*)&value.TotalIdxCount - start), "TotalIdxCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "TotalVtxCount"), (int)((byte*)&value.TotalVtxCount - start), "TotalVtxCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "CmdLists"), (int)((byte*)&value.CmdLists - start), "CmdLists");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "DisplayPos"), (int)((byte*)&value.DisplayPos - start), "DisplayPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "DisplaySize"), (int)((byte*)&value.DisplaySize - start), "DisplaySize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "FramebufferScale"), (int)((byte*)&value.FramebufferScale - start), "FramebufferScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "OwnerViewport"), (int)((byte*)&value.OwnerViewport - start), "OwnerViewport");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawData", "Textures"), (int)((byte*)&value.Textures - start), "Textures");
    }

    [TestMethod]
    public void ImDrawListMatchesNativeLayout()
    {
        ImDrawList value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawList"), sizeof(ImDrawList), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "CmdBuffer"), (int)((byte*)&value.CmdBuffer - start), "CmdBuffer");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "IdxBuffer"), (int)((byte*)&value.IdxBuffer - start), "IdxBuffer");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "VtxBuffer"), (int)((byte*)&value.VtxBuffer - start), "VtxBuffer");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_VtxCurrentIdx"), (int)((byte*)&value._VtxCurrentIdx - start), "_VtxCurrentIdx");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_Data"), (int)((byte*)&value._Data - start), "_Data");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_VtxWritePtr"), (int)((byte*)&value._VtxWritePtr - start), "_VtxWritePtr");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_IdxWritePtr"), (int)((byte*)&value._IdxWritePtr - start), "_IdxWritePtr");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_Path"), (int)((byte*)&value._Path - start), "_Path");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_CmdHeader"), (int)((byte*)&value._CmdHeader - start), "_CmdHeader");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_Splitter"), (int)((byte*)&value._Splitter - start), "_Splitter");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_ClipRectStack"), (int)((byte*)&value._ClipRectStack - start), "_ClipRectStack");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_TextureStack"), (int)((byte*)&value._TextureStack - start), "_TextureStack");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_CallbacksDataBuf"), (int)((byte*)&value._CallbacksDataBuf - start), "_CallbacksDataBuf");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_DrawFlagsStack"), (int)((byte*)&value._DrawFlagsStack - start), "_DrawFlagsStack");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_FringeScale"), (int)((byte*)&value._FringeScale - start), "_FringeScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_InvFringeScale"), (int)((byte*)&value._InvFringeScale - start), "_InvFringeScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_FringeScaleIsInteger"), (int)((byte*)&value._FringeScaleIsInteger - start), "_FringeScaleIsInteger");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawList", "_OwnerName"), (int)((byte*)&value._OwnerName - start), "_OwnerName");
    }

    [TestMethod]
    public void ImDrawListSplitterMatchesNativeLayout()
    {
        ImDrawListSplitter value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawListSplitter"), sizeof(ImDrawListSplitter), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawListSplitter", "_Current"), (int)((byte*)&value._Current - start), "_Current");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawListSplitter", "_Count"), (int)((byte*)&value._Count - start), "_Count");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawListSplitter", "_Channels"), (int)((byte*)&value._Channels - start), "_Channels");
    }

    [TestMethod]
    public void ImDrawVertMatchesNativeLayout()
    {
        ImDrawVert value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImDrawVert"), sizeof(ImDrawVert), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawVert", "pos"), (int)((byte*)&value.pos - start), "pos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawVert", "uv"), (int)((byte*)&value.uv - start), "uv");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImDrawVert", "col"), (int)((byte*)&value.col - start), "col");
    }

    [TestMethod]
    public void ImFontMatchesNativeLayout()
    {
        ImFont value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFont"), sizeof(ImFont), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "LastBaked"), (int)((byte*)&value.LastBaked - start), "LastBaked");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "OwnerAtlas"), (int)((byte*)&value.OwnerAtlas - start), "OwnerAtlas");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "CurrentRasterizerDensity"), (int)((byte*)&value.CurrentRasterizerDensity - start), "CurrentRasterizerDensity");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "FontId"), (int)((byte*)&value.FontId - start), "FontId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "LegacySize"), (int)((byte*)&value.LegacySize - start), "LegacySize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "Sources"), (int)((byte*)&value.Sources - start), "Sources");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "EllipsisChar"), (int)((byte*)&value.EllipsisChar - start), "EllipsisChar");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "FallbackChar"), (int)((byte*)&value.FallbackChar - start), "FallbackChar");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "Used8kPagesMap"), (int)((byte*)&value.Used8kPagesMap - start), "Used8kPagesMap");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "EllipsisAutoBake"), (int)((byte*)&value.EllipsisAutoBake - start), "EllipsisAutoBake");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFont", "RemapPairs"), (int)((byte*)&value.RemapPairs - start), "RemapPairs");
    }

    [TestMethod]
    public void ImFontAtlasMatchesNativeLayout()
    {
        ImFontAtlas value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFontAtlas"), sizeof(ImFontAtlas), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexDesiredFormat"), (int)((byte*)&value.TexDesiredFormat - start), "TexDesiredFormat");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexGlyphPadding"), (int)((byte*)&value.TexGlyphPadding - start), "TexGlyphPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexMinWidth"), (int)((byte*)&value.TexMinWidth - start), "TexMinWidth");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexMinHeight"), (int)((byte*)&value.TexMinHeight - start), "TexMinHeight");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexMaxWidth"), (int)((byte*)&value.TexMaxWidth - start), "TexMaxWidth");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexMaxHeight"), (int)((byte*)&value.TexMaxHeight - start), "TexMaxHeight");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "UserData"), (int)((byte*)&value.UserData - start), "UserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexRef"), (int)((byte*)&value.TexRef - start), "TexRef");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexData"), (int)((byte*)&value.TexData - start), "TexData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexList"), (int)((byte*)&value.TexList - start), "TexList");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "Locked"), (int)((byte*)&value.Locked - start), "Locked");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "RendererHasTextures"), (int)((byte*)&value.RendererHasTextures - start), "RendererHasTextures");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexIsBuilt"), (int)((byte*)&value.TexIsBuilt - start), "TexIsBuilt");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexPixelsUseColors"), (int)((byte*)&value.TexPixelsUseColors - start), "TexPixelsUseColors");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexUvScale"), (int)((byte*)&value.TexUvScale - start), "TexUvScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexUvWhitePixel"), (int)((byte*)&value.TexUvWhitePixel - start), "TexUvWhitePixel");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "Fonts"), (int)((byte*)&value.Fonts - start), "Fonts");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "Sources"), (int)((byte*)&value.Sources - start), "Sources");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "TexNextUniqueID"), (int)((byte*)&value.TexNextUniqueID - start), "TexNextUniqueID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "FontNextUniqueID"), (int)((byte*)&value.FontNextUniqueID - start), "FontNextUniqueID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "DrawListSharedDatas"), (int)((byte*)&value.DrawListSharedDatas - start), "DrawListSharedDatas");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "Builder"), (int)((byte*)&value.Builder - start), "Builder");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "FontLoader"), (int)((byte*)&value.FontLoader - start), "FontLoader");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "FontLoaderName"), (int)((byte*)&value.FontLoaderName - start), "FontLoaderName");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "FontLoaderData"), (int)((byte*)&value.FontLoaderData - start), "FontLoaderData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "FontLoaderFlags"), (int)((byte*)&value.FontLoaderFlags - start), "FontLoaderFlags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "RefCount"), (int)((byte*)&value.RefCount - start), "RefCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlas", "OwnerContext"), (int)((byte*)&value.OwnerContext - start), "OwnerContext");
    }

    [TestMethod]
    public void ImFontAtlasRectMatchesNativeLayout()
    {
        ImFontAtlasRect value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFontAtlasRect"), sizeof(ImFontAtlasRect), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlasRect", "x"), (int)((byte*)&value.x - start), "x");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlasRect", "y"), (int)((byte*)&value.y - start), "y");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlasRect", "w"), (int)((byte*)&value.w - start), "w");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlasRect", "h"), (int)((byte*)&value.h - start), "h");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlasRect", "uv0"), (int)((byte*)&value.uv0 - start), "uv0");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontAtlasRect", "uv1"), (int)((byte*)&value.uv1 - start), "uv1");
    }

    [TestMethod]
    public void ImFontBakedMatchesNativeLayout()
    {
        ImFontBaked value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFontBaked"), sizeof(ImFontBaked), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "IndexAdvanceX"), (int)((byte*)&value.IndexAdvanceX - start), "IndexAdvanceX");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "FallbackAdvanceX"), (int)((byte*)&value.FallbackAdvanceX - start), "FallbackAdvanceX");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "Size"), (int)((byte*)&value.Size - start), "Size");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "RasterizerDensity"), (int)((byte*)&value.RasterizerDensity - start), "RasterizerDensity");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "IndexLookup"), (int)((byte*)&value.IndexLookup - start), "IndexLookup");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "Glyphs"), (int)((byte*)&value.Glyphs - start), "Glyphs");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "FallbackGlyphIndex"), (int)((byte*)&value.FallbackGlyphIndex - start), "FallbackGlyphIndex");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "Ascent"), (int)((byte*)&value.Ascent - start), "Ascent");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "Descent"), (int)((byte*)&value.Descent - start), "Descent");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "LastUsedFrame"), (int)((byte*)&value.LastUsedFrame - start), "LastUsedFrame");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "BakedId"), (int)((byte*)&value.BakedId - start), "BakedId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "OwnerFont"), (int)((byte*)&value.OwnerFont - start), "OwnerFont");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontBaked", "FontLoaderDatas"), (int)((byte*)&value.FontLoaderDatas - start), "FontLoaderDatas");
    }

    [TestMethod]
    public void ImFontConfigMatchesNativeLayout()
    {
        ImFontConfig value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFontConfig"), sizeof(ImFontConfig), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "Name"), (int)((byte*)&value.Name - start), "Name");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontData"), (int)((byte*)&value.FontData - start), "FontData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontDataSize"), (int)((byte*)&value.FontDataSize - start), "FontDataSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontDataOwnedByAtlas"), (int)((byte*)&value.FontDataOwnedByAtlas - start), "FontDataOwnedByAtlas");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "MergeMode"), (int)((byte*)&value.MergeMode - start), "MergeMode");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "PixelSnapH"), (int)((byte*)&value.PixelSnapH - start), "PixelSnapH");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "OversampleH"), (int)((byte*)&value.OversampleH - start), "OversampleH");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "OversampleV"), (int)((byte*)&value.OversampleV - start), "OversampleV");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "EllipsisChar"), (int)((byte*)&value.EllipsisChar - start), "EllipsisChar");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "SizePixels"), (int)((byte*)&value.SizePixels - start), "SizePixels");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "GlyphRanges"), (int)((byte*)&value.GlyphRanges - start), "GlyphRanges");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "GlyphExcludeRanges"), (int)((byte*)&value.GlyphExcludeRanges - start), "GlyphExcludeRanges");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "GlyphOffset"), (int)((byte*)&value.GlyphOffset - start), "GlyphOffset");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "GlyphMinAdvanceX"), (int)((byte*)&value.GlyphMinAdvanceX - start), "GlyphMinAdvanceX");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "GlyphMaxAdvanceX"), (int)((byte*)&value.GlyphMaxAdvanceX - start), "GlyphMaxAdvanceX");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "GlyphExtraAdvanceX"), (int)((byte*)&value.GlyphExtraAdvanceX - start), "GlyphExtraAdvanceX");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontNo"), (int)((byte*)&value.FontNo - start), "FontNo");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontLoaderFlags"), (int)((byte*)&value.FontLoaderFlags - start), "FontLoaderFlags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "RasterizerMultiply"), (int)((byte*)&value.RasterizerMultiply - start), "RasterizerMultiply");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "RasterizerDensity"), (int)((byte*)&value.RasterizerDensity - start), "RasterizerDensity");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "ExtraSizeScale"), (int)((byte*)&value.ExtraSizeScale - start), "ExtraSizeScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "DstFont"), (int)((byte*)&value.DstFont - start), "DstFont");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontLoader"), (int)((byte*)&value.FontLoader - start), "FontLoader");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontConfig", "FontLoaderData"), (int)((byte*)&value.FontLoaderData - start), "FontLoaderData");
    }

    [TestMethod]
    public void ImFontGlyphMatchesNativeLayout()
    {
        ImFontGlyph value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFontGlyph"), sizeof(ImFontGlyph), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "AdvanceX"), (int)((byte*)&value.AdvanceX - start), "AdvanceX");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "X0"), (int)((byte*)&value.X0 - start), "X0");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "Y0"), (int)((byte*)&value.Y0 - start), "Y0");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "X1"), (int)((byte*)&value.X1 - start), "X1");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "Y1"), (int)((byte*)&value.Y1 - start), "Y1");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "U0"), (int)((byte*)&value.U0 - start), "U0");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "V0"), (int)((byte*)&value.V0 - start), "V0");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "U1"), (int)((byte*)&value.U1 - start), "U1");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "V1"), (int)((byte*)&value.V1 - start), "V1");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyph", "PackId"), (int)((byte*)&value.PackId - start), "PackId");
    }

    [TestMethod]
    public void ImFontGlyphRangesBuilderMatchesNativeLayout()
    {
        ImFontGlyphRangesBuilder value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImFontGlyphRangesBuilder"), sizeof(ImFontGlyphRangesBuilder), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImFontGlyphRangesBuilder", "UsedChars"), (int)((byte*)&value.UsedChars - start), "UsedChars");
    }

    [TestMethod]
    public void ImGuiIOMatchesNativeLayout()
    {
        ImGuiIO value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiIO"), sizeof(ImGuiIO), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigFlags"), (int)((byte*)&value.ConfigFlags - start), "ConfigFlags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "BackendFlags"), (int)((byte*)&value.BackendFlags - start), "BackendFlags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "DisplaySize"), (int)((byte*)&value.DisplaySize - start), "DisplaySize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "DisplayFramebufferScale"), (int)((byte*)&value.DisplayFramebufferScale - start), "DisplayFramebufferScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "DeltaTime"), (int)((byte*)&value.DeltaTime - start), "DeltaTime");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "IniSavingRate"), (int)((byte*)&value.IniSavingRate - start), "IniSavingRate");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "IniFilename"), (int)((byte*)&value.IniFilename - start), "IniFilename");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "LogFilename"), (int)((byte*)&value.LogFilename - start), "LogFilename");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "UserData"), (int)((byte*)&value.UserData - start), "UserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "Fonts"), (int)((byte*)&value.Fonts - start), "Fonts");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "FontDefault"), (int)((byte*)&value.FontDefault - start), "FontDefault");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "FontAllowUserScaling"), (int)((byte*)&value.FontAllowUserScaling - start), "FontAllowUserScaling");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavSwapGamepadButtons"), (int)((byte*)&value.ConfigNavSwapGamepadButtons - start), "ConfigNavSwapGamepadButtons");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavMoveSetMousePos"), (int)((byte*)&value.ConfigNavMoveSetMousePos - start), "ConfigNavMoveSetMousePos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavCaptureKeyboard"), (int)((byte*)&value.ConfigNavCaptureKeyboard - start), "ConfigNavCaptureKeyboard");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavEscapeClearFocusItem"), (int)((byte*)&value.ConfigNavEscapeClearFocusItem - start), "ConfigNavEscapeClearFocusItem");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavEscapeClearFocusWindow"), (int)((byte*)&value.ConfigNavEscapeClearFocusWindow - start), "ConfigNavEscapeClearFocusWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavCursorVisibleAuto"), (int)((byte*)&value.ConfigNavCursorVisibleAuto - start), "ConfigNavCursorVisibleAuto");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigNavCursorVisibleAlways"), (int)((byte*)&value.ConfigNavCursorVisibleAlways - start), "ConfigNavCursorVisibleAlways");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDockingNoSplit"), (int)((byte*)&value.ConfigDockingNoSplit - start), "ConfigDockingNoSplit");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDockingNoDockingOver"), (int)((byte*)&value.ConfigDockingNoDockingOver - start), "ConfigDockingNoDockingOver");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDockingWithShift"), (int)((byte*)&value.ConfigDockingWithShift - start), "ConfigDockingWithShift");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDockingAlwaysTabBar"), (int)((byte*)&value.ConfigDockingAlwaysTabBar - start), "ConfigDockingAlwaysTabBar");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDockingTransparentPayload"), (int)((byte*)&value.ConfigDockingTransparentPayload - start), "ConfigDockingTransparentPayload");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigViewportsNoAutoMerge"), (int)((byte*)&value.ConfigViewportsNoAutoMerge - start), "ConfigViewportsNoAutoMerge");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigViewportsNoTaskBarIcon"), (int)((byte*)&value.ConfigViewportsNoTaskBarIcon - start), "ConfigViewportsNoTaskBarIcon");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigViewportsNoDecoration"), (int)((byte*)&value.ConfigViewportsNoDecoration - start), "ConfigViewportsNoDecoration");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigViewportsNoDefaultParent"), (int)((byte*)&value.ConfigViewportsNoDefaultParent - start), "ConfigViewportsNoDefaultParent");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigViewportsPlatformFocusSetsImGuiFocus"), (int)((byte*)&value.ConfigViewportsPlatformFocusSetsImGuiFocus - start), "ConfigViewportsPlatformFocusSetsImGuiFocus");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDpiScaleFonts"), (int)((byte*)&value.ConfigDpiScaleFonts - start), "ConfigDpiScaleFonts");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDpiScaleViewports"), (int)((byte*)&value.ConfigDpiScaleViewports - start), "ConfigDpiScaleViewports");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigMacOSXBehaviors"), (int)((byte*)&value.ConfigMacOSXBehaviors - start), "ConfigMacOSXBehaviors");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigInputTrickleEventQueue"), (int)((byte*)&value.ConfigInputTrickleEventQueue - start), "ConfigInputTrickleEventQueue");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigInputTextCursorBlink"), (int)((byte*)&value.ConfigInputTextCursorBlink - start), "ConfigInputTextCursorBlink");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigInputTextEnterKeepActive"), (int)((byte*)&value.ConfigInputTextEnterKeepActive - start), "ConfigInputTextEnterKeepActive");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigColorEditFlags"), (int)((byte*)&value.ConfigColorEditFlags - start), "ConfigColorEditFlags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDragClickToInputText"), (int)((byte*)&value.ConfigDragClickToInputText - start), "ConfigDragClickToInputText");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigWindowsResizeFromEdges"), (int)((byte*)&value.ConfigWindowsResizeFromEdges - start), "ConfigWindowsResizeFromEdges");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigWindowsMoveFromTitleBarOnly"), (int)((byte*)&value.ConfigWindowsMoveFromTitleBarOnly - start), "ConfigWindowsMoveFromTitleBarOnly");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigWindowsCopyContentsWithCtrlC"), (int)((byte*)&value.ConfigWindowsCopyContentsWithCtrlC - start), "ConfigWindowsCopyContentsWithCtrlC");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigScrollbarScrollByPage"), (int)((byte*)&value.ConfigScrollbarScrollByPage - start), "ConfigScrollbarScrollByPage");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigIniSettingsSaveLastUsedDate"), (int)((byte*)&value.ConfigIniSettingsSaveLastUsedDate - start), "ConfigIniSettingsSaveLastUsedDate");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigIniSettingsAutoDiscardMonths"), (int)((byte*)&value.ConfigIniSettingsAutoDiscardMonths - start), "ConfigIniSettingsAutoDiscardMonths");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugIniSettings"), (int)((byte*)&value.ConfigDebugIniSettings - start), "ConfigDebugIniSettings");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDrawCursor"), (int)((byte*)&value.MouseDrawCursor - start), "MouseDrawCursor");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigMemoryCompactTimer"), (int)((byte*)&value.ConfigMemoryCompactTimer - start), "ConfigMemoryCompactTimer");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDoubleClickTime"), (int)((byte*)&value.MouseDoubleClickTime - start), "MouseDoubleClickTime");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDoubleClickMaxDist"), (int)((byte*)&value.MouseDoubleClickMaxDist - start), "MouseDoubleClickMaxDist");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseSingleClickDelay"), (int)((byte*)&value.MouseSingleClickDelay - start), "MouseSingleClickDelay");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDragThreshold"), (int)((byte*)&value.MouseDragThreshold - start), "MouseDragThreshold");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeyRepeatDelay"), (int)((byte*)&value.KeyRepeatDelay - start), "KeyRepeatDelay");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeyRepeatRate"), (int)((byte*)&value.KeyRepeatRate - start), "KeyRepeatRate");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigErrorRecovery"), (int)((byte*)&value.ConfigErrorRecovery - start), "ConfigErrorRecovery");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigErrorRecoveryEnableAssert"), (int)((byte*)&value.ConfigErrorRecoveryEnableAssert - start), "ConfigErrorRecoveryEnableAssert");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigErrorRecoveryEnableDebugLog"), (int)((byte*)&value.ConfigErrorRecoveryEnableDebugLog - start), "ConfigErrorRecoveryEnableDebugLog");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigErrorRecoveryEnableTooltip"), (int)((byte*)&value.ConfigErrorRecoveryEnableTooltip - start), "ConfigErrorRecoveryEnableTooltip");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugIsDebuggerPresent"), (int)((byte*)&value.ConfigDebugIsDebuggerPresent - start), "ConfigDebugIsDebuggerPresent");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugHighlightIdConflicts"), (int)((byte*)&value.ConfigDebugHighlightIdConflicts - start), "ConfigDebugHighlightIdConflicts");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugHighlightIdConflictsShowItemPicker"), (int)((byte*)&value.ConfigDebugHighlightIdConflictsShowItemPicker - start), "ConfigDebugHighlightIdConflictsShowItemPicker");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugBeginReturnValueOnce"), (int)((byte*)&value.ConfigDebugBeginReturnValueOnce - start), "ConfigDebugBeginReturnValueOnce");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugBeginReturnValueLoop"), (int)((byte*)&value.ConfigDebugBeginReturnValueLoop - start), "ConfigDebugBeginReturnValueLoop");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugIgnoreFocusLoss"), (int)((byte*)&value.ConfigDebugIgnoreFocusLoss - start), "ConfigDebugIgnoreFocusLoss");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "ConfigDebugDrawListDefaultsToStrokeLegacy"), (int)((byte*)&value.ConfigDebugDrawListDefaultsToStrokeLegacy - start), "ConfigDebugDrawListDefaultsToStrokeLegacy");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "BackendPlatformName"), (int)((byte*)&value.BackendPlatformName - start), "BackendPlatformName");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "BackendRendererName"), (int)((byte*)&value.BackendRendererName - start), "BackendRendererName");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "BackendPlatformUserData"), (int)((byte*)&value.BackendPlatformUserData - start), "BackendPlatformUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "BackendRendererUserData"), (int)((byte*)&value.BackendRendererUserData - start), "BackendRendererUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "BackendLanguageUserData"), (int)((byte*)&value.BackendLanguageUserData - start), "BackendLanguageUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "WantCaptureMouse"), (int)((byte*)&value.WantCaptureMouse - start), "WantCaptureMouse");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "WantCaptureKeyboard"), (int)((byte*)&value.WantCaptureKeyboard - start), "WantCaptureKeyboard");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "WantTextInput"), (int)((byte*)&value.WantTextInput - start), "WantTextInput");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "WantSetMousePos"), (int)((byte*)&value.WantSetMousePos - start), "WantSetMousePos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "WantSaveIniSettings"), (int)((byte*)&value.WantSaveIniSettings - start), "WantSaveIniSettings");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "NavActive"), (int)((byte*)&value.NavActive - start), "NavActive");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "NavVisible"), (int)((byte*)&value.NavVisible - start), "NavVisible");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "Framerate"), (int)((byte*)&value.Framerate - start), "Framerate");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MetricsRenderVertices"), (int)((byte*)&value.MetricsRenderVertices - start), "MetricsRenderVertices");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MetricsRenderIndices"), (int)((byte*)&value.MetricsRenderIndices - start), "MetricsRenderIndices");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MetricsRenderWindows"), (int)((byte*)&value.MetricsRenderWindows - start), "MetricsRenderWindows");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MetricsActiveWindows"), (int)((byte*)&value.MetricsActiveWindows - start), "MetricsActiveWindows");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDelta"), (int)((byte*)&value.MouseDelta - start), "MouseDelta");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "Ctx"), (int)((byte*)&value.Ctx - start), "Ctx");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MousePos"), (int)((byte*)&value.MousePos - start), "MousePos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDown"), (int)((byte*)&value.MouseDown - start), "MouseDown");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseWheel"), (int)((byte*)&value.MouseWheel - start), "MouseWheel");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseWheelH"), (int)((byte*)&value.MouseWheelH - start), "MouseWheelH");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseSource"), (int)((byte*)&value.MouseSource - start), "MouseSource");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseHoveredViewport"), (int)((byte*)&value.MouseHoveredViewport - start), "MouseHoveredViewport");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeyCtrl"), (int)((byte*)&value.KeyCtrl - start), "KeyCtrl");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeyShift"), (int)((byte*)&value.KeyShift - start), "KeyShift");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeyAlt"), (int)((byte*)&value.KeyAlt - start), "KeyAlt");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeySuper"), (int)((byte*)&value.KeySuper - start), "KeySuper");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeyMods"), (int)((byte*)&value.KeyMods - start), "KeyMods");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "KeysData"), (int)((byte*)&value.KeysData - start), "KeysData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "WantCaptureMouseUnlessPopupClose"), (int)((byte*)&value.WantCaptureMouseUnlessPopupClose - start), "WantCaptureMouseUnlessPopupClose");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MousePosPrev"), (int)((byte*)&value.MousePosPrev - start), "MousePosPrev");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseClickedPos"), (int)((byte*)&value.MouseClickedPos - start), "MouseClickedPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseClickedTime"), (int)((byte*)&value.MouseClickedTime - start), "MouseClickedTime");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseClicked"), (int)((byte*)&value.MouseClicked - start), "MouseClicked");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDoubleClicked"), (int)((byte*)&value.MouseDoubleClicked - start), "MouseDoubleClicked");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseClickedCount"), (int)((byte*)&value.MouseClickedCount - start), "MouseClickedCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseClickedLastCount"), (int)((byte*)&value.MouseClickedLastCount - start), "MouseClickedLastCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseReleased"), (int)((byte*)&value.MouseReleased - start), "MouseReleased");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseReleasedTime"), (int)((byte*)&value.MouseReleasedTime - start), "MouseReleasedTime");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDownOwned"), (int)((byte*)&value.MouseDownOwned - start), "MouseDownOwned");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDownOwnedUnlessPopupClose"), (int)((byte*)&value.MouseDownOwnedUnlessPopupClose - start), "MouseDownOwnedUnlessPopupClose");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseWheelRequestAxisSwap"), (int)((byte*)&value.MouseWheelRequestAxisSwap - start), "MouseWheelRequestAxisSwap");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseCtrlLeftAsRightClick"), (int)((byte*)&value.MouseCtrlLeftAsRightClick - start), "MouseCtrlLeftAsRightClick");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDownDuration"), (int)((byte*)&value.MouseDownDuration - start), "MouseDownDuration");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDownDurationPrev"), (int)((byte*)&value.MouseDownDurationPrev - start), "MouseDownDurationPrev");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDragMaxDistanceAbs"), (int)((byte*)&value.MouseDragMaxDistanceAbs - start), "MouseDragMaxDistanceAbs");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "MouseDragMaxDistanceSqr"), (int)((byte*)&value.MouseDragMaxDistanceSqr - start), "MouseDragMaxDistanceSqr");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "PenPressure"), (int)((byte*)&value.PenPressure - start), "PenPressure");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "AppFocusLost"), (int)((byte*)&value.AppFocusLost - start), "AppFocusLost");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "AppAcceptingEvents"), (int)((byte*)&value.AppAcceptingEvents - start), "AppAcceptingEvents");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "InputQueueSurrogate"), (int)((byte*)&value.InputQueueSurrogate - start), "InputQueueSurrogate");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiIO", "InputQueueCharacters"), (int)((byte*)&value.InputQueueCharacters - start), "InputQueueCharacters");
    }

    [TestMethod]
    public void ImGuiInputTextCallbackDataMatchesNativeLayout()
    {
        ImGuiInputTextCallbackData value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiInputTextCallbackData"), sizeof(ImGuiInputTextCallbackData), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "Ctx"), (int)((byte*)&value.Ctx - start), "Ctx");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "EventFlag"), (int)((byte*)&value.EventFlag - start), "EventFlag");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "UserData"), (int)((byte*)&value.UserData - start), "UserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "ID"), (int)((byte*)&value.ID - start), "ID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "EventKey"), (int)((byte*)&value.EventKey - start), "EventKey");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "EventChar"), (int)((byte*)&value.EventChar - start), "EventChar");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "EventActivated"), (int)((byte*)&value.EventActivated - start), "EventActivated");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "BufDirty"), (int)((byte*)&value.BufDirty - start), "BufDirty");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "Buf"), (int)((byte*)&value.Buf - start), "Buf");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "BufTextLen"), (int)((byte*)&value.BufTextLen - start), "BufTextLen");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "BufSize"), (int)((byte*)&value.BufSize - start), "BufSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "CursorPos"), (int)((byte*)&value.CursorPos - start), "CursorPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "SelectionStart"), (int)((byte*)&value.SelectionStart - start), "SelectionStart");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiInputTextCallbackData", "SelectionEnd"), (int)((byte*)&value.SelectionEnd - start), "SelectionEnd");
    }

    [TestMethod]
    public void ImGuiKeyDataMatchesNativeLayout()
    {
        ImGuiKeyData value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiKeyData"), sizeof(ImGuiKeyData), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiKeyData", "Down"), (int)((byte*)&value.Down - start), "Down");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiKeyData", "DownDuration"), (int)((byte*)&value.DownDuration - start), "DownDuration");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiKeyData", "DownDurationPrev"), (int)((byte*)&value.DownDurationPrev - start), "DownDurationPrev");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiKeyData", "AnalogValue"), (int)((byte*)&value.AnalogValue - start), "AnalogValue");
    }

    [TestMethod]
    public void ImGuiListClipperMatchesNativeLayout()
    {
        ImGuiListClipper value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiListClipper"), sizeof(ImGuiListClipper), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "DisplayStart"), (int)((byte*)&value.DisplayStart - start), "DisplayStart");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "DisplayEnd"), (int)((byte*)&value.DisplayEnd - start), "DisplayEnd");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "UserIndex"), (int)((byte*)&value.UserIndex - start), "UserIndex");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "ItemsCount"), (int)((byte*)&value.ItemsCount - start), "ItemsCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "ItemsHeight"), (int)((byte*)&value.ItemsHeight - start), "ItemsHeight");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "StartPosY"), (int)((byte*)&value.StartPosY - start), "StartPosY");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "StartSeekOffsetY"), (int)((byte*)&value.StartSeekOffsetY - start), "StartSeekOffsetY");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "Ctx"), (int)((byte*)&value.Ctx - start), "Ctx");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiListClipper", "TempData"), (int)((byte*)&value.TempData - start), "TempData");
    }

    [TestMethod]
    public void ImGuiMultiSelectIOMatchesNativeLayout()
    {
        ImGuiMultiSelectIO value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiMultiSelectIO"), sizeof(ImGuiMultiSelectIO), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiMultiSelectIO", "Requests"), (int)((byte*)&value.Requests - start), "Requests");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiMultiSelectIO", "RangeSrcItem"), (int)((byte*)&value.RangeSrcItem - start), "RangeSrcItem");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiMultiSelectIO", "NavIdItem"), (int)((byte*)&value.NavIdItem - start), "NavIdItem");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiMultiSelectIO", "NavIdSelected"), (int)((byte*)&value.NavIdSelected - start), "NavIdSelected");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiMultiSelectIO", "RangeSrcReset"), (int)((byte*)&value.RangeSrcReset - start), "RangeSrcReset");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiMultiSelectIO", "ItemsCount"), (int)((byte*)&value.ItemsCount - start), "ItemsCount");
    }

    [TestMethod]
    public void ImGuiOnceUponAFrameMatchesNativeLayout()
    {
        ImGuiOnceUponAFrame value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiOnceUponAFrame"), sizeof(ImGuiOnceUponAFrame), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiOnceUponAFrame", "RefFrame"), (int)((byte*)&value.RefFrame - start), "RefFrame");
    }

    [TestMethod]
    public void ImGuiPayloadMatchesNativeLayout()
    {
        ImGuiPayload value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiPayload"), sizeof(ImGuiPayload), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "Data"), (int)((byte*)&value.Data - start), "Data");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "DataSize"), (int)((byte*)&value.DataSize - start), "DataSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "SourceId"), (int)((byte*)&value.SourceId - start), "SourceId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "SourceParentId"), (int)((byte*)&value.SourceParentId - start), "SourceParentId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "DataFrameCount"), (int)((byte*)&value.DataFrameCount - start), "DataFrameCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "DataType"), (int)((byte*)&value.DataType - start), "DataType");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "Preview"), (int)((byte*)&value.Preview - start), "Preview");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPayload", "Delivery"), (int)((byte*)&value.Delivery - start), "Delivery");
    }

    [TestMethod]
    public void ImGuiPlatformIOMatchesNativeLayout()
    {
        ImGuiPlatformIO value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiPlatformIO"), sizeof(ImGuiPlatformIO), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetClipboardTextFn"), (int)((byte*)&value.Platform_GetClipboardTextFn - start), "Platform_GetClipboardTextFn");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetClipboardTextFn"), (int)((byte*)&value.Platform_SetClipboardTextFn - start), "Platform_SetClipboardTextFn");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_ClipboardUserData"), (int)((byte*)&value.Platform_ClipboardUserData - start), "Platform_ClipboardUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_OpenInShellFn"), (int)((byte*)&value.Platform_OpenInShellFn - start), "Platform_OpenInShellFn");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_OpenInShellUserData"), (int)((byte*)&value.Platform_OpenInShellUserData - start), "Platform_OpenInShellUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetImeDataFn"), (int)((byte*)&value.Platform_SetImeDataFn - start), "Platform_SetImeDataFn");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_ImeUserData"), (int)((byte*)&value.Platform_ImeUserData - start), "Platform_ImeUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_LocaleDecimalPoint"), (int)((byte*)&value.Platform_LocaleDecimalPoint - start), "Platform_LocaleDecimalPoint");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SessionDate"), (int)((byte*)&value.Platform_SessionDate - start), "Platform_SessionDate");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_TextureMaxWidth"), (int)((byte*)&value.Renderer_TextureMaxWidth - start), "Renderer_TextureMaxWidth");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_TextureMaxHeight"), (int)((byte*)&value.Renderer_TextureMaxHeight - start), "Renderer_TextureMaxHeight");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_RenderState"), (int)((byte*)&value.Renderer_RenderState - start), "Renderer_RenderState");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "DrawCallback_ResetRenderState"), (int)((byte*)&value.DrawCallback_ResetRenderState - start), "DrawCallback_ResetRenderState");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "DrawCallback_SetSamplerLinear"), (int)((byte*)&value.DrawCallback_SetSamplerLinear - start), "DrawCallback_SetSamplerLinear");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "DrawCallback_SetSamplerNearest"), (int)((byte*)&value.DrawCallback_SetSamplerNearest - start), "DrawCallback_SetSamplerNearest");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "DrawCallback_SetSamplerFromTex"), (int)((byte*)&value.DrawCallback_SetSamplerFromTex - start), "DrawCallback_SetSamplerFromTex");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_CreateWindow"), (int)((byte*)&value.Platform_CreateWindow - start), "Platform_CreateWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_DestroyWindow"), (int)((byte*)&value.Platform_DestroyWindow - start), "Platform_DestroyWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_ShowWindow"), (int)((byte*)&value.Platform_ShowWindow - start), "Platform_ShowWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetWindowPos"), (int)((byte*)&value.Platform_SetWindowPos - start), "Platform_SetWindowPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowPos"), (int)((byte*)&value.Platform_GetWindowPos - start), "Platform_GetWindowPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetWindowSize"), (int)((byte*)&value.Platform_SetWindowSize - start), "Platform_SetWindowSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowSize"), (int)((byte*)&value.Platform_GetWindowSize - start), "Platform_GetWindowSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowFramebufferScale"), (int)((byte*)&value.Platform_GetWindowFramebufferScale - start), "Platform_GetWindowFramebufferScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetWindowFocus"), (int)((byte*)&value.Platform_SetWindowFocus - start), "Platform_SetWindowFocus");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowFocus"), (int)((byte*)&value.Platform_GetWindowFocus - start), "Platform_GetWindowFocus");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowMinimized"), (int)((byte*)&value.Platform_GetWindowMinimized - start), "Platform_GetWindowMinimized");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetWindowTitle"), (int)((byte*)&value.Platform_SetWindowTitle - start), "Platform_SetWindowTitle");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SetWindowAlpha"), (int)((byte*)&value.Platform_SetWindowAlpha - start), "Platform_SetWindowAlpha");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_UpdateWindow"), (int)((byte*)&value.Platform_UpdateWindow - start), "Platform_UpdateWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_RenderWindow"), (int)((byte*)&value.Platform_RenderWindow - start), "Platform_RenderWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_SwapBuffers"), (int)((byte*)&value.Platform_SwapBuffers - start), "Platform_SwapBuffers");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowDpiScale"), (int)((byte*)&value.Platform_GetWindowDpiScale - start), "Platform_GetWindowDpiScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_OnChangedViewport"), (int)((byte*)&value.Platform_OnChangedViewport - start), "Platform_OnChangedViewport");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_GetWindowWorkAreaInsets"), (int)((byte*)&value.Platform_GetWindowWorkAreaInsets - start), "Platform_GetWindowWorkAreaInsets");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Platform_CreateVkSurface"), (int)((byte*)&value.Platform_CreateVkSurface - start), "Platform_CreateVkSurface");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_CreateWindow"), (int)((byte*)&value.Renderer_CreateWindow - start), "Renderer_CreateWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_DestroyWindow"), (int)((byte*)&value.Renderer_DestroyWindow - start), "Renderer_DestroyWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_SetWindowSize"), (int)((byte*)&value.Renderer_SetWindowSize - start), "Renderer_SetWindowSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_RenderWindow"), (int)((byte*)&value.Renderer_RenderWindow - start), "Renderer_RenderWindow");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Renderer_SwapBuffers"), (int)((byte*)&value.Renderer_SwapBuffers - start), "Renderer_SwapBuffers");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Monitors"), (int)((byte*)&value.Monitors - start), "Monitors");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Textures"), (int)((byte*)&value.Textures - start), "Textures");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformIO", "Viewports"), (int)((byte*)&value.Viewports - start), "Viewports");
    }

    [TestMethod]
    public void ImGuiPlatformImeDataMatchesNativeLayout()
    {
        ImGuiPlatformImeData value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiPlatformImeData"), sizeof(ImGuiPlatformImeData), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformImeData", "WantVisible"), (int)((byte*)&value.WantVisible - start), "WantVisible");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformImeData", "WantTextInput"), (int)((byte*)&value.WantTextInput - start), "WantTextInput");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformImeData", "InputPos"), (int)((byte*)&value.InputPos - start), "InputPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformImeData", "InputLineHeight"), (int)((byte*)&value.InputLineHeight - start), "InputLineHeight");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformImeData", "ViewportId"), (int)((byte*)&value.ViewportId - start), "ViewportId");
    }

    [TestMethod]
    public void ImGuiPlatformMonitorMatchesNativeLayout()
    {
        ImGuiPlatformMonitor value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiPlatformMonitor"), sizeof(ImGuiPlatformMonitor), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformMonitor", "MainPos"), (int)((byte*)&value.MainPos - start), "MainPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformMonitor", "MainSize"), (int)((byte*)&value.MainSize - start), "MainSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformMonitor", "WorkPos"), (int)((byte*)&value.WorkPos - start), "WorkPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformMonitor", "WorkSize"), (int)((byte*)&value.WorkSize - start), "WorkSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformMonitor", "DpiScale"), (int)((byte*)&value.DpiScale - start), "DpiScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiPlatformMonitor", "PlatformHandle"), (int)((byte*)&value.PlatformHandle - start), "PlatformHandle");
    }

    [TestMethod]
    public void ImGuiSelectionBasicStorageMatchesNativeLayout()
    {
        ImGuiSelectionBasicStorage value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiSelectionBasicStorage"), sizeof(ImGuiSelectionBasicStorage), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionBasicStorage", "Size"), (int)((byte*)&value.Size - start), "Size");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionBasicStorage", "PreserveOrder"), (int)((byte*)&value.PreserveOrder - start), "PreserveOrder");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionBasicStorage", "UserData"), (int)((byte*)&value.UserData - start), "UserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionBasicStorage", "AdapterIndexToStorageId"), (int)((byte*)&value.AdapterIndexToStorageId - start), "AdapterIndexToStorageId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionBasicStorage", "_SelectionOrder"), (int)((byte*)&value._SelectionOrder - start), "_SelectionOrder");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionBasicStorage", "_Storage"), (int)((byte*)&value._Storage - start), "_Storage");
    }

    [TestMethod]
    public void ImGuiSelectionExternalStorageMatchesNativeLayout()
    {
        ImGuiSelectionExternalStorage value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiSelectionExternalStorage"), sizeof(ImGuiSelectionExternalStorage), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionExternalStorage", "UserData"), (int)((byte*)&value.UserData - start), "UserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionExternalStorage", "AdapterSetItemSelected"), (int)((byte*)&value.AdapterSetItemSelected - start), "AdapterSetItemSelected");
    }

    [TestMethod]
    public void ImGuiSelectionRequestMatchesNativeLayout()
    {
        ImGuiSelectionRequest value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiSelectionRequest"), sizeof(ImGuiSelectionRequest), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionRequest", "Type"), (int)((byte*)&value.Type - start), "Type");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionRequest", "Selected"), (int)((byte*)&value.Selected - start), "Selected");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionRequest", "RangeDirection"), (int)((byte*)&value.RangeDirection - start), "RangeDirection");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionRequest", "RangeFirstItem"), (int)((byte*)&value.RangeFirstItem - start), "RangeFirstItem");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSelectionRequest", "RangeLastItem"), (int)((byte*)&value.RangeLastItem - start), "RangeLastItem");
    }

    [TestMethod]
    public void ImGuiSizeCallbackDataMatchesNativeLayout()
    {
        ImGuiSizeCallbackData value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiSizeCallbackData"), sizeof(ImGuiSizeCallbackData), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSizeCallbackData", "UserData"), (int)((byte*)&value.UserData - start), "UserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSizeCallbackData", "Pos"), (int)((byte*)&value.Pos - start), "Pos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSizeCallbackData", "CurrentSize"), (int)((byte*)&value.CurrentSize - start), "CurrentSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiSizeCallbackData", "DesiredSize"), (int)((byte*)&value.DesiredSize - start), "DesiredSize");
    }

    [TestMethod]
    public void ImGuiStorageMatchesNativeLayout()
    {
        ImGuiStorage value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiStorage"), sizeof(ImGuiStorage), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStorage", "Data"), (int)((byte*)&value.Data - start), "Data");
    }

    [TestMethod]
    public void ImGuiStoragePairMatchesNativeLayout()
    {
        ImGuiStoragePair value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiStoragePair"), sizeof(ImGuiStoragePair), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStoragePair", "key"), (int)((byte*)&value.key - start), "key");
    }

    [TestMethod]
    public void ImGuiStyleMatchesNativeLayout()
    {
        ImGuiStyle value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiStyle"), sizeof(ImGuiStyle), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "FontSizeBase"), (int)((byte*)&value.FontSizeBase - start), "FontSizeBase");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "FontScaleMain"), (int)((byte*)&value.FontScaleMain - start), "FontScaleMain");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "FontScaleDpi"), (int)((byte*)&value.FontScaleDpi - start), "FontScaleDpi");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "Alpha"), (int)((byte*)&value.Alpha - start), "Alpha");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DisabledAlpha"), (int)((byte*)&value.DisabledAlpha - start), "DisabledAlpha");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowPadding"), (int)((byte*)&value.WindowPadding - start), "WindowPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowRounding"), (int)((byte*)&value.WindowRounding - start), "WindowRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowBorderSize"), (int)((byte*)&value.WindowBorderSize - start), "WindowBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowBorderHoverPadding"), (int)((byte*)&value.WindowBorderHoverPadding - start), "WindowBorderHoverPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowMinSize"), (int)((byte*)&value.WindowMinSize - start), "WindowMinSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowTitleAlign"), (int)((byte*)&value.WindowTitleAlign - start), "WindowTitleAlign");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "WindowMenuButtonPosition"), (int)((byte*)&value.WindowMenuButtonPosition - start), "WindowMenuButtonPosition");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ChildRounding"), (int)((byte*)&value.ChildRounding - start), "ChildRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ChildBorderSize"), (int)((byte*)&value.ChildBorderSize - start), "ChildBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "PopupRounding"), (int)((byte*)&value.PopupRounding - start), "PopupRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "PopupBorderSize"), (int)((byte*)&value.PopupBorderSize - start), "PopupBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "FramePadding"), (int)((byte*)&value.FramePadding - start), "FramePadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "FrameRounding"), (int)((byte*)&value.FrameRounding - start), "FrameRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "FrameBorderSize"), (int)((byte*)&value.FrameBorderSize - start), "FrameBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ItemSpacing"), (int)((byte*)&value.ItemSpacing - start), "ItemSpacing");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ItemInnerSpacing"), (int)((byte*)&value.ItemInnerSpacing - start), "ItemInnerSpacing");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "CellPadding"), (int)((byte*)&value.CellPadding - start), "CellPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TouchExtraPadding"), (int)((byte*)&value.TouchExtraPadding - start), "TouchExtraPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "IndentSpacing"), (int)((byte*)&value.IndentSpacing - start), "IndentSpacing");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ColumnsMinSpacing"), (int)((byte*)&value.ColumnsMinSpacing - start), "ColumnsMinSpacing");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ScrollbarSize"), (int)((byte*)&value.ScrollbarSize - start), "ScrollbarSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ScrollbarRounding"), (int)((byte*)&value.ScrollbarRounding - start), "ScrollbarRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ScrollbarPadding"), (int)((byte*)&value.ScrollbarPadding - start), "ScrollbarPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "GrabMinSize"), (int)((byte*)&value.GrabMinSize - start), "GrabMinSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "GrabRounding"), (int)((byte*)&value.GrabRounding - start), "GrabRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "LogSliderDeadzone"), (int)((byte*)&value.LogSliderDeadzone - start), "LogSliderDeadzone");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ImageRounding"), (int)((byte*)&value.ImageRounding - start), "ImageRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ImageBorderSize"), (int)((byte*)&value.ImageBorderSize - start), "ImageBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabRounding"), (int)((byte*)&value.TabRounding - start), "TabRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabBorderSize"), (int)((byte*)&value.TabBorderSize - start), "TabBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabMinWidthBase"), (int)((byte*)&value.TabMinWidthBase - start), "TabMinWidthBase");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabMinWidthShrink"), (int)((byte*)&value.TabMinWidthShrink - start), "TabMinWidthShrink");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabCloseButtonMinWidthSelected"), (int)((byte*)&value.TabCloseButtonMinWidthSelected - start), "TabCloseButtonMinWidthSelected");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabCloseButtonMinWidthUnselected"), (int)((byte*)&value.TabCloseButtonMinWidthUnselected - start), "TabCloseButtonMinWidthUnselected");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabBarBorderSize"), (int)((byte*)&value.TabBarBorderSize - start), "TabBarBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TabBarOverlineSize"), (int)((byte*)&value.TabBarOverlineSize - start), "TabBarOverlineSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TableAngledHeadersAngle"), (int)((byte*)&value.TableAngledHeadersAngle - start), "TableAngledHeadersAngle");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TableAngledHeadersTextAlign"), (int)((byte*)&value.TableAngledHeadersTextAlign - start), "TableAngledHeadersTextAlign");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TreeLinesFlags"), (int)((byte*)&value.TreeLinesFlags - start), "TreeLinesFlags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TreeLinesSize"), (int)((byte*)&value.TreeLinesSize - start), "TreeLinesSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "TreeLinesRounding"), (int)((byte*)&value.TreeLinesRounding - start), "TreeLinesRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "MenuItemRounding"), (int)((byte*)&value.MenuItemRounding - start), "MenuItemRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "SelectableRounding"), (int)((byte*)&value.SelectableRounding - start), "SelectableRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DragDropTargetRounding"), (int)((byte*)&value.DragDropTargetRounding - start), "DragDropTargetRounding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DragDropTargetBorderSize"), (int)((byte*)&value.DragDropTargetBorderSize - start), "DragDropTargetBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DragDropTargetPadding"), (int)((byte*)&value.DragDropTargetPadding - start), "DragDropTargetPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ColorMarkerSize"), (int)((byte*)&value.ColorMarkerSize - start), "ColorMarkerSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ColorButtonPosition"), (int)((byte*)&value.ColorButtonPosition - start), "ColorButtonPosition");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "ButtonTextAlign"), (int)((byte*)&value.ButtonTextAlign - start), "ButtonTextAlign");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "SelectableTextAlign"), (int)((byte*)&value.SelectableTextAlign - start), "SelectableTextAlign");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "InputTextCursorSize"), (int)((byte*)&value.InputTextCursorSize - start), "InputTextCursorSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "SeparatorSize"), (int)((byte*)&value.SeparatorSize - start), "SeparatorSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "SeparatorTextBorderSize"), (int)((byte*)&value.SeparatorTextBorderSize - start), "SeparatorTextBorderSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "SeparatorTextAlign"), (int)((byte*)&value.SeparatorTextAlign - start), "SeparatorTextAlign");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "SeparatorTextPadding"), (int)((byte*)&value.SeparatorTextPadding - start), "SeparatorTextPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DisplayWindowPadding"), (int)((byte*)&value.DisplayWindowPadding - start), "DisplayWindowPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DisplaySafeAreaPadding"), (int)((byte*)&value.DisplaySafeAreaPadding - start), "DisplaySafeAreaPadding");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DockingNodeHasCloseButton"), (int)((byte*)&value.DockingNodeHasCloseButton - start), "DockingNodeHasCloseButton");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "DockingSeparatorSize"), (int)((byte*)&value.DockingSeparatorSize - start), "DockingSeparatorSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "MouseCursorScale"), (int)((byte*)&value.MouseCursorScale - start), "MouseCursorScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "AntiAliasedLines"), (int)((byte*)&value.AntiAliasedLines - start), "AntiAliasedLines");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "AntiAliasedLineEnds"), (int)((byte*)&value.AntiAliasedLineEnds - start), "AntiAliasedLineEnds");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "AntiAliasedFill"), (int)((byte*)&value.AntiAliasedFill - start), "AntiAliasedFill");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "CurveTessellationMaxError"), (int)((byte*)&value.CurveTessellationMaxError - start), "CurveTessellationMaxError");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "CircleTessellationMaxError"), (int)((byte*)&value.CircleTessellationMaxError - start), "CircleTessellationMaxError");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "Colors"), (int)((byte*)&value.Colors - start), "Colors");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "HoverStationaryDelay"), (int)((byte*)&value.HoverStationaryDelay - start), "HoverStationaryDelay");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "HoverDelayShort"), (int)((byte*)&value.HoverDelayShort - start), "HoverDelayShort");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "HoverDelayNormal"), (int)((byte*)&value.HoverDelayNormal - start), "HoverDelayNormal");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "HoverFlagsForTooltipMouse"), (int)((byte*)&value.HoverFlagsForTooltipMouse - start), "HoverFlagsForTooltipMouse");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "HoverFlagsForTooltipNav"), (int)((byte*)&value.HoverFlagsForTooltipNav - start), "HoverFlagsForTooltipNav");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "_MainScale"), (int)((byte*)&value._MainScale - start), "_MainScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiStyle", "_NextFrameFontSizeBase"), (int)((byte*)&value._NextFrameFontSizeBase - start), "_NextFrameFontSizeBase");
    }

    [TestMethod]
    public void ImGuiTableColumnSortSpecsMatchesNativeLayout()
    {
        ImGuiTableColumnSortSpecs value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiTableColumnSortSpecs"), sizeof(ImGuiTableColumnSortSpecs), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableColumnSortSpecs", "ColumnUserID"), (int)((byte*)&value.ColumnUserID - start), "ColumnUserID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableColumnSortSpecs", "ColumnIndex"), (int)((byte*)&value.ColumnIndex - start), "ColumnIndex");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableColumnSortSpecs", "SortOrder"), (int)((byte*)&value.SortOrder - start), "SortOrder");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableColumnSortSpecs", "SortDirection"), (int)((byte*)&value.SortDirection - start), "SortDirection");
    }

    [TestMethod]
    public void ImGuiTableSortSpecsMatchesNativeLayout()
    {
        ImGuiTableSortSpecs value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiTableSortSpecs"), sizeof(ImGuiTableSortSpecs), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableSortSpecs", "Specs"), (int)((byte*)&value.Specs - start), "Specs");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableSortSpecs", "SpecsCount"), (int)((byte*)&value.SpecsCount - start), "SpecsCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTableSortSpecs", "SpecsDirty"), (int)((byte*)&value.SpecsDirty - start), "SpecsDirty");
    }

    [TestMethod]
    public void ImGuiTextBufferMatchesNativeLayout()
    {
        ImGuiTextBuffer value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiTextBuffer"), sizeof(ImGuiTextBuffer), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTextBuffer", "Buf"), (int)((byte*)&value.Buf - start), "Buf");
    }

    [TestMethod]
    public void ImGuiTextFilterMatchesNativeLayout()
    {
        ImGuiTextFilter value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiTextFilter"), sizeof(ImGuiTextFilter), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTextFilter", "InputBuf"), (int)((byte*)&value.InputBuf - start), "InputBuf");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTextFilter", "_CountExclude"), (int)((byte*)&value._CountExclude - start), "_CountExclude");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiTextFilter", "_Items"), (int)((byte*)&value._Items - start), "_Items");
    }

    [TestMethod]
    public void ImGuiViewportMatchesNativeLayout()
    {
        ImGuiViewport value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiViewport"), sizeof(ImGuiViewport), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "ID"), (int)((byte*)&value.ID - start), "ID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "Flags"), (int)((byte*)&value.Flags - start), "Flags");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "Pos"), (int)((byte*)&value.Pos - start), "Pos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "Size"), (int)((byte*)&value.Size - start), "Size");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "FramebufferScale"), (int)((byte*)&value.FramebufferScale - start), "FramebufferScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "WorkPos"), (int)((byte*)&value.WorkPos - start), "WorkPos");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "WorkSize"), (int)((byte*)&value.WorkSize - start), "WorkSize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "DpiScale"), (int)((byte*)&value.DpiScale - start), "DpiScale");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "ParentViewportId"), (int)((byte*)&value.ParentViewportId - start), "ParentViewportId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "ParentViewport"), (int)((byte*)&value.ParentViewport - start), "ParentViewport");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "DrawData"), (int)((byte*)&value.DrawData - start), "DrawData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "RendererUserData"), (int)((byte*)&value.RendererUserData - start), "RendererUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformUserData"), (int)((byte*)&value.PlatformUserData - start), "PlatformUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformIconData"), (int)((byte*)&value.PlatformIconData - start), "PlatformIconData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformHandle"), (int)((byte*)&value.PlatformHandle - start), "PlatformHandle");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformHandleRaw"), (int)((byte*)&value.PlatformHandleRaw - start), "PlatformHandleRaw");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformWindowCreated"), (int)((byte*)&value.PlatformWindowCreated - start), "PlatformWindowCreated");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformRequestMove"), (int)((byte*)&value.PlatformRequestMove - start), "PlatformRequestMove");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformRequestResize"), (int)((byte*)&value.PlatformRequestResize - start), "PlatformRequestResize");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiViewport", "PlatformRequestClose"), (int)((byte*)&value.PlatformRequestClose - start), "PlatformRequestClose");
    }

    [TestMethod]
    public void ImGuiWindowClassMatchesNativeLayout()
    {
        ImGuiWindowClass value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImGuiWindowClass"), sizeof(ImGuiWindowClass), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "ClassId"), (int)((byte*)&value.ClassId - start), "ClassId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "ParentViewportId"), (int)((byte*)&value.ParentViewportId - start), "ParentViewportId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "FocusRouteParentWindowId"), (int)((byte*)&value.FocusRouteParentWindowId - start), "FocusRouteParentWindowId");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "ViewportFlagsOverrideSet"), (int)((byte*)&value.ViewportFlagsOverrideSet - start), "ViewportFlagsOverrideSet");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "ViewportFlagsOverrideClear"), (int)((byte*)&value.ViewportFlagsOverrideClear - start), "ViewportFlagsOverrideClear");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "TabItemFlagsOverrideSet"), (int)((byte*)&value.TabItemFlagsOverrideSet - start), "TabItemFlagsOverrideSet");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "DockNodeFlagsOverrideSet"), (int)((byte*)&value.DockNodeFlagsOverrideSet - start), "DockNodeFlagsOverrideSet");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "DockingAlwaysTabBar"), (int)((byte*)&value.DockingAlwaysTabBar - start), "DockingAlwaysTabBar");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "DockingAllowUnclassed"), (int)((byte*)&value.DockingAllowUnclassed - start), "DockingAllowUnclassed");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImGuiWindowClass", "PlatformIconData"), (int)((byte*)&value.PlatformIconData - start), "PlatformIconData");
    }

    [TestMethod]
    public void ImTextureDataMatchesNativeLayout()
    {
        ImTextureData value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImTextureData"), sizeof(ImTextureData), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "UniqueID"), (int)((byte*)&value.UniqueID - start), "UniqueID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "Status"), (int)((byte*)&value.Status - start), "Status");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "BackendUserData"), (int)((byte*)&value.BackendUserData - start), "BackendUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "QueueUserData"), (int)((byte*)&value.QueueUserData - start), "QueueUserData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "TexID"), (int)((byte*)&value.TexID - start), "TexID");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "Format"), (int)((byte*)&value.Format - start), "Format");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "Width"), (int)((byte*)&value.Width - start), "Width");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "Height"), (int)((byte*)&value.Height - start), "Height");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "BytesPerPixel"), (int)((byte*)&value.BytesPerPixel - start), "BytesPerPixel");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "Pixels"), (int)((byte*)&value.Pixels - start), "Pixels");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "UsedRect"), (int)((byte*)&value.UsedRect - start), "UsedRect");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "UpdateRect"), (int)((byte*)&value.UpdateRect - start), "UpdateRect");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "Updates"), (int)((byte*)&value.Updates - start), "Updates");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "UnusedFrames"), (int)((byte*)&value.UnusedFrames - start), "UnusedFrames");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "RefCount"), (int)((byte*)&value.RefCount - start), "RefCount");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "UseColors"), (int)((byte*)&value.UseColors - start), "UseColors");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureData", "WantDestroyNextFrame"), (int)((byte*)&value.WantDestroyNextFrame - start), "WantDestroyNextFrame");
    }

    [TestMethod]
    public void ImTextureRectMatchesNativeLayout()
    {
        ImTextureRect value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImTextureRect"), sizeof(ImTextureRect), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureRect", "x"), (int)((byte*)&value.x - start), "x");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureRect", "y"), (int)((byte*)&value.y - start), "y");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureRect", "w"), (int)((byte*)&value.w - start), "w");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureRect", "h"), (int)((byte*)&value.h - start), "h");
    }

    [TestMethod]
    public void ImTextureRefMatchesNativeLayout()
    {
        ImTextureRef value = default;
        var start = (byte*)&value;
        Assert.AreEqual(CRYSTAL_ImGuiStructSize("ImTextureRef"), sizeof(ImTextureRef), "sizeof");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureRef", "_TexData"), (int)((byte*)&value._TexData - start), "_TexData");
        Assert.AreEqual(CRYSTAL_ImGuiFieldOffset("ImTextureRef", "_TexID"), (int)((byte*)&value._TexID - start), "_TexID");
    }
}
