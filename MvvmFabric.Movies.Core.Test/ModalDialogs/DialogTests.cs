using System;
using System.Windows;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Movies.Core.ModalDialogs;
using NSubstitute;

namespace MvvmFabric.Movies.Core.Tests.ModalDialogs
{
	[TestClass()]
	public sealed class DialogTests
	{
		[TestMethod()]
		public void ShowMessage()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			var message = Guid.NewGuid().ToString();
			var caption = Guid.NewGuid().ToString();
			var button = MessageBoxButton.OK;
			var result = MessageBoxResult.OK;
			var messageShower = Substitute.For<IMessageShower>();
			messageShower.Show(Arg.Is<string>(message), Arg.Is<string>(caption), Arg.Is<MessageBoxButton>(button))
				.Returns(result);
			
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMessageShower>().Instance(messageShower));

			var actualResult = Dialog.ShowMessage(message, caption, button);

			Assert.AreEqual(result, actualResult);
		}
	}
}
