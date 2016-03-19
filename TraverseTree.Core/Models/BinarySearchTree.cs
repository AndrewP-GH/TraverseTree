using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Represent binary search tree (BST) data structure
	/// </summary>
	/// <typeparam name="TKey">Key for search, must be Comparable: <see cref="IComparable{TKey}"/></typeparam>
	/// <typeparam name="TValue">Value for associated key</typeparam>
	public class BinarySearchTree<TKey, TValue> : IBinaryOrderedTree<TKey, TValue, BinaryTreeNode<TKey, TValue>> 
		where TKey : IComparable<TKey> 
	{
		public delegate BinaryTreeNode<TKey, TValue> BinaryNodeCreator(TKey key, TValue value);

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
		public bool IsEmpty => Root.IsNull();

		/// <summary>
		/// Get's or set's the key comparer
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
				if (value.IsNull()) { 
					throw new ArgumentNullException(nameof(value));
				}

				_keyComparer = value;
			}
		}

		/// <summary>
		/// Get's or set's the value equality comparer
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
				if (value.IsNull()) {
					throw new ArgumentNullException(nameof(value));
				}

				_valueComparer = value;
			}
		}

		/// <summary>
		/// Get's the collection of key's associated with this BST
		/// </summary>
		public ICollection<TKey> Keys =>
			this.Select(x => x.Key).ToArray();

		/// <summary>
		/// Get's the collection of values associated with this BST
		/// </summary>
		public ICollection<TValue> Values =>
			this.Select(x => x.Value).ToArray();

		/// <summary>
		/// Create empty instance of BST
		/// </summary>
		public BinarySearchTree() : 
			this(null, Comparer<TKey>.Default, EqualityComparer<TValue>.Default, (k, v) => new BinaryTreeNode<TKey, TValue>(k, v)) { }

		public BinarySearchTree(Func<BinaryTreeNode<TKey, TValue>> creator) :
			this(null, Comparer<TKey>.Default, EqualityComparer<TValue>.Default, (k, v) => new BinaryTreeNode<TKey, TValue>(k, v)) { }

		/// <summary>
		/// Create instance of BST with specified root node
		/// </summary>
		/// <param name="root"><see cref="BinaryTreeNode{TKey, TValue}"/></param>
		public BinarySearchTree(BinaryTreeNode<TKey, TValue> root) : 
			this(root, Comparer<TKey>.Default, EqualityComparer<TValue>.Default, (k,v) => new BinaryTreeNode<TKey, TValue>(k, v)) { }

		/// <summary>
		/// Create instance of BST with specified key comparer
		/// </summary>
		/// <param name="keyComparer"><seealso cref="IComparable{T}"/></param>
		public BinarySearchTree(IComparer<TKey> keyComparer) : 
			this(null, keyComparer, EqualityComparer<TValue>.Default, (k,v) => new BinaryTreeNode<TKey, TValue>(k,v)) { }

		/// <summary>
		/// Create instance of BST with specified root node and key comparer
		/// </summary>
		/// <param name="root"></param>
		/// <param name="keyComparer"></param>
		public BinarySearchTree(BinaryTreeNode<TKey, TValue> root, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, BinaryNodeCreator creator)
		{
			if (creator.IsNull()) {
				throw new ArgumentNullException(nameof(creator));
			}

			Root = root;
			KeyComparer = keyComparer;
			ValueComparer = valueComparer;
			Count = IsEmpty ? 0 : 1;
			_creator = creator;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(TKey key) =>
			FindOrRemoveAllInternal(key, SearchingMode.SearchByKey).Count > 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(TKey key, TValue value) =>
			FindOrRemoveAllInternal(key, SearchingMode.SearchByKeyAndValue, value).Count > 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(BinaryTreeNode<TKey, TValue> item)
		{
			if (item.IsNull()) {
				throw new ArgumentNullException(nameof(item));
			}
			
			return Contains(item.Key, item.Value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(TKey key, TValue value)
		{
			var createdNode = _creator(key, value);
			BinaryTreeNode<TKey, TValue> current = Root, parent = null;

			while (!current.IsNull())
			{
				parent = current;

				if (createdNode.LessThan(current)) {
					current = current.Left;
				} else {
					current = current.Right;
				}
			}

			createdNode.Parent = parent;

			if (parent.IsNull()) {
				Root = createdNode;
			}
			else if (createdNode.LessThan(parent, KeyComparer)) {
				parent.Left = createdNode;
			}
			else {
				parent.Right = createdNode;
			}
			Count++;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public void Add(BinaryTreeNode<TKey, TValue> item)
		{
			if (item.IsNull()) {
				throw new ArgumentNullException(nameof(item));
			}

			if (item.IsLeaf) {
				Add(item.Key, item.Value);
			} 
			else
			{
				IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>> visitor = 
					new IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>>(item, TraverseMode.Preorder);

				visitor.Each(x => Add(x.Key, x.Value));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pairs"></param>
		public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			if (pairs.IsNull()) {
				throw new ArgumentNullException(nameof(pairs));
			}

			pairs.Each(x => Add(x.Key, x.Value));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public IEnumerable<KeyValuePair<TKey, TValue>> Find(TKey key)
		{
			return FindOrRemoveAllInternal(key, SearchingMode.SearchByKey).
				Transform(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value));
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public IEnumerable<KeyValuePair<TKey, TValue>> Find(TKey key, TValue value)
		{
			return FindOrRemoveAllInternal(key, SearchingMode.SearchByKeyAndValue, value).
				Transform(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public IEnumerable<TValue> FindValues (TKey key)
		{
			IList<BinaryTreeNode<TKey, TValue>> searchResult = 
				FindOrRemoveAllInternal(key, SearchingMode.SearchByKey);

			if (searchResult.Count == 0) {
				throw new KeyNotFoundException("Key not found in BST");
			}

			return searchResult.Transform(x => x.Value);
		}

		/// <summary>
		/// Remove all nodes by key
		/// </summary>
		/// <param name="key"></param>
		public IEnumerable<TValue> RemoveAt (TKey key)
		{
			IList<BinaryTreeNode<TKey, TValue>> removedNodes = 
				FindOrRemoveAllInternal(key, SearchingMode.RemoveByKey);

			if (removedNodes.Count == 0) {
				throw new KeyNotFoundException("Key not found in BST");
			}

			return removedNodes.Transform(node => node.Value);
		}
		
		/// <summary>
		/// Remove all nodes by key and value
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Remove(TKey key, TValue value) =>
			FindOrRemoveAllInternal(key, SearchingMode.RemoveByKeyAndValue, value).Count > 0;

		/// <summary>
		/// Remvoe all nodes selected by key and value
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(BinaryTreeNode<TKey, TValue> item)
		{
			if (item.IsNull()) {
				throw new ArgumentNullException(nameof(item));
			}

			return Remove(item.Key, item.Value);
		}

		/// <summary>
		/// Remove all nodes from BST
		/// </summary>
		public void Clear()
		{
			Root = null;
			Count = 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(BinaryTreeNode<TKey, TValue>[] array, int arrayIndex)
		{
			if (array.IsNull()) {
				throw new ArgumentNullException(nameof(array));
			}

			if (arrayIndex < 0) {
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			}

			if (array.Length - arrayIndex < Count) {
				throw new ArgumentException("Invalid index");
			}

			var enumerator = GetEnumerator();
			for (int i = arrayIndex; i != array.Length || enumerator.MoveNext(); ++i)
			{
				array[i] = enumerator.Current;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator<BinaryTreeNode<TKey, TValue>> GetEnumerator() =>
			new IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>>(Root).GetEnumerator();

		/// <summary>
		/// Get's the string represantation of this BST
		/// </summary>
		/// <returns>
		/// Return string, formated as follows: "Count = {Count}" where count is <see cref="Count"/>
		/// </returns>
		public override string ToString() =>
			String.Format("Count = {0}", Count);

		#region Helper methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="mode"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private List<BinaryTreeNode<TKey, TValue>> FindOrRemoveAllInternal (TKey key, SearchingMode mode, TValue value = default(TValue))
		{
			List<BinaryTreeNode<TKey, TValue>> nodes =
				new List<BinaryTreeNode<TKey, TValue>>();

			BinaryTreeNode<TKey, TValue> current = Root;

			while (!current.IsNull())
			{
				BinaryTreeNode<TKey, TValue> foundNode =
					FindFromInternal(current, key, mode, value);

				if (!foundNode.IsNull())
				{
					nodes.Add(foundNode);

					if (mode.HasFlag(SearchingMode.Remove)) {
						current = ExcludeNodeInternal(foundNode);
					} 
					else {
						current = foundNode.Right;
					}
					
				} else {
					current = foundNode;
				}
			}

			return nodes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="current"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="searchBy"></param>
		/// <returns></returns>
		private BinaryTreeNode<TKey, TValue> FindFromInternal(BinaryTreeNode<TKey, TValue> current, TKey key, SearchingMode mode, TValue value = default(TValue))
		{
			bool searchNext = true;
			do
			{
				int compareResult = KeyComparer.Compare(key, current.Key);

				if (compareResult < 0)
				{
					current = current.Left;
				} else
				{
					if (compareResult == 0)
					{
						searchNext = mode.HasFlag(SearchingMode.ByValue) ?
							!ValueComparer.Equals(current.Value, value) : false;
					}

					if (searchNext) {
						current = current.Right;
					}
				}

			}
			while (!current.IsNull() && searchNext);

			return current;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="parent"></param>
		private BinaryTreeNode<TKey, TValue> ExcludeNodeInternal(BinaryTreeNode<TKey, TValue> node)
		{
			BinaryTreeNode<TKey, TValue> next = null;

			if (node.IsLeaf)
			{
				// if node hasn't child, just remove it
				node.Parent.DetachChild(node);
			}
			else if (node.HasLeftOnly)
			{
				// if node has only left child, then replace it:
				// node -> parent -> (left || right) with node -> left

				node.Parent.ReplaceChild(node, node.Left);
				node.Left.Parent = node.Parent;
				next = node.Left;
			}
			else if (node.HasRightOnly)
			{
				// if node has only right child, then replace it:
				// node -> parent -> (left || right) with node -> right

				node.Parent.ReplaceChild(node, node.Right);
				node.Right.Parent = node.Parent;
				next = node.Right;
			}
			else
			{
				// Otherwise, the worst case, when node contain two children
				// Find the most left node in right subtree
				BinaryTreeNode<TKey, TValue> minimum = node.Right.Leftmost;

				// detach founded node
				minimum.Parent.Left = minimum.Right;

				if (!minimum.Right.IsNull())
				{
					minimum.Right.Parent = minimum.Parent;
				}

				// store left, right, and parent nodes of detached node
				minimum.ChangeRelations(node);

				// change parent of child nodes of detached node
				node.SetParentForChildren(minimum);

				// preserve terminal case, when removing the root of the tree
				if (!node.Parent.IsNull())
				{
					// change child of parent in detached node
					node.Parent.ReplaceChild(node, minimum);
				} else
				{
					// change root of current BST
					Root = minimum;
				}

				next = minimum;
			}

			// remove node's relations for allowing node to be deleted with GC
			node.Detach();
			--Count;

			// return next starting point for searching
			return next;
		}

		#endregion

		#region Helper enum

		/// <summary>
		/// Helper enum
		/// </summary>
		private enum SearchingMode : byte
		{
			/// <summary>
			/// Remove or find by key
			/// </summary>
			ByKey = 0x08,

			/// <summary>
			/// Remove of find by value
			/// </summary>
			ByValue = 0x02,

			/// <summary>
			/// Find and remove node
			/// </summary>
			Remove = 0x80,

			/// <summary>
			/// Only find node
			/// </summary>
			Search = 0x20,

			/// <summary>
			/// Find node only by key
			/// </summary>
			SearchByKey = Search | ByKey,

			/// <summary>
			/// Find node by key and value
			/// </summary>
			SearchByKeyAndValue = Search | ByKey | ByValue,

			/// <summary>
			/// Remove node only by key
			/// </summary>
			RemoveByKey = Remove | ByKey,

			/// <summary>
			/// Remove node by key and value
			/// </summary>
			RemoveByKeyAndValue = Remove | ByKey | ByValue
		}

		#endregion

		private readonly BinaryNodeCreator _creator;
	}
}
