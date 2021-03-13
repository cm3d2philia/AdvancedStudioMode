using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class ComboBox2
{
	// Token: 0x06000043 RID: 67 RVA: 0x000B3324 File Offset: 0x000B2324
	public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle listStyle)
	{
		return this.List(rect, new GUIContent(buttonText), listContent, "button", "box", listStyle);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000B335C File Offset: 0x000B235C
	public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
	{
		return this.List(rect, buttonContent, listContent, "button", "box", listStyle);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000B3390 File Offset: 0x000B2390
	public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
	{
		return this.List(rect, new GUIContent(buttonText), listContent, buttonStyle, boxStyle, listStyle);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000B33B8 File Offset: 0x000B23B8
	private int GetPix(int i)
	{
		float num = 1f + ((float)Screen.width / 1280f - 1f) * 0.6f;
		return (int)(num * (float)i);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000B33F0 File Offset: 0x000B23F0
	public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
	{
		if (ComboBox2.forceToUnShow)
		{
			ComboBox2.forceToUnShow = false;
			this.isClickedComboButton = false;
		}
		bool flag = false;
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		EventType typeForControl = Event.current.GetTypeForControl(controlID);
		if (typeForControl == EventType.MouseUp)
		{
			if (this.isClickedComboButton)
			{
				if (this.scrollPosOld == this.scrollPos)
				{
					flag = true;
				}
			}
		}
		if (GUI.Button(rect, buttonContent, buttonStyle))
		{
			if (ComboBox2.useControlID == -1)
			{
				ComboBox2.useControlID = controlID;
				this.isClickedComboButton = false;
			}
			if (ComboBox2.useControlID != controlID)
			{
				ComboBox2.forceToUnShow = true;
				ComboBox2.useControlID = controlID;
			}
			this.isClickedComboButton = true;
		}
		if (this.isClickedComboButton)
		{
			Rect position = new Rect(rect.x, rect.y + (float)this.GetPix(23), rect.width, listStyle.CalcHeight(listContent[0], 1f) * (float)listContent.Length);
			if (position.y + position.height > this.height)
			{
				position.height = this.height - position.y - 2f;
				position.width += 16f;
			}
			GUI.Box(position, "", boxStyle);
			if (Input.GetMouseButtonDown(0))
			{
				this.scrollPosOld = this.scrollPos;
			}
			Rect rect2 = new Rect(rect.x, rect.y + listStyle.CalcHeight(listContent[0], 1f), rect.width, listStyle.CalcHeight(listContent[0], 1f) * (float)listContent.Length);
			this.scrollPos = GUI.BeginScrollView(position, this.scrollPos, rect2);
			int num = GUI.SelectionGrid(rect2, this.selectedItemIndex, listContent, 1, listStyle);
			if (num != this.selectedItemIndex)
			{
				this.selectedItemIndex = num;
			}
			GUI.EndScrollView();
		}
		if (flag)
		{
			this.isClickedComboButton = false;
		}
		return this.GetSelectedItemIndex();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000B3630 File Offset: 0x000B2630
	public int GetSelectedItemIndex()
	{
		return this.selectedItemIndex;
	}

	// Token: 0x0400030D RID: 781
	private static bool forceToUnShow = false;

	// Token: 0x0400030E RID: 782
	private static int useControlID = -1;

	// Token: 0x0400030F RID: 783
	public bool isClickedComboButton = false;

	// Token: 0x04000310 RID: 784
	public float height;

	// Token: 0x04000311 RID: 785
	public int selectedItemIndex = 0;

	// Token: 0x04000312 RID: 786
	public Vector2 scrollPos = new Vector2(0f, 0f);

	// Token: 0x04000313 RID: 787
	private Vector2 scrollPosOld = new Vector2(0f, 0f);
}
