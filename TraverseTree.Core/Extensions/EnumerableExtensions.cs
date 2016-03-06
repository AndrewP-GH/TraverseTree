using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// Extenssion class for <see cref="IEnumerable{T}"/>
	/// </summary>
	public static class EnumerableExtensions
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

	/// <summary>
	/// Extenssion class for <see cref="IList{T}"/>
	/// </summary>
	internal static class ListExtenssions
	{
		/// <summary>
		/// Perform fast method for <see cref="System.Linq.Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>.
		/// Use indexes instead of usnig <see cref="List{T}.Enumerator"/>
		/// </summary>
		/// <typeparam name="TSource">Source parameter</typeparam>
		/// <typeparam name="TDest">Destination parameter</typeparam>
		/// <param name="list">Source list <see cref="IList{T}"/></param>
		/// <param name="transform">Function to perform <see cref="Func{T, TResult}"/></param>
		/// <returns></returns>
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

	/// <summary>
	/// Extension class for generic
	/// </summary>
	internal static class GenericExtenssions
	{
		/// <summary>
		/// Perform compare with null without boxing-unboxing
		/// </summary>
		/// <typeparam name="T">Generic type</typeparam>
		/// <param name="value">Value</param>
		/// <returns>
		/// If T is reference type, then perform compare with null. 
		/// If T is value type, then perform compate with default
		/// If T is <see cref="Nullable{T}"/> then pefrom compare with null for nullable types
		/// </returns>
		public static bool IsNull<T>(this T value) =>
			EqualityComparer<T>.Default.Equals(value, default(T));
	}
}
