using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Movies.Core.Conversion;
using MvvmFabric.Movies.Core.Models;

namespace MvvmFabric.Movies.Core.Tests.Conversion
{
	[TestClass()]
	public sealed class RatingTemplateSelectorTests
	{
		[TestMethod()]
		public void SelectTemplateG()
		{
			var gTemplate = new DataTemplate();
			var rTemplate = new DataTemplate();
			var selector = new RatingTemplateSelector() { GTemplate = gTemplate, RTemplate = rTemplate };

			var actualTemplate = selector.SelectTemplate(Ratings.G, null);

			Assert.AreSame(gTemplate, actualTemplate);
		}

		[TestMethod()]
		public void SelectTemplateR()
		{
			var gTemplate = new DataTemplate();
			var rTemplate = new DataTemplate();
			var selector = new RatingTemplateSelector() { GTemplate = gTemplate, RTemplate = rTemplate };

			var actualTemplate = selector.SelectTemplate(Ratings.R, null);

			Assert.AreSame(rTemplate, actualTemplate);
		}
	}
}
