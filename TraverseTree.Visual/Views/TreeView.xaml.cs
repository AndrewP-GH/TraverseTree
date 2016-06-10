using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TraverseTree.Visual.ViewModels;

namespace TraverseTree.Visual.Views
{
	public partial class TreeView : UserControl
	{
		public TreeView()
		{
			InitializeComponent();
		}

		private void OnCanvasMouseWheel(object sender, MouseWheelEventArgs args) =>
			( (TreeViewModel)DataContext ).UpdateScale(args.Delta > 0);
	}
}
