using UnityEngine;
using System.Collections.Generic;

namespace SocialModule
{
	public class ChatSocketListener : MonoBehaviour
	{
		public string host;
		public int port;
		
		private AndroidJavaClass _unityPlayer;
		private AndroidJavaObject _activity;
		private AndroidJavaObject _chatClient;
		
		void Start ()
		{
			string[] args = new string[2];
			args[0] = host;
			args[1] = port.ToString();
			
			//_chatClient = new AndroidJavaObject("com.pejuangcinta.chat.ChatClient", args);
			_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			_activity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			_activity.Call("runOnUiThread", new AndroidJavaRunnable(()=>
			                                                        {
				_chatClient = new AndroidJavaObject("com.pejuangcinta.chat.ChatClient", args);
			}
			));
		}
		
		public void ReceivedMessage(string jsonString)
		{
			ChatGUI chatGUI = GameObject.Find("GUI").GetComponent<ChatGUI>();
			chatGUI.AddChatItem(JSONToChatItem(jsonString));
		}
		
		public void SendMessage(ChatItem chatItem)
		{
			bool success = false;
			string jsonString = ChatItemToJSON(chatItem);
			success = _chatClient.Call<bool>("sendMessage", jsonString);
			if (success)
				Debug.Log("Successfully sent message.");
			else
				Debug.Log("Failed to send message.");
		}

		private string ChatItemToJSON(ChatItem item)
		{
			Dictionary<string,string> dict = new Dictionary<string, string>();
			dict["id"] = item.ID;
			dict["name"] = item.Name;
			dict["message"] = item.Message;
			return MiniJSON.Json.Serialize(dict);
		}

		private ChatItem JSONToChatItem(string jsonString)
		{
			//List list = (List)MiniJSON.Json.Deserialize(jsonString); 
			//Dictionary<string, System.Object> dict = list[0] as Dictionary<string, System.Object>;
			Dictionary<string, System.Object> dict = MiniJSON.Json.Deserialize(jsonString) as Dictionary<string, System.Object>;
			return new ChatItem((string)dict["id"], (string)dict["name"], (string)dict["message"]);
		}
	}

}