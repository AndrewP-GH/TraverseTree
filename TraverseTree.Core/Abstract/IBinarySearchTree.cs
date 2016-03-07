namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <typeparam name="TNode"></typeparam>
	public interface IBinarySearchTree<TKey, TValue, TNode> :
		IBinaryTree<TNode> where TNode : IKeyValueNode<TKey, TValue>, IBinaryHierarchical<TNode> { }
}
