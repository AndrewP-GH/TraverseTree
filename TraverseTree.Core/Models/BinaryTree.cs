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
	/// <typeparam name="TValue">Value for associated key</typeparam>
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

		public int Height => Root.RecursiveHeight();

		/// <summary>
		/// Indicates, if the collection is readonly
		/// </summary>
		public bool IsReadOnly => false;

		/// <summary>
		/// Determine, if BST is empty
		/// </summary>
		/// <returns>False if root node is null. Otherwise true</returns>
		public bool IsEmpty => 
			Object.ReferenceEquals(Root, null);

		/// <summary>
		/// 
		/// </summary>
		private IComparer<TKey> _keyComparer;
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return _keyComparer ?? 
					( _keyComparer = Comparer<TKey>.Default );
			}
			set
			{
				if (Object.ReferenceEquals(value, null)) { 
					throw new ArgumentNullException(nameof(value));
				}

				_keyComparer = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private IEqualityComparer<TValue> _valueComparer;
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return _valueComparer ??
					( _valueComparer = EqualityComparer<TValue>.Default );
			}
			set
			{
				if (Object.ReferenceEquals(value, null)) {
					throw new ArgumentNullException(nameof(value));
				}

				_valueComparer = value;
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
			this(root, Comparer<TKey>.Default, EqualityComparer<TValue>.Default) { }

		/// <summary>
		/// Create instance of BST with specified key comparer
		/// </summary>
		/// <param name="keyComparer"><seealso cref="IComparable{T}"/></param>
		public BinaryTree(IComparer<TKey> keyComparer) : 
			this(null, keyComparer, EqualityComparer<TValue>.Default) { }
		
		/// <summary>
		/// Create instance of BST with specified root node and key comparer
		/// </summary>
		/// <param name="root"></param>
		/// <param name="keyComparer"></param>
		public BinaryTree(BinaryTreeNode<TKey, TValue> root, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Root = root;
			KeyComparer = keyComparer;
			ValueComparer = valueComparer;
			Count = IsEmpty ? 0 : 1;
		}

		public bool ContainsKey(TKey key) =>
			Find(key, TruePredicate).Count > 0;

		public bool Contains(TKey key, TValue value) =>
			Find(key, x => ValueComparer.Equals(value, x)).Count > 0;

		public bool Contains(KeyValuePair<TKey, TValue> item) =>
			Contains(item.Key, item.Value);

		public void Add(TKey key, TValue value)
		{
			BinaryTreeNode<TKey, TValue> parent = null;
			BinaryTreeNode<TKey, TValue> current = Root;
			BinaryTreeNode<TKey, TValue> node = new BinaryTreeNode<TKey, TValue>(key, value);

			while (!Object.ReferenceEquals(current, null))
			{
				parent = current;

				if (node.LessThan(current, KeyComparer)) {
					current = current.Left;
				}
				else {
					current = current.Right;
				}
			}

			node.Parent = parent;

			if (Object.ReferenceEquals(parent, null)) {
				Root = node;
			}
			else if (node.LessThan(parent, KeyComparer)) {
				parent.Left = node;
			}
			else {
				parent.Right = node;
			}

			Count++;
		}

		public void Add(KeyValuePair<TKey, TValue> item) => 
			Add(item.Key, item.Value);

		public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
			pairs.Each(x => Add(x.Key, x.Value));

		public IEnumerable<KeyValuePair<TKey, TValue>> Find(TKey key)
		{
			return Find(key, TruePredicate).
				Transform(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value));
		}
		
		public IEnumerable<KeyValuePair<TKey, TValue>> Find(TKey key, TValue value)
		{
			return Find(key, x => ValueComparer.Equals(value, x)).
				Transform(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value));
		}

		public IEnumerable<TValue> FindValues (TKey key)
		{
			IList<BinaryTreeNode<TKey, TValue>> searchResult = Find(key, TruePredicate);

			if (searchResult.Count == 0) {
				throw new KeyNotFoundException("Key not found in BST");
			}
			return searchResult.Transform(x => x.Value);
		}

		public bool RemoveAt (TKey key, bool removeAll = false)
		{
			Remove(key, TruePredicate, removeAll);
			return true;
		}

		public bool Remove(TKey key, TValue value)
		{
			throw new NotImplementedException();
		}

		public bool Remove(KeyValuePair<TKey, TValue> item) =>
			Remove(item.Key, item.Value);

		public bool RemoveAll (TKey key, TValue value)
		{
			throw new NotImplementedException();
		}

		public bool RemoveAll(KeyValuePair<TKey, TValue> item) =>
			RemoveAll(item.Key, item.Value);

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

		private List<BinaryTreeNode<TKey, TValue>> Find (TKey key, Predicate<TValue> valuePredicate)
		{
			BinaryTreeNode<TKey, TValue> current = Root;
			List<BinaryTreeNode<TKey, TValue>> nodes = new List<BinaryTreeNode<TKey, TValue>>();
			
			while(!Object.ReferenceEquals(current, null))
			{
				int compareResult = KeyComparer.Compare(key, current.Key);

				if (compareResult < 0) {
					current = current.Left;
				} 
				else
				{
					if (compareResult == 0 && valuePredicate(current.Value)) {
						nodes.Add(current);
					}

					current = current.Right;
				}
			}

			return nodes;
		}

		private bool Remove(TKey key, Predicate<TValue> valuePredicate, bool removeAll)
		{
			BinaryTreeNode<TKey, TValue> current = Root;

			while (!Object.ReferenceEquals(current, null))
			{
				int compareResult = KeyComparer.Compare(key, current.Key);

				if (compareResult < 0) {
					current = current.Left;
				} else
				{
					if (compareResult == 0 && valuePredicate(current.Value))
					{
						RemoveNode(current, Object.ReferenceEquals(current.Parent?.Left, current));
						--Count;
					}

					current = current.Right;
				}
			}

			return true;
		}

		private bool RemoveNode (BinaryTreeNode<TKey, TValue> node, bool left)
		{
			if (node.IsLeaf) {
				if (left) node.Parent.Left = null;
				else node.Parent.Right = null;
				node.Parent = null;
				//next = null;
			}
			else if (Object.ReferenceEquals(node.Right, null)) {
				if (left) node.Parent.Left = node.Left;
				else node.Parent.Right = node.Left;
				node.Parent = null;

				//next = node.Left;
			} else if (Object.ReferenceEquals(node.Left, null)) {
				if (left) node.Parent.Left = node.Right;
				else node.Parent.Right = node.Right;
				node.Parent = null;

				//next = node.Right;
			} else
			{
				BinaryTreeNode<TKey, TValue> minimum = node.Right.Minimum;

				minimum.Parent.Left = minimum.Right;

				if (left)
				{
					minimum.Parent = node.Parent.Left;
					node.Parent.Left = minimum;
				} else
				{
					minimum.Parent = node.Parent.Right;
					node.Parent.Right = minimum;
				}
				node.Parent = null;

				minimum.Left = node.Left;
				minimum.Right = node.Right;
			}

			return true;
		}

		private static readonly Predicate<TValue> TruePredicate = x => true;
	}
}
