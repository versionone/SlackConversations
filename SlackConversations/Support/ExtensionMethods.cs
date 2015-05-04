using System;
using System.Collections.Generic;

namespace SlackConversations.Support
{
	public static class ExtensionMethods
	{
		public static bool IsBlank(this string input)
		{
			return string.IsNullOrEmpty(input);
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> each)
		{
			foreach (var item in items)
				each(item);

			return items;
		}
	}
}