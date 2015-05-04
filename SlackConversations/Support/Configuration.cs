using System;
using System.Configuration;
/*
using HipChat;
*/

namespace SlackConversations.Support
{
	public class Configuration
	{
/*
		public string HipChatApiToken { get; private set; }
		public int HipChatRoomId { get; private set; }
		public HipChatClient.BackgroundColor BackgroundColor { get; private set; }
		public bool Notify { get; private set; }
*/
		public string V1BaseUrl { get; private set; }

		public Configuration()
		{
/*
			HipChatApiToken = "XXX";
			HipChatRoomId = 0;
			BackgroundColor = HipChatClient.BackgroundColor.red;
			Notify = false;
*/
			V1BaseUrl = "https://your.hosted/instance";

			ReadFromConfig();
		}

		private void ReadFromConfig()
		{
/*
			var token = ConfigurationManager.AppSettings["HipChatApiToken"];
			if (!token.IsBlank()) HipChatApiToken = token;

			var room = ConfigurationManager.AppSettings["HipChatRoomId"];
			if (!room.IsBlank())
			{
				int roomId;
				if (int.TryParse(room, out roomId)) HipChatRoomId = roomId;
			}

			var color = ConfigurationManager.AppSettings["BackgroundColor"];
			if (!color.IsBlank()) BackgroundColor = ConvertToHipChatColor(color);

			var notify = ConfigurationManager.AppSettings["Notify"];
			if (!notify.IsBlank())
			{
				bool notifyRoom;
				if (bool.TryParse(notify, out notifyRoom)) Notify = notifyRoom;
			}
*/

			var url = ConfigurationManager.AppSettings["V1BaseUrl"];
			if (!url.IsBlank()) V1BaseUrl = url;
		}

/*
		private HipChatClient.BackgroundColor ConvertToHipChatColor(string color)
		{
			HipChatClient.BackgroundColor result;
			return Enum.TryParse(color, true, out result) ? result : HipChatClient.BackgroundColor.random;
		}
*/
	}
}