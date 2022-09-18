using System.Collections.Generic;

namespace MiniclipTest.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a random item from the list and then removes it.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T PopRandomItem<T>(this List<T> list)
        {
            T item = list[UnityEngine.Random.Range(0, list.Count)];
            list.Remove(item);

            return item;
        }
    }
}