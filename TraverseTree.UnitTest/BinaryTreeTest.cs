using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraverseTree.Core.Models;

namespace TraverseTree.UnitTest
{
	[TestClass]
	public class BinaryTreeTest
	{
		[TestMethod]
		public void BinaryTreeTest_AddNode()
		{
			BinaryTree<string, string> tree = new BinaryTree<string, string>();

			Assert.AreEqual(tree.IsEmpty, true);

			tree.Add("1", "1");
			Assert.AreEqual(tree.IsEmpty, false);

			tree.Add("2", "2");
			tree.Add("3", "3");
			tree.Add("4", "4");
			tree.Add("5", "5");

			Assert.AreEqual(tree.Count, 5);

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
		}
	}
}
