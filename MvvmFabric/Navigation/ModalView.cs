using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	public class ModalView : Window, IModalView
	{
		public void ShowModal()
		{
			ShowInTaskbar = false;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;

			ShowDialog();
		}


		public void OnRequestClose(object sender, RequestCloseEventArgs e)
		{
			Accepted = e.Accepted;
			ViewResult = e.ViewResult;

			Close();
		}

		public bool Accepted { get; set; }
		public object ViewResult { get; set; }
	}
}
