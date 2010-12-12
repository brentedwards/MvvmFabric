using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BrentEdwards.MVVM.Navigation
{
	public interface IViewConfigurationResolver
	{
		void RegisterViewConfiguration<TView>(ViewTargets viewTarget) where TView : FrameworkElement;
		void RegisterViewConfiguration<TView, TViewModel>(ViewTargets viewTarget) where TView : FrameworkElement;

		ViewConfiguration ResolveConfiguration(ViewTargets viewTarget);
	}
}
