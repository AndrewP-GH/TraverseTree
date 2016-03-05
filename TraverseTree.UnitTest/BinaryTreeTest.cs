using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraverseTree.Core.Models;
using TraverseTree.Core.Extensions;

namespace TraverseTree.UnitTest
{
	[TestClass]
	public class BinaryTreeTest
	{
		[TestMethod]
		public void BinaryTreeTest_CreateAdd()
		{
			/// Arrange
			BinaryTree<string, string> tree = new BinaryTree<string, string>();

			/// Act-Assert
			Assert.AreEqual(tree.IsEmpty, true);
			tree.Add("1", "1");
			Assert.AreEqual(tree.IsEmpty, false);

			/// Act-Assert
			tree.Add("2", "2");
			tree.Add("3", "3");
			tree.Add("4", "4");
			tree.Add("5", "5");

			Assert.AreEqual(tree.Count, 5);

			/// Act-Assert
			tree.AddRange(new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("5", "5"),
					new KeyValuePair<string, string>("4", "4"),
					new KeyValuePair<string, string>("3", "3"),
					new KeyValuePair<string, string>("2", "2"),
					new KeyValuePair<string, string>("1", "1")
				}
			);

			Assert.AreEqual(tree.Count, 10);
			Assert.AreEqual(tree.Root.RecursiveCount(), tree.Count);
		}

		[TestMethod]
		public void BinaryTreeTest_FindContainHeight()
		{
			/// Arrange
			BinaryTree<string, string> tree = new BinaryTree<string, string>();

			tree.AddRange(new KeyValuePair<string, string>[]
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
				}
			);

			/// Act
			var findKey = tree.Find("5");
			var findKeyValue = tree.Find("5", "5");
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
			Assert.AreEqual(findKeyValue.Count(), 3);
			Assert.AreEqual(results[0], true);
			Assert.AreEqual(results[1], false);
			Assert.AreEqual(results[2], true);
			Assert.AreEqual(results[3], false);
			Assert.AreEqual(height, 7);
		}

		[TestMethod]
		public void BinaryTreeTest_Remove()
		{
			/// Arrange
			BinaryTree<int, string> tree = new BinaryTree<int, string>();

			tree.AddRange(new KeyValuePair<int, string>[]
				{
					new KeyValuePair<int, string>(15, "5"),
					new KeyValuePair<int, string>(5, "5"),
					new KeyValuePair<int, string>(16, "5"),
					new KeyValuePair<int, string>(3, "4"),
					new KeyValuePair<int, string>(12, "3"),
					new KeyValuePair<int, string>(10, "2"),
					new KeyValuePair<int, string>(13, "1"),
					new KeyValuePair<int, string>(6, "1"),
					new KeyValuePair<int, string>(7, "4"),
					new KeyValuePair<int, string>(20, "3"),
					new KeyValuePair<int, string>(18, "2"),
					new KeyValuePair<int, string>(23, "1")
				}
			);

			try
			{
				tree.RemoveAt(61, removeAll: true);
			} catch(Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(KeyNotFoundException));
			}

			/// Act-Assert
			Assert.AreEqual(tree.Count, 12);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());

			/// Act-Assert
			tree.RemoveAt(13, removeAll: true);
			Assert.AreEqual(tree.Count, 11);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());

			/// Act-Assert
			tree.RemoveAt(16, removeAll: true);
			Assert.AreEqual(tree.Count, 10);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());

			/// Act-Assert
			tree.RemoveAt(5, removeAll: true);
			Assert.AreEqual(tree.Count, 9);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());

			/// Act-Assert
			tree.RemoveAt(15, removeAll: true);
			Assert.AreEqual(tree.Count, 8);
			Assert.AreEqual(tree.Count, tree.Root.RecursiveCount());
		}
	}
}
