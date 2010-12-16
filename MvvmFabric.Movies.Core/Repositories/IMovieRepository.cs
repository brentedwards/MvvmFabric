using System;
using System.Collections.Generic;
using MvvmFabric.Movies.Core.Models;

namespace MvvmFabric.Movies.Core.Repositories
{
	public interface IMovieRepository
	{
		IList<Movie> Load();
		IList<Movie> Search(String keywords, Genres? genre, Ratings? rating);
		Movie Save(Movie movie);
	}
}
