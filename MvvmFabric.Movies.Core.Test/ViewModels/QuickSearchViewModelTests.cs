using System;
using System.Collections.Generic;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.ViewModels;
using NSubstitute;
using Castle.MicroKernel.Registration;
using System.ComponentModel;

namespace MvvmFabric.Movies.Core.Tests.ViewModels
{
	[TestClass()]
	public sealed class QuickSearchViewModelTests
	{
		[TestMethod()]
		public void Search()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			SearchMessage searchMessage = null;
			var messageBus = Substitute.For<IMessageBus>();
			messageBus
				.When(bus => bus.Publish<SearchMessage>(Arg.Any<SearchMessage>()))
				.Do(arg => searchMessage = arg[0] as SearchMessage);
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMessageBus>().Instance(messageBus));

			var viewModel = new QuickSearchViewModel();

			var keywords = Guid.NewGuid().ToString();
			viewModel.Keywords = keywords;

			viewModel.SearchCommand.Execute(null);

			Assert.IsNotNull(searchMessage);
			Assert.AreEqual(keywords, searchMessage.Keywords);
		}

		private List<String> _ChangedProperties;
		private void HandlePropertyChanged(Object sender, PropertyChangedEventArgs args)
		{
			_ChangedProperties.Add(args.PropertyName);
		}

		[TestMethod()]
		public void Keywords()
		{
			_ChangedProperties = new List<String>();
			var viewModel = new QuickSearchViewModel();
			viewModel.PropertyChanged += HandlePropertyChanged;
			var keywords = Guid.NewGuid().ToString();
			
			viewModel.Keywords = keywords;

			Assert.AreEqual(keywords, viewModel.Keywords);
			Assert.IsTrue(_ChangedProperties.Contains("Keywords"));
		}
	}
}
