using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Kiwi.Extensions
{
	public static class ListExtensions
	{
		public static void Shuffle<T>(this List<T> list)
		{
			int count = list.Count;

			for (var i = 0; i < list.Count; i++)
			{
				int randomNumber = Random.Range(i, count);
				(list[i], list[randomNumber]) = (list[randomNumber], list[i]);
			}
		}

		public static T PopAt<T>(this List<T> list, int index)
		{
			var r = list[index];
			list.RemoveAt(index);
			return r;
		}

		public static T PopFirst<T>(this List<T> list, Predicate<T> predicate)
		{
			var index = list.FindIndex(predicate);
			var r = list[index];
			list.RemoveAt(index);
			return r;
		}

		public static T PopFirstOrDefault<T>(this List<T> list, Predicate<T> predicate) where T : class
		{
			var index = list.FindIndex(predicate);

			if (index > -1)
			{
				var r = list[index];
				list.RemoveAt(index);
				return r;
			}

			return null;
		}

		public static T GetRandom<T>(this List<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}
        
		public static T GetRandomOrDefault<T>(this List<T> list)
		{
			if (list.Count > 0)
			{
				return list[Random.Range(0, list.Count)];
			}

			return default;
		}

		public static T GetRandomExcept<T>(this List<T> list, int exemptIndex)
		{
			int index = Random.Range(0, list.Count - 1);
            
			if (index >= exemptIndex)
			{
				index = (index + 1) % list.Count;
			}
            
			return list[index];
		}

		public static T PopRandom<T>(this List<T> list)
		{
			return list.PopAt(Random.Range(0, list.Count));
		}

		public static T2 SelectFirst<T1, T2>(this List<T1> list, Func<T1, T2> callback) where T2 : class
		{
			foreach (T1 t1 in list)
			{
				T2 t2 = callback(t1);

				if (t2 != null)
					return t2;
			}

			return null;
		}
	}
}