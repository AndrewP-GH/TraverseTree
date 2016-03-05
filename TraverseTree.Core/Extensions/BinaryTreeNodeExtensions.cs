using System;
using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Extensions
{
	public static class BinaryTreeNodeExtensions
	{
		public static bool MoreThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				Comparer<TKey>.Default.Compare(node.Key, other.Key) > 0;

		public static bool LessThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other)
			where TKey : IComparable<TKey> =>
				Comparer<TKey>.Default.Compare(node.Key, other.Key) < 0;

		public static bool MoreThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) > 0;

		public static bool LessThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) < 0;

		internal static int RecursiveCount<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node) =>
			Object.ReferenceEquals(node, null) ? 0 : 1 + node.Left.RecursiveCount() + node.Right.RecursiveCount();

		internal static int RecursiveHeight<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node) =>
			Object.ReferenceEquals(node, null) ? 0 : 1 + Math.Max(node.Left.RecursiveHeight(), node.Right.RecursiveHeight());
	}
}
