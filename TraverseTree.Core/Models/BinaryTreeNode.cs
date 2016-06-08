using System;
using System.Collections.Generic;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	public class BinaryTreeNode<TKey, TValue> : KeyValueNode<TKey, TValue>, IBinaryHierarchical<BinaryTreeNode<TKey, TValue>>
		where TKey: IComparable<TKey>
	{
		public bool IsLeaf =>
			Right.IsNull() && Left.IsNull();

		public bool HasLeftOnly =>
			!Left.IsNull() && Right.IsNull();

		public bool HasRightOnly =>
			Left.IsNull() && !Right.IsNull();

		public virtual BinaryTreeNode<TKey, TValue> Left { get; set; }

		public virtual BinaryTreeNode<TKey, TValue> Right { get; set; }

		public virtual BinaryTreeNode<TKey, TValue> Parent { get; set; }

		public BinaryTreeNode<TKey, TValue> Leftmost
		{
			get
			{
				BinaryTreeNode<TKey, TValue> current = this;

				while (!current.Left.IsNull())
				{
					current = current.Left;
				}
				return current;
			}
		}

		public BinaryTreeNode<TKey, TValue> Rightmost
		{
			get
			{
				BinaryTreeNode<TKey, TValue> current = this;

				while (!current.Right.IsNull())
				{
					current = current.Right;
				}
				return current;
			}
		}

		public override IReadOnlyList<INode<TValue>> InnerNodes
		{
			get
			{
				return new BinaryTreeNode<TKey, TValue>[] { Left, Right };
			}
		}

		public BinaryTreeNode(TKey key, TValue value) :
			this(key, value, null, null) { }

		public BinaryTreeNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> left, BinaryTreeNode<TKey, TValue> right) :
			this(key, value, left, right, null) { }

		public BinaryTreeNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> left, BinaryTreeNode<TKey, TValue> right, BinaryTreeNode<TKey, TValue> parent) :
			base(key, value)
		{
			Left = left;
			Right = right;
			Parent = parent;
		}
	}
}
