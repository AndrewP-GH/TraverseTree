using System;
using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Extensions
{
	internal static class BinarySearchTreeExtensions
	{
		public static bool MoreThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				DefaultNodeComparer<TKey>.Comparer.Compare(node.Key, other.Key) > 0;

		public static bool LessThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				DefaultNodeComparer<TKey>.Comparer.Compare(node.Key, other.Key) < 0;

		public static bool MoreThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) > 0;

		public static bool LessThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) < 0;
	}
}
