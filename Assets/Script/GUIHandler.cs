using UnityEngine;
using System.Collections.Generic;

public class GUIHandler : MonoBehaviour
{
	private Vector2 viewScroll = Vector2.zero;
	private Vector2 pointer = Vector2.zero;
	private int buttonSellect = 0;
	private List<ViewItem> items;
	private Vector2 scrollPos = Vector2.zero;
	private Rect pos;
	private Rect viewPos;
    private Touch touch;
    private float delta;

	void OnGUI()
	{
		viewScroll = GUI.BeginScrollView(new Rect(25, 25, 200, 300), viewScroll,
		                    new Rect(0, 0, 0, 2700));
		for(int i = 0; i < 100; i++)
			if(GUI.Button(new Rect(0, i*25, 100, 20), "Hello " + i + " World!"))
			{
				buttonSellect = i;
				items.Add(new ViewItem("bt"+i, "button"+i));
                scrollPos.y = items.Count * 25;
			}
		GUI.EndScrollView();

		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 100, 100), "(" + Screen.width + ", " + Screen.height + ")");
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), buttonSellect.ToString());
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 100), viewScroll.ToString());
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 100, 100, 100), scrollPos.y.ToString());
		GUI.Label(new Rect(pointer.x, Screen.height - pointer.y + 15, 100, 100), "(" + pointer.x.ToString() + "," 
		          + (Screen.height - pointer.y).ToString() + ")");

		CompoundControl.CScrollView(pos, scrollPos, viewPos, items);
	}

	// Use this for initialization	
	void Start ()
	{
		pos = new Rect(500, 100, 200, 200);
		viewPos = new Rect(0, 0, 175, 250);
		items = new List<ViewItem>();
        touch = Input.touches[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		pointer = Input.mousePosition;
        delta = Input.touches[0].deltaPosition.y;
		viewPos.height = items.Count * 25;
        //if (touch.phase == TouchPhase.Moved)
        //{
        scrollPos.y += delta;
        //}
	}
}
