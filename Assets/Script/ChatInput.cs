using UnityEngine;
using System.Collections;

public class ChatInput : MonoBehaviour
{
	private string _input;
	
	void Start ()
	{
		_input = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnGUI()
	{
		_input = GUI.TextField(new Rect(10, (Screen.height/2)+150, Screen.width - 150, 50), _input);
		if (GUI.Button(new Rect(Screen.width - 120, (Screen.height / 2) + 150, 100, 50), "Send"))
		{
			Debug.Log("Sending message [" + _input + "].");
			SendChatMessage(_input);
			_input = "";
		}
	}
	
	void SendChatMessage(string message)
	{
		GameObject.Find("NetworkSocketListener").GetComponent<NetworkSocketListener>().SendChatMessage(message);
	}
}
