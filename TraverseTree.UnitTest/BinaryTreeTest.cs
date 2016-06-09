using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraverseTree.Core.Models;
using TraverseTree.Core.Extensions;

namespace TraverseTree.UnitTest
{
	internal sealed class NodeCreator<TKey, TValue> where TKey: IComparable<TKey>
	{
		public BinaryTreeNode<TKey, TValue> Create(TKey key, TValue value) =>
			new BinaryTreeNode<TKey, TValue>(key, value);

		public IEnumerable<BinaryTreeNode<TKey, TValue>> CreateRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
			pairs.Select(x => new BinaryTreeNode<TKey, TValue>(x.Key, x.Value));
	}

	[TestClass]
	public class BinaryNodeTest
	{
		[TestMethod]
		public void BinaryNodeTest_MiniMax()
		{
			BinaryTreeNode<int, string> node = null;

			node = new BinaryTreeNode<int, string>(1, "1");
			node.Left = new BinaryTreeNode<int, string>(-1, "-1");
			node.Right = new BinaryTreeNode<int, string>(2, "2");

			Assert.AreEqual(node.Leftmost, node.Left);
			Assert.AreEqual(node.Rightmost, node.Right);

			node.Left.Left = new BinaryTreeNode<int, string>(-2, "-2");
			node.Right.Right = new BinaryTreeNode<int, string>(3, "3");

			Assert.AreEqual(node.Leftmost, node.Left.Left);
			Assert.AreEqual(node.Rightmost, node.Right.Right);

			node.Left.Left.Left = new BinaryTreeNode<int, string>(-3, "-3");
			node.Left.Left.Left.Left = new BinaryTreeNode<int, string>(-4, "4");

			Assert.AreEqual(node.Leftmost, node.Left.Left.Left.Left);
		}
	}

	[TestClass]
	public class BinaryTreeTest
	{
		[TestMethod]
		public void BinaryTreeTest_CreateAdd()
		{
			/// Arrange
			NodeCreator<string, string> creator = new NodeCreator<string, string>();
			BinarySearchTree<string, string> tree = new BinarySearchTree<string, string>();

			/// Act-Assert
			Assert.AreEqual(tree.IsEmpty, true);
			tree.Add(creator.Create("1", "1"));
			Assert.AreEqual(tree.IsEmpty, false);

			/// Act-Assert
			tree.Add(creator.Create("2", "2"));
			tree.Add(creator.Create("3", "3"));
			tree.Add(creator.Create("4", "4"));
			tree.Add(creator.Create("5", "5"));

			Assert.AreEqual(tree.Count, 5);

			/// Act-Assert
			tree.AddRange(creator.CreateRange(
				new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("5", "5"),
					new KeyValuePair<string, string>("4", "4"),
					new KeyValuePair<string, string>("3", "3"),
					new KeyValuePair<string, string>("2", "2"),
					new KeyValuePair<string, string>("1", "1")
				})
			);

