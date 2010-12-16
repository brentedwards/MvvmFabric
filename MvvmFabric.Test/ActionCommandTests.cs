using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Input;

namespace MvvmFabric.Test
{
	[TestClass]
	public sealed class ActionCommandTests
	{
		[TestMethod]
		public void CanExecute_Generic()
		{
			var random = new Random();
			var parameter = random.Next();

			var command = new ActionCommand<int>(null, i => { return true; });

			Assert.IsTrue(command.CanExecute(parameter));
		}

		[TestMethod]
		public void CanExecute_Object_Generic()
		{
			var random = new Random();
			Object parameter = random.Next();

			var command = new ActionCommand<int>(null, i => { return true; });

			Assert.IsTrue(((ICommand)command).CanExecute(parameter));
		}

		[TestMethod]
		public void CanExecute_Null_Generic()
		{
			var command = new ActionCommand<int>(null, i => { return true; });

			Assert.IsTrue(((ICommand)command).CanExecute(null));
		}

		[TestMethod]
		public void CanExecute_Object_Generic_Default()
		{
			var random = new Random();
			Object parameter = random.Next();

			var command = new ActionCommand<int>(null, null);

			Assert.IsTrue(((ICommand)command).CanExecute(parameter));
		}

		[TestMethod]
		public void Execute_Generic()
		{
			var random = new Random();
			int parameter = random.Next();

			int result = -1;

			var command = new ActionCommand<int>(i => result = i);

			command.Execute(parameter);

			Assert.AreEqual(parameter, result);
		}

		[TestMethod]
		public void Execute_Object_Generic()
		{
			var random = new Random();
			Object parameter = random.Next();

			int result = -1;

			var command = new ActionCommand<int>(i => result = i);

			((ICommand)command).Execute(parameter);

			Assert.AreEqual(parameter, result);
		}

		private bool _canExecuteChanged;
		private void CanExecuteChanged(object sender, EventArgs e)
		{
			_canExecuteChanged = true;
		}

		[TestMethod]
		public void NotifyCanExecuteChanged_Generic()
		{
			_canExecuteChanged = false;

			var command = new ActionCommand<int>(null);

			command.CanExecuteChanged += CanExecuteChanged;
			command.NotifyCanExecuteChanged();
			command.CanExecuteChanged -= CanExecuteChanged;

			Assert.IsTrue(_canExecuteChanged);
		}

		[TestMethod]
		public void CanExecute()
		{
			var random = new Random();
			var parameter = random.Next();

			var command = new ActionCommand(null, () => { return true; });

			Assert.IsTrue(command.CanExecute(parameter));
		}

		[TestMethod]
		public void CanExecute_Default()
		{
			var random = new Random();
			var parameter = random.Next();

			var command = new ActionCommand(null, null);

			Assert.IsTrue(command.CanExecute(parameter));
		}

		[TestMethod]
		public void Execute()
		{
			var random = new Random();
			int parameter = random.Next();

			var executed = false;

			var command = new ActionCommand(() => executed = true);

			command.Execute(parameter);

			Assert.IsTrue(executed);
		}

		[TestMethod]
		public void NotifyCanExecuteChanged()
		{
			_canExecuteChanged = false;

			var command = new ActionCommand(null);

			command.CanExecuteChanged += CanExecuteChanged;
			command.NotifyCanExecuteChanged();
			command.CanExecuteChanged -= CanExecuteChanged;

			Assert.IsTrue(_canExecuteChanged);
		}
	}
}
