using System;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// Extenssion class for <see cref="BinaryTreeNode{TKey, TValue}"/>
	/// </summary>
	public static class BinaryTreeNodeExtenssions
	{
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
