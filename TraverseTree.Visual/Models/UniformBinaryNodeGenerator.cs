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
		private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

		public VisualBinaryTreeNode<int, string> CreateNode() => 
			new VisualBinaryTreeNode<int, string>(_random.Next(), String.Empty);

		public VisualBinaryTreeNode<int, string> CreateNode(string value) =>
			new VisualBinaryTreeNode<int, string>(_random.Next(), value);
	}

	public class FakeGenerator : IBinaryNodeGenerator<int, string>
	{
		private readonly int[] _keys = new int[] { 10, 12, 3, 42, 71, 3, 4, 11, 67, 2, 15, 17, 120, 8, 2, 16, 1, 7, 9, 20 };
		private int _active = 0;

		public VisualBinaryTreeNode<int, string> CreateNode() =>
			CreateNode(String.Empty);

		public VisualBinaryTreeNode<int, string> CreateNode(string value) =>
			new VisualBinaryTreeNode<int, string>(_keys[( _active++ ) % _keys.Length], value);
	}
}
