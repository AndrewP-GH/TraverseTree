using System;
using System.Windows.Input;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Visual.Interfaces
{
	public class RelayCommand<TExecuteArg, TCanExecuteArg> : ICommand
	{
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

		public RelayCommand(Action<TExecuteArg> execute) : this(execute, null) { }

		public RelayCommand(Action<TExecuteArg> execute, Predicate<TCanExecuteArg> canExecute)
		{
			execute.NullGuardAssign(out _execute, nameof(execute));
			_canExecute = canExecute;
		}

		bool ICommand.CanExecute(object parameter)
		{
			return _canExecute.IsNull() || 
				_canExecute((TCanExecuteArg)parameter);
		}

		void ICommand.Execute(object parameter)
		{
			_execute((TExecuteArg)parameter);
		}

		private readonly Action<TExecuteArg> _execute;
		private readonly Predicate<TCanExecuteArg> _canExecute;
	}

	public class RelayCommand :	RelayCommand<object, object>
	{
		public RelayCommand(Action<object> execute) : this(execute, null) { }
		public RelayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute) { }
	}
}
