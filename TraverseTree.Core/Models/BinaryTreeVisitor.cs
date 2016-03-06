using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Core.Models
{
	//public class BinaryTreeVisitor<T> : IBinaryTreeVisitor<T>, IEnumerator<T>
	//{
	//	private Func<BinaryTree<T>, IEnumerable<T>> _traverse;

	//	private TraverseMode _traverseMode;
	//	public TraverseMode TraverseMode
	//	{
	//		get
	//		{
	//			return _traverseMode;
	//		}
	//		set
	//		{
	//			if (_traverseMode == value) return;

	//			_traverseMode = value;

	//			_traverse = _traverseMode == TraverseMode.InOrder ? new Func<BinaryTree<T>, IEnumerable<T>>(InorderTraverse) :
	//					  _traverseMode == TraverseMode.PostOrder ? new Func<BinaryTree<T>, IEnumerable<T>>(PostorderTraverse) :
	//																new Func<BinaryTree<T>, IEnumerable<T>>(PreorderTraverse);
	//		}
	//	}

	//	public T Current
	//	{
	//		get
	//		{
	//			throw new NotImplementedException();
	//		}
	//	}

	//	object IEnumerator.Current
	//	{
	//		get
	//		{
	//			throw new NotImplementedException();
	//		}
	//	}

	//	private BinaryTreeVisitor(TraverseMode mode)
	//	{

	//	}

	//	public IEnumerable<T> Visit(BinaryTree<T> tree) => _traverse(tree);

	//	private void TraverseAction()
	//	{

	//	}

	//	private IEnumerable<T> InorderTraverse(BinaryTree<T> tree)
	//	{

	//	}
	//	private IEnumerable<T> PostorderTraverse(BinaryTree<T> tree)
	//	{

	//	}
	//	private IEnumerable<T> PreorderTraverse(BinaryTree<T> tree)
	//	{

	//	}

	//	public bool MoveNext()
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public void Reset()
	//	{
	//		throw new NotImplementedException();
	//	}

	//	#region IDisposable Support
	//	private bool disposedValue = false; // To detect redundant calls

	//	protected virtual void Dispose(bool disposing)
	//	{
	//		if (!disposedValue)
	//		{
	//			if (disposing)
	//			{
	//				// TODO: dispose managed state (managed objects).
	//			}

	//			// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
	//			// TODO: set large fields to null.

	//			disposedValue = true;
	//		}
	//	}

	//	public void Dispose()
	//	{
	//		// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
	//		Dispose(true);
	//		// TODO: uncomment the following line if the finalizer is overridden above.
	//		// GC.SuppressFinalize(this);
	//	}
	//	#endregion
	//}

	public abstract class BinaryTreeNodeVisitorBase<TKey, TValue> : IBinaryTreeNodeVisitor<TKey, TValue>
	{
		public TraverseMode TraverseMode { get; set; }

		public IEnumerable<KeyValuePair<TKey, TValue>> VisitTree(BinaryTreeNode<TKey, TValue> root)
		{
			if (root.IsNull()) {
				throw new ArgumentNullException(nameof(root));
			}

			return Implementation()(root);
		}

		protected BinaryTreeNodeVisitorBase() { }

		protected Func<BinaryTreeNode<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>> Implementation ()
		{
			if (TraverseMode == TraverseMode.Preorder)
				return new Func<BinaryTreeNode<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>(PreorderVisit);
			else if (TraverseMode == TraverseMode.Inorder)
				return new Func<BinaryTreeNode<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>(InorderVisit);

			return new Func<BinaryTreeNode<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>(PostorderVisit);
		}

		protected abstract IEnumerable<KeyValuePair<TKey, TValue>> PreorderVisit(BinaryTreeNode<TKey, TValue> root);
		protected abstract IEnumerable<KeyValuePair<TKey, TValue>> InorderVisit(BinaryTreeNode<TKey, TValue> root);
		protected abstract IEnumerable<KeyValuePair<TKey, TValue>> PostorderVisit(BinaryTreeNode<TKey, TValue> root);
	}

	public class IterativeBinaryTreeNodeVisitor<TKey, TValue> : BinaryTreeNodeVisitorBase<TKey, TValue>
	{
		public IterativeBinaryTreeNodeVisitor(TraverseMode mode)
		{
			TraverseMode = mode;
		}

		protected override IEnumerable<KeyValuePair<TKey, TValue>> InorderVisit(BinaryTreeNode<TKey, TValue> root)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<KeyValuePair<TKey, TValue>> PostorderVisit(BinaryTreeNode<TKey, TValue> root)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<KeyValuePair<TKey, TValue>> PreorderVisit(BinaryTreeNode<TKey, TValue> root)
		{
			throw new NotImplementedException();
		}
	}
}
