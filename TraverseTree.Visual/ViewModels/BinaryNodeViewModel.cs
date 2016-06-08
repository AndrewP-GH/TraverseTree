using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TraverseTree.Visual.Abstract;
using TraverseTree.Core.Models;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Visual.ViewModels
{
	public class BinaryNodeViewModel : ObservableObject
	{
		public BinaryNodeViewModel(BinaryTreeNode<string, string> node)
		{
			node.NullGuardAssign(out _node, nameof(node));
		}

		private readonly BinaryTreeNode<string, string> _node;
	}
}
