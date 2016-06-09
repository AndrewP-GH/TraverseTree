using System;

using TraverseTree.Core.Models;

namespace TraverseTree.Visual.Abstract
{
	public interface IBinaryNodeGenerator<TKey, TValue> where TKey : IComparable<TKey>
	{
		BinaryTreeNode<TKey, TValue> CreateNode();
		BinaryTreeNode<TKey, TValue> CreateNode(TValue value);
	}
}
