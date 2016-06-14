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
using System.Linq;

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

		public string MaximumCount { get; set; } = 10.ToString();

		public int TreeHeight
		{
			get { return _treeHeight; }
			set
			{
				UpdateValue(ref _treeHeight, value, nameof(TreeHeight));
				OnPropertyChanged(nameof(ExpectedHeight));
			}
		}

		public int ExpectedHeight
		{
			get { return _maximumCount == 0 ? 0 : (int)Math.Round(Math.Sqrt(Math.PI * _maximumCount) - 1.5 + 11.0 * Math.Sqrt(Math.PI / _maximumCount * 1.0) / 24.0 + Math.Sqrt(1.0 / Math.Pow(_maximumCount, 3))); }
		}

		public TreeViewModel TreeViewModel { get; }

		public StackViewModel StackViewModel { get; } 

		public MainViewModel()
		{
			GenerateTreeCommand = new RelayCommand(OnTreeGeneration, OnValidateTreeGeneration);
			StartTraverseTreeCommand = new RelayCommand(OnStartTreeTraverse, OnValidateStartTreeTraverse);
			StopTraverseTreeCommand = new RelayCommand(OnStopTreeTraverse);

			TraverseOrder = TraverseMode.Inorder;
			KeyDistributionType = KeyDistributionType.Uniform;

			TreeViewModel = new TreeViewModel(_manager);
			StackViewModel = new StackViewModel(_manager);
		}

		private bool OnValidateStartTreeTraverse(object obj) =>
			_tree.Count == _maximumCount && !_manager.Executing;

		private bool OnValidateTreeGeneration(object arg = null)
		{
			return Int32.TryParse(MaximumCount, out _maximumCount) && 
				_maximumCount > 0 && _maximumCount < 10000 && 
				!_manager.Executing;
		}
			
		private void OnTreeGeneration (object arg = null)
		{
			Clear();

			var generator = Generator;

			Enumerable.Range(0, _maximumCount)
				.Each(x => _tree.Add(generator.CreateNode()));

			TreeHeight = _tree.Height;
			TreeViewModel.UpdateState(_tree);
			StackViewModel.MaximumHeight = _maximumCount;
		}

		private void OnStartTreeTraverse (object arg)
		{
			Visitor.Each(node =>
			{
				_manager.RegisterAction(() => node.Value.VisualType = VisualTreeNodeType.Active);
				_manager.RegisterAction(() => node.Value.VisualType = VisualTreeNodeType.InsertedToTree);
			});
			_manager.StartExecutingActions();
		}

		private void OnStopTreeTraverse (object arg) =>
			_manager.StopExecutingActions();

		private void Clear()
		{
			_tree.Clear();
			TreeViewModel.Clear();
		}

		private IEnumerable<BinaryTreeNode<int, ViewData>> Visitor =>
			_tree.GetEnumerator(TraverseOrder, StackViewModel.Stack);

		private IBinaryNodeGenerator<int, ViewData> Generator
		{
			get
			{
				if (KeyDistributionType == KeyDistributionType.Uniform) {
					return new UniformBinaryNodeGenerator();
				}

				throw new ArgumentException("Invalid argument");
			}
		}
		
		private int _maximumCount;
		private int _treeHeight;
		private ActionManager _manager = new ActionManager();
		private BinarySearchTree<int, ViewData> _tree = new BinarySearchTree<int, ViewData>();
	}
}
