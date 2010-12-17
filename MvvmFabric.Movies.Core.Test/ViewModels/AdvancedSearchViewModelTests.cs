using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using Castle.Windsor;
using MvvmFabric.Movies.Core.ViewModels;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Messaging;
using NSubstitute;

namespace MvvmFabric.Movies.Core.Tests.ViewModels
{
	[TestClass]
	public sealed class AdvancedSearchViewModelTests
	{
		private List<String> _ChangedProperties;
		private void HandlePropertyChanged(Object sender, PropertyChangedEventArgs args)
		{
			_ChangedProperties.Add(args.PropertyName);
		}

		[TestInitialize()]
		public void TestInitialize()
		{
			_ChangedProperties = new List<String>();
		}

		[TestMethod]
		public void Keywords()
		{
			var viewModel = new AdvancedSearchViewModel();
			viewModel.PropertyChanged += HandlePropertyChanged;
			var keywords = Guid.NewGuid().ToString();

			viewModel.Keywords = keywords;

			Assert.AreEqual(keywords, viewModel.Keywords);
			Assert.IsTrue(_ChangedProperties.Contains("Keywords"));
		}

		[TestMethod]
		public void Genres()
		{
			var viewModel = new AdvancedSearchViewModel();

			Assert.IsNotNull(viewModel.Genres);
		}

		[TestMethod]
		public void SelectedGenre()
		{
			var viewModel = new AdvancedSearchViewModel();
			viewModel.PropertyChanged += HandlePropertyChanged;
			var genre = Core.Models.Genres.Action;

			viewModel.SelectedGenre = genre;

			Assert.AreEqual(genre, viewModel.SelectedGenre);
			Assert.IsTrue(_ChangedProperties.Contains("SelectedGenre"));
		}

		[TestMethod]
		public void Ratings()
		{
			var viewModel = new AdvancedSearchViewModel();

			Assert.IsNotNull(viewModel.Ratings);
		}

		[TestMethod]
		public void SelectedRating()
		{
			var viewModel = new AdvancedSearchViewModel();
			viewModel.PropertyChanged += HandlePropertyChanged;
			var rating = Core.Models.Ratings.G;

			viewModel.SelectedRating = rating;

			Assert.AreEqual(rating, viewModel.SelectedRating);
			Assert.IsTrue(_ChangedProperties.Contains("SelectedRating"));
		}

		private bool _viewAccepted;
		void viewModel_RequestClose(object sender, MvvmFabric.Navigation.RequestCloseEventArgs e)
		{
			_viewAccepted = e.Accepted;
		}

		[TestMethod]
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

			var viewModel = new AdvancedSearchViewModel();

			var keywords = Guid.NewGuid().ToString();
			var genre = Core.Models.Genres.Action;
			var rating = Core.Models.Ratings.G;

			viewModel.Keywords = keywords;
			viewModel.SelectedGenre = genre;
			viewModel.SelectedRating = rating;

			_viewAccepted = false;
			viewModel.RequestClose += viewModel_RequestClose;
			viewModel.SearchCommand.Execute(null);
			viewModel.RequestClose -= viewModel_RequestClose;

			Assert.IsNotNull(searchMessage);
			Assert.AreEqual(keywords, searchMessage.Keywords, "Keywords");
			Assert.AreEqual(genre, searchMessage.Genre, "Genre");
			Assert.AreEqual(rating, searchMessage.Rating, "Rating");
			Assert.IsTrue(_viewAccepted);
		}

		[TestMethod]
		public void Cancel()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			var messageBus = Substitute.For<IMessageBus>();
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMessageBus>().Instance(messageBus));

			var viewModel = new AdvancedSearchViewModel();

			_viewAccepted = true;
			viewModel.RequestClose += viewModel_RequestClose;
			viewModel.CancelCommand.Execute(null);
			viewModel.RequestClose -= viewModel_RequestClose;

			Assert.IsFalse(_viewAccepted);
		}
	}
}
