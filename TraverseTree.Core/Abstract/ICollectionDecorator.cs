using System.Collections;

namespace TraverseTree.Core.Abstract
{
	public interface ICollectionDecorator<TItem> : ICollection
	{
		TItem Top { get; }

		TItem Get();
		void Put(TItem item);
		void Clear();
	}
}
