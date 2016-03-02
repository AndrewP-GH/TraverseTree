using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	internal static class EnumerableExtensions
	{
		public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
			{
				action(item);
			}
			return source;
		}
	}
}
