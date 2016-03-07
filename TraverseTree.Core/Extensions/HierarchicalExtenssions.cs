using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class HierarchicalExtenssions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static int RecursiveCount<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.IsNull() ? 0 : 1 + node.Left.RecursiveCount() + node.Right.RecursiveCount();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static int RecursiveHeight<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.IsNull() ? 0 : 1 + Math.Max(node.Left.RecursiveHeight(), node.Right.RecursiveHeight());

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static bool IsLeaf<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.Right.IsNull() && node.Left.IsNull();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static bool HasLeftOnly<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			node.Left.IsNull() && !node.Right.IsNull();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		internal static bool HasRightOnly<TNode>(this IBinaryHierarchical<TNode> node) where TNode : IBinaryHierarchical<TNode> =>
			!node.Left.IsNull() && node.Right.IsNull();
	}
}
