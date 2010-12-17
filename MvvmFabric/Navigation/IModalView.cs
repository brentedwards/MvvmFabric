using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Interface for a modal view.
	/// </summary>
	public interface IModalView
	{
		/// <summary>
		/// Gets or sets the Window which owns the view.
		/// </summary>
		Window Owner { get; set; }

		/// <summary>
		/// Gets or sets whether the view was accepted or cancelled.
		/// </summary>
		bool Accepted { get; set; }

		/// <summary>
		/// The optional result returned by the view.
		/// </summary>
		Object ViewResult { get; set; }

		/// <summary>
		/// Shows the view as a modal dialog.
		/// </summary>
		void ShowModal();

		/// <summary>
		/// Method which is called when the view is requested to be closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnRequestClose(object sender, RequestCloseEventArgs e);
	}
}
