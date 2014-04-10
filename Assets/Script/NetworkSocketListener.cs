using UnityEngine;
using System.Collections;

public class NetworkSocketListener : MonoBehaviour
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

	void Update ()
	{
		
	}

	public void MessageReceived(string message)
	{
		Debug.Log("Received chat message [" + message + "].");
		ChatOutput chatOutput = GameObject.Find("GUI").GetComponent<ChatOutput>();
		chatOutput.AddMessage(message);
	}
	
	public void SendChatMessage(string message)
	{
		bool success = false;
		Debug.Log("Attempting to send message [" + message + "].");
		success = _chatClient.Call<bool>("sendMessage", message);
		if (success)
		{
			Debug.Log("Successfully sent message.");
		}
		else
		{
			Debug.Log("Failed to send message.");
		}
	}
}
