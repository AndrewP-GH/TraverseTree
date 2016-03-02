using System;
using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// Define traverse mode.
	/// In case of Inorder : left node, current, right node.
	/// In case of Postorder: left node, right node, current node.
	/// In case of Preorder: current node, left node, right node.
	/// </summary>
	public enum TraverseMode
	{
		InOrder,
		PostOrder,
		PreOrder
	}

	/// <summary>
	/// Basic traverse functionality for binary tree
	/// </summary>
	/// <typeparam name="TKey">Key parameter for binary tree <see cref="BinaryTree{TKey, TValue}"/></typeparam>
	/// <typeparam name="TValue">Value parameter for binary tree <see cref="BinaryTree{TKey, TValue}"/></typeparam>
	public interface IBinaryTreeVisitor<TKey, TValue> where TKey : IComparable<TKey>
	{
		/// <summary>
		/// Visit each node and perform action <see cref="Action{TKey}"/>
		/// </summary>
		/// <param name="tree">Binary tree to visit</param>
		/// <param name="action">Action to perform on each node</param>
		void Visit(BinaryTree<TKey, TValue> tree, Action<BinaryTreeNode<TKey, TValue>> action);

		/// <summary>
		/// Visit each node and perform action that return some value <see cref="Func{T, TResult}"/>
		/// </summary>
		/// <typeparam name="TDest">Desctination parameter</typeparam>
		/// <param name="tree">Binary tree to visit</param>
		/// <param name="action">Action to perform</param>
		/// <returns>Aggregate result of action on each node: <see cref="IEnumerable{T}"/></returns>
		IEnumerable<TDest> Visit<TDest>(BinaryTree<TKey, TValue> tree, Func<BinaryTreeNode<TKey, TValue>, TDest> action);
	}
}
