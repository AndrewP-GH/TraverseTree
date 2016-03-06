using System;
using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public abstract class Node<TValue> : INode<TValue>
	{
		/// <summary>
		/// 
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public abstract IEnumerable<INode<TValue>> InnerNodes { get; }

		/// <summary>
		/// 
		/// </summary>
		public Node() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public Node(TValue data)
		{
			if (data == null) { 
				throw new ArgumentNullException(nameof(data));
			}

			Value = data;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString() =>
			Value.ToString();
	}
}
