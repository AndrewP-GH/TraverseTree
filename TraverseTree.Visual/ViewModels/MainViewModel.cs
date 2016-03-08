using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;
using TraverseTree.Core.Models;
using TraverseTree.Visual.Abstract;

namespace TraverseTree.Visual.ViewModels
{
	/// <summary>
	/// 
	/// </summary>
	public class MainViewModel : BaseViewModel
	{
		/// <summary>
		/// 
		/// </summary>
		public ICommand CloseCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICommand AboutCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICommand GenerateTreeCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICollectionDecorator<BinaryTreeNode<int, string>> NodeCollection
		{
			get { return _collection; }
			set
			{
				if (_collection != value)
				{
					_collection = value;
					base.OnPropertyChanged(nameof(NodeCollection));
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public TraverseMode TraverseOrder
		{
			get { return _traverseOrder; }
			set
			{
				if (_traverseOrder != value)
				{
					_traverseOrder = value;
					base.OnPropertyChanged(nameof(TraverseOrder));
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public MainViewModel()
		{
			_tree = new BinarySearchTree<int, string>();
		}

		private void OnGenerateTree ()
		{

		}

		private void ChangeTraverseOrder()
		{

		}

		private TraverseMode _traverseOrder;
		private ICollectionDecorator<BinaryTreeNode<int, string>> _collection;
		private readonly BinarySearchTree<int, string> _tree;
	}
}
