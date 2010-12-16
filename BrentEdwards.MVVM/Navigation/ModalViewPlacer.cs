using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentEdwards.MVVM.Messaging;
using System.Windows;

namespace BrentEdwards.MVVM.Navigation
{
	public sealed class ModalViewPlacer : IViewPlacer
	{
		private Window AppWindow { get; set; }
		private IMessageBus MessageBus { get; set; }

		public ModalViewPlacer(Window appWindow, IMessageBus messageBus)
		{
			AppWindow = appWindow;
			MessageBus = messageBus;
		}

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
