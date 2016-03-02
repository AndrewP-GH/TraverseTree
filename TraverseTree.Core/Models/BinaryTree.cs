using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Represent binary search tree (BST) data structure
	/// </summary>
	/// <typeparam name="TKey">Key for search, must be Comparable: <see cref="IComparable{TKey}"/></typeparam>
	/// <typeparam name="TValue">Value for key, must be new()</typeparam>
	public class BinaryTree<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>> 
		where TKey : IComparable<TKey> 
	{
		/// <summary>
		/// Represents the root of BST
		/// </summary>
		public BinaryTreeNode<TKey, TValue> Root { get; private set; }

		/// <summary>
		/// Gets the number of elements contained in the collection
		/// </summary>
		public int Count { get; private set; }

		/// <summary>
		/// Indicates, if the collection is readonly
		/// </summary>
		public bool IsReadOnly => false;

		/// <summary>
		/// Determine, if BST is empty
		/// </summary>
		/// <returns>False if root node is null. Otherwise true</returns>
		public bool IsEmpty =>
			( Root == null );

		/// <summary>
		/// 
		/// </summary>
		private IComparer<TKey> _comparer;
		public IComparer<TKey> Comparer
		{
			get
			{
				return _comparer ?? 
					( _comparer = DefaultNodeComparer<TKey>.Comparer );
			}
			set
			{
				if (Object.ReferenceEquals(value, null))
					throw new ArgumentNullException(nameof(value));

				_comparer = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ICollection<TKey> Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ICollection<TValue> Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Create empty instance of BST
		/// </summary>
		public BinaryTree() { }

		/// <summary>
		/// Create instance of BST with specified root node
		/// </summary>
		/// <param name="root"><see cref="BinaryTreeNode{TKey, TValue}"/></param>
		public BinaryTree(BinaryTreeNode<TKey, TValue> root) : 
			this(root, new DefaultNodeComparer<TKey>()) { }

		/// <summary>
		/// Create instance of BST with specified key comparer
		/// </summary>
		/// <param name="keyComparer"><seealso cref="IComparable{T}"/></param>
		public BinaryTree(IComparer<TKey> keyComparer) : 
			this(null, keyComparer) { }
		
		/// <summary>
		/// Create instance of BST with specified root node and key comparer
		/// </summary>
		/// <param name="root"></param>
		/// <param name="keyComparer"></param>
		public BinaryTree(BinaryTreeNode<TKey, TValue> root, IComparer<TKey> keyComparer)
		{
			Root = root;
			Comparer = keyComparer;
			Count = IsEmpty ? 0 : 1;
		}

		public bool ContainsKey(TKey key)
		{
			throw new NotImplementedException();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			throw new NotImplementedException();
		}

		public void Add(TKey key, TValue value)
		{
			BinaryTreeNode<TKey, TValue> current = Root;
			BinaryTreeNode<TKey, TValue> node = new BinaryTreeNode<TKey, TValue>(key, value);

			while (current != null && current.HasInnerNodes)
			{
				if (node.LessThan(current, Comparer))
				{
					current = current.Left;
				}
				else
				{
					current = current.Right;
				}
			}

			node.Parent = current;

			if (current == null)
			{
				Root = node;
			}
			else if (node.LessThan(current))
			{
				current.Left = node;
			}
			else
			{
				current.Right = node;
			}

			Count++;
		}

		public void Add(KeyValuePair<TKey, TValue> item) => 
			Add(item.Key, item.Value);

		public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
			pairs.Each(x => Add(x.Key, x.Value));

		public void Find (TKey key)
		{
			throw new NotImplementedException();
		}

		public bool Remove(TKey key)
		{
			throw new NotImplementedException();
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotImplementedException();
		}

		public bool RemoveAll (KeyValuePair<TKey, TValue> item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
