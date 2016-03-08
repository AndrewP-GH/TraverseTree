using System.ComponentModel;

namespace TraverseTree.Visual.Abstract
{
	/// <summary>
	/// Base class for view models. Implement INotifyPropertyChanged
	/// </summary>
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// <seealso cref="INotifyPropertyChanged.PropertyChanged"/>
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Fire, when proprty changed
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
