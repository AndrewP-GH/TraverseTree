using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Abstract
{
	public interface IBinaryOrderedTree<TKey, TValue, TNode> : ICollection<TNode> 
		where TNode: IBinaryHierarchical<TNode>, IKeyValueNode<TKey, TValue>
		where TKey: IComparable<TKey>
	{
		TNode Root { get; }
	}
}
