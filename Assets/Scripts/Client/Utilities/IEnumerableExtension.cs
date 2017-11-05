using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterSon.Utilities
{
	public static class IEnumerableExtension
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
			{
				action(item);
			}
		}

		public static T PickRandomly<T>(this IEnumerable<T> container)
		{
			return container.PickRandomly(new Random());
		}

		public static T PickRandomly<T>(this IEnumerable<T> container, Random randomTable)
		{
			return container.ElementAt(randomTable.Next(0, container.Count()));
		}
	}
}