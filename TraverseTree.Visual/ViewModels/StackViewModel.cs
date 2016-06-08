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
	public class StackViewModel : ObservableObject
	{
		public int MaximumHeight
		{
			get
			{
				return _maximumHeight;
			}

			set
			{
				if (_maximumHeight == value)
				{
					return;
				}

				_maximumHeight = value;
				OnPropertyChanged(nameof(MaximumHeight));
			}
		}

		public int ExpectedHeight
		{
			get
			{
				return _expectedHeight;
			}

			set
			{
				if (_expectedHeight == value)
				{
					return;
				}

				_expectedHeight = value;
				OnPropertyChanged(nameof(ExpectedHeight));
			}
		}

		public int ActualHeight
		{
			get
			{
				return _actualHeight;
			}

			set
			{
				if (_actualHeight == value)
				{
					return;
				}

				_actualHeight = value;
				OnPropertyChanged(nameof(ActualHeight));
			}
		}

		public ObservableStack<BinaryTreeNode<int, string>> Collection => _collection;

		public StackViewModel()
		{
			_collection = new ObservableStack<BinaryTreeNode<int, string>>();
			_collection.CollectionChanged += new NotifyCollectionChangedEventHandler(OnTreeTraversal);
		}

		public void OnTreeTraversal (object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add)
			{
				if (args.NewItems != null && args.NewItems.Count > 0)
				{
					( (VisualBinaryTreeNode<int, string>)args.NewItems[0] ).TreeNodeType = VisualTreeNodeType.InsertedForTraverse;
				}

			} else if (args.Action == NotifyCollectionChangedAction.Remove)
			{
				if (args.OldItems != null && args.OldItems.Count > 0)
				{
					( (VisualBinaryTreeNode<int, string>)args.OldItems[0] ).TreeNodeType = VisualTreeNodeType.InsertedToTree;
				}
			}

			ActualHeight = Collection.Count;
		}

		private int _maximumHeight;
		private int _expectedHeight;
		private int _actualHeight;
		private readonly ObservableStack<BinaryTreeNode<int, string>> _collection;
	}
}
