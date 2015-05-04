using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SlackConversations.Support
{
	public class AutoHyperlink 
	{
		private const string PATTERN =
			@"(?i)\b((?:(https?|ftp)://|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))";

		private static readonly Regex Matcher = new Regex(PATTERN, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

		public string DoIt(string text)
		{
			var builder = new StringBuilder();
			int start = 0;
			var match = Matcher.Match(text);
			while (match.Success)
			{
				builder.Append(HttpUtility.HtmlEncode(text.Substring(start, match.Index - start)));
				builder.AppendFormat("<a href='{0}'>{1}</a>", HttpUtility.HtmlAttributeEncode(match.Value), HttpUtility.HtmlEncode(match.Value));
				start = match.Index + match.Length;
				match = match.NextMatch();
			}
			builder.Append(HttpUtility.HtmlEncode(text.Substring(start, text.Length - start)));
			return builder.ToString();
		}

		public string Transform(string text)
		{
			return Regex.Replace(text, PATTERN, Replacer);
		}

		private static string Replacer(Match m)
		{
			var url = m.Value;
			return string.Format("<a href=\"{0}\">{1}</a>", HttpUtility.UrlEncode(url), url.Replace("&", "%26"));
		}

	}
}