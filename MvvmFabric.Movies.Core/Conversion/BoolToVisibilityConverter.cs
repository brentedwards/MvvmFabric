using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace MvvmFabric.Movies.Core.Conversion
{
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var visibility = Visibility.Visible;

			if (value is Boolean)
			{
				visibility = (Boolean)value ? Visibility.Visible : Visibility.Collapsed;
			}

			return visibility;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
