using System;
using System.Collections.Generic;
using System.Windows.Input;
using BrentEdwards.MVVM.Messaging;
using BrentEdwards.MVVM.Movies.Core.Messaging;
using BrentEdwards.MVVM.Movies.Core.Models;
using BrentEdwards.MVVM.Movies.Core.Navigation;
using BrentEdwards.MVVM.Movies.Core.Repositories;

namespace BrentEdwards.MVVM.Movies.Core.ViewModels
{
	public sealed class MasterViewModel : CoreViewModelBase
	{
		public MasterViewModel()
			: base()
		{
			MovieCommand = new ActionCommand<Movie>(SelectMovie);
			NewMovieCommand = new ActionCommand(NewMovie);

			MessageBus.Subscribe<SearchMessage>(HandleSearch);

			Movies = MovieRepository.Load();
		}

		public ICommand MovieCommand { get; private set; }
		public ICommand NewMovieCommand { get; private set; }

		public IMessageBus MessageBus { get; set; }
		public IMovieRepository MovieRepository { get; set; }

		private IList<Movie> _Movies;
		public IList<Movie> Movies
		{
			get { return _Movies; }
			private set
			{
				_Movies = value;
				NotifyPropertyChanged("Movies");
			}
		}

		public void SelectMovie(Object movie)
		{
			var message = new ShowViewMessage(MoviesViewTargets.Detail, movie);

			MessageBus.Publish<ShowViewMessage>(message);
		}

		public void NewMovie()
		{
			var message = new ShowViewMessage(MoviesViewTargets.Detail, new Movie());

			MessageBus.Publish<ShowViewMessage>(message);
		}

		private void HandleSearch(SearchMessage search)
		{
			Movies = MovieRepository.Search(search.Keywords, search.Genre, search.Rating);
		}
	}
}
