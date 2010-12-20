using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Conversion;
using System.Windows;

namespace MvvmFabric.Test.Conversion
{
	[TestClass]
	public class BoolToVisibilityConverterTests
	{
		[TestMethod()]
		public void ConvertTrue()
		{
			var converter = new BoolToVisibilityConverter();

			var visibility = converter.Convert(true, null, null, null);

			Assert.AreEqual(Visibility.Visible, visibility);
		}

		[TestMethod()]
		public void ConvertFalse()
		{
			var converter = new BoolToVisibilityConverter();

			var visibility = converter.Convert(false, null, null, null);

			Assert.AreEqual(Visibility.Collapsed, visibility);
		}

		[TestMethod()]
		public void ConvertNotBool()
		{
			var converter = new BoolToVisibilityConverter();

			var visibility = converter.Convert(new Object(), null, null, null);

			Assert.AreEqual(Visibility.Visible, visibility);
		}

		[TestMethod(), ExpectedException(typeof(NotImplementedException))]
		public void ConvertBack()
		{
			var converter = new BoolToVisibilityConverter();

			converter.ConvertBack(null, null, null, null);
		}

		[TestMethod()]
		public void ConvertHidden()
		{
			var converter = new BoolToVisibilityConverter();
			converter.OffVisibility = Visibility.Hidden;

			var visibility = converter.Convert(false, null, null, null);

			Assert.AreEqual(Visibility.Hidden, visibility);
		}
	}
}
