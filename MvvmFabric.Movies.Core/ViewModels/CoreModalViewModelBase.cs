using System.ComponentModel;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public abstract class CoreModalViewModelBase : ModalViewModelBase
	{
		public CoreModalViewModelBase()
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				ComponentContainer.BuildUp(this);
			}
		}
	}
}
