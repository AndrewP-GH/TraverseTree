using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	public class StackDecorator<TItem> : Stack<TItem>, ICollectionDecorator<TItem>
	{
		TItem ICollectionDecorator<TItem>.Top => Peek();

		TItem ICollectionDecorator<TItem>.Get() => Pop();

		void ICollectionDecorator<TItem>.Put(TItem item) => Push(item);
	}
}
