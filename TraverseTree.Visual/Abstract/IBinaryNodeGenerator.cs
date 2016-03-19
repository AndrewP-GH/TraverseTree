using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Visual.Models;

namespace TraverseTree.Visual.Abstract
{
	public interface IBinaryNodeGenerator<TKey, TValue> where TKey : IComparable<TKey>
	{
		VisualBinaryTreeNode<TKey, TValue> CreateNode();
		VisualBinaryTreeNode<TKey, TValue> CreateNode(TValue value);
	}
}
