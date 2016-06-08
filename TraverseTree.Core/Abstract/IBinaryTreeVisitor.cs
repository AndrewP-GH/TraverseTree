using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Abstract
{
	public interface IBinaryNodeVisitor<TNode> : IEnumerable<TNode> 
		where TNode : IBinaryHierarchical<TNode>
	{
		TraverseMode TraverseMode { get; set; }

		TNode StartNode { get; set; }
	}
}
