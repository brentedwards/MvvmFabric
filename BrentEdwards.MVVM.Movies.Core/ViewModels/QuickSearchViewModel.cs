using System;
using System.Windows.Input;
using BrentEdwards.MVVM.Messaging;
using BrentEdwards.MVVM.Movies.Core.Messaging;

namespace BrentEdwards.MVVM.Movies.Core.ViewModels
{
	public sealed class QuickSearchViewModel : CoreViewModelBase
	{
		public QuickSearchViewModel()
			: base()
		{
			SearchCommand = new ActionCommand(Search);
		}

		public ICommand SearchCommand { get; private set; }

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

		public void Search()
		{
			var search = new SearchMessage(_Keywords);

			MessageBus.Publish<SearchMessage>(search);
		}
	}
}
