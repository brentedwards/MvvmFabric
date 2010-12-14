using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BrentEdwards.MVVM.Movies.Core.Models
{
	public sealed class Movie : ModelBase, IDataErrorInfo
	{
		public Movie()
		{
			Id = -1;
		}

		public Movie(int id, String name, Genres genre, Ratings rating)
		{
			Id = id;
			Name = name;
			Genre = genre;
			Rating = rating;
		}

		public int Id { get; set; }

		private String _Name;
		public String Name
		{
			get { return _Name; }
			set
			{
				_Name = value;
				NotifyPropertyChanged("Name");
			}
		}

		private Genres _Genre;
		public Genres Genre
		{
			get { return _Genre; }
			set
			{
				_Genre = value;
				NotifyPropertyChanged("Genre");
			}
		}

		private Ratings _Rating;
		public Ratings Rating
		{
			get { return _Rating; }
			set
			{
				_Rating = value;
				NotifyPropertyChanged("Rating");
			}
		}

		#region IDataErrorInfo Members

		public string Error
		{
			get { throw new NotImplementedException(); }
		}

		public string this[string name]
		{
			get
			{
				String result = null;

				if (name == "Name")
				{
					if (String.IsNullOrEmpty(Name))
					{
						result = "The movie must have a Name.";
					}
				}

				return result;
			}
		}

		#endregion
	}
}
