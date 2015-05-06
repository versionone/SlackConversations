using System.Configuration;

namespace SlackConversations.Support
{
	public class Configuration
	{
		public string V1BaseUrl { get { return ConfigurationManager.AppSettings["V1BaseUrl"]; } }
		public string SlackIncomingWebhookUrl { get { return ConfigurationManager.AppSettings["SlackIncomingWebhookUrl"]; } }
		public string SlackChannel { get { return ConfigurationManager.AppSettings["SlackChannel"]; } }
	}
}