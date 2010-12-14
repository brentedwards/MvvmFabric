using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BrentEdwards.MVVM.Movies.Core.ModalDialogs
{
	public sealed class MessageShower : IMessageShower
	{
		#region IMessageShower Members

		public MessageBoxResult Show(String message, String caption, MessageBoxButton button)
		{
			return MessageBox.Show(message, caption, button);
		}

		#endregion
	}
}
