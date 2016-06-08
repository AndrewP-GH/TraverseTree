using System;
using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Extensions
{
	public static class KeyValueNodeExtenssions
	{
		public static bool MoreThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				Comparer<TKey>.Default.Compare(node.Key, other.Key) > 0;

		public static bool LessThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				Comparer<TKey>.Default.Compare(node.Key, other.Key) < 0;

		public static bool MoreThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) > 0;

		public static bool LessThan<TKey, TValue>(this IKeyValueNode<TKey, TValue> node, IKeyValueNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) < 0;

		public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this IKeyValueNode<TKey, TValue> node)
			where TKey : IComparable<TKey> =>
				new KeyValuePair<TKey, TValue>(node.Key, node.Value);
	}
}
