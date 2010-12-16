using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Movies.Core.ModalDialogs
{
	public interface IMessageShower
	{
		MessageBoxResult Show(String message, String caption, MessageBoxButton button);
	}
}
