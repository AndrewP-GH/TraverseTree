using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Extensions
{
	public static class HierarchicalExtenssions
	{
		internal static int RecursiveCount<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.IsNull() ? 0 : 1 + node.Left.RecursiveCount() + node.Right.RecursiveCount();

		internal static int RecursiveHeight<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.IsNull() ? 0 : 1 + Math.Max(node.Left.RecursiveHeight(), node.Right.RecursiveHeight());

		public static bool IsLeaf<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.Left.IsNull() && node.Right.IsNull();

		public static bool IsLeftChild<TNode>(this IBinaryHierarchical<TNode> node, IBinaryHierarchical<TNode> child) where TNode : IBinaryHierarchical<TNode> =>
			Object.ReferenceEquals(node.Left, child);

		public static bool IsRightChild<TNode>(this IBinaryHierarchical<TNode> node, IBinaryHierarchical<TNode> child) where TNode : IBinaryHierarchical<TNode> =>
			Object.ReferenceEquals(node.Right, child);
	}
}
