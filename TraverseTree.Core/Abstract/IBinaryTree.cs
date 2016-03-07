using System.Collections.Generic;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TNode"></typeparam>
	public interface IBinaryTree<TNode> : ICollection<TNode> where TNode : IBinaryHierarchical<TNode>
	{
		/// <summary>
		/// 
		/// </summary>
		TNode Root { get; }
	}
}
