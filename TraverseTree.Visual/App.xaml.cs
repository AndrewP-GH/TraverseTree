using System.Windows;
using TraverseTree.Visual.Interfaces;
using TraverseTree.Visual.ViewModels;
using TraverseTree.Visual.Views;

namespace TraverseTree.Visual
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			MainView window = new MainView();
			MainViewModel mvm = new MainViewModel()
			{
				CloseCommand = new RelayCommand(arg => window.Close())
			};

			window.DataContext = mvm;
			window.Show();
		}
	}
}
