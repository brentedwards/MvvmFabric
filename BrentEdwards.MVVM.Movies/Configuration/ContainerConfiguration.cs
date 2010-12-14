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

namespace Movies.Client.Configuration
{
	public sealed class ContainerConfiguration
	{
		public static void InitContainer()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

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
					.LifeStyle.Singleton);

			InitNavigation();
		}

		private static void InitNavigation()
		{
			RegisterViewConfiguration<Detail>(MoviesViewTargets.Detail);
		}


		private static void RegisterViewConfiguration<TView>(ViewTargets viewTarget) where TView : FrameworkElement
		{
			// If a view is named "Blah", register the ViewConfiguration as "Blah",
			// the view as "BlahView", and the view model (if it needs to be explicitly
			// defined) as "BlahViewModel".
			ComponentContainer.Container.Register(
				Component.For<FrameworkElement>()
					.Named(String.Format("{0}View", viewTarget.ToString()))
					.ImplementedBy<TView>()
					.LifeStyle.Transient,
				Component.For<ViewConfiguration>()
					.Named(viewTarget.ToString())
					.LifeStyle.Transient
					.Parameters(Parameter.ForKey("view").Eq(String.Format("${{{0}View}}", viewTarget.ToString()))));
		}

		private static void RegisterViewConfiguration<TView, TViewModel>(ViewTargets viewTarget) where TView : FrameworkElement
		{
			ComponentContainer.Container.Register(
				Component.For<Object>()
					.Named(String.Format("{0}ViewModel", viewTarget.ToString()))
					.ImplementedBy<TViewModel>()
					.LifeStyle.Transient,
				Component.For<FrameworkElement>()
					.Named(String.Format("{0}View", viewTarget.ToString()))
					.ImplementedBy<TView>()
					.LifeStyle.Transient,
				Component.For<ViewConfiguration>()
					.Named(viewTarget.ToString())
					.LifeStyle.Transient
					.Parameters(
						Parameter.ForKey("view").Eq(String.Format("${{{0}View}}", viewTarget.ToString())),
						Parameter.ForKey("viewModel").Eq(String.Format("${{{0}ViewModel}}", viewTarget.ToString()))));
		}
	}
}
