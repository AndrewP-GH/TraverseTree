using System.ComponentModel;

namespace TraverseTree.Core.Models
{
	public enum TraverseMode
	{
		[Description("Обход в прямом порядке")]
		Preorder,

		[Description("Симметричный обход")]
		Inorder,

		[Description("Обход в обратном порядке")]
		Postorder,

		[Description("Обход в ширину")]
		Leverorder
	}
}
