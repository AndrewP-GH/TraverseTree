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

		public int Level { get; private set; }

		public int LevelOrder { get; private set; }

		public BinaryTreeNode<TKey, TValue> Left { get; set; }

		public BinaryTreeNode<TKey, TValue> Right { get; set; }

		public BinaryTreeNode<TKey, TValue> Parent
		{
			get { return _parent; }
			set
			{
				_parent = value;
				Level = _parent.IsNull() ? 0 : ( 1 + _parent.Level );
				LevelOrder = _parent.IsNull() ? 0 :
					( _parent.IsLeftChild(this) ? ( _parent.LevelOrder * 2 ) : ( _parent.LevelOrder * 2 + 1 ) );
			}
		}

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

		private BinaryTreeNode<TKey, TValue> _parent;
	}
}
