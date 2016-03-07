using System;
using System.Collections.Generic;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Abstract
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
		protected Node() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		protected Node(TValue value)
		{
			if (value.IsNull())
			{
				throw new ArgumentNullException(nameof(value));
			}

			Value = value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString() => Value.ToString();
	}
}
