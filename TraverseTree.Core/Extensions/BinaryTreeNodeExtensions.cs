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
			node.IsNull() ? 0 : 1 + node.Left.RecursiveCount() + node.Right.RecursiveCount();

		internal static int RecursiveHeight<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node) =>
			node.IsNull() ? 0 : 1 + Math.Max(node.Left.RecursiveHeight(), node.Right.RecursiveHeight());

		internal static void ReplaceChild<TKey, TValue>(this BinaryTreeNode<TKey, TValue> parent, BinaryTreeNode<TKey, TValue> old, BinaryTreeNode<TKey, TValue> next)
		{
			if (Object.ReferenceEquals(parent.Left, old)) {
				parent.Left = next;
			} else {
				parent.Right = next;
			}
		}

		internal static void DetachChild<TKey, TValue>(this BinaryTreeNode<TKey, TValue> parent, BinaryTreeNode<TKey, TValue> child) =>
			parent.ReplaceChild(child, null);

		internal static void ChangeRelations<TKey, TValue> (this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> next)
		{
			node.Left = next.Left;
			node.Right = next.Right;
			node.Parent = next.Parent;
		}

		internal static void SetParentForChildren<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> parent)
		{
			node.Left.Parent = parent;
			node.Right.Parent = parent;
		}
	}
}
