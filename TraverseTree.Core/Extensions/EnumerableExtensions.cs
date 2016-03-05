using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	public static class EnumerableExtensions
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

	internal static class ListExtenssions
	{
		public static IList<TDest> Transform<TSource, TDest> (this IList<TSource> list, Func<TSource, TDest> transform)
		{
			IList<TDest> result = new List<TDest>();

			for (int i = 0; i < list.Count; i++)
			{
				result.Add(transform(list[i]));
			}
			return result;
		}
	}
}
