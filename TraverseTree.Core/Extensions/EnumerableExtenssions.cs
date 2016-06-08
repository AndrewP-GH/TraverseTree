using System;
using System.Collections.Generic;
using System.Linq;

namespace TraverseTree.Core.Extensions
{
	public static class EnumerableExtenssions
	{
		public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
		{
			source.NullGuard(nameof(source));
			action.NullGuard(nameof(action));

			foreach (var item in source)
			{
				action(item);
				yield return item;
			}
		}
	}
}
