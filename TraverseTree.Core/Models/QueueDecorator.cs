using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	public class QueueDecorator<TItem> : Queue<TItem>, ICollectionDecorator<TItem>
	{
		TItem ICollectionDecorator<TItem>.Top => Peek();

		TItem ICollectionDecorator<TItem>.Get() => Dequeue();

		void ICollectionDecorator<TItem>.Put(TItem item) => Enqueue(item);
	}
}
