using System.Numerics;

namespace Crystal.ImGui;

public static unsafe partial class ImGui
{
    public static ImGuiPayloadPtr AcceptDragDropPayload(string type, ImGuiDragDropFlags flags)
    {
        using var typeUtf8 = new ImGuiUtf8String(type, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImGuiPayloadPtr(ImGuiNative.igAcceptDragDropPayload(typeUtf8.Pointer, flags));
    }

    public static ImGuiPayloadPtr AcceptDragDropPayload(string type)
    {
        using var typeUtf8 = new ImGuiUtf8String(type, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return new ImGuiPayloadPtr(ImGuiNative.igAcceptDragDropPayload(typeUtf8.Pointer, (ImGuiDragDropFlags)(0)));
    }

    public static void AlignTextToFramePadding()
    {
        ImGuiNative.igAlignTextToFramePadding();
    }

    public static bool ArrowButton(string strId, ImGuiDir dir)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igArrowButton(strIdUtf8.Pointer, dir);
    }

    public static bool Begin(string name, ref bool pOpen, ImGuiWindowFlags flags)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pOpenPointer = &pOpen)
        {
            return ImGuiNative.igBegin(nameUtf8.Pointer, pOpenPointer, flags);
        }
    }

    public static bool Begin(string name, ref bool pOpen)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pOpenPointer = &pOpen)
        {
            return ImGuiNative.igBegin(nameUtf8.Pointer, pOpenPointer, (ImGuiWindowFlags)(0));
        }
    }

    public static bool Begin(string name)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBegin(nameUtf8.Pointer, null, (ImGuiWindowFlags)(0));
    }

    public static bool BeginChild(string strId, Vector2 size, ImGuiChildFlags childFlags, ImGuiWindowFlags windowFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginChild_Str(strIdUtf8.Pointer, size, childFlags, windowFlags);
    }

    public static bool BeginChild(string strId, Vector2 size, ImGuiChildFlags childFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginChild_Str(strIdUtf8.Pointer, size, childFlags, (ImGuiWindowFlags)(0));
    }

    public static bool BeginChild(string strId, Vector2 size)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginChild_Str(strIdUtf8.Pointer, size, (ImGuiChildFlags)(0), (ImGuiWindowFlags)(0));
    }

    public static bool BeginChild(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginChild_Str(strIdUtf8.Pointer, new Vector2(0f, 0f), (ImGuiChildFlags)(0), (ImGuiWindowFlags)(0));
    }

    public static bool BeginChild(uint id, Vector2 size, ImGuiChildFlags childFlags, ImGuiWindowFlags windowFlags)
    {
        return ImGuiNative.igBeginChild_ID(id, size, childFlags, windowFlags);
    }

    public static bool BeginChild(uint id, Vector2 size, ImGuiChildFlags childFlags)
    {
        return ImGuiNative.igBeginChild_ID(id, size, childFlags, (ImGuiWindowFlags)(0));
    }

    public static bool BeginChild(uint id, Vector2 size)
    {
        return ImGuiNative.igBeginChild_ID(id, size, (ImGuiChildFlags)(0), (ImGuiWindowFlags)(0));
    }

    public static bool BeginChild(uint id)
    {
        return ImGuiNative.igBeginChild_ID(id, new Vector2(0f, 0f), (ImGuiChildFlags)(0), (ImGuiWindowFlags)(0));
    }

    public static bool BeginCombo(string label, string previewValue, ImGuiComboFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var previewValueUtf8 = new ImGuiUtf8String(previewValue, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginCombo(labelUtf8.Pointer, previewValueUtf8.Pointer, flags);
    }

    public static bool BeginCombo(string label, string previewValue)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var previewValueUtf8 = new ImGuiUtf8String(previewValue, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginCombo(labelUtf8.Pointer, previewValueUtf8.Pointer, (ImGuiComboFlags)(0));
    }

    public static void BeginDisabled(bool disabled)
    {
        ImGuiNative.igBeginDisabled(disabled);
    }

    public static void BeginDisabled()
    {
        ImGuiNative.igBeginDisabled(true);
    }

    public static bool BeginDragDropSource(ImGuiDragDropFlags flags)
    {
        return ImGuiNative.igBeginDragDropSource(flags);
    }

    public static bool BeginDragDropSource()
    {
        return ImGuiNative.igBeginDragDropSource((ImGuiDragDropFlags)(0));
    }

    public static bool BeginDragDropTarget()
    {
        return ImGuiNative.igBeginDragDropTarget();
    }

    public static void BeginGroup()
    {
        ImGuiNative.igBeginGroup();
    }

    public static bool BeginItemTooltip()
    {
        return ImGuiNative.igBeginItemTooltip();
    }

    public static bool BeginListBox(string label, Vector2 size)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginListBox(labelUtf8.Pointer, size);
    }

    public static bool BeginListBox(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginListBox(labelUtf8.Pointer, new Vector2(0f, 0f));
    }

    public static bool BeginMainMenuBar()
    {
        return ImGuiNative.igBeginMainMenuBar();
    }

    public static bool BeginMenu(string label, bool enabled)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginMenu(labelUtf8.Pointer, enabled);
    }

    public static bool BeginMenu(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginMenu(labelUtf8.Pointer, true);
    }

    public static bool BeginMenuBar()
    {
        return ImGuiNative.igBeginMenuBar();
    }

    public static ImGuiMultiSelectIOPtr BeginMultiSelect(ImGuiMultiSelectFlags flags, int selectionSize, int itemsCount)
    {
        return new ImGuiMultiSelectIOPtr(ImGuiNative.igBeginMultiSelect(flags, selectionSize, itemsCount));
    }

    public static ImGuiMultiSelectIOPtr BeginMultiSelect(ImGuiMultiSelectFlags flags, int selectionSize)
    {
        return new ImGuiMultiSelectIOPtr(ImGuiNative.igBeginMultiSelect(flags, selectionSize, -1));
    }

    public static ImGuiMultiSelectIOPtr BeginMultiSelect(ImGuiMultiSelectFlags flags)
    {
        return new ImGuiMultiSelectIOPtr(ImGuiNative.igBeginMultiSelect(flags, -1, -1));
    }

    public static bool BeginPopup(string strId, ImGuiWindowFlags flags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopup(strIdUtf8.Pointer, flags);
    }

    public static bool BeginPopup(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopup(strIdUtf8.Pointer, (ImGuiWindowFlags)(0));
    }

    public static bool BeginPopupContextItem(string strId, ImGuiPopupFlags popupFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupContextItem(strIdUtf8.Pointer, popupFlags);
    }

    public static bool BeginPopupContextItem(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupContextItem(strIdUtf8.Pointer, (ImGuiPopupFlags)(0));
    }

    public static bool BeginPopupContextItem()
    {
        return ImGuiNative.igBeginPopupContextItem(null, (ImGuiPopupFlags)(0));
    }

    public static bool BeginPopupContextVoid(string strId, ImGuiPopupFlags popupFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupContextVoid(strIdUtf8.Pointer, popupFlags);
    }

    public static bool BeginPopupContextVoid(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupContextVoid(strIdUtf8.Pointer, (ImGuiPopupFlags)(0));
    }

    public static bool BeginPopupContextVoid()
    {
        return ImGuiNative.igBeginPopupContextVoid(null, (ImGuiPopupFlags)(0));
    }

    public static bool BeginPopupContextWindow(string strId, ImGuiPopupFlags popupFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupContextWindow(strIdUtf8.Pointer, popupFlags);
    }

    public static bool BeginPopupContextWindow(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupContextWindow(strIdUtf8.Pointer, (ImGuiPopupFlags)(0));
    }

    public static bool BeginPopupContextWindow()
    {
        return ImGuiNative.igBeginPopupContextWindow(null, (ImGuiPopupFlags)(0));
    }

    public static bool BeginPopupModal(string name, ref bool pOpen, ImGuiWindowFlags flags)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pOpenPointer = &pOpen)
        {
            return ImGuiNative.igBeginPopupModal(nameUtf8.Pointer, pOpenPointer, flags);
        }
    }

    public static bool BeginPopupModal(string name, ref bool pOpen)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pOpenPointer = &pOpen)
        {
            return ImGuiNative.igBeginPopupModal(nameUtf8.Pointer, pOpenPointer, (ImGuiWindowFlags)(0));
        }
    }

    public static bool BeginPopupModal(string name)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginPopupModal(nameUtf8.Pointer, null, (ImGuiWindowFlags)(0));
    }

    public static bool BeginTabBar(string strId, ImGuiTabBarFlags flags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTabBar(strIdUtf8.Pointer, flags);
    }

    public static bool BeginTabBar(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTabBar(strIdUtf8.Pointer, (ImGuiTabBarFlags)(0));
    }

    public static bool BeginTabItem(string label, ref bool pOpen, ImGuiTabItemFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pOpenPointer = &pOpen)
        {
            return ImGuiNative.igBeginTabItem(labelUtf8.Pointer, pOpenPointer, flags);
        }
    }

    public static bool BeginTabItem(string label, ref bool pOpen)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pOpenPointer = &pOpen)
        {
            return ImGuiNative.igBeginTabItem(labelUtf8.Pointer, pOpenPointer, (ImGuiTabItemFlags)(0));
        }
    }

    public static bool BeginTabItem(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTabItem(labelUtf8.Pointer, null, (ImGuiTabItemFlags)(0));
    }

    public static bool BeginTable(string strId, int columns, ImGuiTableFlags flags, Vector2 outerSize, float innerWidth)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTable(strIdUtf8.Pointer, columns, flags, outerSize, innerWidth);
    }

    public static bool BeginTable(string strId, int columns, ImGuiTableFlags flags, Vector2 outerSize)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTable(strIdUtf8.Pointer, columns, flags, outerSize, 0f);
    }

    public static bool BeginTable(string strId, int columns, ImGuiTableFlags flags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTable(strIdUtf8.Pointer, columns, flags, new Vector2(0f, 0f), 0f);
    }

    public static bool BeginTable(string strId, int columns)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igBeginTable(strIdUtf8.Pointer, columns, (ImGuiTableFlags)(0), new Vector2(0f, 0f), 0f);
    }

    public static bool BeginTooltip()
    {
        return ImGuiNative.igBeginTooltip();
    }

    public static void Bullet()
    {
        ImGuiNative.igBullet();
    }

    public static void BulletText(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igBulletText(textUtf8.Pointer);
    }

    public static bool Button(string label, Vector2 size)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igButton(labelUtf8.Pointer, size);
    }

    public static bool Button(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igButton(labelUtf8.Pointer, new Vector2(0f, 0f));
    }

    public static float CalcItemWidth()
    {
        return ImGuiNative.igCalcItemWidth();
    }

    public static Vector2 CalcTextSize(string text, bool hideTextAfterDoubleHash, float wrapWidth)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igCalcTextSize(textUtf8.Pointer, null, hideTextAfterDoubleHash, wrapWidth);
    }

    public static Vector2 CalcTextSize(string text, bool hideTextAfterDoubleHash)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igCalcTextSize(textUtf8.Pointer, null, hideTextAfterDoubleHash, -1f);
    }

    public static Vector2 CalcTextSize(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igCalcTextSize(textUtf8.Pointer, null, false, -1f);
    }

    public static bool Checkbox(string label, ref bool v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* vPointer = &v)
        {
            return ImGuiNative.igCheckbox(labelUtf8.Pointer, vPointer);
        }
    }

    public static bool CheckboxFlags(string label, ref int flags, int flagsValue)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* flagsPointer = &flags)
        {
            return ImGuiNative.igCheckboxFlags_IntPtr(labelUtf8.Pointer, flagsPointer, flagsValue);
        }
    }

    public static bool CheckboxFlags(string label, ref uint flags, uint flagsValue)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (uint* flagsPointer = &flags)
        {
            return ImGuiNative.igCheckboxFlags_UintPtr(labelUtf8.Pointer, flagsPointer, flagsValue);
        }
    }

    public static void CloseCurrentPopup()
    {
        ImGuiNative.igCloseCurrentPopup();
    }

    public static bool CollapsingHeader(string label, ImGuiTreeNodeFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igCollapsingHeader_TreeNodeFlags(labelUtf8.Pointer, flags);
    }

    public static bool CollapsingHeader(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igCollapsingHeader_TreeNodeFlags(labelUtf8.Pointer, (ImGuiTreeNodeFlags)(0));
    }

    public static bool CollapsingHeader(string label, ref bool pVisible, ImGuiTreeNodeFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pVisiblePointer = &pVisible)
        {
            return ImGuiNative.igCollapsingHeader_BoolPtr(labelUtf8.Pointer, pVisiblePointer, flags);
        }
    }

    public static bool CollapsingHeader(string label, ref bool pVisible)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pVisiblePointer = &pVisible)
        {
            return ImGuiNative.igCollapsingHeader_BoolPtr(labelUtf8.Pointer, pVisiblePointer, (ImGuiTreeNodeFlags)(0));
        }
    }

    public static bool ColorButton(string descId, Vector4 col, ImGuiColorEditFlags flags, Vector2 size)
    {
        using var descIdUtf8 = new ImGuiUtf8String(descId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igColorButton(descIdUtf8.Pointer, col, flags, size);
    }

    public static bool ColorButton(string descId, Vector4 col, ImGuiColorEditFlags flags)
    {
        using var descIdUtf8 = new ImGuiUtf8String(descId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igColorButton(descIdUtf8.Pointer, col, flags, new Vector2(0f, 0f));
    }

    public static bool ColorButton(string descId, Vector4 col)
    {
        using var descIdUtf8 = new ImGuiUtf8String(descId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igColorButton(descIdUtf8.Pointer, col, (ImGuiColorEditFlags)(0), new Vector2(0f, 0f));
    }

    public static uint ColorConvertFloat4ToU32(Vector4 @in)
    {
        return ImGuiNative.igColorConvertFloat4ToU32(@in);
    }

    public static void ColorConvertHSVtoRGB(float h, float s, float v, ref float outR, ref float outG, ref float outB)
    {
        fixed (float* outRPointer = &outR)
        fixed (float* outGPointer = &outG)
        fixed (float* outBPointer = &outB)
        {
            ImGuiNative.igColorConvertHSVtoRGB(h, s, v, outRPointer, outGPointer, outBPointer);
        }
    }

    public static void ColorConvertRGBtoHSV(float r, float g, float b, ref float outH, ref float outS, ref float outV)
    {
        fixed (float* outHPointer = &outH)
        fixed (float* outSPointer = &outS)
        fixed (float* outVPointer = &outV)
        {
            ImGuiNative.igColorConvertRGBtoHSV(r, g, b, outHPointer, outSPointer, outVPointer);
        }
    }

    public static Vector4 ColorConvertU32ToFloat4(uint @in)
    {
        return ImGuiNative.igColorConvertU32ToFloat4(@in);
    }

    public static bool ColorEdit3(string label, ref Vector3 col, ImGuiColorEditFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* colPointer = &col)
        {
            return ImGuiNative.igColorEdit3(labelUtf8.Pointer, (float*)colPointer, flags);
        }
    }

    public static bool ColorEdit3(string label, ref Vector3 col)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* colPointer = &col)
        {
            return ImGuiNative.igColorEdit3(labelUtf8.Pointer, (float*)colPointer, (ImGuiColorEditFlags)(0));
        }
    }

    public static bool ColorEdit4(string label, ref Vector4 col, ImGuiColorEditFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* colPointer = &col)
        {
            return ImGuiNative.igColorEdit4(labelUtf8.Pointer, (float*)colPointer, flags);
        }
    }

    public static bool ColorEdit4(string label, ref Vector4 col)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* colPointer = &col)
        {
            return ImGuiNative.igColorEdit4(labelUtf8.Pointer, (float*)colPointer, (ImGuiColorEditFlags)(0));
        }
    }

    public static bool ColorPicker3(string label, ref Vector3 col, ImGuiColorEditFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* colPointer = &col)
        {
            return ImGuiNative.igColorPicker3(labelUtf8.Pointer, (float*)colPointer, flags);
        }
    }

    public static bool ColorPicker3(string label, ref Vector3 col)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* colPointer = &col)
        {
            return ImGuiNative.igColorPicker3(labelUtf8.Pointer, (float*)colPointer, (ImGuiColorEditFlags)(0));
        }
    }

    public static bool ColorPicker4(string label, ref Vector4 col, ImGuiColorEditFlags flags, float* refCol)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* colPointer = &col)
        {
            return ImGuiNative.igColorPicker4(labelUtf8.Pointer, (float*)colPointer, flags, refCol);
        }
    }

    public static bool ColorPicker4(string label, ref Vector4 col, ImGuiColorEditFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* colPointer = &col)
        {
            return ImGuiNative.igColorPicker4(labelUtf8.Pointer, (float*)colPointer, flags, null);
        }
    }

    public static bool ColorPicker4(string label, ref Vector4 col)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* colPointer = &col)
        {
            return ImGuiNative.igColorPicker4(labelUtf8.Pointer, (float*)colPointer, (ImGuiColorEditFlags)(0), null);
        }
    }

    public static void Columns(int count, string id, bool borders)
    {
        using var idUtf8 = new ImGuiUtf8String(id, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igColumns(count, idUtf8.Pointer, borders);
    }

    public static void Columns(int count, string id)
    {
        using var idUtf8 = new ImGuiUtf8String(id, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igColumns(count, idUtf8.Pointer, true);
    }

    public static void Columns(int count)
    {
        ImGuiNative.igColumns(count, null, true);
    }

    public static void Columns()
    {
        ImGuiNative.igColumns(1, null, true);
    }

    public static bool Combo(string label, ref int currentItem, string[] items, int popupMaxHeightInItems)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var itemsUtf8 = new ImGuiUtf8StringArray(items);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igCombo_Str_arr(labelUtf8.Pointer, currentItemPointer, itemsUtf8.Pointer, items.Length, popupMaxHeightInItems);
        }
    }

    public static bool Combo(string label, ref int currentItem, string[] items)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var itemsUtf8 = new ImGuiUtf8StringArray(items);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igCombo_Str_arr(labelUtf8.Pointer, currentItemPointer, itemsUtf8.Pointer, items.Length, -1);
        }
    }

    public static bool Combo(string label, ref int currentItem, string itemsSeparatedByZeros, int popupMaxHeightInItems)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var itemsSeparatedByZerosUtf8 = new ImGuiUtf8String(itemsSeparatedByZeros, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igCombo_Str(labelUtf8.Pointer, currentItemPointer, itemsSeparatedByZerosUtf8.Pointer, popupMaxHeightInItems);
        }
    }

    public static bool Combo(string label, ref int currentItem, string itemsSeparatedByZeros)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var itemsSeparatedByZerosUtf8 = new ImGuiUtf8String(itemsSeparatedByZeros, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igCombo_Str(labelUtf8.Pointer, currentItemPointer, itemsSeparatedByZerosUtf8.Pointer, -1);
        }
    }

    public static bool Combo(string label, ref int currentItem, void* getter, void* userData, int itemsCount, int popupMaxHeightInItems)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igCombo_FnStrPtr(labelUtf8.Pointer, currentItemPointer, getter, userData, itemsCount, popupMaxHeightInItems);
        }
    }

    public static bool Combo(string label, ref int currentItem, void* getter, void* userData, int itemsCount)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igCombo_FnStrPtr(labelUtf8.Pointer, currentItemPointer, getter, userData, itemsCount, -1);
        }
    }

    public static ImGuiContext* CreateContext(ImFontAtlasPtr sharedFontAtlas)
    {
        return ImGuiNative.igCreateContext(sharedFontAtlas.Handle);
    }

    public static ImGuiContext* CreateContext()
    {
        return ImGuiNative.igCreateContext(null);
    }

    public static bool DebugCheckVersionAndDataLayout(string versionStr, nuint szIo, nuint szStyle, nuint szVec2, nuint szVec4, nuint szDrawvert, nuint szDrawidx)
    {
        using var versionStrUtf8 = new ImGuiUtf8String(versionStr, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDebugCheckVersionAndDataLayout(versionStrUtf8.Pointer, szIo, szStyle, szVec2, szVec4, szDrawvert, szDrawidx);
    }

    public static void DebugFlashStyleColor(ImGuiCol idx)
    {
        ImGuiNative.igDebugFlashStyleColor(idx);
    }

    public static void DebugLog(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igDebugLog(textUtf8.Pointer);
    }

    public static void DebugStartItemPicker()
    {
        ImGuiNative.igDebugStartItemPicker();
    }

    public static void DebugTextEncoding(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igDebugTextEncoding(textUtf8.Pointer);
    }

    public static void DestroyContext(ImGuiContext* ctx)
    {
        ImGuiNative.igDestroyContext(ctx);
    }

    public static void DestroyContext()
    {
        ImGuiNative.igDestroyContext(null);
    }

    public static void DestroyPlatformWindows()
    {
        ImGuiNative.igDestroyPlatformWindows();
    }

    public static uint DockSpace(uint dockspaceId, Vector2 size, ImGuiDockNodeFlags flags, ImGuiWindowClassPtr windowClass)
    {
        return ImGuiNative.igDockSpace(dockspaceId, size, flags, windowClass.Handle);
    }

    public static uint DockSpace(uint dockspaceId, Vector2 size, ImGuiDockNodeFlags flags)
    {
        return ImGuiNative.igDockSpace(dockspaceId, size, flags, null);
    }

    public static uint DockSpace(uint dockspaceId, Vector2 size)
    {
        return ImGuiNative.igDockSpace(dockspaceId, size, (ImGuiDockNodeFlags)(0), null);
    }

    public static uint DockSpace(uint dockspaceId)
    {
        return ImGuiNative.igDockSpace(dockspaceId, new Vector2(0f, 0f), (ImGuiDockNodeFlags)(0), null);
    }

    public static uint DockSpaceOverViewport(uint dockspaceId, ImGuiViewportPtr viewport, ImGuiDockNodeFlags flags, ImGuiWindowClassPtr windowClass)
    {
        return ImGuiNative.igDockSpaceOverViewport(dockspaceId, viewport.Handle, flags, windowClass.Handle);
    }

    public static uint DockSpaceOverViewport(uint dockspaceId, ImGuiViewportPtr viewport, ImGuiDockNodeFlags flags)
    {
        return ImGuiNative.igDockSpaceOverViewport(dockspaceId, viewport.Handle, flags, null);
    }

    public static uint DockSpaceOverViewport(uint dockspaceId, ImGuiViewportPtr viewport)
    {
        return ImGuiNative.igDockSpaceOverViewport(dockspaceId, viewport.Handle, (ImGuiDockNodeFlags)(0), null);
    }

    public static uint DockSpaceOverViewport(uint dockspaceId)
    {
        return ImGuiNative.igDockSpaceOverViewport(dockspaceId, null, (ImGuiDockNodeFlags)(0), null);
    }

    public static uint DockSpaceOverViewport()
    {
        return ImGuiNative.igDockSpaceOverViewport(0u, null, (ImGuiDockNodeFlags)(0), null);
    }

    public static bool DragFloat(string label, ref float v, float vSpeed, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igDragFloat(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragFloat(string label, ref float v, float vSpeed, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igDragFloat(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat(string label, ref float v, float vSpeed, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igDragFloat(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat(string label, ref float v, float vSpeed, float vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igDragFloat(labelUtf8.Pointer, vPointer, vSpeed, vMin, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat(string label, ref float v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igDragFloat(labelUtf8.Pointer, vPointer, vSpeed, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat(string label, ref float v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igDragFloat(labelUtf8.Pointer, vPointer, 1f, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat2(string label, ref Vector2 v, float vSpeed, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igDragFloat2(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragFloat2(string label, ref Vector2 v, float vSpeed, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igDragFloat2(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat2(string label, ref Vector2 v, float vSpeed, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igDragFloat2(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat2(string label, ref Vector2 v, float vSpeed, float vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igDragFloat2(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat2(string label, ref Vector2 v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igDragFloat2(labelUtf8.Pointer, (float*)vPointer, vSpeed, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat2(string label, ref Vector2 v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igDragFloat2(labelUtf8.Pointer, (float*)vPointer, 1f, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat3(string label, ref Vector3 v, float vSpeed, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igDragFloat3(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragFloat3(string label, ref Vector3 v, float vSpeed, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igDragFloat3(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat3(string label, ref Vector3 v, float vSpeed, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igDragFloat3(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat3(string label, ref Vector3 v, float vSpeed, float vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igDragFloat3(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat3(string label, ref Vector3 v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igDragFloat3(labelUtf8.Pointer, (float*)vPointer, vSpeed, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat3(string label, ref Vector3 v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igDragFloat3(labelUtf8.Pointer, (float*)vPointer, 1f, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat4(string label, ref Vector4 v, float vSpeed, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igDragFloat4(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragFloat4(string label, ref Vector4 v, float vSpeed, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igDragFloat4(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat4(string label, ref Vector4 v, float vSpeed, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igDragFloat4(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat4(string label, ref Vector4 v, float vSpeed, float vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igDragFloat4(labelUtf8.Pointer, (float*)vPointer, vSpeed, vMin, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat4(string label, ref Vector4 v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igDragFloat4(labelUtf8.Pointer, (float*)vPointer, vSpeed, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloat4(string label, ref Vector4 v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igDragFloat4(labelUtf8.Pointer, (float*)vPointer, 1f, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed, float vMin, float vMax, string format, string formatMax, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatMaxUtf8 = new ImGuiUtf8String(formatMax, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, formatMaxUtf8.Pointer, flags);
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed, float vMin, float vMax, string format, string formatMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatMaxUtf8 = new ImGuiUtf8String(formatMax, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, formatMaxUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed, float vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, 0f, ImGuiUtf8.Literal("%.3f\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vCurrentMinPointer = &vCurrentMin)
        fixed (float* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragFloatRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, 1f, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt(string label, ref int v, float vSpeed, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igDragInt(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragInt(string label, ref int v, float vSpeed, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igDragInt(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt(string label, ref int v, float vSpeed, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igDragInt(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt(string label, ref int v, float vSpeed, int vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igDragInt(labelUtf8.Pointer, vPointer, vSpeed, vMin, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt(string label, ref int v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igDragInt(labelUtf8.Pointer, vPointer, vSpeed, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt(string label, ref int v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igDragInt(labelUtf8.Pointer, vPointer, 1f, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt2(string label, Span<int> v, float vSpeed, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt2(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragInt2(string label, Span<int> v, float vSpeed, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt2(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt2(string label, Span<int> v, float vSpeed, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt2(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt2(string label, Span<int> v, float vSpeed, int vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt2(labelUtf8.Pointer, vPointer, vSpeed, vMin, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt2(string label, Span<int> v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt2(labelUtf8.Pointer, vPointer, vSpeed, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt2(string label, Span<int> v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt2(labelUtf8.Pointer, vPointer, 1f, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt3(string label, Span<int> v, float vSpeed, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt3(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragInt3(string label, Span<int> v, float vSpeed, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt3(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt3(string label, Span<int> v, float vSpeed, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt3(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt3(string label, Span<int> v, float vSpeed, int vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt3(labelUtf8.Pointer, vPointer, vSpeed, vMin, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt3(string label, Span<int> v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt3(labelUtf8.Pointer, vPointer, vSpeed, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt3(string label, Span<int> v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt3(labelUtf8.Pointer, vPointer, 1f, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt4(string label, Span<int> v, float vSpeed, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt4(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool DragInt4(string label, Span<int> v, float vSpeed, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt4(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt4(string label, Span<int> v, float vSpeed, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt4(labelUtf8.Pointer, vPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt4(string label, Span<int> v, float vSpeed, int vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt4(labelUtf8.Pointer, vPointer, vSpeed, vMin, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt4(string label, Span<int> v, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt4(labelUtf8.Pointer, vPointer, vSpeed, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragInt4(string label, Span<int> v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igDragInt4(labelUtf8.Pointer, vPointer, 1f, 0, 0, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed, int vMin, int vMax, string format, string formatMax, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatMaxUtf8 = new ImGuiUtf8String(formatMax, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, formatMaxUtf8.Pointer, flags);
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed, int vMin, int vMax, string format, string formatMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatMaxUtf8 = new ImGuiUtf8String(formatMax, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, formatMaxUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, formatUtf8.Pointer, null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed, int vMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, vMin, 0, ImGuiUtf8.Literal("%d\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, vSpeed, 0, 0, ImGuiUtf8.Literal("%d\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vCurrentMinPointer = &vCurrentMin)
        fixed (int* vCurrentMaxPointer = &vCurrentMax)
        {
            return ImGuiNative.igDragIntRange2(labelUtf8.Pointer, vCurrentMinPointer, vCurrentMaxPointer, 1f, 0, 0, ImGuiUtf8.Literal("%d\0"u8), null, (ImGuiSliderFlags)(0));
        }
    }

    public static bool DragScalar(string label, ImGuiDataType dataType, void* pData, float vSpeed, void* pMin, void* pMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalar(labelUtf8.Pointer, dataType, pData, vSpeed, pMin, pMax, formatUtf8.Pointer, flags);
    }

    public static bool DragScalar(string label, ImGuiDataType dataType, void* pData, float vSpeed, void* pMin, void* pMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalar(labelUtf8.Pointer, dataType, pData, vSpeed, pMin, pMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalar(string label, ImGuiDataType dataType, void* pData, float vSpeed, void* pMin, void* pMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalar(labelUtf8.Pointer, dataType, pData, vSpeed, pMin, pMax, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalar(string label, ImGuiDataType dataType, void* pData, float vSpeed, void* pMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalar(labelUtf8.Pointer, dataType, pData, vSpeed, pMin, null, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalar(string label, ImGuiDataType dataType, void* pData, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalar(labelUtf8.Pointer, dataType, pData, vSpeed, null, null, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalar(string label, ImGuiDataType dataType, void* pData)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalar(labelUtf8.Pointer, dataType, pData, 1f, null, null, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalarN(string label, ImGuiDataType dataType, void* pData, int components, float vSpeed, void* pMin, void* pMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalarN(labelUtf8.Pointer, dataType, pData, components, vSpeed, pMin, pMax, formatUtf8.Pointer, flags);
    }

    public static bool DragScalarN(string label, ImGuiDataType dataType, void* pData, int components, float vSpeed, void* pMin, void* pMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalarN(labelUtf8.Pointer, dataType, pData, components, vSpeed, pMin, pMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalarN(string label, ImGuiDataType dataType, void* pData, int components, float vSpeed, void* pMin, void* pMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalarN(labelUtf8.Pointer, dataType, pData, components, vSpeed, pMin, pMax, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalarN(string label, ImGuiDataType dataType, void* pData, int components, float vSpeed, void* pMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalarN(labelUtf8.Pointer, dataType, pData, components, vSpeed, pMin, null, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalarN(string label, ImGuiDataType dataType, void* pData, int components, float vSpeed)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalarN(labelUtf8.Pointer, dataType, pData, components, vSpeed, null, null, null, (ImGuiSliderFlags)(0));
    }

    public static bool DragScalarN(string label, ImGuiDataType dataType, void* pData, int components)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igDragScalarN(labelUtf8.Pointer, dataType, pData, components, 1f, null, null, null, (ImGuiSliderFlags)(0));
    }

    public static void Dummy(Vector2 size)
    {
        ImGuiNative.igDummy(size);
    }

    public static void End()
    {
        ImGuiNative.igEnd();
    }

    public static void EndChild()
    {
        ImGuiNative.igEndChild();
    }

    public static void EndCombo()
    {
        ImGuiNative.igEndCombo();
    }

    public static void EndDisabled()
    {
        ImGuiNative.igEndDisabled();
    }

    public static void EndDragDropSource()
    {
        ImGuiNative.igEndDragDropSource();
    }

    public static void EndDragDropTarget()
    {
        ImGuiNative.igEndDragDropTarget();
    }

    public static void EndFrame()
    {
        ImGuiNative.igEndFrame();
    }

    public static void EndGroup()
    {
        ImGuiNative.igEndGroup();
    }

    public static void EndListBox()
    {
        ImGuiNative.igEndListBox();
    }

    public static void EndMainMenuBar()
    {
        ImGuiNative.igEndMainMenuBar();
    }

    public static void EndMenu()
    {
        ImGuiNative.igEndMenu();
    }

    public static void EndMenuBar()
    {
        ImGuiNative.igEndMenuBar();
    }

    public static ImGuiMultiSelectIOPtr EndMultiSelect()
    {
        return new ImGuiMultiSelectIOPtr(ImGuiNative.igEndMultiSelect());
    }

    public static void EndPopup()
    {
        ImGuiNative.igEndPopup();
    }

    public static void EndTabBar()
    {
        ImGuiNative.igEndTabBar();
    }

    public static void EndTabItem()
    {
        ImGuiNative.igEndTabItem();
    }

    public static void EndTable()
    {
        ImGuiNative.igEndTable();
    }

    public static void EndTooltip()
    {
        ImGuiNative.igEndTooltip();
    }

    public static ImGuiViewportPtr FindViewportByID(uint viewportId)
    {
        return new ImGuiViewportPtr(ImGuiNative.igFindViewportByID(viewportId));
    }

    public static ImGuiViewportPtr FindViewportByPlatformHandle(void* platformHandle)
    {
        return new ImGuiViewportPtr(ImGuiNative.igFindViewportByPlatformHandle(platformHandle));
    }

    public static void GetAllocatorFunctions(void** pAllocFunc, void** pFreeFunc, void** pUserData)
    {
        ImGuiNative.igGetAllocatorFunctions(pAllocFunc, pFreeFunc, pUserData);
    }

    public static ImDrawListPtr GetBackgroundDrawList(ImGuiViewportPtr viewport)
    {
        return new ImDrawListPtr(ImGuiNative.igGetBackgroundDrawList(viewport.Handle));
    }

    public static ImDrawListPtr GetBackgroundDrawList()
    {
        return new ImDrawListPtr(ImGuiNative.igGetBackgroundDrawList(null));
    }

    public static string GetClipboardText()
    {
        return ImGuiUtf8.ToString(ImGuiNative.igGetClipboardText());
    }

    public static uint GetColorU32(ImGuiCol idx, float alphaMul)
    {
        return ImGuiNative.igGetColorU32_Col(idx, alphaMul);
    }

    public static uint GetColorU32(ImGuiCol idx)
    {
        return ImGuiNative.igGetColorU32_Col(idx, 1f);
    }

    public static uint GetColorU32(Vector4 col)
    {
        return ImGuiNative.igGetColorU32_Vec4(col);
    }

    public static uint GetColorU32(uint col, float alphaMul)
    {
        return ImGuiNative.igGetColorU32_U32(col, alphaMul);
    }

    public static uint GetColorU32(uint col)
    {
        return ImGuiNative.igGetColorU32_U32(col, 1f);
    }

    public static int GetColumnIndex()
    {
        return ImGuiNative.igGetColumnIndex();
    }

    public static float GetColumnOffset(int columnIndex)
    {
        return ImGuiNative.igGetColumnOffset(columnIndex);
    }

    public static float GetColumnOffset()
    {
        return ImGuiNative.igGetColumnOffset(-1);
    }

    public static float GetColumnWidth(int columnIndex)
    {
        return ImGuiNative.igGetColumnWidth(columnIndex);
    }

    public static float GetColumnWidth()
    {
        return ImGuiNative.igGetColumnWidth(-1);
    }

    public static int GetColumnsCount()
    {
        return ImGuiNative.igGetColumnsCount();
    }

    public static Vector2 GetContentRegionAvail()
    {
        return ImGuiNative.igGetContentRegionAvail();
    }

    public static ImGuiContext* GetCurrentContext()
    {
        return ImGuiNative.igGetCurrentContext();
    }

    public static Vector2 GetCursorPos()
    {
        return ImGuiNative.igGetCursorPos();
    }

    public static float GetCursorPosX()
    {
        return ImGuiNative.igGetCursorPosX();
    }

    public static float GetCursorPosY()
    {
        return ImGuiNative.igGetCursorPosY();
    }

    public static Vector2 GetCursorScreenPos()
    {
        return ImGuiNative.igGetCursorScreenPos();
    }

    public static Vector2 GetCursorStartPos()
    {
        return ImGuiNative.igGetCursorStartPos();
    }

    public static ImGuiPayloadPtr GetDragDropPayload()
    {
        return new ImGuiPayloadPtr(ImGuiNative.igGetDragDropPayload());
    }

    public static ImDrawDataPtr GetDrawData()
    {
        return new ImDrawDataPtr(ImGuiNative.igGetDrawData());
    }

    public static ImDrawListSharedData* GetDrawListSharedData()
    {
        return ImGuiNative.igGetDrawListSharedData();
    }

    public static ImFontPtr GetFont()
    {
        return new ImFontPtr(ImGuiNative.igGetFont());
    }

    public static ImFontBakedPtr GetFontBaked()
    {
        return new ImFontBakedPtr(ImGuiNative.igGetFontBaked());
    }

    public static float GetFontSize()
    {
        return ImGuiNative.igGetFontSize();
    }

    public static Vector2 GetFontTexUvWhitePixel()
    {
        return ImGuiNative.igGetFontTexUvWhitePixel();
    }

    public static ImDrawListPtr GetForegroundDrawList(ImGuiViewportPtr viewport)
    {
        return new ImDrawListPtr(ImGuiNative.igGetForegroundDrawList(viewport.Handle));
    }

    public static ImDrawListPtr GetForegroundDrawList()
    {
        return new ImDrawListPtr(ImGuiNative.igGetForegroundDrawList(null));
    }

    public static int GetFrameCount()
    {
        return ImGuiNative.igGetFrameCount();
    }

    public static float GetFrameHeight()
    {
        return ImGuiNative.igGetFrameHeight();
    }

    public static float GetFrameHeightWithSpacing()
    {
        return ImGuiNative.igGetFrameHeightWithSpacing();
    }

    public static uint GetID(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igGetID_Str(strIdUtf8.Pointer);
    }

    public static uint GetID(void* ptrId)
    {
        return ImGuiNative.igGetID_Ptr(ptrId);
    }

    public static uint GetID(int intId)
    {
        return ImGuiNative.igGetID_Int(intId);
    }

    public static ImGuiIOPtr GetIO()
    {
        return new ImGuiIOPtr(ImGuiNative.igGetIO());
    }

    public static int GetItemClickedCountWithSingleClickDelay(ImGuiMouseButton mouseButton, float delay)
    {
        return ImGuiNative.igGetItemClickedCountWithSingleClickDelay(mouseButton, delay);
    }

    public static int GetItemClickedCountWithSingleClickDelay(ImGuiMouseButton mouseButton)
    {
        return ImGuiNative.igGetItemClickedCountWithSingleClickDelay(mouseButton, -1f);
    }

    public static int GetItemClickedCountWithSingleClickDelay()
    {
        return ImGuiNative.igGetItemClickedCountWithSingleClickDelay((ImGuiMouseButton)(0), -1f);
    }

    public static ImGuiItemFlags GetItemFlags()
    {
        return ImGuiNative.igGetItemFlags();
    }

    public static uint GetItemID()
    {
        return ImGuiNative.igGetItemID();
    }

    public static Vector2 GetItemRectMax()
    {
        return ImGuiNative.igGetItemRectMax();
    }

    public static Vector2 GetItemRectMin()
    {
        return ImGuiNative.igGetItemRectMin();
    }

    public static Vector2 GetItemRectSize()
    {
        return ImGuiNative.igGetItemRectSize();
    }

    public static string GetKeyName(ImGuiKey key)
    {
        return ImGuiUtf8.ToString(ImGuiNative.igGetKeyName(key));
    }

    public static int GetKeyPressedAmount(ImGuiKey key, float repeatDelay, float rate)
    {
        return ImGuiNative.igGetKeyPressedAmount(key, repeatDelay, rate);
    }

    public static ImGuiViewportPtr GetMainViewport()
    {
        return new ImGuiViewportPtr(ImGuiNative.igGetMainViewport());
    }

    public static int GetMouseClickedCount(ImGuiMouseButton button)
    {
        return ImGuiNative.igGetMouseClickedCount(button);
    }

    public static ImGuiMouseCursor GetMouseCursor()
    {
        return ImGuiNative.igGetMouseCursor();
    }

    public static Vector2 GetMouseDragDelta(ImGuiMouseButton button, float lockThreshold)
    {
        return ImGuiNative.igGetMouseDragDelta(button, lockThreshold);
    }

    public static Vector2 GetMouseDragDelta(ImGuiMouseButton button)
    {
        return ImGuiNative.igGetMouseDragDelta(button, -1f);
    }

    public static Vector2 GetMouseDragDelta()
    {
        return ImGuiNative.igGetMouseDragDelta((ImGuiMouseButton)(0), -1f);
    }

    public static Vector2 GetMousePos()
    {
        return ImGuiNative.igGetMousePos();
    }

    public static Vector2 GetMousePosOnOpeningCurrentPopup()
    {
        return ImGuiNative.igGetMousePosOnOpeningCurrentPopup();
    }

    public static ImGuiPlatformIOPtr GetPlatformIO()
    {
        return new ImGuiPlatformIOPtr(ImGuiNative.igGetPlatformIO());
    }

    public static float GetScrollMaxX()
    {
        return ImGuiNative.igGetScrollMaxX();
    }

    public static float GetScrollMaxY()
    {
        return ImGuiNative.igGetScrollMaxY();
    }

    public static float GetScrollX()
    {
        return ImGuiNative.igGetScrollX();
    }

    public static float GetScrollY()
    {
        return ImGuiNative.igGetScrollY();
    }

    public static ImGuiStoragePtr GetStateStorage()
    {
        return new ImGuiStoragePtr(ImGuiNative.igGetStateStorage());
    }

    public static ImGuiStylePtr GetStyle()
    {
        return new ImGuiStylePtr(ImGuiNative.igGetStyle());
    }

    public static string GetStyleColorName(ImGuiCol idx)
    {
        return ImGuiUtf8.ToString(ImGuiNative.igGetStyleColorName(idx));
    }

    public static Vector4* GetStyleColorVec4(ImGuiCol idx)
    {
        return ImGuiNative.igGetStyleColorVec4(idx);
    }

    public static float GetTextLineHeight()
    {
        return ImGuiNative.igGetTextLineHeight();
    }

    public static float GetTextLineHeightWithSpacing()
    {
        return ImGuiNative.igGetTextLineHeightWithSpacing();
    }

    public static double GetTime()
    {
        return ImGuiNative.igGetTime();
    }

    public static float GetTreeNodeToLabelSpacing()
    {
        return ImGuiNative.igGetTreeNodeToLabelSpacing();
    }

    public static string GetVersion()
    {
        return ImGuiUtf8.ToString(ImGuiNative.igGetVersion());
    }

    public static uint GetWindowDockID()
    {
        return ImGuiNative.igGetWindowDockID();
    }

    public static float GetWindowDpiScale()
    {
        return ImGuiNative.igGetWindowDpiScale();
    }

    public static ImDrawListPtr GetWindowDrawList()
    {
        return new ImDrawListPtr(ImGuiNative.igGetWindowDrawList());
    }

    public static float GetWindowHeight()
    {
        return ImGuiNative.igGetWindowHeight();
    }

    public static Vector2 GetWindowPos()
    {
        return ImGuiNative.igGetWindowPos();
    }

    public static Vector2 GetWindowSize()
    {
        return ImGuiNative.igGetWindowSize();
    }

    public static ImGuiViewportPtr GetWindowViewport()
    {
        return new ImGuiViewportPtr(ImGuiNative.igGetWindowViewport());
    }

    public static float GetWindowWidth()
    {
        return ImGuiNative.igGetWindowWidth();
    }

    public static void Image(ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1)
    {
        ImGuiNative.igImage(texRef, imageSize, uv0, uv1);
    }

    public static void Image(ImTextureRef texRef, Vector2 imageSize, Vector2 uv0)
    {
        ImGuiNative.igImage(texRef, imageSize, uv0, new Vector2(1f, 1f));
    }

    public static void Image(ImTextureRef texRef, Vector2 imageSize)
    {
        ImGuiNative.igImage(texRef, imageSize, new Vector2(0f, 0f), new Vector2(1f, 1f));
    }

    public static bool ImageButton(string strId, ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1, Vector4 bgCol, Vector4 tintCol)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igImageButton(strIdUtf8.Pointer, texRef, imageSize, uv0, uv1, bgCol, tintCol);
    }

    public static bool ImageButton(string strId, ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1, Vector4 bgCol)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igImageButton(strIdUtf8.Pointer, texRef, imageSize, uv0, uv1, bgCol, new Vector4(1f, 1f, 1f, 1f));
    }

    public static bool ImageButton(string strId, ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igImageButton(strIdUtf8.Pointer, texRef, imageSize, uv0, uv1, new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
    }

    public static bool ImageButton(string strId, ImTextureRef texRef, Vector2 imageSize, Vector2 uv0)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igImageButton(strIdUtf8.Pointer, texRef, imageSize, uv0, new Vector2(1f, 1f), new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
    }

    public static bool ImageButton(string strId, ImTextureRef texRef, Vector2 imageSize)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igImageButton(strIdUtf8.Pointer, texRef, imageSize, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
    }

    public static void ImageWithBg(ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1, Vector4 bgCol, Vector4 tintCol)
    {
        ImGuiNative.igImageWithBg(texRef, imageSize, uv0, uv1, bgCol, tintCol);
    }

    public static void ImageWithBg(ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1, Vector4 bgCol)
    {
        ImGuiNative.igImageWithBg(texRef, imageSize, uv0, uv1, bgCol, new Vector4(1f, 1f, 1f, 1f));
    }

    public static void ImageWithBg(ImTextureRef texRef, Vector2 imageSize, Vector2 uv0, Vector2 uv1)
    {
        ImGuiNative.igImageWithBg(texRef, imageSize, uv0, uv1, new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
    }

    public static void ImageWithBg(ImTextureRef texRef, Vector2 imageSize, Vector2 uv0)
    {
        ImGuiNative.igImageWithBg(texRef, imageSize, uv0, new Vector2(1f, 1f), new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
    }

    public static void ImageWithBg(ImTextureRef texRef, Vector2 imageSize)
    {
        ImGuiNative.igImageWithBg(texRef, imageSize, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
    }

    public static void Indent(float indentW)
    {
        ImGuiNative.igIndent(indentW);
    }

    public static void Indent()
    {
        ImGuiNative.igIndent(0f);
    }

    public static bool InputDouble(string label, ref double v, double step, double stepFast, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (double* vPointer = &v)
        {
            return ImGuiNative.igInputDouble(labelUtf8.Pointer, vPointer, step, stepFast, formatUtf8.Pointer, flags);
        }
    }

    public static bool InputDouble(string label, ref double v, double step, double stepFast, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (double* vPointer = &v)
        {
            return ImGuiNative.igInputDouble(labelUtf8.Pointer, vPointer, step, stepFast, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputDouble(string label, ref double v, double step, double stepFast)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (double* vPointer = &v)
        {
            return ImGuiNative.igInputDouble(labelUtf8.Pointer, vPointer, step, stepFast, ImGuiUtf8.Literal("%.6f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputDouble(string label, ref double v, double step)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (double* vPointer = &v)
        {
            return ImGuiNative.igInputDouble(labelUtf8.Pointer, vPointer, step, 0d, ImGuiUtf8.Literal("%.6f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputDouble(string label, ref double v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (double* vPointer = &v)
        {
            return ImGuiNative.igInputDouble(labelUtf8.Pointer, vPointer, 0d, 0d, ImGuiUtf8.Literal("%.6f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat(string label, ref float v, float step, float stepFast, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igInputFloat(labelUtf8.Pointer, vPointer, step, stepFast, formatUtf8.Pointer, flags);
        }
    }

    public static bool InputFloat(string label, ref float v, float step, float stepFast, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igInputFloat(labelUtf8.Pointer, vPointer, step, stepFast, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat(string label, ref float v, float step, float stepFast)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igInputFloat(labelUtf8.Pointer, vPointer, step, stepFast, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat(string label, ref float v, float step)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igInputFloat(labelUtf8.Pointer, vPointer, step, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat(string label, ref float v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igInputFloat(labelUtf8.Pointer, vPointer, 0f, 0f, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat2(string label, ref Vector2 v, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igInputFloat2(labelUtf8.Pointer, (float*)vPointer, formatUtf8.Pointer, flags);
        }
    }

    public static bool InputFloat2(string label, ref Vector2 v, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igInputFloat2(labelUtf8.Pointer, (float*)vPointer, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat2(string label, ref Vector2 v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igInputFloat2(labelUtf8.Pointer, (float*)vPointer, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat3(string label, ref Vector3 v, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igInputFloat3(labelUtf8.Pointer, (float*)vPointer, formatUtf8.Pointer, flags);
        }
    }

    public static bool InputFloat3(string label, ref Vector3 v, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igInputFloat3(labelUtf8.Pointer, (float*)vPointer, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat3(string label, ref Vector3 v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igInputFloat3(labelUtf8.Pointer, (float*)vPointer, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat4(string label, ref Vector4 v, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igInputFloat4(labelUtf8.Pointer, (float*)vPointer, formatUtf8.Pointer, flags);
        }
    }

    public static bool InputFloat4(string label, ref Vector4 v, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igInputFloat4(labelUtf8.Pointer, (float*)vPointer, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputFloat4(string label, ref Vector4 v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igInputFloat4(labelUtf8.Pointer, (float*)vPointer, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputInt(string label, ref int v, int step, int stepFast, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igInputInt(labelUtf8.Pointer, vPointer, step, stepFast, flags);
        }
    }

    public static bool InputInt(string label, ref int v, int step, int stepFast)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igInputInt(labelUtf8.Pointer, vPointer, step, stepFast, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputInt(string label, ref int v, int step)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igInputInt(labelUtf8.Pointer, vPointer, step, 100, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputInt(string label, ref int v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igInputInt(labelUtf8.Pointer, vPointer, 1, 100, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputInt2(string label, Span<int> v, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igInputInt2(labelUtf8.Pointer, vPointer, flags);
        }
    }

    public static bool InputInt2(string label, Span<int> v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igInputInt2(labelUtf8.Pointer, vPointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputInt3(string label, Span<int> v, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igInputInt3(labelUtf8.Pointer, vPointer, flags);
        }
    }

    public static bool InputInt3(string label, Span<int> v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igInputInt3(labelUtf8.Pointer, vPointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputInt4(string label, Span<int> v, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igInputInt4(labelUtf8.Pointer, vPointer, flags);
        }
    }

    public static bool InputInt4(string label, Span<int> v)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igInputInt4(labelUtf8.Pointer, vPointer, (ImGuiInputTextFlags)(0));
        }
    }

    public static bool InputScalar(string label, ImGuiDataType dataType, void* pData, void* pStep, void* pStepFast, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalar(labelUtf8.Pointer, dataType, pData, pStep, pStepFast, formatUtf8.Pointer, flags);
    }

    public static bool InputScalar(string label, ImGuiDataType dataType, void* pData, void* pStep, void* pStepFast, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalar(labelUtf8.Pointer, dataType, pData, pStep, pStepFast, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalar(string label, ImGuiDataType dataType, void* pData, void* pStep, void* pStepFast)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalar(labelUtf8.Pointer, dataType, pData, pStep, pStepFast, null, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalar(string label, ImGuiDataType dataType, void* pData, void* pStep)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalar(labelUtf8.Pointer, dataType, pData, pStep, null, null, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalar(string label, ImGuiDataType dataType, void* pData)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalar(labelUtf8.Pointer, dataType, pData, null, null, null, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pStep, void* pStepFast, string format, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalarN(labelUtf8.Pointer, dataType, pData, components, pStep, pStepFast, formatUtf8.Pointer, flags);
    }

    public static bool InputScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pStep, void* pStepFast, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalarN(labelUtf8.Pointer, dataType, pData, components, pStep, pStepFast, formatUtf8.Pointer, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pStep, void* pStepFast)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalarN(labelUtf8.Pointer, dataType, pData, components, pStep, pStepFast, null, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pStep)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalarN(labelUtf8.Pointer, dataType, pData, components, pStep, null, null, (ImGuiInputTextFlags)(0));
    }

    public static bool InputScalarN(string label, ImGuiDataType dataType, void* pData, int components)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInputScalarN(labelUtf8.Pointer, dataType, pData, components, null, null, null, (ImGuiInputTextFlags)(0));
    }

    public static bool InputText(string label, Span<byte> buf, ImGuiInputTextFlags flags, void* callback, void* userData)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputText(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, flags, callback, userData);
        }
    }

    public static bool InputText(string label, Span<byte> buf, ImGuiInputTextFlags flags, void* callback)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputText(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, flags, callback, null);
        }
    }

    public static bool InputText(string label, Span<byte> buf, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputText(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, flags, null, null);
        }
    }

    public static bool InputText(string label, Span<byte> buf)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputText(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, (ImGuiInputTextFlags)(0), null, null);
        }
    }

    public static bool InputTextMultiline(string label, Span<byte> buf, Vector2 size, ImGuiInputTextFlags flags, void* callback, void* userData)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextMultiline(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, size, flags, callback, userData);
        }
    }

    public static bool InputTextMultiline(string label, Span<byte> buf, Vector2 size, ImGuiInputTextFlags flags, void* callback)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextMultiline(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, size, flags, callback, null);
        }
    }

    public static bool InputTextMultiline(string label, Span<byte> buf, Vector2 size, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextMultiline(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, size, flags, null, null);
        }
    }

    public static bool InputTextMultiline(string label, Span<byte> buf, Vector2 size)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextMultiline(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, size, (ImGuiInputTextFlags)(0), null, null);
        }
    }

    public static bool InputTextMultiline(string label, Span<byte> buf)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextMultiline(labelUtf8.Pointer, bufPointer, (nuint)buf.Length, new Vector2(0f, 0f), (ImGuiInputTextFlags)(0), null, null);
        }
    }

    public static bool InputTextWithHint(string label, string hint, Span<byte> buf, ImGuiInputTextFlags flags, void* callback, void* userData)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var hintUtf8 = new ImGuiUtf8String(hint, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextWithHint(labelUtf8.Pointer, hintUtf8.Pointer, bufPointer, (nuint)buf.Length, flags, callback, userData);
        }
    }

    public static bool InputTextWithHint(string label, string hint, Span<byte> buf, ImGuiInputTextFlags flags, void* callback)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var hintUtf8 = new ImGuiUtf8String(hint, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextWithHint(labelUtf8.Pointer, hintUtf8.Pointer, bufPointer, (nuint)buf.Length, flags, callback, null);
        }
    }

    public static bool InputTextWithHint(string label, string hint, Span<byte> buf, ImGuiInputTextFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var hintUtf8 = new ImGuiUtf8String(hint, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextWithHint(labelUtf8.Pointer, hintUtf8.Pointer, bufPointer, (nuint)buf.Length, flags, null, null);
        }
    }

    public static bool InputTextWithHint(string label, string hint, Span<byte> buf)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var hintUtf8 = new ImGuiUtf8String(hint, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (byte* bufPointer = buf)
        {
            return ImGuiNative.igInputTextWithHint(labelUtf8.Pointer, hintUtf8.Pointer, bufPointer, (nuint)buf.Length, (ImGuiInputTextFlags)(0), null, null);
        }
    }

    public static bool InvisibleButton(string strId, Vector2 size, ImGuiButtonFlags flags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInvisibleButton(strIdUtf8.Pointer, size, flags);
    }

    public static bool InvisibleButton(string strId, Vector2 size)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igInvisibleButton(strIdUtf8.Pointer, size, (ImGuiButtonFlags)(0));
    }

    public static bool IsAnyItemActive()
    {
        return ImGuiNative.igIsAnyItemActive();
    }

    public static bool IsAnyItemFocused()
    {
        return ImGuiNative.igIsAnyItemFocused();
    }

    public static bool IsAnyItemHovered()
    {
        return ImGuiNative.igIsAnyItemHovered();
    }

    public static bool IsAnyMouseDown()
    {
        return ImGuiNative.igIsAnyMouseDown();
    }

    public static bool IsItemActivated()
    {
        return ImGuiNative.igIsItemActivated();
    }

    public static bool IsItemActive()
    {
        return ImGuiNative.igIsItemActive();
    }

    public static bool IsItemClicked(ImGuiMouseButton mouseButton)
    {
        return ImGuiNative.igIsItemClicked(mouseButton);
    }

    public static bool IsItemClicked()
    {
        return ImGuiNative.igIsItemClicked((ImGuiMouseButton)(0));
    }

    public static bool IsItemDeactivated()
    {
        return ImGuiNative.igIsItemDeactivated();
    }

    public static bool IsItemDeactivatedAfterEdit()
    {
        return ImGuiNative.igIsItemDeactivatedAfterEdit();
    }

    public static bool IsItemEdited()
    {
        return ImGuiNative.igIsItemEdited();
    }

    public static bool IsItemFocused()
    {
        return ImGuiNative.igIsItemFocused();
    }

    public static bool IsItemHovered(ImGuiHoveredFlags flags)
    {
        return ImGuiNative.igIsItemHovered(flags);
    }

    public static bool IsItemHovered()
    {
        return ImGuiNative.igIsItemHovered((ImGuiHoveredFlags)(0));
    }

    public static bool IsItemToggledOpen()
    {
        return ImGuiNative.igIsItemToggledOpen();
    }

    public static bool IsItemToggledSelection()
    {
        return ImGuiNative.igIsItemToggledSelection();
    }

    public static bool IsItemVisible()
    {
        return ImGuiNative.igIsItemVisible();
    }

    public static bool IsKeyChordPressed(int keyChord)
    {
        return ImGuiNative.igIsKeyChordPressed(keyChord);
    }

    public static bool IsKeyDown(ImGuiKey key)
    {
        return ImGuiNative.igIsKeyDown(key);
    }

    public static bool IsKeyPressed(ImGuiKey key, bool repeat)
    {
        return ImGuiNative.igIsKeyPressed(key, repeat);
    }

    public static bool IsKeyPressed(ImGuiKey key)
    {
        return ImGuiNative.igIsKeyPressed(key, true);
    }

    public static bool IsKeyReleased(ImGuiKey key)
    {
        return ImGuiNative.igIsKeyReleased(key);
    }

    public static bool IsMouseClicked(ImGuiMouseButton button, bool repeat)
    {
        return ImGuiNative.igIsMouseClicked(button, repeat);
    }

    public static bool IsMouseClicked(ImGuiMouseButton button)
    {
        return ImGuiNative.igIsMouseClicked(button, false);
    }

    public static bool IsMouseDoubleClicked(ImGuiMouseButton button)
    {
        return ImGuiNative.igIsMouseDoubleClicked(button);
    }

    public static bool IsMouseDown(ImGuiMouseButton button)
    {
        return ImGuiNative.igIsMouseDown(button);
    }

    public static bool IsMouseDragging(ImGuiMouseButton button, float lockThreshold)
    {
        return ImGuiNative.igIsMouseDragging(button, lockThreshold);
    }

    public static bool IsMouseDragging(ImGuiMouseButton button)
    {
        return ImGuiNative.igIsMouseDragging(button, -1f);
    }

    public static bool IsMouseHoveringRect(Vector2 rMin, Vector2 rMax, bool clip)
    {
        return ImGuiNative.igIsMouseHoveringRect(rMin, rMax, clip);
    }

    public static bool IsMouseHoveringRect(Vector2 rMin, Vector2 rMax)
    {
        return ImGuiNative.igIsMouseHoveringRect(rMin, rMax, true);
    }

    public static bool IsMousePosValid(Vector2* mousePos)
    {
        return ImGuiNative.igIsMousePosValid(mousePos);
    }

    public static bool IsMousePosValid()
    {
        return ImGuiNative.igIsMousePosValid(null);
    }

    public static bool IsMouseReleased(ImGuiMouseButton button)
    {
        return ImGuiNative.igIsMouseReleased(button);
    }

    public static bool IsMouseReleasedWithDelay(ImGuiMouseButton button, float delay)
    {
        return ImGuiNative.igIsMouseReleasedWithDelay(button, delay);
    }

    public static bool IsMouseReleasedWithDelay(ImGuiMouseButton button)
    {
        return ImGuiNative.igIsMouseReleasedWithDelay(button, -1f);
    }

    public static bool IsPopupOpen(string strId, ImGuiPopupFlags flags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igIsPopupOpen(strIdUtf8.Pointer, flags);
    }

    public static bool IsPopupOpen(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igIsPopupOpen(strIdUtf8.Pointer, (ImGuiPopupFlags)(0));
    }

    public static bool IsRectVisible(Vector2 size)
    {
        return ImGuiNative.igIsRectVisible_Nil(size);
    }

    public static bool IsRectVisible(Vector2 rectMin, Vector2 rectMax)
    {
        return ImGuiNative.igIsRectVisible_Vec2(rectMin, rectMax);
    }

    public static bool IsWindowAppearing()
    {
        return ImGuiNative.igIsWindowAppearing();
    }

    public static bool IsWindowCollapsed()
    {
        return ImGuiNative.igIsWindowCollapsed();
    }

    public static bool IsWindowDocked()
    {
        return ImGuiNative.igIsWindowDocked();
    }

    public static bool IsWindowFocused(ImGuiFocusedFlags flags)
    {
        return ImGuiNative.igIsWindowFocused(flags);
    }

    public static bool IsWindowFocused()
    {
        return ImGuiNative.igIsWindowFocused((ImGuiFocusedFlags)(0));
    }

    public static bool IsWindowHovered(ImGuiHoveredFlags flags)
    {
        return ImGuiNative.igIsWindowHovered(flags);
    }

    public static bool IsWindowHovered()
    {
        return ImGuiNative.igIsWindowHovered((ImGuiHoveredFlags)(0));
    }

    public static void LabelText(string label, string text)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igLabelText(labelUtf8.Pointer, textUtf8.Pointer);
    }

    public static bool ListBox(string label, ref int currentItem, string[] items, int heightInItems)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var itemsUtf8 = new ImGuiUtf8StringArray(items);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igListBox_Str_arr(labelUtf8.Pointer, currentItemPointer, itemsUtf8.Pointer, items.Length, heightInItems);
        }
    }

    public static bool ListBox(string label, ref int currentItem, string[] items)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var itemsUtf8 = new ImGuiUtf8StringArray(items);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igListBox_Str_arr(labelUtf8.Pointer, currentItemPointer, itemsUtf8.Pointer, items.Length, -1);
        }
    }

    public static bool ListBox(string label, ref int currentItem, void* getter, void* userData, int itemsCount, int heightInItems)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igListBox_FnStrPtr(labelUtf8.Pointer, currentItemPointer, getter, userData, itemsCount, heightInItems);
        }
    }

    public static bool ListBox(string label, ref int currentItem, void* getter, void* userData, int itemsCount)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* currentItemPointer = &currentItem)
        {
            return ImGuiNative.igListBox_FnStrPtr(labelUtf8.Pointer, currentItemPointer, getter, userData, itemsCount, -1);
        }
    }

    public static void LoadIniSettingsFromDisk(string iniFilename)
    {
        using var iniFilenameUtf8 = new ImGuiUtf8String(iniFilename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igLoadIniSettingsFromDisk(iniFilenameUtf8.Pointer);
    }

    public static void LoadIniSettingsFromMemory(string iniData, nuint iniSize)
    {
        using var iniDataUtf8 = new ImGuiUtf8String(iniData, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igLoadIniSettingsFromMemory(iniDataUtf8.Pointer, iniSize);
    }

    public static void LoadIniSettingsFromMemory(string iniData)
    {
        using var iniDataUtf8 = new ImGuiUtf8String(iniData, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igLoadIniSettingsFromMemory(iniDataUtf8.Pointer, (nuint)0);
    }

    public static void LogButtons()
    {
        ImGuiNative.igLogButtons();
    }

    public static void LogFinish()
    {
        ImGuiNative.igLogFinish();
    }

    public static void LogText(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igLogText(textUtf8.Pointer);
    }

    public static void LogToClipboard(int autoOpenDepth)
    {
        ImGuiNative.igLogToClipboard(autoOpenDepth);
    }

    public static void LogToClipboard()
    {
        ImGuiNative.igLogToClipboard(-1);
    }

    public static void LogToFile(int autoOpenDepth, string filename)
    {
        using var filenameUtf8 = new ImGuiUtf8String(filename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igLogToFile(autoOpenDepth, filenameUtf8.Pointer);
    }

    public static void LogToFile(int autoOpenDepth)
    {
        ImGuiNative.igLogToFile(autoOpenDepth, null);
    }

    public static void LogToFile()
    {
        ImGuiNative.igLogToFile(-1, null);
    }

    public static void LogToTTY(int autoOpenDepth)
    {
        ImGuiNative.igLogToTTY(autoOpenDepth);
    }

    public static void LogToTTY()
    {
        ImGuiNative.igLogToTTY(-1);
    }

    public static void* MemAlloc(nuint size)
    {
        return ImGuiNative.igMemAlloc(size);
    }

    public static void MemFree(void* ptr)
    {
        ImGuiNative.igMemFree(ptr);
    }

    public static bool MenuItem(string label, string shortcut, bool selected, bool enabled)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var shortcutUtf8 = new ImGuiUtf8String(shortcut, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igMenuItem_Bool(labelUtf8.Pointer, shortcutUtf8.Pointer, selected, enabled);
    }

    public static bool MenuItem(string label, string shortcut, bool selected)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var shortcutUtf8 = new ImGuiUtf8String(shortcut, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igMenuItem_Bool(labelUtf8.Pointer, shortcutUtf8.Pointer, selected, true);
    }

    public static bool MenuItem(string label, string shortcut)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var shortcutUtf8 = new ImGuiUtf8String(shortcut, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igMenuItem_Bool(labelUtf8.Pointer, shortcutUtf8.Pointer, false, true);
    }

    public static bool MenuItem(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igMenuItem_Bool(labelUtf8.Pointer, null, false, true);
    }

    public static bool MenuItem(string label, string shortcut, ref bool pSelected, bool enabled)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var shortcutUtf8 = new ImGuiUtf8String(shortcut, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pSelectedPointer = &pSelected)
        {
            return ImGuiNative.igMenuItem_BoolPtr(labelUtf8.Pointer, shortcutUtf8.Pointer, pSelectedPointer, enabled);
        }
    }

    public static bool MenuItem(string label, string shortcut, ref bool pSelected)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var shortcutUtf8 = new ImGuiUtf8String(shortcut, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pSelectedPointer = &pSelected)
        {
            return ImGuiNative.igMenuItem_BoolPtr(labelUtf8.Pointer, shortcutUtf8.Pointer, pSelectedPointer, true);
        }
    }

    public static void NewFrame()
    {
        ImGuiNative.igNewFrame();
    }

    public static void NewLine()
    {
        ImGuiNative.igNewLine();
    }

    public static void NextColumn()
    {
        ImGuiNative.igNextColumn();
    }

    public static bool OpenPopup(string strId, ImGuiPopupFlags popupFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igOpenPopup_Str(strIdUtf8.Pointer, popupFlags);
    }

    public static bool OpenPopup(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igOpenPopup_Str(strIdUtf8.Pointer, (ImGuiPopupFlags)(0));
    }

    public static bool OpenPopup(uint id, ImGuiPopupFlags popupFlags)
    {
        return ImGuiNative.igOpenPopup_ID(id, popupFlags);
    }

    public static bool OpenPopup(uint id)
    {
        return ImGuiNative.igOpenPopup_ID(id, (ImGuiPopupFlags)(0));
    }

    public static bool OpenPopupOnItemClick(string strId, ImGuiPopupFlags popupFlags)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igOpenPopupOnItemClick(strIdUtf8.Pointer, popupFlags);
    }

    public static bool OpenPopupOnItemClick(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igOpenPopupOnItemClick(strIdUtf8.Pointer, (ImGuiPopupFlags)(0));
    }

    public static bool OpenPopupOnItemClick()
    {
        return ImGuiNative.igOpenPopupOnItemClick(null, (ImGuiPopupFlags)(0));
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin, float scaleMax, Vector2 graphSize, int stride)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, graphSize, stride);
        }
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin, float scaleMax, Vector2 graphSize)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, graphSize, (int)sizeof(float));
        }
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin, float scaleMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, float.MaxValue, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotHistogram(string label, ReadOnlySpan<float> values)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotHistogram_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, 0, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotHistogram(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText, float scaleMin, float scaleMax, Vector2 graphSize)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotHistogram_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, graphSize);
    }

    public static void PlotHistogram(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText, float scaleMin, float scaleMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotHistogram_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, new Vector2(0f, 0f));
    }

    public static void PlotHistogram(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText, float scaleMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotHistogram_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, scaleMin, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotHistogram(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotHistogram_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, float.MaxValue, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotHistogram(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotHistogram_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotHistogram(string label, void* valuesGetter, void* data, int valuesCount)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotHistogram_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, 0, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin, float scaleMax, Vector2 graphSize, int stride)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, graphSize, stride);
        }
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin, float scaleMax, Vector2 graphSize)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, graphSize, (int)sizeof(float));
        }
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin, float scaleMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText, float scaleMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, scaleMin, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset, string overlayText)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, overlayTextUtf8.Pointer, float.MaxValue, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, valuesOffset, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotLines(string label, ReadOnlySpan<float> values)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* valuesPointer = values)
        {
            ImGuiNative.igPlotLines_FloatPtr(labelUtf8.Pointer, valuesPointer, values.Length, 0, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f), (int)sizeof(float));
        }
    }

    public static void PlotLines(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText, float scaleMin, float scaleMax, Vector2 graphSize)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotLines_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, graphSize);
    }

    public static void PlotLines(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText, float scaleMin, float scaleMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotLines_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, scaleMin, scaleMax, new Vector2(0f, 0f));
    }

    public static void PlotLines(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText, float scaleMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotLines_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, scaleMin, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotLines(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset, string overlayText)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var overlayTextUtf8 = new ImGuiUtf8String(overlayText, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotLines_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, overlayTextUtf8.Pointer, float.MaxValue, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotLines(string label, void* valuesGetter, void* data, int valuesCount, int valuesOffset)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotLines_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, valuesOffset, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PlotLines(string label, void* valuesGetter, void* data, int valuesCount)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPlotLines_FnFloatPtr(labelUtf8.Pointer, valuesGetter, data, valuesCount, 0, null, float.MaxValue, float.MaxValue, new Vector2(0f, 0f));
    }

    public static void PopClipRect()
    {
        ImGuiNative.igPopClipRect();
    }

    public static void PopFont()
    {
        ImGuiNative.igPopFont();
    }

    public static void PopID()
    {
        ImGuiNative.igPopID();
    }

    public static void PopItemFlag()
    {
        ImGuiNative.igPopItemFlag();
    }

    public static void PopItemWidth()
    {
        ImGuiNative.igPopItemWidth();
    }

    public static void PopStyleColor(int count)
    {
        ImGuiNative.igPopStyleColor(count);
    }

    public static void PopStyleColor()
    {
        ImGuiNative.igPopStyleColor(1);
    }

    public static void PopStyleVar(int count)
    {
        ImGuiNative.igPopStyleVar(count);
    }

    public static void PopStyleVar()
    {
        ImGuiNative.igPopStyleVar(1);
    }

    public static void PopTextWrapPos()
    {
        ImGuiNative.igPopTextWrapPos();
    }

    public static void ProgressBar(float fraction, Vector2 sizeArg, string overlay)
    {
        using var overlayUtf8 = new ImGuiUtf8String(overlay, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igProgressBar(fraction, sizeArg, overlayUtf8.Pointer);
    }

    public static void ProgressBar(float fraction, Vector2 sizeArg)
    {
        ImGuiNative.igProgressBar(fraction, sizeArg, null);
    }

    public static void ProgressBar(float fraction)
    {
        ImGuiNative.igProgressBar(fraction, new Vector2(-1.17549435E-38f, 0f), null);
    }

    public static void PushClipRect(Vector2 clipRectMin, Vector2 clipRectMax, bool intersectWithCurrentClipRect)
    {
        ImGuiNative.igPushClipRect(clipRectMin, clipRectMax, intersectWithCurrentClipRect);
    }

    public static void PushFont(ImFontPtr font, float fontSizeBaseUnscaled)
    {
        ImGuiNative.igPushFont(font.Handle, fontSizeBaseUnscaled);
    }

    public static void PushID(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igPushID_Str(strIdUtf8.Pointer);
    }

    public static void PushID(void* ptrId)
    {
        ImGuiNative.igPushID_Ptr(ptrId);
    }

    public static void PushID(int intId)
    {
        ImGuiNative.igPushID_Int(intId);
    }

    public static void PushItemFlag(ImGuiItemFlags option, bool enabled)
    {
        ImGuiNative.igPushItemFlag(option, enabled);
    }

    public static void PushItemWidth(float itemWidth)
    {
        ImGuiNative.igPushItemWidth(itemWidth);
    }

    public static void PushStyleColor(ImGuiCol idx, uint col)
    {
        ImGuiNative.igPushStyleColor_U32(idx, col);
    }

    public static void PushStyleColor(ImGuiCol idx, Vector4 col)
    {
        ImGuiNative.igPushStyleColor_Vec4(idx, col);
    }

    public static void PushStyleVar(ImGuiStyleVar idx, float val)
    {
        ImGuiNative.igPushStyleVar_Float(idx, val);
    }

    public static void PushStyleVar(ImGuiStyleVar idx, Vector2 val)
    {
        ImGuiNative.igPushStyleVar_Vec2(idx, val);
    }

    public static void PushStyleVarX(ImGuiStyleVar idx, float valX)
    {
        ImGuiNative.igPushStyleVarX(idx, valX);
    }

    public static void PushStyleVarY(ImGuiStyleVar idx, float valY)
    {
        ImGuiNative.igPushStyleVarY(idx, valY);
    }

    public static void PushTextWrapPos(float wrapLocalPosX)
    {
        ImGuiNative.igPushTextWrapPos(wrapLocalPosX);
    }

    public static void PushTextWrapPos()
    {
        ImGuiNative.igPushTextWrapPos(0f);
    }

    public static bool RadioButton(string label, bool active)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igRadioButton_Bool(labelUtf8.Pointer, active);
    }

    public static bool RadioButton(string label, ref int v, int vButton)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igRadioButton_IntPtr(labelUtf8.Pointer, vPointer, vButton);
        }
    }

    public static void Render()
    {
        ImGuiNative.igRender();
    }

    public static void RenderPlatformWindowsDefault(void* platformRenderArg, void* rendererRenderArg)
    {
        ImGuiNative.igRenderPlatformWindowsDefault(platformRenderArg, rendererRenderArg);
    }

    public static void RenderPlatformWindowsDefault(void* platformRenderArg)
    {
        ImGuiNative.igRenderPlatformWindowsDefault(platformRenderArg, null);
    }

    public static void RenderPlatformWindowsDefault()
    {
        ImGuiNative.igRenderPlatformWindowsDefault(null, null);
    }

    public static void ResetMouseDragDelta(ImGuiMouseButton button)
    {
        ImGuiNative.igResetMouseDragDelta(button);
    }

    public static void ResetMouseDragDelta()
    {
        ImGuiNative.igResetMouseDragDelta((ImGuiMouseButton)(0));
    }

    public static void SameLine(float offsetFromStartX, float spacing)
    {
        ImGuiNative.igSameLine(offsetFromStartX, spacing);
    }

    public static void SameLine(float offsetFromStartX)
    {
        ImGuiNative.igSameLine(offsetFromStartX, -1f);
    }

    public static void SameLine()
    {
        ImGuiNative.igSameLine(0f, -1f);
    }

    public static void SaveIniSettingsToDisk(string iniFilename)
    {
        using var iniFilenameUtf8 = new ImGuiUtf8String(iniFilename, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSaveIniSettingsToDisk(iniFilenameUtf8.Pointer);
    }

    public static string SaveIniSettingsToMemory(ref nuint outIniSize)
    {
        fixed (nuint* outIniSizePointer = &outIniSize)
        {
            return ImGuiUtf8.ToString(ImGuiNative.igSaveIniSettingsToMemory(outIniSizePointer));
        }
    }

    public static string SaveIniSettingsToMemory()
    {
        return ImGuiUtf8.ToString(ImGuiNative.igSaveIniSettingsToMemory(null));
    }

    public static bool Selectable(string label, bool selected, ImGuiSelectableFlags flags, Vector2 size)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSelectable_Bool(labelUtf8.Pointer, selected, flags, size);
    }

    public static bool Selectable(string label, bool selected, ImGuiSelectableFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSelectable_Bool(labelUtf8.Pointer, selected, flags, new Vector2(0f, 0f));
    }

    public static bool Selectable(string label, bool selected)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSelectable_Bool(labelUtf8.Pointer, selected, (ImGuiSelectableFlags)(0), new Vector2(0f, 0f));
    }

    public static bool Selectable(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSelectable_Bool(labelUtf8.Pointer, false, (ImGuiSelectableFlags)(0), new Vector2(0f, 0f));
    }

    public static bool Selectable(string label, ref bool pSelected, ImGuiSelectableFlags flags, Vector2 size)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pSelectedPointer = &pSelected)
        {
            return ImGuiNative.igSelectable_BoolPtr(labelUtf8.Pointer, pSelectedPointer, flags, size);
        }
    }

    public static bool Selectable(string label, ref bool pSelected, ImGuiSelectableFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pSelectedPointer = &pSelected)
        {
            return ImGuiNative.igSelectable_BoolPtr(labelUtf8.Pointer, pSelectedPointer, flags, new Vector2(0f, 0f));
        }
    }

    public static bool Selectable(string label, ref bool pSelected)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (bool* pSelectedPointer = &pSelected)
        {
            return ImGuiNative.igSelectable_BoolPtr(labelUtf8.Pointer, pSelectedPointer, (ImGuiSelectableFlags)(0), new Vector2(0f, 0f));
        }
    }

    public static void Separator()
    {
        ImGuiNative.igSeparator();
    }

    public static void SeparatorText(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSeparatorText(labelUtf8.Pointer);
    }

    public static void SetAllocatorFunctions(void* allocFunc, void* freeFunc, void* userData)
    {
        ImGuiNative.igSetAllocatorFunctions(allocFunc, freeFunc, userData);
    }

    public static void SetAllocatorFunctions(void* allocFunc, void* freeFunc)
    {
        ImGuiNative.igSetAllocatorFunctions(allocFunc, freeFunc, null);
    }

    public static void SetClipboardText(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetClipboardText(textUtf8.Pointer);
    }

    public static void SetColumnOffset(int columnIndex, float offsetX)
    {
        ImGuiNative.igSetColumnOffset(columnIndex, offsetX);
    }

    public static void SetColumnWidth(int columnIndex, float width)
    {
        ImGuiNative.igSetColumnWidth(columnIndex, width);
    }

    public static void SetCurrentContext(ImGuiContext* ctx)
    {
        ImGuiNative.igSetCurrentContext(ctx);
    }

    public static void SetCursorPos(Vector2 localPos)
    {
        ImGuiNative.igSetCursorPos(localPos);
    }

    public static void SetCursorPosX(float localX)
    {
        ImGuiNative.igSetCursorPosX(localX);
    }

    public static void SetCursorPosY(float localY)
    {
        ImGuiNative.igSetCursorPosY(localY);
    }

    public static void SetCursorScreenPos(Vector2 pos)
    {
        ImGuiNative.igSetCursorScreenPos(pos);
    }

    public static bool SetDragDropPayload(string type, void* data, nuint sz, ImGuiCond cond)
    {
        using var typeUtf8 = new ImGuiUtf8String(type, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSetDragDropPayload(typeUtf8.Pointer, data, sz, cond);
    }

    public static bool SetDragDropPayload(string type, void* data, nuint sz)
    {
        using var typeUtf8 = new ImGuiUtf8String(type, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSetDragDropPayload(typeUtf8.Pointer, data, sz, (ImGuiCond)(0));
    }

    public static void SetItemDefaultFocus()
    {
        ImGuiNative.igSetItemDefaultFocus();
    }

    public static bool SetItemKeyOwner(ImGuiKey key)
    {
        return ImGuiNative.igSetItemKeyOwner(key);
    }

    public static void SetItemTooltip(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igSetItemTooltip(textUtf8.Pointer);
    }

    public static void SetKeyboardFocusHere(int offset)
    {
        ImGuiNative.igSetKeyboardFocusHere(offset);
    }

    public static void SetKeyboardFocusHere()
    {
        ImGuiNative.igSetKeyboardFocusHere(0);
    }

    public static void SetMouseCursor(ImGuiMouseCursor cursorType)
    {
        ImGuiNative.igSetMouseCursor(cursorType);
    }

    public static void SetNavCursorVisible(bool visible)
    {
        ImGuiNative.igSetNavCursorVisible(visible);
    }

    public static void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard)
    {
        ImGuiNative.igSetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);
    }

    public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse)
    {
        ImGuiNative.igSetNextFrameWantCaptureMouse(wantCaptureMouse);
    }

    public static void SetNextItemAllowOverlap()
    {
        ImGuiNative.igSetNextItemAllowOverlap();
    }

    public static void SetNextItemOpen(bool isOpen, ImGuiCond cond)
    {
        ImGuiNative.igSetNextItemOpen(isOpen, cond);
    }

    public static void SetNextItemOpen(bool isOpen)
    {
        ImGuiNative.igSetNextItemOpen(isOpen, (ImGuiCond)(0));
    }

    public static void SetNextItemSelectionUserData(long selectionUserData)
    {
        ImGuiNative.igSetNextItemSelectionUserData(selectionUserData);
    }

    public static void SetNextItemShortcut(int keyChord, ImGuiInputFlags flags)
    {
        ImGuiNative.igSetNextItemShortcut(keyChord, flags);
    }

    public static void SetNextItemShortcut(int keyChord)
    {
        ImGuiNative.igSetNextItemShortcut(keyChord, (ImGuiInputFlags)(0));
    }

    public static void SetNextItemStorageID(uint storageId)
    {
        ImGuiNative.igSetNextItemStorageID(storageId);
    }

    public static void SetNextItemWidth(float itemWidth)
    {
        ImGuiNative.igSetNextItemWidth(itemWidth);
    }

    public static void SetNextWindowBgAlpha(float alpha)
    {
        ImGuiNative.igSetNextWindowBgAlpha(alpha);
    }

    public static void SetNextWindowClass(ImGuiWindowClassPtr windowClass)
    {
        ImGuiNative.igSetNextWindowClass(windowClass.Handle);
    }

    public static void SetNextWindowCollapsed(bool collapsed, ImGuiCond cond)
    {
        ImGuiNative.igSetNextWindowCollapsed(collapsed, cond);
    }

    public static void SetNextWindowCollapsed(bool collapsed)
    {
        ImGuiNative.igSetNextWindowCollapsed(collapsed, (ImGuiCond)(0));
    }

    public static void SetNextWindowContentSize(Vector2 size)
    {
        ImGuiNative.igSetNextWindowContentSize(size);
    }

    public static void SetNextWindowDockID(uint dockId, ImGuiCond cond)
    {
        ImGuiNative.igSetNextWindowDockID(dockId, cond);
    }

    public static void SetNextWindowDockID(uint dockId)
    {
        ImGuiNative.igSetNextWindowDockID(dockId, (ImGuiCond)(0));
    }

    public static void SetNextWindowFocus()
    {
        ImGuiNative.igSetNextWindowFocus();
    }

    public static void SetNextWindowPos(Vector2 pos, ImGuiCond cond, Vector2 pivot)
    {
        ImGuiNative.igSetNextWindowPos(pos, cond, pivot);
    }

    public static void SetNextWindowPos(Vector2 pos, ImGuiCond cond)
    {
        ImGuiNative.igSetNextWindowPos(pos, cond, new Vector2(0f, 0f));
    }

    public static void SetNextWindowPos(Vector2 pos)
    {
        ImGuiNative.igSetNextWindowPos(pos, (ImGuiCond)(0), new Vector2(0f, 0f));
    }

    public static void SetNextWindowScroll(Vector2 scroll)
    {
        ImGuiNative.igSetNextWindowScroll(scroll);
    }

    public static void SetNextWindowSize(Vector2 size, ImGuiCond cond)
    {
        ImGuiNative.igSetNextWindowSize(size, cond);
    }

    public static void SetNextWindowSize(Vector2 size)
    {
        ImGuiNative.igSetNextWindowSize(size, (ImGuiCond)(0));
    }

    public static void SetNextWindowSizeConstraints(Vector2 sizeMin, Vector2 sizeMax, void* customCallback, void* customCallbackData)
    {
        ImGuiNative.igSetNextWindowSizeConstraints(sizeMin, sizeMax, customCallback, customCallbackData);
    }

    public static void SetNextWindowSizeConstraints(Vector2 sizeMin, Vector2 sizeMax, void* customCallback)
    {
        ImGuiNative.igSetNextWindowSizeConstraints(sizeMin, sizeMax, customCallback, null);
    }

    public static void SetNextWindowSizeConstraints(Vector2 sizeMin, Vector2 sizeMax)
    {
        ImGuiNative.igSetNextWindowSizeConstraints(sizeMin, sizeMax, null, null);
    }

    public static void SetNextWindowViewport(uint viewportId)
    {
        ImGuiNative.igSetNextWindowViewport(viewportId);
    }

    public static void SetScrollFromPosX(float localX, float centerXRatio)
    {
        ImGuiNative.igSetScrollFromPosX(localX, centerXRatio);
    }

    public static void SetScrollFromPosX(float localX)
    {
        ImGuiNative.igSetScrollFromPosX(localX, 0.5f);
    }

    public static void SetScrollFromPosY(float localY, float centerYRatio)
    {
        ImGuiNative.igSetScrollFromPosY(localY, centerYRatio);
    }

    public static void SetScrollFromPosY(float localY)
    {
        ImGuiNative.igSetScrollFromPosY(localY, 0.5f);
    }

    public static void SetScrollHereX(float centerXRatio)
    {
        ImGuiNative.igSetScrollHereX(centerXRatio);
    }

    public static void SetScrollHereX()
    {
        ImGuiNative.igSetScrollHereX(0.5f);
    }

    public static void SetScrollHereY(float centerYRatio)
    {
        ImGuiNative.igSetScrollHereY(centerYRatio);
    }

    public static void SetScrollHereY()
    {
        ImGuiNative.igSetScrollHereY(0.5f);
    }

    public static void SetScrollX(float scrollX)
    {
        ImGuiNative.igSetScrollX(scrollX);
    }

    public static void SetScrollY(float scrollY)
    {
        ImGuiNative.igSetScrollY(scrollY);
    }

    public static void SetStateStorage(ImGuiStoragePtr storage)
    {
        ImGuiNative.igSetStateStorage(storage.Handle);
    }

    public static void SetTabItemClosed(string tabOrDockedWindowLabel)
    {
        using var tabOrDockedWindowLabelUtf8 = new ImGuiUtf8String(tabOrDockedWindowLabel, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetTabItemClosed(tabOrDockedWindowLabelUtf8.Pointer);
    }

    public static void SetTooltip(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igSetTooltip(textUtf8.Pointer);
    }

    public static void SetWindowCollapsed(bool collapsed, ImGuiCond cond)
    {
        ImGuiNative.igSetWindowCollapsed_Bool(collapsed, cond);
    }

    public static void SetWindowCollapsed(bool collapsed)
    {
        ImGuiNative.igSetWindowCollapsed_Bool(collapsed, (ImGuiCond)(0));
    }

    public static void SetWindowCollapsed(string name, bool collapsed, ImGuiCond cond)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowCollapsed_Str(nameUtf8.Pointer, collapsed, cond);
    }

    public static void SetWindowCollapsed(string name, bool collapsed)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowCollapsed_Str(nameUtf8.Pointer, collapsed, (ImGuiCond)(0));
    }

    public static void SetWindowFocus()
    {
        ImGuiNative.igSetWindowFocus_Nil();
    }

    public static void SetWindowFocus(string name)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowFocus_Str(nameUtf8.Pointer);
    }

    public static void SetWindowPos(Vector2 pos, ImGuiCond cond)
    {
        ImGuiNative.igSetWindowPos_Vec2(pos, cond);
    }

    public static void SetWindowPos(Vector2 pos)
    {
        ImGuiNative.igSetWindowPos_Vec2(pos, (ImGuiCond)(0));
    }

    public static void SetWindowPos(string name, Vector2 pos, ImGuiCond cond)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowPos_Str(nameUtf8.Pointer, pos, cond);
    }

    public static void SetWindowPos(string name, Vector2 pos)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowPos_Str(nameUtf8.Pointer, pos, (ImGuiCond)(0));
    }

    public static void SetWindowSize(Vector2 size, ImGuiCond cond)
    {
        ImGuiNative.igSetWindowSize_Vec2(size, cond);
    }

    public static void SetWindowSize(Vector2 size)
    {
        ImGuiNative.igSetWindowSize_Vec2(size, (ImGuiCond)(0));
    }

    public static void SetWindowSize(string name, Vector2 size, ImGuiCond cond)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowSize_Str(nameUtf8.Pointer, size, cond);
    }

    public static void SetWindowSize(string name, Vector2 size)
    {
        using var nameUtf8 = new ImGuiUtf8String(name, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igSetWindowSize_Str(nameUtf8.Pointer, size, (ImGuiCond)(0));
    }

    public static bool Shortcut(int keyChord, ImGuiInputFlags flags)
    {
        return ImGuiNative.igShortcut(keyChord, flags);
    }

    public static bool Shortcut(int keyChord)
    {
        return ImGuiNative.igShortcut(keyChord, (ImGuiInputFlags)(0));
    }

    public static void ShowAboutWindow(ref bool pOpen)
    {
        fixed (bool* pOpenPointer = &pOpen)
        {
            ImGuiNative.igShowAboutWindow(pOpenPointer);
        }
    }

    public static void ShowAboutWindow()
    {
        ImGuiNative.igShowAboutWindow(null);
    }

    public static void ShowDebugLogWindow(ref bool pOpen)
    {
        fixed (bool* pOpenPointer = &pOpen)
        {
            ImGuiNative.igShowDebugLogWindow(pOpenPointer);
        }
    }

    public static void ShowDebugLogWindow()
    {
        ImGuiNative.igShowDebugLogWindow(null);
    }

    public static void ShowDemoWindow(ref bool pOpen)
    {
        fixed (bool* pOpenPointer = &pOpen)
        {
            ImGuiNative.igShowDemoWindow(pOpenPointer);
        }
    }

    public static void ShowDemoWindow()
    {
        ImGuiNative.igShowDemoWindow(null);
    }

    public static void ShowFontSelector(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igShowFontSelector(labelUtf8.Pointer);
    }

    public static void ShowIDStackToolWindow(ref bool pOpen)
    {
        fixed (bool* pOpenPointer = &pOpen)
        {
            ImGuiNative.igShowIDStackToolWindow(pOpenPointer);
        }
    }

    public static void ShowIDStackToolWindow()
    {
        ImGuiNative.igShowIDStackToolWindow(null);
    }

    public static void ShowMetricsWindow(ref bool pOpen)
    {
        fixed (bool* pOpenPointer = &pOpen)
        {
            ImGuiNative.igShowMetricsWindow(pOpenPointer);
        }
    }

    public static void ShowMetricsWindow()
    {
        ImGuiNative.igShowMetricsWindow(null);
    }

    public static void ShowStyleEditor(ImGuiStylePtr @ref)
    {
        ImGuiNative.igShowStyleEditor(@ref.Handle);
    }

    public static void ShowStyleEditor()
    {
        ImGuiNative.igShowStyleEditor(null);
    }

    public static bool ShowStyleSelector(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igShowStyleSelector(labelUtf8.Pointer);
    }

    public static void ShowUserGuide()
    {
        ImGuiNative.igShowUserGuide();
    }

    public static bool SliderAngle(string label, ref float vRad, float vDegreesMin, float vDegreesMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vRadPointer = &vRad)
        {
            return ImGuiNative.igSliderAngle(labelUtf8.Pointer, vRadPointer, vDegreesMin, vDegreesMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderAngle(string label, ref float vRad, float vDegreesMin, float vDegreesMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vRadPointer = &vRad)
        {
            return ImGuiNative.igSliderAngle(labelUtf8.Pointer, vRadPointer, vDegreesMin, vDegreesMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderAngle(string label, ref float vRad, float vDegreesMin, float vDegreesMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vRadPointer = &vRad)
        {
            return ImGuiNative.igSliderAngle(labelUtf8.Pointer, vRadPointer, vDegreesMin, vDegreesMax, ImGuiUtf8.Literal("%.0f deg\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderAngle(string label, ref float vRad, float vDegreesMin)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vRadPointer = &vRad)
        {
            return ImGuiNative.igSliderAngle(labelUtf8.Pointer, vRadPointer, vDegreesMin, 360f, ImGuiUtf8.Literal("%.0f deg\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderAngle(string label, ref float vRad)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vRadPointer = &vRad)
        {
            return ImGuiNative.igSliderAngle(labelUtf8.Pointer, vRadPointer, -360f, 360f, ImGuiUtf8.Literal("%.0f deg\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat(string label, ref float v, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderFloat(string label, ref float v, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat(string label, ref float v, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat(labelUtf8.Pointer, vPointer, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat2(string label, ref Vector2 v, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat2(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderFloat2(string label, ref Vector2 v, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat2(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat2(string label, ref Vector2 v, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector2* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat2(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat3(string label, ref Vector3 v, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat3(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderFloat3(string label, ref Vector3 v, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat3(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat3(string label, ref Vector3 v, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector3* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat3(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat4(string label, ref Vector4 v, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat4(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderFloat4(string label, ref Vector4 v, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat4(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderFloat4(string label, ref Vector4 v, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (Vector4* vPointer = &v)
        {
            return ImGuiNative.igSliderFloat4(labelUtf8.Pointer, (float*)vPointer, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt(string label, ref int v, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igSliderInt(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderInt(string label, ref int v, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igSliderInt(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt(string label, ref int v, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igSliderInt(labelUtf8.Pointer, vPointer, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt2(string label, Span<int> v, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt2(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderInt2(string label, Span<int> v, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt2(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt2(string label, Span<int> v, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 2) throw new ArgumentException("v needs 2 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt2(labelUtf8.Pointer, vPointer, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt3(string label, Span<int> v, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt3(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderInt3(string label, Span<int> v, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt3(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt3(string label, Span<int> v, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 3) throw new ArgumentException("v needs 3 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt3(labelUtf8.Pointer, vPointer, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt4(string label, Span<int> v, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt4(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool SliderInt4(string label, Span<int> v, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt4(labelUtf8.Pointer, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderInt4(string label, Span<int> v, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        if (v.Length < 4) throw new ArgumentException("v needs 4 elements.", nameof(v));
        fixed (int* vPointer = v)
        {
            return ImGuiNative.igSliderInt4(labelUtf8.Pointer, vPointer, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool SliderScalar(string label, ImGuiDataType dataType, void* pData, void* pMin, void* pMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSliderScalar(labelUtf8.Pointer, dataType, pData, pMin, pMax, formatUtf8.Pointer, flags);
    }

    public static bool SliderScalar(string label, ImGuiDataType dataType, void* pData, void* pMin, void* pMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSliderScalar(labelUtf8.Pointer, dataType, pData, pMin, pMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
    }

    public static bool SliderScalar(string label, ImGuiDataType dataType, void* pData, void* pMin, void* pMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSliderScalar(labelUtf8.Pointer, dataType, pData, pMin, pMax, null, (ImGuiSliderFlags)(0));
    }

    public static bool SliderScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pMin, void* pMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSliderScalarN(labelUtf8.Pointer, dataType, pData, components, pMin, pMax, formatUtf8.Pointer, flags);
    }

    public static bool SliderScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pMin, void* pMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSliderScalarN(labelUtf8.Pointer, dataType, pData, components, pMin, pMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
    }

    public static bool SliderScalarN(string label, ImGuiDataType dataType, void* pData, int components, void* pMin, void* pMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSliderScalarN(labelUtf8.Pointer, dataType, pData, components, pMin, pMax, null, (ImGuiSliderFlags)(0));
    }

    public static bool SmallButton(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igSmallButton(labelUtf8.Pointer);
    }

    public static void Spacing()
    {
        ImGuiNative.igSpacing();
    }

    public static void StyleColorsClassic(ImGuiStylePtr dst)
    {
        ImGuiNative.igStyleColorsClassic(dst.Handle);
    }

    public static void StyleColorsClassic()
    {
        ImGuiNative.igStyleColorsClassic(null);
    }

    public static void StyleColorsDark(ImGuiStylePtr dst)
    {
        ImGuiNative.igStyleColorsDark(dst.Handle);
    }

    public static void StyleColorsDark()
    {
        ImGuiNative.igStyleColorsDark(null);
    }

    public static void StyleColorsLight(ImGuiStylePtr dst)
    {
        ImGuiNative.igStyleColorsLight(dst.Handle);
    }

    public static void StyleColorsLight()
    {
        ImGuiNative.igStyleColorsLight(null);
    }

    public static bool TabItemButton(string label, ImGuiTabItemFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTabItemButton(labelUtf8.Pointer, flags);
    }

    public static bool TabItemButton(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTabItemButton(labelUtf8.Pointer, (ImGuiTabItemFlags)(0));
    }

    public static void TableAngledHeadersRow()
    {
        ImGuiNative.igTableAngledHeadersRow();
    }

    public static int TableGetColumnCount()
    {
        return ImGuiNative.igTableGetColumnCount();
    }

    public static ImGuiTableColumnFlags TableGetColumnFlags(int columnN)
    {
        return ImGuiNative.igTableGetColumnFlags(columnN);
    }

    public static ImGuiTableColumnFlags TableGetColumnFlags()
    {
        return ImGuiNative.igTableGetColumnFlags(-1);
    }

    public static int TableGetColumnIndex()
    {
        return ImGuiNative.igTableGetColumnIndex();
    }

    public static string TableGetColumnName(int columnN)
    {
        return ImGuiUtf8.ToString(ImGuiNative.igTableGetColumnName(columnN));
    }

    public static string TableGetColumnName()
    {
        return ImGuiUtf8.ToString(ImGuiNative.igTableGetColumnName(-1));
    }

    public static int TableGetHoveredColumn()
    {
        return ImGuiNative.igTableGetHoveredColumn();
    }

    public static int TableGetRowIndex()
    {
        return ImGuiNative.igTableGetRowIndex();
    }

    public static ImGuiTableSortSpecsPtr TableGetSortSpecs()
    {
        return new ImGuiTableSortSpecsPtr(ImGuiNative.igTableGetSortSpecs());
    }

    public static void TableHeader(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTableHeader(labelUtf8.Pointer);
    }

    public static void TableHeadersRow()
    {
        ImGuiNative.igTableHeadersRow();
    }

    public static bool TableNextColumn()
    {
        return ImGuiNative.igTableNextColumn();
    }

    public static void TableNextRow(ImGuiTableRowFlags rowFlags, float minRowHeight)
    {
        ImGuiNative.igTableNextRow(rowFlags, minRowHeight);
    }

    public static void TableNextRow(ImGuiTableRowFlags rowFlags)
    {
        ImGuiNative.igTableNextRow(rowFlags, 0f);
    }

    public static void TableNextRow()
    {
        ImGuiNative.igTableNextRow((ImGuiTableRowFlags)(0), 0f);
    }

    public static void TableSetBgColor(ImGuiTableBgTarget target, uint color, int columnN)
    {
        ImGuiNative.igTableSetBgColor(target, color, columnN);
    }

    public static void TableSetBgColor(ImGuiTableBgTarget target, uint color)
    {
        ImGuiNative.igTableSetBgColor(target, color, -1);
    }

    public static void TableSetColumnEnabled(int columnN, bool v)
    {
        ImGuiNative.igTableSetColumnEnabled(columnN, v);
    }

    public static bool TableSetColumnIndex(int columnN)
    {
        return ImGuiNative.igTableSetColumnIndex(columnN);
    }

    public static void TableSetupColumn(string label, ImGuiTableColumnFlags flags, float initWidthOrWeight, uint userData)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTableSetupColumn(labelUtf8.Pointer, flags, initWidthOrWeight, userData);
    }

    public static void TableSetupColumn(string label, ImGuiTableColumnFlags flags, float initWidthOrWeight)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTableSetupColumn(labelUtf8.Pointer, flags, initWidthOrWeight, 0u);
    }

    public static void TableSetupColumn(string label, ImGuiTableColumnFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTableSetupColumn(labelUtf8.Pointer, flags, 0f, 0u);
    }

    public static void TableSetupColumn(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTableSetupColumn(labelUtf8.Pointer, (ImGuiTableColumnFlags)(0), 0f, 0u);
    }

    public static void TableSetupScrollFreeze(int cols, int rows)
    {
        ImGuiNative.igTableSetupScrollFreeze(cols, rows);
    }

    public static void Text(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igText(textUtf8.Pointer);
    }

    public static void TextColored(Vector4 col, string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igTextColored(col, textUtf8.Pointer);
    }

    public static void TextDisabled(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igTextDisabled(textUtf8.Pointer);
    }

    public static bool TextLink(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTextLink(labelUtf8.Pointer);
    }

    public static bool TextLinkOpenURL(string label, string url)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var urlUtf8 = new ImGuiUtf8String(url, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTextLinkOpenURL(labelUtf8.Pointer, urlUtf8.Pointer);
    }

    public static bool TextLinkOpenURL(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTextLinkOpenURL(labelUtf8.Pointer, null);
    }

    public static void TextUnformatted(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTextUnformatted(textUtf8.Pointer, null);
    }

    public static void TextWrapped(string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.CRYSTAL_igTextWrapped(textUtf8.Pointer);
    }

    public static bool TreeNode(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTreeNode_Str(labelUtf8.Pointer);
    }

    public static bool TreeNode(string strId, string text)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.CRYSTAL_igTreeNode_StrStr(strIdUtf8.Pointer, textUtf8.Pointer);
    }

    public static bool TreeNode(void* ptrId, string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.CRYSTAL_igTreeNode_Ptr(ptrId, textUtf8.Pointer);
    }

    public static bool TreeNodeEx(string label, ImGuiTreeNodeFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTreeNodeEx_Str(labelUtf8.Pointer, flags);
    }

    public static bool TreeNodeEx(string label)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igTreeNodeEx_Str(labelUtf8.Pointer, (ImGuiTreeNodeFlags)(0));
    }

    public static bool TreeNodeEx(string strId, ImGuiTreeNodeFlags flags, string text)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.CRYSTAL_igTreeNodeEx_StrStr(strIdUtf8.Pointer, flags, textUtf8.Pointer);
    }

    public static bool TreeNodeEx(void* ptrId, ImGuiTreeNodeFlags flags, string text)
    {
        using var textUtf8 = new ImGuiUtf8String(text, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.CRYSTAL_igTreeNodeEx_Ptr(ptrId, flags, textUtf8.Pointer);
    }

    public static bool TreeNodeGetOpen(uint storageId)
    {
        return ImGuiNative.igTreeNodeGetOpen(storageId);
    }

    public static void TreePop()
    {
        ImGuiNative.igTreePop();
    }

    public static void TreePush(string strId)
    {
        using var strIdUtf8 = new ImGuiUtf8String(strId, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igTreePush_Str(strIdUtf8.Pointer);
    }

    public static void TreePush(void* ptrId)
    {
        ImGuiNative.igTreePush_Ptr(ptrId);
    }

    public static void Unindent(float indentW)
    {
        ImGuiNative.igUnindent(indentW);
    }

    public static void Unindent()
    {
        ImGuiNative.igUnindent(0f);
    }

    public static void UpdatePlatformWindows()
    {
        ImGuiNative.igUpdatePlatformWindows();
    }

    public static bool VSliderFloat(string label, Vector2 size, ref float v, float vMin, float vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igVSliderFloat(labelUtf8.Pointer, size, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool VSliderFloat(string label, Vector2 size, ref float v, float vMin, float vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igVSliderFloat(labelUtf8.Pointer, size, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool VSliderFloat(string label, Vector2 size, ref float v, float vMin, float vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (float* vPointer = &v)
        {
            return ImGuiNative.igVSliderFloat(labelUtf8.Pointer, size, vPointer, vMin, vMax, ImGuiUtf8.Literal("%.3f\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool VSliderInt(string label, Vector2 size, ref int v, int vMin, int vMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igVSliderInt(labelUtf8.Pointer, size, vPointer, vMin, vMax, formatUtf8.Pointer, flags);
        }
    }

    public static bool VSliderInt(string label, Vector2 size, ref int v, int vMin, int vMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igVSliderInt(labelUtf8.Pointer, size, vPointer, vMin, vMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
        }
    }

    public static bool VSliderInt(string label, Vector2 size, ref int v, int vMin, int vMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        fixed (int* vPointer = &v)
        {
            return ImGuiNative.igVSliderInt(labelUtf8.Pointer, size, vPointer, vMin, vMax, ImGuiUtf8.Literal("%d\0"u8), (ImGuiSliderFlags)(0));
        }
    }

    public static bool VSliderScalar(string label, Vector2 size, ImGuiDataType dataType, void* pData, void* pMin, void* pMax, string format, ImGuiSliderFlags flags)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igVSliderScalar(labelUtf8.Pointer, size, dataType, pData, pMin, pMax, formatUtf8.Pointer, flags);
    }

    public static bool VSliderScalar(string label, Vector2 size, ImGuiDataType dataType, void* pData, void* pMin, void* pMax, string format)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var formatUtf8 = new ImGuiUtf8String(format, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igVSliderScalar(labelUtf8.Pointer, size, dataType, pData, pMin, pMax, formatUtf8.Pointer, (ImGuiSliderFlags)(0));
    }

    public static bool VSliderScalar(string label, Vector2 size, ImGuiDataType dataType, void* pData, void* pMin, void* pMax)
    {
        using var labelUtf8 = new ImGuiUtf8String(label, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        return ImGuiNative.igVSliderScalar(labelUtf8.Pointer, size, dataType, pData, pMin, pMax, null, (ImGuiSliderFlags)(0));
    }

    public static void Value(string prefix, bool b)
    {
        using var prefixUtf8 = new ImGuiUtf8String(prefix, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igValue_Bool(prefixUtf8.Pointer, b);
    }

    public static void Value(string prefix, int v)
    {
        using var prefixUtf8 = new ImGuiUtf8String(prefix, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igValue_Int(prefixUtf8.Pointer, v);
    }

    public static void Value(string prefix, uint v)
    {
        using var prefixUtf8 = new ImGuiUtf8String(prefix, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igValue_Uint(prefixUtf8.Pointer, v);
    }

    public static void Value(string prefix, float v, string floatFormat)
    {
        using var prefixUtf8 = new ImGuiUtf8String(prefix, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        using var floatFormatUtf8 = new ImGuiUtf8String(floatFormat, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igValue_Float(prefixUtf8.Pointer, v, floatFormatUtf8.Pointer);
    }

    public static void Value(string prefix, float v)
    {
        using var prefixUtf8 = new ImGuiUtf8String(prefix, stackalloc byte[ImGuiUtf8String.StackCapacity]);
        ImGuiNative.igValue_Float(prefixUtf8.Pointer, v, null);
    }
}
