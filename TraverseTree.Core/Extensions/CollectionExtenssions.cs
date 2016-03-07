using System;
using System.Collections;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	internal static class CollectionExtennsions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection) => collection.Count == 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static bool IsEmpty(this ICollection collection) => collection.Count == 0;
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
		public static IList<TDest> Transform<TSource, TDest>(this IList<TSource> list, Func<TSource, TDest> transform)
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
