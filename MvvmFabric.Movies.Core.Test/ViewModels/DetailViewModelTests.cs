using System;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.ModalDialogs;
using MvvmFabric.Movies.Core.Models;
using MvvmFabric.Movies.Core.Repositories;
using MvvmFabric.Movies.Core.ViewModels;
using NSubstitute;

namespace MvvmFabric.Movies.Core.Tests.ViewModels
{
	[TestClass()]
	public sealed class DetailViewModelTests
	{
		private List<String> _ChangedProperties;
		private void HandlePropertyChanged(Object sender, PropertyChangedEventArgs args)
		{
			_ChangedProperties.Add(args.PropertyName);
		}

		[TestMethod()]
		public void IsEditable()
		{
			_ChangedProperties = new List<String>();
			var viewModel = new DetailViewModel();
			viewModel.PropertyChanged += HandlePropertyChanged;

			viewModel.IsEditable = true;

			Assert.IsTrue(viewModel.IsEditable);
			Assert.IsTrue(_ChangedProperties.Contains("IsEditable"));
		}

		[TestMethod()]
		public void LoadMovie()
		{
			_ChangedProperties = new List<String>();
			var viewModel = new DetailViewModel();
			viewModel.PropertyChanged += HandlePropertyChanged;

			var movie = new Movie();
			viewModel.Load(movie);

			Assert.AreSame(movie, viewModel.Movie);
			Assert.IsTrue(_ChangedProperties.Contains("Movie"));
		}

		[TestMethod()]
		public void Title()
		{
			var viewModel = new DetailViewModel();

			var movie = new Movie(new Random().Next(),
				Guid.NewGuid().ToString(),
				Core.Models.Genres.Action,
				Core.Models.Ratings.G);
			viewModel.Load(movie);

			Assert.AreEqual(String.Format("{0} Details", movie.Name), viewModel.Title);
		}

		[TestMethod()]
		public void Save()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			var repository = Substitute.For<IMovieRepository>();
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMovieRepository>().Instance(repository));

			var messageShower = Substitute.For<IMessageShower>();
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMessageShower>().Instance(messageShower));

			var movie = new Movie();
			var viewModel = new DetailViewModel();
			viewModel.Load(movie);

			viewModel.SaveCommand.Execute(null);

			repository.Received().Save(Arg.Is<Movie>(movie));
		}

		[TestMethod()]
		public void Close()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			var actualViewName = String.Empty;
			var messageBus = Substitute.For<IMessageBus>();
			messageBus
				.When(bus => bus.Publish<CloseViewMessage>(Arg.Any<CloseViewMessage>()))
				.Do(arg => actualViewName = ((CloseViewMessage)arg[0]).ViewName);
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMessageBus>().Instance(messageBus));

			var movie = new Movie();
			var viewModel = new DetailViewModel();
			viewModel.Load(movie);

			viewModel.CloseCommand.Execute(null);

			messageBus.ReceivedCalls();
			Assert.AreEqual(viewModel.Title, actualViewName);
		}

		[TestMethod()]
		public void Genres()
		{
			var viewModel = new DetailViewModel();

			var genres = viewModel.Genres;

			Assert.IsNotNull(genres);
			Assert.IsTrue(genres.Length > 0);
		}

		[TestMethod()]
		public void Ratings()
		{
			var viewModel = new DetailViewModel();

			var ratings = viewModel.Ratings;

			Assert.IsNotNull(ratings);
			Assert.IsTrue(ratings.Length > 0);
		}
	}
}
