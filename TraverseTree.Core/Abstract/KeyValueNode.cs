using System;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public abstract class KeyValueNode<TKey, TValue> : Node<TValue>, IKeyValueNode<TKey, TValue>
	{
		/// <summary>
		/// 
		/// </summary>
		public TKey Key { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		protected KeyValueNode(TKey key, TValue value) : base(value)
		{
			if (key.IsNull())
			{
				throw new ArgumentNullException(nameof(value));
			}

			Key = key;
		}

		/// <summary>
		/// Get's string representation from this node
		/// </summary>
		/// <returns>String, formatted as follows:[ Key {Key}; Value {Value} ]</returns>
		public override string ToString() =>
			String.Format("[ Key : {0}; Value : {1} ]", Key, Value);
	}
}
