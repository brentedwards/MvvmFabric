using System;
using System.Windows.Input;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.Models;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public sealed class AdvancedSearchViewModel : CoreModalViewModelBase
	{
		public AdvancedSearchViewModel()
			: base()
		{
			SearchCommand = new ActionCommand(Search);
			CancelCommand = new ActionCommand(Cancel);
		}

		public ICommand SearchCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }

		public IMessageBus MessageBus { get; set; }

		private String _Keywords;
		public String Keywords
		{
			get { return _Keywords; }
			set
			{
				_Keywords = value;
				NotifyPropertyChanged("Keywords");
			}
		}

		public Array Genres
		{
			get
			{
				return Enum.GetValues(typeof(Genres));
			}
		}

		private Genres? _SelectedGenre;
		public Genres? SelectedGenre
		{
			get { return _SelectedGenre; }
			set
			{
				_SelectedGenre = value;
				NotifyPropertyChanged("SelectedGenre");
			}
		}

		public Array Ratings
		{
			get
			{
				return Enum.GetValues(typeof(Ratings));
			}
		}

		private Ratings? _SelectedRating;
		public Ratings? SelectedRating
		{
			get { return _SelectedRating; }
			set
			{
				_SelectedRating = value;
				NotifyPropertyChanged("SelectedRating");
			}
		}

		public void Search()
		{
			var message = new SearchMessage(Keywords, SelectedGenre, SelectedRating);

			MessageBus.Publish<SearchMessage>(message);

			NotifyCloseRequest(true);
		}

		public void Cancel()
		{
			NotifyCloseRequest(false);
		}
	}
}
