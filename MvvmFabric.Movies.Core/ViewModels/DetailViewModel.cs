using System;
using System.Windows;
using System.Windows.Input;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.ModalDialogs;
using MvvmFabric.Movies.Core.Models;
using MvvmFabric.Movies.Core.Repositories;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public sealed class DetailViewModel : CoreViewModelBase, ITitledViewModel
	{
		public DetailViewModel()
		{
			SaveCommand = new ActionCommand(Save);
			CloseCommand = new ActionCommand(Close);
		}

		public ICommand SaveCommand { get; private set; }
		public ICommand CloseCommand { get; private set; }

		public IMessageBus MessageBus { get; set; }
		public IMovieRepository MovieRepository { get; set; }

		private Boolean _IsEditable;
		public Boolean IsEditable
		{
			get { return _IsEditable; }
			set
			{
				_IsEditable = value;
				NotifyPropertyChanged("IsEditable");
			}
		}

		private Movie _Movie;
		public Movie Movie
		{
			get { return _Movie; }
			private set
			{
				_Movie = value;
				NotifyPropertyChanged("Movie");
			}
		}

		public Array Genres
		{
			get
			{
				return Enum.GetValues(typeof(Genres));
			}
		}

		public Array Ratings
		{
			get
			{
				return Enum.GetValues(typeof(Ratings));
			}
		}

		#region ITitledViewModel Members

		public String Title { get; private set; }

		#endregion

		public void Load(Movie movie)
		{
			Movie = movie;
			if (movie.Id == -1)
			{
				IsEditable = true;
			}

			Title = String.Format("{0} Details", Movie.Name);
		}

		public void Save()
		{
			MovieRepository.Save(Movie);

			var message = String.Format("'{0}' was saved.", Movie.Name);
			Dialog.ShowMessage(message, "Movie Saved", MessageBoxButton.OK);
		}

		public void Close()
		{
			var message = new CloseViewMessage(Title);

			MessageBus.Publish<CloseViewMessage>(message);
		}
	}
}
