using System;
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
	public class BinaryTreeNode<TKey, TValue> : Node<TValue>
	{
		/// <summary>
		/// Key associated with node
		/// </summary>
		public TKey Key { get; }

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
		public BinaryTreeNode<TKey, TValue> Parent { get; set; }
		/// <summary>
		/// Indicates if there is left and right nodes
		/// </summary>
		public bool IsLeaf =>
			Left.IsNull() && Right.IsNull();

		/// <summary>
		/// Indicates, if node contain only left child
		/// </summary>
		public bool HasLeftOnly => 
			Right.IsNull() && !Left.IsNull();

		/// <summary>
		/// Indicated, if node contain only right child
		/// </summary>
		public bool HasRightOnly => 
			Left.IsNull() && !Right.IsNull();

		/// <summary>
		/// Get the most left node from current
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Minimum
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
		public BinaryTreeNode<TKey, TValue> Maximum
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
			base(value)
		{
			if (key.IsNull())
			{
				throw new ArgumentNullException(nameof(key));
			}

			Key = key;
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

		/// <summary>
		/// Get's string representation from this node
		/// </summary>
		/// <returns>String, formatted as follows: Key {Key} Value {Value}</returns>
		public override string ToString() =>
			String.Format("Key: {0} Value: {1}", Key, Value);

		/// <summary>
		/// Create's node with only left child
		/// </summary>
		/// <param name="key">The key for this node</param>
		/// <param name="value">The value for this node</param>
		/// <param name="leftKey">The key for left node</param>
		/// <param name="leftData">The value for left node</param>
		/// <returns>Binary node with only left child</returns>
		public static BinaryTreeNode<TKey, TValue> LeftOnly(TKey key, TValue value, TKey leftKey, TValue leftData)
			=> LeftOnly(key, value, new BinaryTreeNode<TKey, TValue>(leftKey, leftData));

		/// <summary>
		/// Create's node with only left child
		/// </summary>
		/// <param name="key">The key for this node</param>
		/// <param name="value">The value for this node</param>
		/// <param name="left">The left node</param>
		/// <returns>Binary node with only left child</returns>
		public static BinaryTreeNode<TKey, TValue> LeftOnly(TKey key, TValue value, BinaryTreeNode<TKey, TValue> left)
			=> new BinaryTreeNode<TKey, TValue>(key, value, left, null);

		/// <summary>
		/// Create's node with only right child
		/// </summary>
		/// <param name="key">The key for this node</param>
		/// <param name="value">The value for this node</param>
		/// <param name="rightKey">The key for right node</param>
		/// <param name="rightData">The value for right node</param>
		/// <returns>Binary node with only right child</returns>
		public static BinaryTreeNode<TKey, TValue> RightOnly(TKey key, TValue value, TKey rightKey, TValue rightData)
			=> RightOnly(key, value, new BinaryTreeNode<TKey, TValue>(rightKey, rightData));

		/// <summary>
		/// Create's node with only right child
		/// </summary>
		/// <param name="key">The key for this node</param>
		/// <param name="value">The value for this node</param>
		/// <param name="right">the right node</param>
		/// <returns>Binary node with only right child</returns>
		public static BinaryTreeNode<TKey, TValue> RightOnly(TKey key, TValue value, BinaryTreeNode<TKey, TValue> right)
			=> new BinaryTreeNode<TKey, TValue>(key, value, null, right);
	}
}
