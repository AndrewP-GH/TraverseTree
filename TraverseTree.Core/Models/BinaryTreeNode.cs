using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class BinaryTreeNode<TKey, TValue> : Node<TValue>
	{
		private readonly TKey _key;

		/// <summary>
		/// 
		/// </summary>
		public TKey Key => _key;

		/// <summary>
		/// 
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Left { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Right { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Parent { get; set; }

		public bool HasInnerNodes =>
			( Left != null ) || ( Right != null );

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentNullException" />
		public BinaryTreeNode<TKey, TValue> Minimum
		{
			get
			{
				BinaryTreeNode<TKey, TValue> current = this;

				while(current.Left != null)
					current = current.Left;
				return current;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Maximum
		{
			get
			{
				BinaryTreeNode<TKey, TValue> current = this;

				while (current.Right != null)
					current = current.Right;
				return current;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Successor
		{
			get
			{
				if (Right != null)
					return Right.Minimum;

				BinaryTreeNode<TKey, TValue> current = this;
				while (current.Parent != null && current != current.Parent.Right)
					current = current.Right;

				return current;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Predecessor
		{
			get
			{
				if (Left != null)
					return Left.Maximum;

				BinaryTreeNode<TKey, TValue> current = this;
				while (current.Parent != null && current != current.Parent.Left)
					current = current.Right;

				return current;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public override IEnumerable<INode<TValue>> InnerNodes
		{
			get
			{
				return new BinaryTreeNode<TKey, TValue>[] { Left, Right };
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		public BinaryTreeNode(TKey key, TValue data) :
			this(key, data, null, null, null) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		public BinaryTreeNode(TKey key, TValue data, BinaryTreeNode<TKey, TValue> left, BinaryTreeNode<TKey, TValue> right) : 
			this(key, data, left, right, null) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="parent"></param>
		public BinaryTreeNode(TKey key, TValue data, BinaryTreeNode<TKey, TValue> left, BinaryTreeNode<TKey, TValue> right, BinaryTreeNode<TKey, TValue> parent) : base(data)
		{
			_key = key;
			Left = left;
			Right = right;
			Parent = parent;
		}

		public override string ToString()
		{
			if (Object.ReferenceEquals(this, null))
				return "Empty";

			return String.Format("Left : {0}\nCurrent: {1}\nRight : {2}", Left, Value, Right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="leftKey"></param>
		/// <param name="leftData"></param>
		/// <returns></returns>
		public static BinaryTreeNode<TKey, TValue> LeftOnly(TKey key, TValue data, TKey leftKey, TValue leftData)
			=> LeftOnly(key, data, new BinaryTreeNode<TKey, TValue>(leftKey, leftData));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="left"></param>
		/// <returns></returns>
		public static BinaryTreeNode<TKey, TValue> LeftOnly(TKey key, TValue data, BinaryTreeNode<TKey, TValue> left)
			=> new BinaryTreeNode<TKey, TValue>(key, data, left, null, null);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="rightKey"></param>
		/// <param name="rightData"></param>
		/// <returns></returns>
		public static BinaryTreeNode<TKey, TValue> RightOnly(TKey key, TValue data, TKey rightKey, TValue rightData)
			=> RightOnly(key, data, new BinaryTreeNode<TKey, TValue>(rightKey, rightData));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static BinaryTreeNode<TKey, TValue> RightOnly(TKey key, TValue data, BinaryTreeNode<TKey, TValue> right)
			=> new BinaryTreeNode<TKey, TValue>(key, data, null, right, null);
	}
}
