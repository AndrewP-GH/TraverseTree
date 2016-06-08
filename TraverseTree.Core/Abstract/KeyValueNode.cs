using System;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Abstract
{
	public abstract class KeyValueNode<TKey, TValue> : Node<TValue>, IKeyValueNode<TKey, TValue>
		where TKey: IComparable<TKey>
	{
		public TKey Key => _key;

		protected KeyValueNode(TKey key, TValue value) : base(value)
		{
			key.NullGuardAssign(out _key, nameof(Key));
		}

		public override string ToString() =>
			$"[ Key : {Key}; Value : {Value} ]";

		private readonly TKey _key;
	}
}
