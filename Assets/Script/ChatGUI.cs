using UnityEngine;
using System.Collections.Generic;

namespace SocialModule
{
	public class ChatGUI : MonoBehaviour
	{
		LinkedList<ChatItem> chats;
		LinkedListNode<ChatItem> chatItem;
		Rect chatWindowPos;
		Rect chatViewSize;
		Rect inputPos;
		Rect buttonPos;
		Vector2 chatScroll;
		string input;
		string playerID = "5110100082";
		string playerName = "Hizkia";
		
		void Start ()
		{
			chats = new LinkedList<ChatItem>();
			chatWindowPos = new Rect(10, Screen.height * 0.6f, Screen.width * 0.5f, (Screen.height * 0.9f) - (Screen.height * 0.6f));
			chatViewSize = new Rect(5, 0, chatWindowPos.width - 20, 0);
			inputPos = new Rect(10, Screen.height * 0.9f + 10, Screen.width * 0.4f, 30);
			buttonPos = new Rect(Screen.width * 0.4f + 15, inputPos.y, Screen.width * 0.1f, 30);
			chatScroll = Vector2.zero;

			for(int i = 0; i < 50; i++)
			{
				chats.AddLast(new LinkedListNode<ChatItem>(new ChatItem("1","Hizkia","Message " + (i+1))));
			}
			chatViewSize.height = chats.Count * 20;
			chatScroll.y = chatViewSize.height;
			input = string.Empty;
		}

		void OnGUI()
		{
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && 
			   chatViewSize.Contains(new Vector2(Input.GetTouch(0).position.x, Screen.height - Input.GetTouch(0).position.y)))
				chatScroll.y += Input.GetTouch(0).deltaPosition.y;

			chatScroll = GUI.BeginScrollView(chatWindowPos, chatScroll, chatViewSize);
			chatItem = chats.First;
			int i = 0;
			while(chatItem != null)
			{
				GUI.Label(new Rect(10, i * 20, chatViewSize.width * 0.2f - 10, 20), chatItem.Value.Name);
				GUI.Label(new Rect(chatViewSize.width * 0.2f, i * 20, chatViewSize.width * 0.8f, 20), chatItem.Value.Message);
				chatItem = chatItem.Next;
				i++;
			}
			GUI.EndScrollView();

			input = GUI.TextField(inputPos, input);
			if(GUI.Button(buttonPos, "Send"))
			{
				SendMessage();
			}
		}

		public void AddChatItem(ChatItem item)
		{
			chats.AddLast(new LinkedListNode<ChatItem>(item));
			chatViewSize.height += 20;
			chatScroll.y = chatViewSize.height;
		}

		private void SendMessage()
		{
			ChatSocketListener cls = GameObject.Find("ChatListener").GetComponent<ChatSocketListener>();
			cls.SendMessage(new ChatItem(playerID, playerName, input));
			input = "";
		}
	}
}