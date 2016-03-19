using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraverseTree.Visual.Models
{
	/// <summary>
	/// Node selection state
	/// </summary>
	public enum VisualTreeNodeType
	{
		/// <summary>
		/// 
		/// </summary>
		Hidden,

		/// <summary>
		/// Node created and inserted to tree
		/// </summary>
		InsertedToTree,

		/// <summary>
		/// Node added to stack 
		/// </summary>
		InsertedForTraverse,

		/// <summary>
		/// Node selected while traversing
		/// </summary>
		Active
	}
}
