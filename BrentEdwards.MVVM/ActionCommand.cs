using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BrentEdwards.MVVM
{
	public sealed class ActionCommand<TParameter> : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private Action<TParameter> ExecuteMethod { get; set; }
		private Func<TParameter, bool> CanExecuteMethod { get; set; }

		public ActionCommand(Action<TParameter> executeMethod)
		{
			ExecuteMethod = executeMethod;
		}

		public ActionCommand(Action<TParameter> executeMethod, Func<TParameter, bool> canExecuteMethod)
			: this(executeMethod)
		{
			CanExecuteMethod = canExecuteMethod;
		}
		
		public bool CanExecute(TParameter parameter)
		{
			var canExecute = true;
			if (CanExecuteMethod != null)
			{
				canExecute = CanExecuteMethod(parameter);
			}

			return canExecute;
		}

		bool ICommand.CanExecute(object parameter)
		{
			var canExecute = false;
			if (parameter is TParameter)
			{
				canExecute = CanExecute((TParameter)parameter);
			}

			return canExecute;
		}

		public void Execute(TParameter parameter)
		{
			if (ExecuteMethod != null)
			{
				ExecuteMethod(parameter);
			}
		}

		void ICommand.Execute(object parameter)
		{
			Execute((TParameter)parameter);
		}

		public void NotifyCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, new EventArgs());
			}
		}
	}

	public sealed class ActionCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private Action ExecuteMethod { get; set; }
		private Func<bool> CanExecuteMethod { get; set; }

		public ActionCommand(Action executeMethod)
		{
			ExecuteMethod = executeMethod;
		}

		public ActionCommand(Action executeMethod, Func<bool> canExecuteMethod)
			: this(executeMethod)
		{
			CanExecuteMethod = canExecuteMethod;
		}

		public bool CanExecute(object parameter)
		{
			var canExecute = true;
			if (CanExecuteMethod != null)
			{
				canExecute = CanExecuteMethod();
			}

			return canExecute;
		}

		public void Execute(object parameter)
		{
			if (ExecuteMethod != null)
			{
				ExecuteMethod();
			}
		}

		public void NotifyCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, new EventArgs());
			}
		}
	}
}
