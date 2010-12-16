using System;
using System.Windows.Input;
using MvvmFabric.Messaging;
using MvvmFabric.Movies.Core.Messaging;
using MvvmFabric.Movies.Core.Navigation;

namespace MvvmFabric.Movies.Core.ViewModels
{
	public sealed class QuickSearchViewModel : CoreViewModelBase
	{
		public QuickSearchViewModel()
			: base()
		{
			SearchCommand = new ActionCommand(Search);
			AdvancedCommand = new ActionCommand(Advanced);
		}

		public ICommand SearchCommand { get; private set; }
		public ICommand AdvancedCommand { get; private set; }

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

		public void Advanced()
		{
			var message = new ShowViewMessage(MoviesViewTargets.AdvancedSearch);
			MessageBus.Publish<ShowViewMessage>(message);
		}
	}
}
