using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Models;

namespace TraverseTree.Visual.Models
{
	public class VisualBinaryTreeNode<TKey, TValue> : BinaryTreeNode<TKey, TValue>, INotifyPropertyChanged
		where TKey: IComparable<TKey>
	{
		public static int LevelOffset { get; set; }

		public static int NodeOffset { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public int Radius { get; set; }

		public Point Center { get; }

		public Point From =>
			new Point(Center.X, Center.Y + Radius);

		public Point ToLeft { get; set; }

		public Point ToRight { get; set; }

		public SelectionMode SelectionMode
		{
			get { return _mode; }
			set
			{
				if(_mode != value)
				{
					_mode = value;
					OnPropertyChanged(nameof(SelectionMode));
				}
			}
		}
		
		public VisualBinaryTreeNode(TKey key, TValue value) : base(key, value)
		{
			
		}

		protected virtual void OnPropertyChanged(string property)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(property));
			}
		}

		private SelectionMode _mode;
	}

	public class ObservableStack<T> : ICollection, IReadOnlyCollection<T>, IEnumerable<T>, INotifyCollectionChanged
	{
		private Stack<T> _items;

		private ICollection Collection => _items;

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public int Count => _items.Count;

		bool ICollection.IsSynchronized => Collection.IsSynchronized;

		object ICollection.SyncRoot => Collection.SyncRoot;

		public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

		void ICollection.CopyTo(Array array, int index) => Collection.CopyTo(array, index);

		IEnumerator IEnumerable.GetEnumerator() => ( (IEnumerable)_items ).GetEnumerator();
	}

	public class ObservableTreeNodeStack<TKey, TValue> : ObservableStack<VisualBinaryTreeNode<TKey, TValue>>, ICollectionDecorator<VisualBinaryTreeNode<TKey, TValue>>
		where TKey : IComparable<TKey>
	{
		public VisualBinaryTreeNode<TKey, TValue> Top
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public VisualBinaryTreeNode<TKey, TValue> Get()
		{
			throw new NotImplementedException();
		}

		public void Put(VisualBinaryTreeNode<TKey, TValue> item)
		{
			throw new NotImplementedException();
		}
	}
}
