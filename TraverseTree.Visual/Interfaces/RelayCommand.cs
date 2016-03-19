using System;
using System.Windows.Input;

namespace TraverseTree.Visual.Interfaces
{
	/// <summary>
	/// Basic implementation of <see cref="ICommand"/>
	/// </summary>
	/// <typeparam name="TExecuteArg"></typeparam>
	/// <typeparam name="TCanExecuteArg"></typeparam>
	public class RelayCommand<TExecuteArg, TCanExecuteArg> : ICommand
	{
		/// <summary>
		/// 
		/// </summary>
		event EventHandler ICommand.CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}

			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="execute"></param>
		public RelayCommand(Action<TExecuteArg> execute) : this(execute, null) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="execute"></param>
		/// <param name="canExecute"></param>
		public RelayCommand(Action<TExecuteArg> execute, Predicate<TCanExecuteArg> canExecute)
		{
			if (execute == null) {
				throw new ArgumentNullException(nameof(execute));
			}

			_execute = execute;
			_canExecute = canExecute;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		bool ICommand.CanExecute(object parameter)
		{
			return _canExecute == null || 
				_canExecute((TCanExecuteArg)parameter);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		void ICommand.Execute(object parameter)
		{
			_execute((TExecuteArg)parameter);
		}

		private readonly Action<TExecuteArg> _execute;
		private readonly Predicate<TCanExecuteArg> _canExecute;
	}

	/// <summary>
	/// 
	/// </summary>
	public class RelayCommand :	RelayCommand<object, object>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="execute"></param>
		public RelayCommand(Action<object> execute) : this(execute, null) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="execute"></param>
		/// <param name="canExecute"></param>
		public RelayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute) { }
	}
}
