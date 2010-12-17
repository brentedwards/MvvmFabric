using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFabric;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public abstract class CoreViewModelBase : ViewModelBase
	{
		public CoreViewModelBase()
		{
			if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				ComponentContainer.BuildUp(this);
			}
		}
	}
}
