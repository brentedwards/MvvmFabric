using System;
using System.Collections.Generic;
using System.Linq;
using BrentEdwards.MVVM.Movies.Core.Models;

namespace BrentEdwards.MVVM.Movies.Core.Repositories
{
	public sealed class MovieRepository : IMovieRepository
	{
		private IList<Movie> _Movies;

		public MovieRepository()
		{
			_Movies = GetMovies();
		}

		#region IMovieRepository Members

		public IList<Movie> Load()
		{
			return _Movies;
		}

		public IList<Movie> Search(String keywords, Genres? genre, Ratings? rating)
		{
			var movies = _Movies.AsEnumerable();

			if (!string.IsNullOrEmpty(keywords))
			{
				movies = _Movies.Where(m => m.Name.ToLower().Contains(keywords.ToLower()));
			}

			if (genre.HasValue)
			{
				movies = movies.Where(m => m.Genre == genre.Value);
			}

			if (rating.HasValue)
			{
				movies = movies.Where(m => m.Rating == rating.Value);
			}

			return movies.ToList();
		}

		public Movie Save(Movie movie)
		{
			if (movie.Id == -1)
			{
				var newest = _Movies.OrderByDescending(m => m.Id).First();
				movie.Id = newest.Id + 1;
				_Movies.Add(movie);
			}

			return movie;
		}

		#endregion

		private IList<Movie> GetMovies()
		{
			_Movies = new List<Movie>();
			_Movies.Add(new Movie(1, "Cars", Genres.Family, Ratings.G));
			_Movies.Add(new Movie(2, "How to Train Your Dragon", Genres.Family, Ratings.PG));
			_Movies.Add(new Movie(3, "Avatar", Genres.Fantasy, Ratings.PG13));
			_Movies.Add(new Movie(4, "The Hangover", Genres.Comedy, Ratings.R));


			return _Movies;
		}
	}
}
