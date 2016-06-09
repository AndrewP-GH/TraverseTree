using System;
using System.Collections.Generic;
using System.Windows.Input;

using TraverseTree.Core.Extensions;
using TraverseTree.Core.Models;
using TraverseTree.Visual.Abstract;
using TraverseTree.Visual.Models;
using TraverseTree.Visual.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TraverseTree.Visual.ViewModels
{
	public class MainViewModel : ObservableObject
	{
		#region Commands
		public ICommand CloseCommand { get; set; }
		public ICommand AboutCommand { get; set; }

		public ICommand GenerateTreeCommand { get; } 
		public ICommand StartTraverseTreeCommand { get; }
		public ICommand StopTraverseTreeCommand { get; }

		#endregion

		public string[] TraversalOrders { get; } =
			EnumHelper.Descriptions<TraverseMode>();

		public string[] KeyDistributions { get; } =
			EnumHelper.Descriptions<KeyDistributionType>();

		public TraverseMode TraverseOrder { get; set; }

		public KeyDistributionType KeyDistributionType { get; set; }

		public string MaximumCount { get; set; }

		public TreeViewModel TreeViewModel { get; } =
			new TreeViewModel();

		public StackViewModel StackViewModel { get; } =
			new StackViewModel();

		public MainViewModel()
		{
			GenerateTreeCommand = new RelayCommand(OnTreeGeneration, OnValidateTreeGeneration);
			StartTraverseTreeCommand = new RelayCommand(OnTreeTraverse);

			TraverseOrder = TraverseMode.Inorder;
			KeyDistributionType = KeyDistributionType.Uniform;
		}

		private bool OnValidateTreeGeneration(object arg = null) =>
			( Int32.TryParse(MaximumCount, out _maximumCount) && _maximumCount > 0 && _maximumCount < 10000 );

		private void OnTreeGeneration (object arg = null)
		{
			Clear();

			var generator = GetGenerator();

			for(int i = 0; i < _maximumCount; ++i)
			{
				_tree.Add(generator.CreateNode());
			}

			TreeViewModel.UpdateState(_tree);
		}

		private void OnTreeTraverse (object arg)
		{
			GetVisitor().Each(node => 
			{
				node.Value.VisualType = VisualTreeNodeType.Active;
				node.Value.VisualType = VisualTreeNodeType.InsertedToTree;
			});
		}

		private void Clear()
		{
			_tree.Clear();
			TreeViewModel.Collection.Clear();
		}

		private IEnumerable<BinaryTreeNode<int, ViewData>> GetVisitor() =>
			_tree.GetEnumerator(TraverseOrder, StackViewModel.Stack);

		private IBinaryNodeGenerator<int, ViewData> GetGenerator()
		{
			if (KeyDistributionType == KeyDistributionType.Uniform) {
				return new FakeGenerator();
			}

			throw new ArgumentException("Invalid argument");
		}

		private int _maximumCount;
		private BinarySearchTree<int, ViewData> _tree = new BinarySearchTree<int, ViewData>();
	}
}
