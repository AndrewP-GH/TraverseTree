using System;
using System.Collections;
using System.Collections.Generic;
using TraverseTree.Core.Models;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// Basic traverse functionality for binary tree
	/// </summary>
	/// <typeparam name="TKey">Key parameter for binary tree <see cref="IComparable{TKey}"/></typeparam>
	/// <typeparam name="TValue">Value parameter for binary tree</typeparam>
	public interface IBinaryNodeVisitor<TNode> : IEnumerable<TNode> where TNode : IBinaryHierarchical<TNode>
	{
		/// <summary>
		/// Get's or set's traverse mode <see cref="Abstract.TraverseMode"/>
		/// </summary>
		TraverseMode TraverseMode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		TNode StartNode { get; set; }
	}
}
