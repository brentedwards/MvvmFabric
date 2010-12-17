using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// A base class for Modal Views.
	/// </summary>
	public abstract class ModalView : Window, IModalView
	{
		/// <summary>
		/// Gets or sets whether the view was accepted or cancelled.
		/// </summary>
		public bool Accepted { get; set; }

		/// <summary>
		/// The optional result returned by the view.
		/// </summary>
		public object ViewResult { get; set; }

		/// <summary>
		/// Shows the view as a modal dialog.
		/// </summary>
		public void ShowModal()
		{
			ShowInTaskbar = false;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;

			ShowDialog();
		}

		/// <summary>
		/// Method which is called when the view is requested to be closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnRequestClose(object sender, RequestCloseEventArgs e)
		{
			Accepted = e.Accepted;
			ViewResult = e.ViewResult;

			Close();
		}
	}
}
