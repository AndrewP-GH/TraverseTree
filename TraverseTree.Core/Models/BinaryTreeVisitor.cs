using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TNode"></typeparam>
	public class IterativeBinaryNodeVisitor<TNode> : IBinaryNodeVisitor<TNode> where TNode : class, IBinaryHierarchical<TNode>
	{
		public TNode StartNode => _start;

		public TraverseMode TraverseMode { get; set; }

		public IterativeBinaryNodeVisitor(TNode startNode) :
			this(startNode, new StackDecorator<TNode>(), TraverseMode.Inorder) { }

		public IterativeBinaryNodeVisitor(TNode startNode, TraverseMode traverseMode) :
			this(startNode, new StackDecorator<TNode>(), traverseMode) { }

		public IterativeBinaryNodeVisitor(TNode startNode, ICollectionDecorator<TNode> stack) :
			this(startNode, stack, TraverseMode.Inorder) { }

		public IterativeBinaryNodeVisitor(TNode startNode, ICollectionDecorator<TNode> stack, TraverseMode traverseMode)
		{
			if (startNode.IsNull()) {
				throw new ArgumentNullException(nameof(startNode));
			}

			if (stack.IsNull()) {
				throw new ArgumentNullException(nameof(stack));
			}

			_start = startNode;
			_stack = stack;
			TraverseMode = traverseMode;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<TNode> GetEnumerator()
		{
			if (TraverseMode == TraverseMode.Inorder) {
				return new InorderEnumerator(this);
			} else if (TraverseMode == TraverseMode.Postorder) {
				return new PostorderEnumerator(this);
			}
			
			return new PreorderEnumerator(this);
		}

		private readonly TNode _start;
		private readonly ICollectionDecorator<TNode> _stack;

		/// <summary>
		/// 
		/// </summary>
		internal abstract class BaseEnumerator : IEnumerator<TNode>
		{
			/// <summary>
			/// 
			/// </summary>
			object IEnumerator.Current => Current;

			/// <summary>
			/// 
			/// </summary>
			public TNode Current => _current;

			/// <summary>
			/// 
			/// </summary>
			protected ICollectionDecorator<TNode> StoredNodes => _holder._stack;

			/// <summary>
			/// 
			/// </summary>
			public virtual void Dispose()
			{
				Reset();
				_holder = null;
			}
			
			/// <summary>
			/// 
			/// </summary>
			public virtual void Reset()
			{
				_current = null;
				_pointer = _holder.StartNode;
				StoredNodes.Clear();
			}

			/// <summary>
			/// Advanse enumerator in the next element to the collection
			/// </summary>
			/// <returns>
			/// True if the enumerator was successfully advanced to the next element; 
			/// false if the enumerator has passed the end of the collection.
			/// </returns>
			public bool MoveNext()
			{
				bool stop = ( !StoredNodes.IsEmpty() || !_pointer.IsNull() );

				if (stop) {
					AdvanceNext();
				} else {
					Reset();
				}

				return stop;
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="holder"></param>
			protected BaseEnumerator(IterativeBinaryNodeVisitor<TNode> holder)
			{
				_current = null;
				_pointer = holder._start;
				_holder = holder;
			}

			/// <summary>
			/// Implement's in derived classes
			/// </summary>
			protected abstract void AdvanceNext();

			protected TNode _current;
			protected TNode _pointer;
			protected IterativeBinaryNodeVisitor<TNode> _holder;
		}

		/// <summary>
		/// 
		/// </summary>
		internal class InorderEnumerator : BaseEnumerator
		{
			public InorderEnumerator(IterativeBinaryNodeVisitor<TNode> holder) : base(holder) { }

			/// <summary>
			/// Inorder traversal
			/// </summary>
			protected override void AdvanceNext()
			{
				if (!_pointer.IsNull())
				{
					while (!_pointer.IsNull())
					{
						StoredNodes.Put(_pointer);
						_pointer = _pointer.Left;
					}
				}

				_pointer = StoredNodes.Get();
				_current = _pointer;
				_pointer = _pointer.Right;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		internal class PreorderEnumerator : BaseEnumerator
		{
			public PreorderEnumerator(IterativeBinaryNodeVisitor<TNode> holder) : base(holder) { }

			protected override void AdvanceNext()
			{
				if (_pointer.IsNull())
				{
					_pointer = StoredNodes.Get();
				}

				_current = _pointer;

				if (!_pointer.Right.IsNull())
				{
					StoredNodes.Put(_pointer.Right);
				}

				_pointer = _pointer.Left;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		// TODO: Implenent postorder traversal
		internal class PostorderEnumerator : BaseEnumerator
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
	}
}
