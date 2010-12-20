using System;
using System.Windows;
using System.Windows.Data;

namespace MvvmFabric.Conversion
{
	/// <summary>
	/// Converts a bool to a Visibility value.
	/// </summary>
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Gets or sets the value used when the value being converted is false.
		/// The default value is Visibility.Collapsed.
		/// </summary>
		public Visibility OffVisibility { get; set; }

		/// <summary>
		/// Constructor for BoolToVisibilityConverter.
		/// </summary>
		public BoolToVisibilityConverter()
		{
			OffVisibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Converts a bool to a Visibility value.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var visibility = Visibility.Visible;

			if (value is bool)
			{
				visibility = (bool)value ? Visibility.Visible : OffVisibility;
			}

			return visibility;
		}

		/// <summary>
		/// ConvertBack is not implemented.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
