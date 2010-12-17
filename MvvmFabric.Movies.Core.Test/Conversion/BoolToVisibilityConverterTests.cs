using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Movies.Core.Conversion;

namespace MvvmFabric.Movies.Core.Tests.Conversion
{
	[TestClass]
	public sealed class BoolToVisibilityConverterTests
	{
		[TestMethod]
		public void ConvertTrue()
		{
			var converter = new BoolToVisibilityConverter();

			var visibility = converter.Convert(true, null, null, null);

			Assert.AreEqual(Visibility.Visible, visibility);
		}

		[TestMethod]
		public void ConvertFalse()
		{
			var converter = new BoolToVisibilityConverter();

			var visibility = converter.Convert(false, null, null, null);

			Assert.AreEqual(Visibility.Collapsed, visibility);
		}

		[TestMethod]
		public void ConvertNotBool()
		{
			var converter = new BoolToVisibilityConverter();

			var visibility = converter.Convert(new Object(), null, null, null);

			Assert.AreEqual(Visibility.Visible, visibility);
		}

		[TestMethod, ExpectedException(typeof(NotImplementedException))]
		public void ConvertBack()
		{
			var converter = new BoolToVisibilityConverter();

			converter.ConvertBack(null, null, null, null);
		}
	}
}
