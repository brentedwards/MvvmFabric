using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentEdwards.MVVM;

namespace BrentEdwards.MVVM.Movies.Core.ViewModels
{
	public abstract class CoreViewModelBase : ViewModelBase
	{
		public CoreViewModelBase()
		{
			ComponentContainer.BuildUp(this);
		}
	}
}
