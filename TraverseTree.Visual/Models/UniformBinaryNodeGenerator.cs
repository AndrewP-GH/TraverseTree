using System;

using TraverseTree.Visual.Abstract;
using TraverseTree.Core.Models;

namespace TraverseTree.Visual.Models
{
	internal sealed class UniformBinaryNodeGenerator : IBinaryNodeGenerator<int, ViewData>
	{
		private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

		public BinaryTreeNode<int, ViewData> CreateNode() =>
			new BinaryTreeNode<int, ViewData>(_random.Next(), new ViewData());

		public BinaryTreeNode<int, ViewData> CreateNode(ViewData data) =>
			new BinaryTreeNode<int, ViewData>(_random.Next(), data);
	}

	internal sealed class FakeGenerator : IBinaryNodeGenerator<int, ViewData>
	{
		private readonly int[] _keys = new int[] { 10, 12, 3, 42, 71, 3, 4, 11, 67, 2, 15, 17, 120, 8, 2, 16, 1, 7, 9, 20 };
		private int _active = 0;

		public BinaryTreeNode<int, ViewData> CreateNode() =>
			CreateNode(new ViewData());

		public BinaryTreeNode<int, ViewData> CreateNode(ViewData data) =>
			new BinaryTreeNode<int, ViewData>(_keys[( _active++ ) % _keys.Length], data);
	}
}
