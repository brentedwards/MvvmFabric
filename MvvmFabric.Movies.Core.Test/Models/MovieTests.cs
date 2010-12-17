using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Movies.Core.Models;

namespace MvvmFabric.Movies.Core.Tests.Models
{
	[TestClass]
	public sealed class MovieTests
	{
		[TestMethod]
		public void Create()
		{
			var movie = new Movie();

			Assert.AreEqual(-1, movie.Id);
		}

		[TestMethod]
		public void CreateWithParams()
		{
			var id = new Random().Next();
			var name = Guid.NewGuid().ToString();
			var genre = Genres.Action;
			var rating = Ratings.G;

			var movie = new Movie(id, name, genre, rating);

			Assert.AreEqual(id, movie.Id);
			Assert.AreEqual(name, movie.Name);
			Assert.AreEqual(genre, movie.Genre);
			Assert.AreEqual(rating, movie.Rating);
		}

		private List<String> _PropertiesChanged;
		private void HandlePropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			_PropertiesChanged.Add(e.PropertyName);
		}

		[TestMethod]
		public void Name()
		{
			_PropertiesChanged = new List<String>();

			var name = Guid.NewGuid().ToString();

			var movie = new Movie();
			movie.PropertyChanged += HandlePropertyChanged;

			movie.Name = name;

			Assert.AreEqual(name, movie.Name);
			Assert.IsTrue(_PropertiesChanged.Contains("Name"));
		}

		[TestMethod]
		public void Genre()
		{
			_PropertiesChanged = new List<String>();

			var genre = Genres.Comedy;

			var movie = new Movie();
			movie.PropertyChanged += HandlePropertyChanged;

			movie.Genre = genre;

			Assert.AreEqual(genre, movie.Genre);
			Assert.IsTrue(_PropertiesChanged.Contains("Genre"));
		}

		[TestMethod]
		public void Rating()
		{
			_PropertiesChanged = new List<String>();

			var rating = Ratings.PG;

			var movie = new Movie();
			movie.PropertyChanged += HandlePropertyChanged;

			movie.Rating = rating;

			Assert.AreEqual(rating, movie.Rating);
			Assert.IsTrue(_PropertiesChanged.Contains("Rating"));
		}

		[TestMethod, ExpectedException(typeof(NotImplementedException))]
		public void Error()
		{
			var movie = new Movie();

			var error = movie.Error;
		}

		[TestMethod]
		public void Validates()
		{
			var name = Guid.NewGuid().ToString();
			var movie = new Movie();
			movie.Name = name;

			var validationError = movie["Name"];

			Assert.IsTrue(String.IsNullOrEmpty(validationError));
		}

		[TestMethod]
		public void DoesNotValidate()
		{
			var movie = new Movie();

			var validationError = movie["Name"];

			Assert.IsFalse(String.IsNullOrEmpty(validationError));
		}
	}
}
