using System;
using System.Collections.Generic;
using BrentEdwards.MVVM.Movies.Core.Models;

namespace BrentEdwards.MVVM.Movies.Core.Repositories
{
	public interface IMovieRepository
	{
		IList<Movie> Load();
		IList<Movie> Search(String keywords, Genres? genre, Ratings? rating);
		Movie Save(Movie movie);
	}
}
