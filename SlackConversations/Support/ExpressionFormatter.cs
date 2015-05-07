using System.Collections.Generic;
using System.Linq;
using SlackConversations.Models;

namespace SlackConversations.Support
{
	public class ExpressionFormatter
	{
		private readonly Configuration _config;

		public ExpressionFormatter(Configuration config)
		{
			_config = config;
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
				{ "text", "<" + _config.V1BaseUrl + "/conversations.v1/show?id=" + expression.Id + "|View Conversation>" },
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