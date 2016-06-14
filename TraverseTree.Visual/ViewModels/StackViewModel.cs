using System;
using System.Collections.Specialized;

using TraverseTree.Core.Models;
using TraverseTree.Core.Extensions;
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
			set
			{
				UpdateValue(ref _maximumHeight, value, nameof(MaximumHeight));
				OnPropertyChanged(nameof(ExpectedHeight));
			}
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
			set
			{
				UpdateValue(ref _actualHeight, value, nameof(ActualHeight));
			}
		}

		public StackViewModel(IActionManager manager)
		{
			manager.NullGuardAssign(out _manager, nameof(manager));
			_stack = new ObservableStack<Node>();
			_stack.CollectionChanged += new NotifyCollectionChangedEventHandler(OnTreeTraversal);
		}

		public void OnTreeTraversal (object sender, NotifyCollectionChangedEventArgs args)
		{
			int count = _stack.Count;

			if (args.Action == NotifyCollectionChangedAction.Add)
			{
				if (args.NewItems != null && args.NewItems.Count > 0)
				{
					_manager.RegisterAction(() => {
						ActualHeight = count;
						( (Node)args.NewItems[0] ).Value.VisualType = VisualTreeNodeType.InsertedForTraverse;
					});
				}

			} else if (args.Action == NotifyCollectionChangedAction.Remove)
			{
				if (args.OldItems != null && args.OldItems.Count > 0)
				{
					_manager.RegisterAction(() => {
						ActualHeight = count;
						( (Node)args.OldItems[0] ).Value.VisualType = VisualTreeNodeType.InsertedToTree;
					});
				}
			}
		}

		private int _maximumHeight;
		private int _actualHeight;
		private readonly IActionManager _manager;
		private readonly ObservableStack<Node> _stack;
	}
}
