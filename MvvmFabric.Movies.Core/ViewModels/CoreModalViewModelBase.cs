using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public abstract class CoreModalViewModelBase : ModalViewModelBase
	{
		public CoreModalViewModelBase()
		{
			if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				ComponentContainer.BuildUp(this);
			}
		}
	}
}
