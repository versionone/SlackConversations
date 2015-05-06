using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SlackConversations.Models;
using SlackConversations.Support;

namespace SlackConversations.Controllers
{
	[ValidateInput(false)]
	public class ExpressionController : Controller
	{
		private Configuration _config;
		private ExpressionFormatter _formatter;

		public Configuration Config
		{
			get { return _config = _config ?? new Configuration(); }
		}

		public ExpressionFormatter Formatter
		{
			get { return _formatter = _formatter ?? new ExpressionFormatter(Config); }
		}

		[HttpPost]
		public ActionResult Changed(Expression expression)
		{
			if (expression.IsNewlyCreated)
			{
				var client = new SlackClient(Config);
				client.PostMessage(Formatter.FormatSlackMessagePayload(expression));
			}

			return new EmptyResult();
		}

		[HttpGet]
		public ActionResult Test()
		{
			return View();
		}
	}

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
