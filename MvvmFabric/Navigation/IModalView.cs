using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	public interface IModalView
	{
		Window Owner { get; set; }
		bool Accepted { get; set; }
		Object ViewResult { get; set; }

		void ShowModal();
		void OnRequestClose(object sender, RequestCloseEventArgs e);
	}
}
