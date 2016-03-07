using System.Collections.Generic;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public interface INode<out TValue>
	{
		/// <summary>
		/// 
		/// </summary>
		TValue Value { get; }

		/// <summary>
		/// 
		/// </summary>
		IEnumerable<INode<TValue>> InnerNodes { get; }
	}
}
