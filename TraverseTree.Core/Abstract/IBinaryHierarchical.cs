using System;

namespace TraverseTree.Core.Abstract
{
	public interface IBinaryHierarchical<out THierarchical> where THierarchical : IBinaryHierarchical<THierarchical>
	{
		THierarchical Left { get; }

		THierarchical Right { get; }

		THierarchical Parent { get; }
	}
}
