using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.Models;
using MvvmFabric.Movies.Core.Navigation;
using MvvmFabric.Movies.Core.Repositories;
using MvvmFabric.Xaml;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public sealed class MasterViewModel : CoreViewModelBase
	{
		public MasterViewModel()
			: base()
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				Init();
			}
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

		private void Init()
		{
			MovieCommand = new ActionCommand<Movie>(SelectMovie);
			NewMovieCommand = new ActionCommand(NewMovie);

			MessageBus.Subscribe<SearchMessage>(HandleSearch);

			Movies = MovieRepository.Load();
		}

		public void SelectMovie(Movie movie)
		{
			var message = new ShowViewMessage(MoviesViewTargets.Detail, movie);

			MessageBus.Publish<ShowViewMessage>(message);
		}

		public void MovieSelected(object sender, ExecuteEventArgs args)
		{
			var movie = args.MethodParameter as Movie;
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
