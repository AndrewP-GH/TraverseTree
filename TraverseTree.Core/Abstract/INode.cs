using System.Collections.Generic;

namespace TraverseTree.Core.Abstract
{
	public interface INode<out TValue>
	{
		TValue Value { get; }

		IReadOnlyList<INode<TValue>> InnerNodes { get; }
	}
}
