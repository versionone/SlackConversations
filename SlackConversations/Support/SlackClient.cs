using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace SlackConversations.Support
{
	public class SlackClient
	{
		private readonly string _webhookUrl;
		private readonly string _channel;

		public SlackClient(Configuration config)
		{
			_webhookUrl = config.SlackIncomingWebhookUrl;
			_channel = config.SlackChannel;
		}

		public void PostMessage(IDictionary<string, object> payload)
		{
			if (!_channel.IsBlank())
				payload["channel"] = _channel;

			var json = new JavaScriptSerializer().Serialize(payload);

			using (var client = new WebClient { Encoding = Encoding.UTF8 })
			{
				client.Headers.Add("Content-Type", "application/json");
				client.UploadString(_webhookUrl, "POST", json);
			}
		}
	}
}