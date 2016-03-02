using System.Collections.Generic;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface INode<out T>
	{
		/// <summary>
		/// 
		/// </summary>
		T Value { get; }

		/// <summary>
		/// 
		/// </summary>
		IEnumerable<INode<T>> InnerNodes { get; }
	}
}
