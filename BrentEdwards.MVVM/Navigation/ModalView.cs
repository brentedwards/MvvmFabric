using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BrentEdwards.MVVM.Navigation
{
	public class ModalView : Window, IModalView
	{
		public void ShowModal()
		{
			ShowInTaskbar = false;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;

			MouseLeftButtonDown += delegate { DragMove(); };

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
