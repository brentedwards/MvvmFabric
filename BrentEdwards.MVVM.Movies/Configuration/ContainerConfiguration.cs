using System;
using System.Windows;
using BrentEdwards.MVVM;
using BrentEdwards.MVVM.Messaging;
using BrentEdwards.MVVM.Movies.Core;
using BrentEdwards.MVVM.Movies.Core.ModalDialogs;
using BrentEdwards.MVVM.Movies.Core.Navigation;
using BrentEdwards.MVVM.Movies.Core.Repositories;
using BrentEdwards.MVVM.Navigation;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using BrentEdwards.MVVM.Movies.Client.Views;
using BrentEdwards.MVVM.Castle.Windsor;

namespace Movies.Client.Configuration
{
	public sealed class ContainerConfiguration
	{
		public static void InitContainer()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;
			
			var viewConfigResolver = new WindsorViewConfigurationResolver(container);

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
		}
	}
}
