using System.ComponentModel;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Define traverse mode.
	/// </summary>
	public enum TraverseMode
	{
		/// <summary>
		/// Visit at first current node, than left node and right node.
		/// </summary>
		[Description("Обход в прямом порядке")]
		Preorder,

		/// <summary>
		/// Visit at first left node, than current node and right node.
		/// </summary>
		[Description("Симметричный обход")]
		Inorder,

		/// <summary>
		/// Visit at first left node, than right node and current node.
		/// </summary>
		[Description("Обход в обратном порядке")]
		Postorder,

		/// <summary>
		/// Visit tree on each level
		/// </summary>
		[Description("Обход в ширину")]
		Leverorder
	}
}
