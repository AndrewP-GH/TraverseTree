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
using TraverseTree.Visual.Models;
using TraverseTree.Visual.Interfaces;

namespace TraverseTree.Visual.ViewModels
{
	/// <summary>
	/// 
	/// </summary>
	public class MainViewModel : BaseViewModel
	{
		public TreeViewModel TreeViewModel { get; set; }

		public StackViewModel StackViewModel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public TraverseMode TraverseOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public KeyDistributionType KeyDistributionType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int MaximumCount { get; set; } = 100;

		/// <summary>
		/// 
		/// </summary>
		// TODO: Calculate expect count 
		public int ExpectedCount
		{
			get
			{
				return (int)Math.Round(Math.Sqrt(Math.PI * MaximumCount) - 1.5 + 11.0 * Math.Sqrt(Math.PI / MaximumCount * 1.0) / 24.0 + Math.Sqrt(1.0 / Math.Pow(MaximumCount, 3)) );
			}
		}

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
		public ICommand TraverseTreeCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public MainViewModel()
		{
			_tree = new BinarySearchTree<int, string>();
			StackViewModel = new StackViewModel();
			TreeViewModel = new TreeViewModel();

			GenerateTreeCommand = new RelayCommand(GenerateTree);
			TraverseTreeCommand = new RelayCommand(TraverseTree);
		}

		protected void GenerateTree (object arg)
		{
			_tree.Clear();

			StackViewModel.MaximumHeight = MaximumCount;
			StackViewModel.ExpectedHeight = ExpectedCount;

			IBinaryNodeGenerator<int, string> generator = GetGenerator();

			for(int i = 0; i != MaximumCount; ++i)
			{
				VisualBinaryTreeNode<int, string> node = generator.CreateNode();

				_tree.Add(node);

				TreeViewModel.Collection.Add(node);
			}
		}

		protected void TraverseTree (object arg)
		{
			var visitor = GetVisitor();
			foreach (VisualBinaryTreeNode<int, string> visual in visitor)
			{
				visual.TreeNodeType = VisualTreeNodeType.Active;
			}
		}

		protected IBinaryNodeGenerator<int, string> GetGenerator()
		{
			if (KeyDistributionType == KeyDistributionType.Uniform)
			{
				return new UniformBinaryNodeGenerator();
			}

			throw new ArgumentException("Invalid argument");
		}

		protected IBinaryNodeVisitor<BinaryTreeNode<int, string>> GetVisitor() =>
			new IterativeBinaryNodeVisitor<BinaryTreeNode<int, string>>(_tree.Root, StackViewModel.Collection, TraverseOrder);

		private readonly BinarySearchTree<int, string> _tree;
	}
}
