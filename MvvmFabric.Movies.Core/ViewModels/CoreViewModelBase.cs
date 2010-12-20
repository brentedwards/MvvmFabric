using System.ComponentModel;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public abstract class CoreViewModelBase : ViewModelBase
	{
		public CoreViewModelBase()
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				ComponentContainer.BuildUp(this);
			}
		}
	}
}
