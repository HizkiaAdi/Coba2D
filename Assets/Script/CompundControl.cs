using UnityEngine;
using System.Collections.Generic;

public class CompoundControl : MonoBehaviour
{	
	public static Vector2 CScrollView(Rect pos, Vector2 scrollPos, Rect viewPos, List<ViewItem> items)
	{
		scrollPos = GUI.BeginScrollView(pos, scrollPos, viewPos);
		for(int i = 0; i < items.Count; i++)
		{
			GUI.Box(new Rect(0, i * (viewPos.height / items.Count), viewPos.width, viewPos.height / items.Count),
			        "ID: [" + items[i].ID + "] Name: [" + items[i].Name + "]");
		}
		GUI.EndScrollView();

		return scrollPos;
	}
}
