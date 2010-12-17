using System;
using System.Collections.Generic;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.Models;
using MvvmFabric.Movies.Core.Repositories;
using MvvmFabric.Movies.Core.ViewModels;
using NSubstitute;

namespace MvvmFabric.Movies.Core.Tests.ViewModels
{
	[TestClass]
	public sealed class MasterViewModelTests
	{
		private IMessageBus _MessageBus;
		private IMovieRepository _MovieRepository;

		private void CreateContainer(Boolean includeMockBus)
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			if (includeMockBus)
			{
				_MessageBus = Substitute.For<IMessageBus>();
				container.Register(
					Castle.MicroKernel.Registration.Component.For<IMessageBus>().Instance(_MessageBus));
			}

			var movies = new List<Movie>();
			_MovieRepository = Substitute.For<IMovieRepository>();
			_MovieRepository.Load().Returns(movies);
			container.Register(
				Castle.MicroKernel.Registration.Component.For<IMovieRepository>().Instance(_MovieRepository));
		}

		[TestMethod]
		public void Movies()
		{
			CreateContainer(true);
			var viewModel = new MasterViewModel();

			Assert.IsNotNull(viewModel.Movies);
		}

		[TestMethod]
		public void MovieSelected()
		{
			CreateContainer(true);
			var viewModel = new MasterViewModel();

			viewModel.MovieCommand.Execute(null);

			_MessageBus.Received().Publish<ShowViewMessage>(Arg.Any<ShowViewMessage>());
		}

		[TestMethod]
		public void NewMovie()
		{
			CreateContainer(true);
			var viewModel = new MasterViewModel();

			viewModel.NewMovieCommand.Execute(null);

			_MessageBus.Received().Publish<ShowViewMessage>(Arg.Any<ShowViewMessage>());
		}

		[TestMethod]
		public void Search()
		{
			CreateContainer(false);
			_MessageBus = new MessageBus();
			ComponentContainer.Container.Register(
				Castle.MicroKernel.Registration.Component.For<IMessageBus>().Instance(_MessageBus));

			var keywords = Guid.NewGuid().ToString();
			var genre = Genres.Action;
			var rating = Ratings.G;
			var searchMessage = new SearchMessage(keywords, genre, rating);

			var viewModel = new MasterViewModel();

			_MessageBus.Publish<SearchMessage>(searchMessage);

			_MovieRepository.Received().Search(
				Arg.Is<String>(keywords),
				Arg.Is<Genres>(genre),
				Arg.Is<Ratings>(rating));
		}
	}
}
