using System.Collections.Generic;

namespace Utilities.GenericPool
{
    public static class PoolHandler
    {
        private static class PoolsOfType<T> where T : class
        {
            //No named pool
            private static Pool<T> defaultPool = null;

            private static Dictionary<string, Pool<T>> namedPools = null;

            public static Pool<T> GetPool(string _name = null)
            {
                if (string.IsNullOrEmpty(_name))
                {
                    if (defaultPool == null)
                        defaultPool = new Pool<T>();

                    return defaultPool;
                }
                else
                {
                    Pool<T> pool;

                    if (namedPools == null)
                    {
                        namedPools = new Dictionary<string, Pool<T>>();

                        pool = new Pool<T>();
                        namedPools.Add(_name, pool);
                    }
                    else if (!namedPools.TryGetValue(_name, out pool))
                    {
                        pool = new Pool<T>();
                        namedPools.Add(_name, pool);
                    }

                    return pool;
                }
            }
        }

        public static Pool<T> GetPool<T>(string _name = null) where T : class
        {
            return PoolsOfType<T>.GetPool(_name);
        }

        public static void Push<T>(T _instance, string _poolName = null) where T : class
        {
            PoolsOfType<T>.GetPool(_poolName).Push(_instance);
        }

        public static T Pop<T>(string _poolName = null) where T : class
        {
            return PoolsOfType<T>.GetPool(_poolName).Pop();
        }

        //Extension
        public static void Pool<T>(this T _instance, string _poolName = null) where T : class
        {
            PoolsOfType<T>.GetPool(_poolName).Push(_instance);
        }
    }
}
