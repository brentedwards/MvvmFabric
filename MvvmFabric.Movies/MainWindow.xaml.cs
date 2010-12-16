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
using MvvmFabric.Navigation;
using MvvmFabric.Movies.Core.ViewModels;
using MvvmFabric.Movies.Core;
using Castle.MicroKernel.Registration;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Navigation;

namespace MvvmFabric.Movies
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
