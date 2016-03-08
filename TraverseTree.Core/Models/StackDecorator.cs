using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Stack decorator, implements <see cref="ICollectionDecorator{TItem}"/>
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public class StackDecorator<TItem> : Stack<TItem>, ICollectionDecorator<TItem>
	{
		/// <summary>
		/// 
		/// </summary>
		TItem ICollectionDecorator<TItem>.Top => Peek();

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		TItem ICollectionDecorator<TItem>.Get() => Pop();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		void ICollectionDecorator<TItem>.Put(TItem item) => Push(item);

		/// <summary>
		/// 
		/// </summary>
		void ICollectionDecorator<TItem>.Clear() => Clear();
	}
}
