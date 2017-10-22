using System;
using System.Collections.Generic;

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
	}
}