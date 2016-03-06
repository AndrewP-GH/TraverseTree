using System;
using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// Define traverse mode.
	/// </summary>
	public enum TraverseMode
	{
		/// <summary>
		/// Visit at first current node, than left node and right node.
		/// </summary>
		Preorder,

		/// <summary>
		/// Visit at first left node, than current node and right node.
		/// </summary>
		Inorder,

		/// <summary>
		/// Visit at first left node, than right node and current node.
		/// </summary>
		Postorder
	}

	/// <summary>
	/// Basic traverse functionality for binary tree
	/// </summary>
	/// <typeparam name="TKey">Key parameter for binary tree <see cref="IComparable{TKey}"/></typeparam>
	/// <typeparam name="TValue">Value parameter for binary tree</typeparam>
	public interface IBinaryTreeNodeVisitor<TKey, TValue>
	{
		/// <summary>
		/// Get's or set's traverse mode <see cref="Abstract.TraverseMode"/>
		/// </summary>
		TraverseMode TraverseMode { get; set; }

		/// <summary>
		/// Visit each node of binary tree
		/// </summary>
		/// <param name="root">The root node</param>
		/// <returns>
		/// Retrun <see cref="IEnumerable{T}"/>
		/// </returns>
		IEnumerable<KeyValuePair<TKey, TValue>> VisitTree(BinaryTreeNode<TKey, TValue> root);
	}
}
