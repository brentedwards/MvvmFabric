using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFabric.Navigation;
using Castle.Windsor;
using System.Windows;
using Castle.MicroKernel.Registration;

namespace MvvmFabric.Castle.Windsor
{
	public sealed class WindsorViewConfigurationResolver : IViewConfigurationResolver
	{
		private IWindsorContainer Container { get; set; }

		public WindsorViewConfigurationResolver(IWindsorContainer container)
		{
			Container = container;
		}

		public void RegisterViewConfiguration<TView>(ViewTargets viewTarget) where TView : FrameworkElement
		{
			// If a view is named "Blah", register the ViewConfiguration as "Blah",
			// the view as "BlahView", and the view model (if it needs to be explicitly
			// defined) as "BlahViewModel".
			Container.Register(
				Component.For<FrameworkElement>()
					.Named(String.Format("{0}View", viewTarget.ToString()))
					.ImplementedBy<TView>()
					.LifeStyle.Transient,
				Component.For<ViewConfiguration>()
					.Named(viewTarget.ToString())
					.LifeStyle.Transient
					.Parameters(Parameter.ForKey("view").Eq(String.Format("${{{0}View}}", viewTarget.ToString()))));
		}

		public void RegisterViewConfiguration<TView, TViewModel>(ViewTargets viewTarget) where TView : FrameworkElement
		{
			Container.Register(
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

		public ViewConfiguration ResolveConfiguration(ViewTargets viewTarget)
		{
			return Container.Resolve<ViewConfiguration>(viewTarget.ToString());
		}
	}
}
