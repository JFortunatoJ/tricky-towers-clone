using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blazewing
{
    [ExecuteInEditMode]
    public static class DataController
    {
        private static Dictionary<Type, object> m_dataList;

        static DataController()
        {
            m_dataList = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Add a item to be cached
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void Add<T>(T data) where T : struct
        {
            if (Exist<T>())
                Remove<T>();

            m_dataList.Add(typeof(T), data);
        }

        /// <summary>
        /// Get a item cached by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Get<T>() where T : struct
        {
            object item;
            m_dataList.TryGetValue(typeof(T), out item);

            if (item != null)
                return (T)item;
            else
                return default;
        }

        public static Dictionary<Type, object> GetAll()
        {
            return m_dataList;
        }

        /// <summary>
        /// Remove a item cached
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void Remove<T>() where T : struct
        {
            Type myData = typeof(T);

            if (Exist<T>())
                m_dataList.Remove(myData);
        }

        /// <summary>
        /// Verify if this item is already on cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Exist<T>() where T : struct
        {
            return m_dataList.ContainsKey(typeof(T));
        }
    }
}