using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BrentEdwards.MVVM.Navigation;
using BrentEdwards.MVVM.Movies.Core.ViewModels;
using BrentEdwards.MVVM.Movies.Core;
using Castle.MicroKernel.Registration;
using BrentEdwards.MVVM.Movies.Core.Messaging;
using BrentEdwards.MVVM.Messaging;
using BrentEdwards.MVVM.Movies.Core.Navigation;

namespace BrentEdwards.MVVM.Movies
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ViewController ViewController { get; set; }
		private IViewPlacer ViewPlacer { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			ViewPlacer = new ViewPlacer(this, MainTabControl);

			ComponentContainer.Container.Register(Component.For<IViewPlacer>().Instance(ViewPlacer));

			ViewController = ComponentContainer.Container.Resolve<ViewController>();
		}

		
	}
}
