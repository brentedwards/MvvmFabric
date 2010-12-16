using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using BrentEdwards.MVVM.Movies.Configuration;

namespace BrentEdwards.MVVM.Movies
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			ContainerConfiguration.InitContainer();
		}
	}
}
