using System.Collections.Generic;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TNode"></typeparam>
	public interface IBinaryOrderedTree<TKey, TValue, TNode> : ICollection<TNode> 
		where TNode : IBinaryHierarchical<TNode>, IKeyValueNode<TKey, TValue>
	{
		/// <summary>
		/// 
		/// </summary>
		TNode Root { get; }
	}
}
