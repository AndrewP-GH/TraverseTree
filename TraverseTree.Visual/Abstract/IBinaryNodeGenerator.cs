using System;
using TraverseTree.Visual.Models;

namespace TraverseTree.Visual.Abstract
{
	public interface IBinaryNodeGenerator<TKey, TValue> where TKey : IComparable<TKey>
	{
		VisualBinaryTreeNode<TKey, TValue> CreateNode();
		VisualBinaryTreeNode<TKey, TValue> CreateNode(TValue value);
	}
}
