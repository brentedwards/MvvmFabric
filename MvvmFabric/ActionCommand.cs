﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmFabric
{
	/// <summary>
	/// An ActionCommand is an ICommand which executes an Action with a specific parameter type.
	/// </summary>
	/// <typeparam name="TParameter">The type of parameter which the Action takes.</typeparam>
	public sealed class ActionCommand<TParameter> : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private Action<TParameter> ExecuteMethod { get; set; }
		private Func<TParameter, bool> CanExecuteMethod { get; set; }

		/// <summary>
		/// Constructor for ActionCommand.
		/// </summary>
		/// <param name="executeMethod">The Action to be executed.</param>
		public ActionCommand(Action<TParameter> executeMethod)
		{
			ExecuteMethod = executeMethod;
		}

		/// <summary>
		/// Constructor for ActionCommand.
		/// </summary>
		/// <param name="executeMethod">The Action to be executed.</param>
		/// <param name="canExecuteMethod">
		/// The optional Func to be called when determining if the command can be executed.
		/// </param>
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
			else if (parameter == null)
			{
				canExecute = CanExecute(default(TParameter));
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

	/// <summary>
	/// An ActionCommand is an ICommand which executes an Action.
	/// </summary>
	public sealed class ActionCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private Action ExecuteMethod { get; set; }
		private Func<bool> CanExecuteMethod { get; set; }

		/// <summary>
		/// Constructor for ActionCommand.
		/// </summary>
		/// <param name="executeMethod">The Action to be executed.</param>
		public ActionCommand(Action executeMethod)
		{
			ExecuteMethod = executeMethod;
		}

		/// <summary>
		/// Constructor for ActionCommand.
		/// </summary>
		/// <param name="executeMethod">The Action to be executed.</param>
		/// <param name="canExecuteMethod">
		/// The optional Func to be called when determining if the command can be executed.
		/// </param>
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
