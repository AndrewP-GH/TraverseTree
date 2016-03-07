using System;
using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class KeyValueNodeExtenssions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool MoreThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				Comparer<TKey>.Default.Compare(node.Key, other.Key) > 0;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool LessThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				Comparer<TKey>.Default.Compare(node.Key, other.Key) < 0;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="other"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static bool MoreThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) > 0;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="other"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static bool LessThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) < 0;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this IKeyValueNode<TKey, TValue> node) =>
			new KeyValuePair<TKey, TValue>(node.Key, node.Value);
	}
}
