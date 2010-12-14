using System.Windows;
using System.Windows.Controls;
using BrentEdwards.MVVM.Movies.Core.Models;

namespace BrentEdwards.MVVM.Movies.Core.Conversion
{
	public sealed class RatingTemplateSelector : DataTemplateSelector
	{
		public DataTemplate GTemplate { get; set; }
		public DataTemplate RTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate template = null;
			if (item is Ratings)
			{
				switch ((Ratings)item)
				{
					case Ratings.G:
						template = GTemplate;
						break;

					case Ratings.R:
						template = RTemplate;
						break;
				}
			}

			return template;
		}
	}
}
