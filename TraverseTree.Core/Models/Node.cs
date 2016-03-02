using System;
using System.Collections.Generic;
using TraverseTree.Core.Abstract;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Node<T> : INode<T>
	{
		/// <summary>
		/// 
		/// </summary>
		public T Value { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public abstract IEnumerable<INode<T>> InnerNodes { get; }

		/// <summary>
		/// 
		/// </summary>
		public Node() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public Node(T data)
		{
			if (data == null)
				throw new ArgumentNullException(nameof(data));

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
