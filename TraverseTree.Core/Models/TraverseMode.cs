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
		Preorder,

		/// <summary>
		/// Visit at first left node, than current node and right node.
		/// </summary>
		Inorder,

		/// <summary>
		/// Visit at first left node, than right node and current node.
		/// </summary>
		Postorder
	}
}
