using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace MvvmFabric
{
	/// <summary>
	/// Base class for view models.
	/// </summary>
	public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies any listeners that a property has changed.
		/// </summary>
		/// <param name="propertyName">The name of the property which has changed.</param>
		protected void NotifyPropertyChanged(String propertyName)
		{
			if (PropertyChanged != null)
			{
				var args = new PropertyChangedEventArgs(propertyName);
				PropertyChanged(this, args);
			}
		}
	}
}
