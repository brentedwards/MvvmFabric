using System;
using System.Windows;

namespace MvvmFabric.Movies.Core.ModalDialogs
{
	public sealed class Dialog
	{
		public static MessageBoxResult ShowMessage(String message, String caption, MessageBoxButton button)
		{
			var messageShower = ComponentContainer.Container.Resolve<IMessageShower>();
			return messageShower.Show(message, caption, button);
		}
	}
}
