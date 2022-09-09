using System;
using System.Collections.Generic;

namespace Blazewing
{
    public class DataEvent
    {
        private static Dictionary<int, List<object>> m_observers;

        static DataEvent()
        {
            m_observers = new Dictionary<int, List<object>>();
        }

        public static void Register<T>(Action<T> observer) where T : struct
        {
            List<object> m_list;

            var hash = GetGenericHash<T>();

            if (!m_observers.TryGetValue(hash, out m_list))
                m_list = new List<object>();

            m_list.Add(observer);

            m_observers[hash] = m_list;
        }

        public static void Unregister<T>(Action<T> observer) where T : struct
        {
            var hash = GetGenericHash<T>();

            if (m_observers.ContainsKey(hash))
            {
                List<object> m_list;

                if (m_observers.TryGetValue(hash, out m_list))
                {
                    m_list.Remove(observer);
                    if (m_list.Count == 0)
                        m_observers.Remove(hash);
                }
            }
        }

        public static void Notify<T>(T value) where T : struct
        {
            List<object> m_list;

            if (!m_observers.TryGetValue(GetGenericHash<T>(), out m_list))
                m_list = new List<object>();

            for (int i = 0; i < m_list.Count; i++)
            {
                ((Action<T>)m_list[i])?.Invoke(value);
            }
        }

        private static int GetGenericHash<T>()
        {   
            return typeof(T).GetHashCode();
        }
    }
}