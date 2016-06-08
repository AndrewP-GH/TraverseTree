using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Visual.Models
{
	public class ObservableStack<T> : ICollectionDecorator<T>, IReadOnlyCollection<T>, ICollection, INotifyCollectionChanged
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		#region ICollection properties
		private ICollection Collection => _collection;

		bool ICollection.IsSynchronized => Collection.IsSynchronized;

		object ICollection.SyncRoot => Collection.SyncRoot;
		#endregion

		public int Count => _collection.Count;

		public ObservableStack()
		{
			_collection = new Stack<T>();
		}
		public ObservableStack(int capacity)
		{
			_collection = new Stack<T>(capacity);
		}
		public ObservableStack(IEnumerable<T> collection)
		{
			_collection = new Stack<T>(collection);
		}

		#region IEnumerable
		public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();
		#endregion

		#region ICollection methods
		void ICollection.CopyTo(Array array, int index) => Collection.CopyTo(array, index);
		#endregion

		#region Collection decorator
		public void Put (T item)
		{
			item.NullGuard(nameof(item));
			_collection.Push(item);

			OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
		}

		public T Get ()
		{
			T item = _collection.Pop();

			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);

			return item;
		}

		public T Top => _collection.Peek();

		public void Clear ()
		{
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, _collection.ToArray());
			_collection.Clear();
		}
		#endregion

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T changedItem) =>
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem));

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T[] items) =>
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, items));

		private readonly Stack<T> _collection;
	}
}
