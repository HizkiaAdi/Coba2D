using UnityEngine;
using System.Collections;

public class ChatOutput : MonoBehaviour 
{
	private string chatOutput = "";

	void Start () 
	{
		chatOutput = "";
	}

	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 30), "Chat Output");
		GUI.TextArea(new Rect(10, 30, Screen.width-20, (Screen.height/2)+100), chatOutput);
	}
	
	public void AddMessage(string message)
	{
		chatOutput += "\n" + message;
	}
}
