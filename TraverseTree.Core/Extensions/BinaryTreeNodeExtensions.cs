using System;
using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// Extenssion class for <see cref="BinaryTreeNode{TKey, TValue}"/>
	/// </summary>
	public static class BinaryTreeNodeExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool MoreThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other)
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
		public static bool LessThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other)
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
		public static bool MoreThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other, IComparer<TKey> comparer)
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
		public static bool LessThan<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> other, IComparer<TKey> comparer)
			where TKey : IComparable<TKey> =>
				comparer.Compare(node.Key, other.Key) < 0;

		public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node) =>
			new KeyValuePair<TKey, TValue>(node.Key, node.Value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static int RecursiveCount<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node) =>
			node.IsNull() ? 0 : 1 + node.Left.RecursiveCount() + node.Right.RecursiveCount();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static int RecursiveHeight<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node) =>
			node.IsNull() ? 0 : 1 + Math.Max(node.Left.RecursiveHeight(), node.Right.RecursiveHeight());

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="parent"></param>
		/// <param name="old"></param>
		/// <param name="next"></param>
		internal static void ReplaceChild<TKey, TValue>(this BinaryTreeNode<TKey, TValue> parent, BinaryTreeNode<TKey, TValue> old, BinaryTreeNode<TKey, TValue> next)
		{
			if (Object.ReferenceEquals(parent.Left, old)) {
				parent.Left = next;
			} else {
				parent.Right = next;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		internal static void DetachChild<TKey, TValue>(this BinaryTreeNode<TKey, TValue> parent, BinaryTreeNode<TKey, TValue> child) =>
			parent.ReplaceChild(child, null);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="next"></param>
		internal static void ChangeRelations<TKey, TValue> (this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> next)
		{
			node.Left = next.Left;
			node.Right = next.Right;
			node.Parent = next.Parent;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <param name="parent"></param>
		internal static void SetParentForChildren<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> parent)
		{
			node.Left.Parent = parent;
			node.Right.Parent = parent;
		}
	}
}
