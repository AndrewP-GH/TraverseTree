using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	public class IterativeBinaryNodeVisitor<TNode> : IBinaryNodeVisitor<TNode> where TNode : class, IBinaryHierarchical<TNode>
	{
		public TNode StartNode { get; set; }

		public TraverseMode TraverseMode { get; set; }

		public IterativeBinaryNodeVisitor(TNode startNode) :
			this(startNode, new StackDecorator<TNode>(), TraverseMode.Inorder) { }

		public IterativeBinaryNodeVisitor(TNode startNode, TraverseMode traverseMode) :
			this(startNode, ( traverseMode == TraverseMode.Leverorder ) ? new QueueDecorator<TNode>() as ICollectionDecorator<TNode> : new StackDecorator<TNode>(), traverseMode) { }

		public IterativeBinaryNodeVisitor(TNode startNode, ICollectionDecorator<TNode> stack) :
			this(startNode, stack, TraverseMode.Inorder) { }

		public IterativeBinaryNodeVisitor(TNode startNode, ICollectionDecorator<TNode> decorator, TraverseMode traverseMode)
		{
			startNode.NullGuard(nameof(startNode));
			decorator.NullGuard(nameof(decorator));

			StartNode = startNode;
			TraverseMode = traverseMode;
			_nodes = decorator;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<TNode> GetEnumerator()
		{
			switch (TraverseMode)
			{
				case TraverseMode.Inorder:
					return new InorderEnumerator(this);
				case TraverseMode.Postorder:
					return new PostorderEnumerator(this);
				case TraverseMode.Leverorder:
					return new LevelorderEnumerator(this);
				default:
					return new PreorderEnumerator(this);
			}
		}

		private readonly ICollectionDecorator<TNode> _nodes;

		internal abstract class BaseEnumerator : IEnumerator<TNode>
		{
			object IEnumerator.Current => Current;

			public TNode Current => _current;

			public virtual void Dispose() => Reset();
			
			public virtual void Reset()
			{
				_current = null;
				_pointer = _start;
				_nodes.Clear();
			} 

			public bool MoveNext()
			{
				bool stop = ( _nodes.Count > 0 || !_pointer.IsNull() );

				if (stop) {
					AdvanceNext();
				} else {
					Reset();
				}

				return stop;
			}

			protected BaseEnumerator(IterativeBinaryNodeVisitor<TNode>  visitor)
			{
				_current = null;
				_start = visitor.StartNode;
				_pointer = visitor.StartNode;
				_nodes = visitor._nodes;
			}

			protected abstract void AdvanceNext();

			protected TNode _current;
			protected TNode _pointer;
			protected readonly TNode _start;
			protected readonly ICollectionDecorator<TNode> _nodes;
		}

		internal sealed class InorderEnumerator : BaseEnumerator
		{
			public InorderEnumerator(IterativeBinaryNodeVisitor<TNode> holder) : base(holder) { }

			protected override void AdvanceNext()
			{
				if (!_pointer.IsNull())
				{
					while (!_pointer.IsNull())
					{
						_nodes.Put(_pointer);
						_pointer = _pointer.Left;
					}
				}

				_pointer = _nodes.Get();
				_current = _pointer;
				_pointer = _pointer.Right;
			}
		}

		internal sealed class PreorderEnumerator : BaseEnumerator
		{
			public PreorderEnumerator(IterativeBinaryNodeVisitor<TNode> holder) : base(holder) { }

			protected override void AdvanceNext()
			{
				if (_pointer.IsNull())
				{
					_pointer = _nodes.Get();
				}

				_current = _pointer;

				if (!_pointer.Right.IsNull())
				{
					_nodes.Put(_pointer.Right);
				}

				_pointer = _pointer.Left;
			}
		}

		// TODO: Implenent postorder traversal
		internal sealed class PostorderEnumerator : BaseEnumerator
		{
			public PostorderEnumerator(IterativeBinaryNodeVisitor<TNode> holder) : base(holder) { }

			protected override void AdvanceNext()
			{
				throw new NotImplementedException();

				//if (_pointer.IsLeaf()) {
				//	_pointer = StoredNodes.Get();
				//}
				//
				//while(!_pointer.IsLeaf())
				//{
				//	if (!_pointer.Left.IsNull() && _pointer.Left != _current) {
				//		StoredNodes.Put(_pointer.Left);
				//	} else if (!_pointer.Right.IsNull() && _pointer.Right != _current) {
				//		StoredNodes.Put(_pointer.Right);
				//	}
				//
				//	_pointer = _pointer.Left;
				//}
				//
				//_current = _pointer;
			}
		}

		internal sealed class LevelorderEnumerator : BaseEnumerator
		{
			public LevelorderEnumerator(IterativeBinaryNodeVisitor<TNode> holder) : base(holder) { }

			protected override void AdvanceNext()
			{
				if (_nodes.IsEmpty() && _current.IsNull()) {
					_nodes.Put(_pointer);
				}

				_pointer = _nodes.Get();

				if (!_pointer.Left.IsNull()) {
					_nodes.Put(_pointer.Left);
				}

				if (!_pointer.Right.IsNull()) {
					_nodes.Put(_pointer.Right);
				}

				_current = _pointer;

				if (_nodes.IsEmpty() && _pointer.IsLeaf()) {
					_pointer = null;
				}
			}
		}
	}
}
