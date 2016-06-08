using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TraverseTree.Visual.Abstract
{
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string property) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

		protected virtual void UpdateValue<TValue>(ref TValue storage, TValue argument, string property)
		{
			if (!EqualityComparer<TValue>.Default.Equals(storage, argument))
			{
				storage = argument;
				OnPropertyChanged(property);
			}
		}
	}
}
