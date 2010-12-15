using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrentEdwards.MVVM.Test.Navigation
{
	[TestClass]
	public class ViewTargetsTests
	{
		[TestMethod]
		public void Enumeration_Default()
		{
			var enumeration = new ViewTargets();

			Assert.AreEqual(0, (int)enumeration);
		}

		[TestMethod]
		public void ImplicitOperator_Enumeration()
		{
			int viewTargetValue = ViewTargets.DefaultView;

			Assert.AreEqual(1, viewTargetValue);
		}

		[TestMethod]
		public void ImplicitOperator_ViewTargets()
		{
			ViewTargets viewTarget = 1;

			Assert.AreEqual(ViewTargets.DefaultView, viewTarget);
		}

		[TestMethod]
		public void EqualsTest()
		{
			Assert.AreEqual(ViewTargets.DefaultView, ViewTargets.DefaultView);
		}

		[TestMethod]
		public void ToStringTest()
		{
			var viewTarget = ViewTargets.DefaultView;

			Assert.AreEqual("DefaultView", viewTarget.ToString());
		}

		[TestMethod]
		public void ToString_PropertyDoesntExist()
		{
			ViewTargets viewTarget = 0;

			Assert.AreEqual("0", viewTarget.ToString());
		}

		[TestMethod]
		public void GetHashCodeTest()
		{
			var viewTarget = ViewTargets.DefaultView;

			Assert.AreEqual(((int)1).GetHashCode(), viewTarget.GetHashCode());
		}
	}
}
