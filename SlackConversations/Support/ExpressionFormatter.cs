using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using SlackConversations.Models;

namespace SlackConversations.Support
{
	public class ExpressionFormatter
	{
		private readonly Configuration _config;
		private readonly AutoHyperlink _hyperlinker;

		public ExpressionFormatter(Configuration config)
		{
			_config = config;
			_hyperlinker = new AutoHyperlink();
		}

		public string Format(Expression expression)
		{
			var builder = new StringBuilder();

			var hyperlinked = _hyperlinker.DoIt(expression.Content);
			var newLined = new Regex("(\r\n)|(\n)").Replace(hyperlinked, "<br/>");
			builder.Append(newLined);

			builder.AppendFormat(" - <a href='{1}/conversations.v1/show?id={0}'>View Conversation</a>", expression.Id, HttpUtility.HtmlAttributeEncode(_config.V1BaseUrl));

			if (expression.MentionedAssets != null)
			{
				builder.Append(" - ");
				builder.Append(string.Join(", ", expression.MentionedAssets.Select(FormatMentionedAsset)));
			}

			return builder.ToString();
		}
		
		private string FormatMentionedAsset(MentionedAsset mentioned)
		{
			var text = string.Format(mentioned.Number.IsBlank() ? "{0}" : "{0} ({1})", HttpUtility.HtmlEncode(mentioned.Name), mentioned.Number);
			return mentioned.Id.IsBlank() ? text : string.Format("<a href='{2}/assetdetail.v1?oid={0}'>{1}</a>", mentioned.Id, text, HttpUtility.HtmlAttributeEncode(_config.V1BaseUrl));
		}

		public IDictionary<string, object> FormatSlackMessagePayload(Expression expression)
		{
			IDictionary<string, object> body = new Dictionary<string, object>
			{
				{ "fallback", expression.Content },
				{ "author_name", expression.AuthorName },
				{ "author_link", AssetDetailUrl(expression.AuthorId) },
				{ "text", SlackEncode(expression.Content) },
				{ "color", "#7d2248" }
			};

			var payload = new Dictionary<string, object>
			{
				{ "text", "<" + AssetDetailUrl(expression.Id) + "|View Conversation>" },
				{ "attachments", new[] { body } }
			};

			if (expression.MentionedAssets != null)
				body["pretext"] = "Mentions: " + string.Join(", ", expression
					.MentionedAssets
					.Select(SlackFormatMentionedAsset));

			return payload;
		}

		private string SlackFormatMentionedAsset(MentionedAsset mention)
		{
			return "<" + AssetDetailUrl(mention.Id) + "|" + string.Format(mention.Number.IsBlank() ? "{0}" : "{0} ({1})", SlackEncode(mention.Name), mention.Number) + ">";
		}

		private string AssetTypeIcon(string type)
		{
			return _config.V1BaseUrl + "/css/images/icons/" + type + "-Icon.gif";
		}

		private string AssetDetailUrl(string id)
		{
			return _config.V1BaseUrl + "/assetdetail.v1?oid=" + id;
		}

		private string SlackEncode(string message)
		{
			return message
				.Replace("&", "&amp;")
				.Replace("<", "&lt;")
				.Replace(">", "&gt;");
		}
	}
}