using System.Web.Mvc;
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
}
