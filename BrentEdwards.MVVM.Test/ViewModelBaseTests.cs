using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace BrentEdwards.MVVM.Test
{
	[TestClass]
	public sealed class ViewModelBaseTests
	{
		private class MockViewModel : ViewModelBase
		{
			public void TriggerPropertyChanged(string propertyName)
			{
				NotifyPropertyChanged(propertyName);
			}
		}

		private List<string> _propertiesChanged;
		private void PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			_propertiesChanged.Add(e.PropertyName);
		}

		[TestMethod]
		public void NotifyPropertyChanged()
		{
			_propertiesChanged = new List<string>();

			var viewModel = new MockViewModel();
			var propertyName = Guid.NewGuid().ToString();

			viewModel.PropertyChanged += PropertyChanged;
			viewModel.TriggerPropertyChanged(propertyName);
			viewModel.PropertyChanged -= PropertyChanged;

			Assert.AreEqual(1, _propertiesChanged.Count);
			Assert.IsTrue(_propertiesChanged.Contains(propertyName));
		}
	}
}
