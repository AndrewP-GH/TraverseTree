using System;

namespace TraverseTree.Core.Abstract
{
	public interface IKeyValueNode<out TKey, out TValue> : INode<TValue> 
		where TKey: IComparable<TKey>
	{
		TKey Key { get; }
	}
}
