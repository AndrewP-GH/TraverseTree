using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	public class BinarySearchTree<TKey, TValue> : IBinaryOrderedTree<TKey, TValue, BinaryTreeNode<TKey, TValue>> 
		where TKey : IComparable<TKey> 
	{
		public BinaryTreeNode<TKey, TValue> Root { get; private set; }

		public int Count { get; private set; }

		public int Height => Root.RecursiveHeight();

		public bool IsReadOnly => false;

		public bool IsEmpty => Root.IsNull();

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
				value.NullGuardAssign(out _keyComparer, nameof(value));
			}
		}

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
				value.NullGuardAssign(out _valueComparer, nameof(value));
			}
		}

		public ICollection<TKey> Keys =>
			this.Select(x => x.Key).ToArray();

		public ICollection<TValue> Values =>
			this.Select(x => x.Value).ToArray();

		public BinarySearchTree() : 
			this(null, Comparer<TKey>.Default, EqualityComparer<TValue>.Default) { }

		public BinarySearchTree(BinaryTreeNode<TKey, TValue> root) : 
			this(root, Comparer<TKey>.Default, EqualityComparer<TValue>.Default) { }

		public BinarySearchTree(IComparer<TKey> keyComparer) : 
			this(null, keyComparer, EqualityComparer<TValue>.Default) { }

		public BinarySearchTree(BinaryTreeNode<TKey, TValue> root, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Root = root;
			KeyComparer = keyComparer;
			ValueComparer = valueComparer;
			Count = IsEmpty ? 0 : 1;
		}

		public bool ContainsKey(TKey key) =>
			FindOrRemoveAllInternal(key, SearchingMode.SearchByKey).Count > 0;

		public bool Contains(TKey key, TValue value) =>
			FindOrRemoveAllInternal(key, SearchingMode.SearchByKeyAndValue, value).Count > 0;

		public bool Contains(BinaryTreeNode<TKey, TValue> item)
		{
			item.NullGuard(nameof(item));
			return Contains(item.Key, item.Value);
		}

		public void Add(BinaryTreeNode<TKey, TValue> node)
		{
			node.NullGuard(nameof(node));

			if (node.IsLeaf)
			{
				AddInternal(node);
			} 
			else
			{
				( new IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>>(node, TraverseMode.Preorder) )
					.Each(x => Add(x));
			}
		}

		public void AddRange(IEnumerable<BinaryTreeNode<TKey, TValue>> nodes)
		{
			nodes.NullGuard(nameof(nodes));
			nodes.Each(x => Add(x));
		}

		public IEnumerable<BinaryTreeNode<TKey, TValue>> Find(TKey key) =>
			FindOrRemoveAllInternal(key, SearchingMode.SearchByKey);

		public IEnumerable<KeyValuePair<TKey, TValue>> FindPairs(TKey key)
		{
			return FindOrRemoveAllInternal(key, SearchingMode.SearchByKey).
				Select(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value));
		}

		public IEnumerable<BinaryTreeNode<TKey, TValue>> Find(TKey key, TValue value) =>
			FindOrRemoveAllInternal(key, SearchingMode.SearchByKeyAndValue, value);

		public IEnumerable<KeyValuePair<TKey, TValue>> FindPairs(TKey key, TValue value)
		{
			return FindOrRemoveAllInternal(key, SearchingMode.SearchByKeyAndValue, value).
				Select(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value));
		}

		public IEnumerable<TValue> FindValues (TKey key)
		{
			IList<BinaryTreeNode<TKey, TValue>> searchResult = 
				FindOrRemoveAllInternal(key, SearchingMode.SearchByKey);

			if (searchResult.Count == 0) {
				throw new KeyNotFoundException("Key not found in BST");
			}

			return searchResult.Select(x => x.Value);
		}

		public IEnumerable<TValue> RemoveAt (TKey key)
		{
			IList<BinaryTreeNode<TKey, TValue>> removedNodes = 
				FindOrRemoveAllInternal(key, SearchingMode.RemoveByKey);

			if (removedNodes.Count == 0) {
				throw new KeyNotFoundException("Key not found in BST");
			}

			return removedNodes.Select(node => node.Value);
		}
		
		public bool Remove(TKey key, TValue value) =>
			FindOrRemoveAllInternal(key, SearchingMode.RemoveByKeyAndValue, value).Count > 0;

		public bool Remove(BinaryTreeNode<TKey, TValue> item)
		{
			item.NullGuard(nameof(item));
			return Remove(item.Key, item.Value);
		}

		public void Clear()
		{
			Root = null;
			Count = 0;
		}

		public void CopyTo(BinaryTreeNode<TKey, TValue>[] array, int arrayIndex)
		{
			array.NullGuard(nameof(array));

			if (arrayIndex < 0) {
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			}

			if (array.Length - arrayIndex < Count) {
				throw new ArgumentException("Invalid index");
			}

			var enumerator = GetEnumerator();
			for (int i = arrayIndex; i != array.Length && enumerator.MoveNext(); ++i)
			{
				array[i] = enumerator.Current;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<BinaryTreeNode<TKey, TValue>> GetEnumerator() =>
			new IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>>(Root).GetEnumerator();

		public IEnumerable<BinaryTreeNode<TKey, TValue>> GetEnumerator(TraverseMode mode) =>
			new IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>>(Root, mode);

		public IEnumerable<BinaryTreeNode<TKey, TValue>> GetEnumerator(TraverseMode mode, ICollectionDecorator<BinaryTreeNode<TKey, TValue>> decorator) =>
			new IterativeBinaryNodeVisitor<BinaryTreeNode<TKey, TValue>>(Root, decorator, mode);

		public override string ToString() => $"Count = {Count}";

		#region Helper methods

		private void AddInternal(BinaryTreeNode<TKey, TValue> node)
		{
			BinaryTreeNode<TKey, TValue> current = Root, parent = null;

			while (!current.IsNull())
			{
				parent = current;

				if (node.LessThan(current))
				{
					current = current.Left;
				} else {
					current = current.Right;
				}
			}

			if (parent.IsNull())
			{
				Root = node;
			} else if (node.LessThan(parent, KeyComparer))
			{
				parent.Left = node;
			} else {
				parent.Right = node;
			}

			node.Parent = parent;

			Count++;
		}

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

			// change count of nodes
			--Count;

			// return next starting point for searching
			return next;
		}

		#endregion

		#region Helper enum

		private enum SearchingMode : byte
		{
			ByKey = 0x08,
			ByValue = 0x02,
			Remove = 0x80,
			Search = 0x20,
			SearchByKey = Search | ByKey,
			SearchByKeyAndValue = Search | ByKey | ByValue,
			RemoveByKey = Remove | ByKey,
			RemoveByKeyAndValue = Remove | ByKey | ByValue
		}

		#endregion
	}
}
