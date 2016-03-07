namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public interface IKeyValueNode<out TKey, out TValue> : INode<TValue>
	{
		/// <summary>
		/// 
		/// </summary>
		TKey Key { get; }
	}
}
