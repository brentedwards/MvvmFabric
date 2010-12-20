using System;
using System.Windows;
using MvvmFabric;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core;
using MvvmFabric.Movies.Core.ModalDialogs;
using MvvmFabric.Movies.Core.Navigation;
using MvvmFabric.Movies.Core.Repositories;
using MvvmFabric.Navigation;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MvvmFabric.Movies.Client.Views;
using MvvmFabric.Castle.Windsor;

namespace MvvmFabric.Movies.Configuration
{
	public sealed class ContainerConfiguration
	{
		public static void InitContainer()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			var viewConfigResolver = new ViewConfigurationResolver();

			container.Register(
				Component.For<IMessageBus>()
					.ImplementedBy<MessageBus>()
					.LifeStyle.Singleton,

				Component.For<IMessageShower>()
					.ImplementedBy<MessageShower>()
					.LifeStyle.Transient,
					
				Component.For<IMovieRepository>()
					.ImplementedBy<MovieRepository>()
					.LifeStyle.Singleton,
					
				Component.For<IViewFactory>()
					.ImplementedBy<ViewFactory>()
					.LifeStyle.Singleton,

				Component.For<ViewController>()
					.ImplementedBy<ViewController>()
					.LifeStyle.Singleton,
					
				Component.For<IViewConfigurationResolver>()
				.Instance(viewConfigResolver));

			InitNavigation(viewConfigResolver);
		}

		private static void InitNavigation(IViewConfigurationResolver viewConfigResolver)
		{
			viewConfigResolver.RegisterViewConfiguration<Detail>(MoviesViewTargets.Detail);
			viewConfigResolver.RegisterViewConfiguration<AdvancedSearch>(MoviesViewTargets.AdvancedSearch);
		}
	}
}
