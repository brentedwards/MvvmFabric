using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM.Movies.Core.ViewModels
{
	public abstract class CoreModalViewModelBase : ModalViewModelBase
	{
		public CoreModalViewModelBase()
		{
			ComponentContainer.BuildUp(this);
		}
	}
}
