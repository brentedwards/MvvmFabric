using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BrentEdwards.MVVM.Movies.Core.Models
{
	public abstract class ModelBase : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

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
