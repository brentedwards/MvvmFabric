using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using System.Reflection;
using Castle.MicroKernel.ComponentActivator;

namespace BrentEdwards.MVVM.Movies.Core
{
	public class ComponentContainer
	{
		public static IWindsorContainer Container { get; set; }

		public static void BuildUp(object target)
		{
			var type = target.GetType();
			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				if (property.CanWrite && Container.Kernel.HasComponent(property.PropertyType))
				{
					var value = Container.Resolve(property.PropertyType);
					try
					{
						property.SetValue(target, value, null);
					}
					catch (Exception ex)
					{
						var message = string.Format("Error setting property {0} on type {1}, See inner exception for more information.", property.Name, type.FullName);
						throw new ComponentActivatorException(message, ex);
					}
				}
			}

		}
	}
}
