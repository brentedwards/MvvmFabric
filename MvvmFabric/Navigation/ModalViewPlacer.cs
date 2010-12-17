using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFabric.Messaging;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// A View Placer for Modal Views.
	/// </summary>
	public sealed class ModalViewPlacer : IViewPlacer
	{
		private Window AppWindow { get; set; }
		private IMessageBus MessageBus { get; set; }

		/// <summary>
		/// Constructor for ModalViewPlacer
		/// </summary>
		/// <param name="appWindow">
		/// The Window which owns the modal views.
		/// </param>
		/// <param name="messageBus">
		/// The <see cref="IMessageBus"/> used by the application.
		/// </param>
		public ModalViewPlacer(Window appWindow, IMessageBus messageBus)
		{
			AppWindow = appWindow;
			MessageBus = messageBus;
		}

		/// <summary>
		/// Places a view into the user interface.
		/// </summary>
		/// <param name="viewResult">
		/// The <see cref="ViewResult"/> containing the view to be placed.
		/// </param>
		public void PlaceView(ViewResult viewResult)
		{
			var view = viewResult.View as IModalView;
			if (view != null)
			{
				var modalViewModel = viewResult.View.DataContext as IModalViewModel;
				if (modalViewModel != null)
				{
					modalViewModel.RequestClose += view.OnRequestClose;
				}

				view.Owner = AppWindow;
				view.ShowModal();

				var closedMessage = new ModalViewClosedMessage(viewResult.ViewTarget, view.Accepted, view.ViewResult);
				MessageBus.Publish<ModalViewClosedMessage>(closedMessage);
			}
			else
			{
				throw new ArgumentException("Views must implement IModalView.");
			}
		}
	}
}
