using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// Extenssion class for <see cref="IEnumerable{T}"/>
	/// </summary>
	public static class EnumerableExtenssions
	{
		/// <summary>
		/// Perform action on each element of <see cref="IEnumerable{T}"/>
		/// </summary>
		/// <typeparam name="T">Paramater of <see cref="IEnumerable{T}"/></typeparam>
		/// <param name="source">Source</param>
		/// <param name="action">Action to perform <see cref="Action{T}"/></param>
		/// <returns>
		/// Return <see cref="IEnumerable{T}"/>
		/// </returns>
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
