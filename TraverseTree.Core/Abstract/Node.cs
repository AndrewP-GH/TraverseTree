using System;
using System.Collections.Generic;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Abstract
{
	public abstract class Node<TValue> : INode<TValue>
	{
		public TValue Value { get; set; }

		public abstract IReadOnlyList<INode<TValue>> InnerNodes { get; }

		protected Node() { }

		protected Node(TValue value)
		{
			value.NullGuard(nameof(value));
			Value = value;
		}

		public override string ToString() => Value.ToString();
	}
}
