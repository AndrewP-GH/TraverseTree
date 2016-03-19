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
	public enum SelectionMode
	{
		/// <summary>
		/// Node selected
		/// </summary>
		Selected,

		/// <summary>
		/// Node added to tree
		/// </summary>
		Posted,

		/// <summary>
		/// Node free
		/// </summary>
		Free
	}
}