			Assert.AreEqual(tree.Count, 10);
			Assert.AreEqual(tree.Root.RecursiveCount(), tree.Count);
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());
		}

		[TestMethod]
		public void BinaryTreeTest_FindContainHeight()
		{
			/// Arrange
			NodeCreator<string, string> creator = new NodeCreator<string, string>();
			BinarySearchTree<string, string> tree = new BinarySearchTree<string, string>();

			tree.AddRange(creator.CreateRange(
				new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("5", "5"),
					new KeyValuePair<string, string>("5", "5"),
					new KeyValuePair<string, string>("5", "5"),
					new KeyValuePair<string, string>("4", "4"),
					new KeyValuePair<string, string>("3", "3"),
					new KeyValuePair<string, string>("2", "2"),
					new KeyValuePair<string, string>("1", "1"),
					new KeyValuePair<string, string>("5", "1"),
					new KeyValuePair<string, string>("4", "4"),
					new KeyValuePair<string, string>("3", "3"),
					new KeyValuePair<string, string>("2", "2"),
					new KeyValuePair<string, string>("1", "1"),
					new KeyValuePair<string, string>("5", "1"),
					new KeyValuePair<string, string>("4", "4"),
					new KeyValuePair<string, string>("3", "3"),
					new KeyValuePair<string, string>("2", "2"),
					new KeyValuePair<string, string>("1", "1")
				})
			);

			/// Act
			var findKey = tree.Find("5");
			var findKeyValue = tree.Find("5", "1");
			bool[] results = new bool[]
			{
				tree.ContainsKey("1"),
				tree.ContainsKey("63"),
				tree.Contains("2", "2"),
				tree.Contains("2", "22")
			};
			int height = tree.Height;

			/// Assert
			Assert.AreEqual(findKey.Count(), 5);
			Assert.AreEqual(findKeyValue.Count(), 2);
			Assert.AreEqual(results[0], true);
			Assert.AreEqual(results[1], false);
			Assert.AreEqual(results[2], true);
			Assert.AreEqual(results[3], false);
			Assert.AreEqual(height, 7);
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());
		}

		[TestMethod]
		public void BinaryTreeTest_Remove()
		{
			/// Arrange
			NodeCreator<int, string> creator = new NodeCreator<int, string>();
			BinarySearchTree<int, string> tree = new BinarySearchTree<int, string>();

			tree.AddRange(creator.CreateRange(
				new KeyValuePair<int, string>[]
				{
					new KeyValuePair<int, string>(15, "5"),
					new KeyValuePair<int, string>(5, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(3, "4"),
					new KeyValuePair<int, string>(2, "4"),
					new KeyValuePair<int, string>(12, "3"),
					new KeyValuePair<int, string>(10, "2"),
					new KeyValuePair<int, string>(13, "1"),
					new KeyValuePair<int, string>(6, "1"),
					new KeyValuePair<int, string>(7, "4"),
					new KeyValuePair<int, string>(20, "3"),
					new KeyValuePair<int, string>(18, "2"),
					new KeyValuePair<int, string>(23, "1")
				})
			);

			try
			{
				tree.RemoveAt(61);
			} catch(Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(KeyNotFoundException));
			}

			/// Act-Assert
			Assert.AreEqual(tree.Count, 14);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());

			/// Act-Assert
			tree.RemoveAt(13);
			Assert.AreEqual(tree.Count, 13);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());

			/// Act-Assert
			tree.RemoveAt(3);
			Assert.AreEqual(tree.Count, 12);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());

			/// Act-Assert
			tree.RemoveAt(16);
			Assert.AreEqual(tree.Count, 10);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());

			/// Act-Assert
			tree.RemoveAt(5);
			Assert.AreEqual(tree.Count, 9);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());

			/// Act-Assert
			tree.RemoveAt(15);
			Assert.AreEqual(tree.Count, 8);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
			Assert.AreEqual(tree.Height, tree.Root.RecursiveHeight());
		}

		[TestMethod]
		public void BinaryTreeTest_Clear()
		{
			/// Arrange
			NodeCreator<int, string> creator = new NodeCreator<int, string>();
			BinarySearchTree<int, string> tree = new BinarySearchTree<int, string>();

			tree.AddRange(creator.CreateRange(
				new KeyValuePair<int, string>[]
				{
					new KeyValuePair<int, string>(15, "5"),
					new KeyValuePair<int, string>(5, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(3, "4"),
					new KeyValuePair<int, string>(2, "4"),
					new KeyValuePair<int, string>(12, "3"),
					new KeyValuePair<int, string>(10, "2"),
					new KeyValuePair<int, string>(13, "1"),
					new KeyValuePair<int, string>(6, "1"),
					new KeyValuePair<int, string>(7, "4"),
					new KeyValuePair<int, string>(20, "3"),
					new KeyValuePair<int, string>(18, "2"),
					new KeyValuePair<int, string>(23, "1")
				})
			);

			/// Act
			long before = GC.GetTotalMemory(true);
			tree.Clear();

			long after = GC.GetTotalMemory(true);

			/// Assert
			Assert.AreNotEqual(after - before, 0L);
		}
	}

	[TestClass]
	public class BinaryTreeTraverse
	{
		private readonly BinarySearchTree<int, string> _tree;

		public BinaryTreeTraverse()
		{
			NodeCreator<int, string> creator = new NodeCreator<int, string>();
			_tree = new BinarySearchTree<int, string>();

			_tree.AddRange(creator.CreateRange(new KeyValuePair<int, string>[]
				{
					new KeyValuePair<int, string>(15, "5"),
					new KeyValuePair<int, string>(5, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(3, "4"),
					new KeyValuePair<int, string>(2, "4"),
					new KeyValuePair<int, string>(12, "3"),
					new KeyValuePair<int, string>(10, "2"),
					new KeyValuePair<int, string>(13, "1"),
					new KeyValuePair<int, string>(6, "1"),
					new KeyValuePair<int, string>(7, "4"),
					new KeyValuePair<int, string>(20, "3"),
					new KeyValuePair<int, string>(18, "2"),
					new KeyValuePair<int, string>(23, "1")
				})
			);

		}

		[TestMethod]
		public void BinaryTreeTraverse_Inorder()
		{
			/// Arrange
			var visitor = _tree.GetEnumerator(TraverseMode.Inorder);

			/// Act
			foreach(var pair in visitor)
			{
				var key = pair.Key;
			}

			var keys = visitor.Select(x => x.Key).ToArray();
			var expected = new int[] { 2, 3, 5, 6, 7, 10, 12, 13, 15, 16, 16, 18, 20, 23 };

			/// Assert
			Assert.AreEqual(keys.Length, _tree.Count);

			for(int i = 0; i != expected.Length; i++)
			{
				Assert.AreEqual(expected[i], keys[i]);
			}
		}

		[TestMethod]
		public void BinaryTreeTraverse_Preorder()
		{
			/// Arrange
			var visitor = _tree.GetEnumerator(TraverseMode.Preorder);

			/// Act
			foreach (var pair in visitor)
			{
				var key = pair.Key;
			}

			var keys = visitor.Select(x => x.Key).ToArray();
			var expected = new int[] { 15, 5, 3, 2, 12, 10, 6, 7, 13, 16, 16, 20, 18, 23 };

			/// Assert
			Assert.AreEqual(keys.Length, _tree.Count);

			for (int i = 0; i != expected.Length; i++)
			{
				Assert.AreEqual(expected[i], keys[i]);
			}
		}

		[TestMethod]
		public void BinaryTreeTraverse_Postorder()
		{
			/// Arrange
			var visitor = _tree.GetEnumerator(TraverseMode.Postorder);

			/// Act
			foreach (var pair in visitor)
			{
				var key = pair.Key;
			}

			var keys = visitor.Select(x => x.Key).ToArray();
			var expected = new int[] { 2, 3, 6, 7, 10, 13, 12, 5, 18, 23, 20, 16, 16, 15 };

			/// Assert
			Assert.AreEqual(keys.Length, _tree.Count);

			for (int i = 0; i != expected.Length; i++)
			{
				Assert.AreEqual(expected[i], keys[i]);
			}
		}

		[TestMethod]
		public void BinaryTreeTraverse_Levelorder()
		{
			var visitor = _tree.GetEnumerator(TraverseMode.Leverorder);
			/// Act
			foreach (var pair in visitor)
			{
				var key = pair.Key;
			}

			var keys = visitor.Select(x => x.Key).ToArray();
			var expected = new int[] { 15, 5, 16, 3, 12, 16, 2, 10, 13, 20, 6, 18, 23, 7 };

			/// Assert
			Assert.AreEqual(keys.Count(), _tree.Count);

			for (int i = 0; i != expected.Length; i++)
			{
				Assert.AreEqual(expected[i], keys.ElementAt(i));
			}
		}
	}
}
