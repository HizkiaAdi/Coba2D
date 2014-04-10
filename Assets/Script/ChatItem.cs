using UnityEngine;
using System.Collections;

namespace SocialModule
{
	public class ChatItem
	{
		private string id;
		private string name;
		private string message;

		public ChatItem(string id, string name, string message)
		{
			this.id = id;
			this.name = name;
			this.message = message;
		}

		public string ID { get { return this.id; } }
		public string Name { get { return this.name; } }
		public string Message { get { return this.message; } }
	}
}