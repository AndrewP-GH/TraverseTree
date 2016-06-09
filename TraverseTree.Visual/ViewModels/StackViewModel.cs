using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Core.Models;
using TraverseTree.Visual.Abstract;
using TraverseTree.Visual.Models;


namespace TraverseTree.Visual.ViewModels
{
	using Node = BinaryTreeNode<int, ViewData>;

	public class StackViewModel : ObservableObject
	{
		public ObservableStack<Node> Stack
		{
			get { return _stack; }
		}

		public int MaximumHeight
		{
			get { return _maximumHeight; }
			set { UpdateValue(ref _maximumHeight, value, nameof(MaximumHeight)); }
		}

		public int ExpectedHeight
		{
			get
			{
				return (int)Math.Round(Math.Sqrt(Math.PI * MaximumHeight) - 1.5 + 11.0 * Math.Sqrt(Math.PI / MaximumHeight * 1.0) / 24.0 + Math.Sqrt(1.0 / Math.Pow(MaximumHeight, 3)) );
			}
		}

		public int ActualHeight
		{
			get { return _actualHeight; }
			set { UpdateValue(ref _actualHeight, value, nameof(ActualHeight)); }
		}

		public StackViewModel()
		{
			_stack = new ObservableStack<Node>();
			_stack.CollectionChanged += new NotifyCollectionChangedEventHandler(OnTreeTraversal);
		}

		public void OnTreeTraversal (object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add)
			{
				if (args.NewItems != null && args.NewItems.Count > 0)
				{
					( (Node)args.NewItems[0] ).Value.VisualType = VisualTreeNodeType.InsertedForTraverse;
				}

			} else if (args.Action == NotifyCollectionChangedAction.Remove)
			{
				if (args.OldItems != null && args.OldItems.Count > 0)
				{
					( (Node)args.OldItems[0] ).Value.VisualType = VisualTreeNodeType.InsertedToTree;
				}
			}

			ActualHeight = _stack.Count;
		}

		private int _maximumHeight;
		private int _actualHeight;
		private readonly ObservableStack<Node> _stack;
	}
}
