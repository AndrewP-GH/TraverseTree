using System;

using TraverseTree.Visual.Abstract;
using TraverseTree.Core.Models;
using System.Linq;

namespace TraverseTree.Visual.Models
{
	internal sealed class UniformBinaryNodeGenerator : IBinaryNodeGenerator<int, ViewData>
	{
		public BinaryTreeNode<int, ViewData> CreateNode() =>
			new BinaryTreeNode<int, ViewData>(_random.Next(1, MaxRange), new ViewData());

		public BinaryTreeNode<int, ViewData> CreateNode(ViewData data) =>
			new BinaryTreeNode<int, ViewData>(_random.Next(1, MaxRange), data);

		private const int MaxRange = 20;
		private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
	}

	internal sealed class FakeGenerator : IBinaryNodeGenerator<int, ViewData>
	{
		private readonly int[] _keys = Enumerable.Range(-10, 10).Concat(Enumerable.Range(1, 10)).ToArray();
		private int _active = 0;

		public BinaryTreeNode<int, ViewData> CreateNode() =>
			CreateNode(new ViewData());

		public BinaryTreeNode<int, ViewData> CreateNode(ViewData data) =>
			new BinaryTreeNode<int, ViewData>(_keys[( _active++ ) % _keys.Length], data);
	}
}
