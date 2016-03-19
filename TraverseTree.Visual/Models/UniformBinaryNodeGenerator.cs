using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraverseTree.Visual.Abstract;

namespace TraverseTree.Visual.Models
{
	public class UniformBinaryNodeGenerator : IBinaryNodeGenerator<int, string>
	{
		private readonly Random _random;

		public UniformBinaryNodeGenerator()
		{
			_random = new Random(Guid.NewGuid().GetHashCode());
		}

		public VisualBinaryTreeNode<int, string> CreateNode() => 
			new VisualBinaryTreeNode<int, string>(_random.Next(), String.Empty);

		public VisualBinaryTreeNode<int, string> CreateNode(string value) =>
			new VisualBinaryTreeNode<int, string>(_random.Next(), value);
	}
}
