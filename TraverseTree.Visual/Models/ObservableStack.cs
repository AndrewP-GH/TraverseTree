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
		/// <summary>
		/// 
		/// </summary>
		private ICollection Collection => _collection;

		/// <summary>
		/// 
		/// </summary>
		bool ICollection.IsSynchronized => Collection.IsSynchronized;

		/// <summary>
		/// 
		/// </summary>
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
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();
		#endregion

		#region ICollection methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		void ICollection.CopyTo(Array array, int index) => Collection.CopyTo(array, index);
		#endregion

		#region Collection decorator
		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public void Put (T item)
		{
			if (item.IsNull()) {
				throw new ArgumentNullException(nameof(item));
			}

			_collection.Push(item);

			OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public T Get ()
		{
			T item = _collection.Pop();

			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);

			return item;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public T Top => _collection.Peek();

		/// <summary>
		/// 
		/// </summary>
		public void Clear ()
		{
			T[] items = _collection.ToArray();

			_collection.Clear();

			OnCollectionChanged(NotifyCollectionChangedAction.Remove, items);
		}
		#endregion

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T changedItem) =>
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem));

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T[] items) =>
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, items));

		private readonly Stack<T> _collection;
	}
}
