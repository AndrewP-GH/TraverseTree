using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Visual.Abstract;
using TraverseTree.Visual.Models;

namespace TraverseTree.Visual.ViewModels
{
	public class TreeViewModel : ObservableObject
	{
		public ObservableCollection<VisualBinaryTreeNode<int, string>> Collection => _collection;

		public TreeViewModel()
		{
			_collection = new ObservableCollection<VisualBinaryTreeNode<int, string>>();
			_collection.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);
		}

		protected void OnCollectionChanged (object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add)
			{
				if (args.NewItems != null && args.NewItems.Count > 0)
				{
					( (VisualBinaryTreeNode<int, string>)args.NewItems[0] ).TreeNodeType = VisualTreeNodeType.InsertedToTree;
				}
			} else if (args.Action == NotifyCollectionChangedAction.Remove && args.NewItems != null)
			{
				foreach(VisualBinaryTreeNode<int, string> node in args.OldItems)
				{
					node.TreeNodeType = VisualTreeNodeType.Hidden;
				}
			}
		}

		private readonly ObservableCollection<VisualBinaryTreeNode<int, string>> _collection;
	}
}
