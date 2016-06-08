using System;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Extensions
{
	internal static class BinaryTreeNodeExtenssions
	{
		internal static void ReplaceChild<TKey, TValue>(this BinaryTreeNode<TKey, TValue> parent, BinaryTreeNode<TKey, TValue> old, BinaryTreeNode<TKey, TValue> next)
			where TKey : IComparable<TKey>
		{
			if (Object.ReferenceEquals(parent.Left, old)) {
				parent.Left = next;
			} else {
				parent.Right = next;
			}
		}

		internal static void DetachChild<TKey, TValue>(this BinaryTreeNode<TKey, TValue> parent, BinaryTreeNode<TKey, TValue> child)
			where TKey : IComparable<TKey> => 
				parent.ReplaceChild(child, null);

		internal static void ChangeRelations<TKey, TValue> (this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> next) 
			where TKey : IComparable<TKey>
		{
			node.Left = next.Left;
			node.Right = next.Right;
			node.Parent = next.Parent;
		}

		internal static void SetParentForChildren<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> parent)
			where TKey : IComparable<TKey>
		{
			node.Left.Parent = parent;
			node.Right.Parent = parent;
		}

		internal static void Detach<TKey, TValue>(this BinaryTreeNode<TKey, TValue> node)
			where TKey : IComparable<TKey>
		{
			node.Left = null;
			node.Right = null;
			node.Parent = null;
		}
	}
}
