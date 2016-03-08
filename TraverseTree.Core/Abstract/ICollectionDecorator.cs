using System.Collections;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// Collection decorator for changing traverse mode
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public interface ICollectionDecorator<TItem> : ICollection
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		TItem Get();

		/// <summary>
		/// 
		/// </summary>
		TItem Top { get; }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		void Put(TItem item);

		/// <summary>
		/// 
		/// </summary>
		void Clear();
	}
}
