using UnityEngine;
using SocialModule;
using System.Collections.Generic;
using MiniJSON;

public class FacebookGUI : MonoBehaviour
{
    public FacebookHandler FH;
    static float width;
    static float height;
	Rect svPos;
	Rect viewRect;
	Rect svPos2;
	Rect viewRect2;
	Rect modalRect;
	Rect layoutRect;
	Vector2 svScrollPos;
	Vector2 svScrollPos2;
	bool showModal;
	List<string> friends;
	string newFriend;
	FBFriends fbFriends;

	void Start ()
    {
        FH = new FacebookHandler();
		//FH.CallFBInit();
		width = Screen.width;
		height = Screen.height;
		svPos = new Rect(10, 10, width / 2, height - 120);
		viewRect = new Rect(0, 0, width / 2 - 30, height * 2);
		modalRect = new Rect(width / 2 - 150, height / 2 - 200, 300, 500);
		svPos2 = new Rect(10, 10, modalRect.width - 10, modalRect.height - 100);
		layoutRect = new Rect(10, 20, 350, 450);
		svScrollPos = Vector2.zero;
		svScrollPos2 = Vector2.zero;
		showModal = false;

		friends = new List<string>();
		for(int i = 0; i < 25; i++)
		{
			friends.Add("Dummy " + (i+1));
		}

		viewRect2 = new Rect(0, 0, svPos2.width - 35, friends.Count * 25);
		newFriend = string.Empty;

		FH.FeedLink = "http://www.facebook.com/";
		FH.FeedLinkName = "Facebook!";
		FH.FeedLinkDescription = "This is a description";
		FH.FeedLinkCaption = "This is a caption";
	}
	
	void Update ()
    {
		
	}

    void OnGUI()
    {
		#region FB ScrollView
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && 
		   svPos.Contains(new Vector2(Input.GetTouch(0).position.x, height - Input.GetTouch(0).position.y))
		   && !showModal)
			svScrollPos.y += Input.GetTouch(0).deltaPosition.y;

		svScrollPos = GUI.BeginScrollView(svPos, svScrollPos, viewRect);

		if(GUI.Button(new Rect(viewRect.x + 10, 10, viewRect.width - 10, 25), "FB Init"))
		{
			FH.CallFBInit();
		}
		if(GUI.Button(new Rect(viewRect.x + 10, viewRect.y + 50, viewRect.width / 2 - 10, 25), "FB Login"))
		{
			FH.CallFBLogin();
		}
		if(GUI.Button(new Rect(viewRect.width / 2 + 10, viewRect.y + 50, viewRect.width / 2 - 10, 25), "FB Logout"))
		{
			FH.CallFBLogout();
		}
		/*if(GUI.Button(new Rect(viewRect.x + 10, viewRect.y + 50, 125, 25), "FB Publish Install"))
		{
			FH.CallFBPublishInstall();
		}*/
		GUI.Label(new Rect(viewRect.x + 10, viewRect.y + 80, 125, 25), "API Query");
		FH.ApiQuery = GUI.TextField(new Rect(viewRect.x + 10, viewRect.y + 100, viewRect.width - 10, 25), FH.ApiQuery);
		if(GUI.Button(new Rect(viewRect.x + 10, viewRect.y + 130, 125, 25), "FB API"))
		{
			FH.CallFBAPI();
		}
		
		GUI.Label(new Rect(viewRect.x + 10, viewRect.y + 160, 200, 25), "To ID (Optional)");
		FH.FeedToId = GUI.TextField(new Rect(viewRect.x + 10 , viewRect.y + 185, viewRect.width - 10, 25), FH.FeedToId);
		GUI.Label(new Rect(viewRect.x + 10, viewRect.y + 210, 200, 25), "Link (Optional)");
		FH.FeedLink = GUI.TextField(new Rect(viewRect.x + 10 , viewRect.y + 235, viewRect.width - 10, 25), FH.FeedLink);
		GUI.Label(new Rect(viewRect.x + 10, viewRect.y + 260, 200, 25), "Link Name (Optional)");
		FH.FeedLinkName = GUI.TextField(new Rect(viewRect.x + 10 , viewRect.y + 285, viewRect.width - 10, 25), FH.FeedLinkName);
		GUI.Label(new Rect(viewRect.x + 10, viewRect.y + 310, 200, 25), "Link Desc (Optional)");
		FH.FeedLinkDescription = GUI.TextField(new Rect(viewRect.x + 10 , viewRect.y + 335, viewRect.width - 10, 25), FH.FeedLinkDescription);
		GUI.Label(new Rect(viewRect.x + 10, viewRect.y + 360, 200, 25), "Link Caption (Optional)");
		FH.FeedLinkCaption = GUI.TextField(new Rect(viewRect.x + 10 , viewRect.y + 385, viewRect.width - 10, 25), FH.FeedLinkCaption);
		if(GUI.Button(new Rect(viewRect.x + 10, viewRect.x + 420, viewRect.width - 10, 25), "FB Feed"))
		{
			FH.CallFBFeed();
		}
		
		GUI.EndScrollView();
		#endregion

		#region Friend List
		if(GUI.Button(new Rect(width / 2 + 20, 10, width / 2 - 35, 30), "Friends List"))
			showModal = true;
		if(showModal)
			modalRect = GUI.ModalWindow(0, modalRect, WindowFunction, "Friends List");
		#endregion

		GUI.TextArea(
			new Rect(10, height - 100, width - 10, 100),
            string.Format(
			"FBInit: {0}\nAccessToken: {1}\nLastResponse: {2}",
			FH.IsInit,
			FH.AccessToken,
			FH.LastResponseString
			));
    }

	void WindowFunction(int windowId)
	{
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved &&
		   modalRect.Contains(new Vector2(Input.GetTouch(0).position.x, height - Input.GetTouch(0).position.y)) &&
		   showModal)
			svScrollPos2.y += Input.GetTouch(0).deltaPosition.y;

		svScrollPos2 = GUI.BeginScrollView(svPos2, svScrollPos2, viewRect2);

		for(int i = 0; i < friends.Count; i++)
		GUI.Label(new Rect(10, i*25, viewRect.width - 10, 25), friends[i]);

		GUI.EndScrollView();

		newFriend = GUI.TextField(new Rect(10, modalRect.height - 90, modalRect.width - 25, 25), newFriend);
		if(GUI.Button(new Rect(10, modalRect.height - 65, modalRect.width - 25, 25), "Add Friend") && newFriend.Length != 0)
		{
			friends.Add(newFriend);
			newFriend = "";
			viewRect2.height = friends.Count * 25;
			svScrollPos2.y = viewRect.height;
		}

		if(GUI.Button(new Rect(10, modalRect.height - 30, modalRect.width - 25, 25), "Close"))
			showModal = false;

		GUI.DragWindow(new Rect(0, 0, 200, 40));
	}
}
