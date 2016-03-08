using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Queue decorator, implements <see cref="ICollectionDecorator{TItem}"/>
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public class QueueDecorator<TItem> : Queue<TItem>, ICollectionDecorator<TItem>
	{
		/// <summary>
		/// 
		/// </summary>
		TItem ICollectionDecorator<TItem>.Top => Peek();

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		TItem ICollectionDecorator<TItem>.Get() => Dequeue();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		void ICollectionDecorator<TItem>.Put(TItem item) => Enqueue(item);

		/// <summary>
		/// 
		/// </summary>
		void ICollectionDecorator<TItem>.Clear() => Clear();
	}
}
