using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentEdwards.MVVM.Navigation;
using System.Windows.Controls;
using BrentEdwards.MVVM.Movies.Core.Messaging;
using BrentEdwards.MVVM.Movies.Core.ViewModels;
using BrentEdwards.MVVM.Messaging;

namespace BrentEdwards.MVVM.Movies.Core.Navigation
{
	public sealed class ViewPlacer : IViewPlacer
	{
		private TabControl MainTabControl { get; set; }
		private IMessageBus MessageBus { get; set; }

		public ViewPlacer(TabControl mainTabControl)
		{
			MainTabControl = mainTabControl;

			MessageBus = ComponentContainer.Container.Resolve<IMessageBus>();
			MessageBus.Subscribe<CloseViewMessage>(HandleCloseView);
		}

		public void PlaceView(ViewResult viewResult)
		{
			var exists = false;
			var title = GetTitleFromViewModel(viewResult.View.DataContext);
			foreach (TabItem tabItem in MainTabControl.Items)
			{
				if (tabItem.Header.ToString() == title)
				{
					exists = true;
					break;
				}
			}

			if (!exists)
			{
				var newTabItem = new TabItem() { Header = title, Content = viewResult.View };
				MainTabControl.Items.Add(newTabItem);

				newTabItem.Focus();
			}
		}

		private String GetTitleFromViewModel(Object viewModel)
		{
			var title = String.Empty;

			var titledViewModel = viewModel as ITitledViewModel;
			if (titledViewModel != null)
			{
				title = titledViewModel.Title;
			}
			else
			{
				throw new ArgumentException(
					String.Format("'{0}' does not inherit from ITitledViewModel.",
					viewModel.GetType().Name));
			}

			return title;
		}

		private void HandleCloseView(CloseViewMessage args)
		{
			TabItem openTabItem = null;
			foreach (TabItem tabItem in MainTabControl.Items)
			{
				if (tabItem.Header.ToString() == args.ViewName)
				{
					openTabItem = tabItem;
					break;
				}
			}

			if (openTabItem != null)
			{
				MainTabControl.Items.Remove(openTabItem);
			}
		}
	}
}
