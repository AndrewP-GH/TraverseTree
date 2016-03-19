﻿using System;
using System.Collections.Generic;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Pair of (Key, Value) that contains left and right children
	/// </summary>
	/// <typeparam name="TKey">The key for node</typeparam>
	/// <typeparam name="TValue">The value for node</typeparam>
	public class BinaryTreeNode<TKey, TValue> : KeyValueNode<TKey, TValue>, IBinaryHierarchical<BinaryTreeNode<TKey, TValue>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool IsLeaf =>
			Right.IsNull() && Left.IsNull();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool HasLeftOnly =>
			!Left.IsNull() && Right.IsNull();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool HasRightOnly =>
			Left.IsNull() && !Right.IsNull();

		public int Height { get; protected set; }

		/// <summary>
		/// Get's or set's the left node from current
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Left { get; set; }

		/// <summary>
		/// Get's or set's the right node from current
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Right { get; set; }

		/// <summary>
		/// Get's or set's the parent node from current
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Parent
		{
			get { return _parent; }
			set
			{
				if (value.IsNull())
				{
					Height = 0;
				}
				else
				{
					Height = 1 + value.Height;
				}

				_parent = value;
			}
		}
		private BinaryTreeNode<TKey, TValue> _parent;

		/// <summary>
		/// Get the most left node from current
		/// </summary>
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

		/// <summary>
		/// Get the most right node from current
		/// </summary>
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

		/// <summary>
		/// Get's both children associated with this node
		/// </summary>
		public override IEnumerable<INode<TValue>> InnerNodes
		{
			get
			{
				return new BinaryTreeNode<TKey, TValue>[] { Left, Right };
			}
		}

		/// <summary>
		/// Create binary node from key and value
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public BinaryTreeNode(TKey key, TValue value) :
			this(key, value, null, null) { }

		/// <summary>
		/// Create binary node from key, value, left and right child
		/// </summary>
		/// <param name="key">The key for this node</param>
		/// <param name="value">The value for this node</param>
		/// <param name="left">Left child</param>
		/// <param name="right">Right child</param>
		public BinaryTreeNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> left, BinaryTreeNode<TKey, TValue> right) :
			this(key, value, left, right, null) { }

		/// <summary>
		/// Create binary node from key, value, left, right and parent node
		/// </summary>
		/// <param name="key">The key for this node</param>
		/// <param name="value">The value for this node</param>
		/// <param name="left">Left child</param>
		/// <param name="right">Right child</param>
		/// <param name="parent">Parent node</param>
		public BinaryTreeNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> left, BinaryTreeNode<TKey, TValue> right, BinaryTreeNode<TKey, TValue> parent) :
			base(key, value)
		{
			Left = left;
			Right = right;
			Parent = parent;
		}
		
		/// <summary>
		/// Set's left and right children to null
		/// </summary>
		public void Detach ()
		{
			Left = null;
			Right = null;
			Parent = null;
		}
	}
}
