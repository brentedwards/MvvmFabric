using System;
using MvvmFabric.Movies.Core.Models;

namespace MvvmFabric.Movies.Core.Messaging
{
	public sealed class SearchMessage
	{
		public SearchMessage(String keywords)
		{
			Keywords = keywords;
		}

		public SearchMessage(String keywords, Genres? genre, Ratings? rating)
			: this(keywords)
		{
			Genre = genre;
			Rating = rating;
		}

		public String Keywords { get; private set; }
		public Genres? Genre { get; private set; }
		public Ratings? Rating { get; private set; }
	}
}
