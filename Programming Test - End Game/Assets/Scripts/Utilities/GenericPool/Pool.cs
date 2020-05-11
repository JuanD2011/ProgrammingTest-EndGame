using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Utilities.Factory;

namespace Utilities.GenericPool
{
    public class Pool<T> where T : class
    {
        private Stack<T> pool = null;

        public T Prefab { get; set; }

        public Func<T, T> Create;

        public Action<T> OnPush = null, OnPop = null;

        public Pool(Func<T, T> _createMethod = null, Action<T> _onPush = null, Action<T> _onPop = null, T _prefab = null)
        {
            pool = new Stack<T>();

            Create = _createMethod;
            OnPush = _onPush;
            OnPop = _onPop;
            Prefab = _prefab;
        }

        /// <summary>
        /// Create new instance of the given prefab
        /// </summary>
        /// <param name="_prefab"></param>
        /// <returns></returns>
        private T CreateNew(T _prefab)
        {
            if (Create != null)
                return Create(_prefab);
            if (_prefab == null || !(_prefab is Object))
                return null;

            return Factory<Object>.Create(_prefab as Object) as T;
        }

        /// <summary>
        /// Push to the pool the given instance
        /// </summary>
        /// <param name="_instance"></param>
        public void Push(T _instance)
        {
            if (_instance == null)
                return;

            OnPush?.Invoke(_instance);
            pool.Push(_instance);
        }

        /// <summary>
        /// Push to the pool the given multiple instances
        /// </summary>
        /// <param name="_instances"></param>
        public void Push(IEnumerable<T> _instances)
        {
            if (_instances == null)
                return;

            foreach (T item in _instances)
                Push(item);
        }

        /// <summary>
        /// Populates pool with multiple instances of the given prefab
        /// </summary>
        /// <param name="_prefab"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool Populate(T _prefab, int count)
        {
            if (count <= 0)
                return true;

            T tempObject = CreateNew(_prefab);
            if (tempObject == null) return false;

            Push(tempObject);

            for (int i = 1; i < count; i++)
            {
                Push(CreateNew(_prefab));
            }

            return true;
        }

        /// <summary>
        /// Pupulate pool with the default prefab
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public bool Populate(int _count) => Populate(Prefab, _count);

        /// <summary>
        /// Pop an instance from the pool
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T instanceToPop;

            //If pool is empty, create a new instance of the default prefab
            if (pool.Count == 0)
                instanceToPop = CreateNew(Prefab);
            //Pool has instances, pops the first item in the pool
            else
                instanceToPop = pool.Pop();

            OnPop?.Invoke(instanceToPop);
            return instanceToPop;
        }

        /// <summary>
        /// Pops multiple instances from the pool
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public T[] Pop(int _count)
        {
            if (_count <= 0)
                return new T[0];

            T[] instances = new T[_count];

            for (int i = 0; i < _count; i++)
                instances[i] = Pop();

            return instances;
        }

        /// <summary>
        /// Clears the pool and destroy objects depending on given boolean
        /// </summary>
        /// <param name="_destroy"></param>
        public void Clear(bool _destroy = true)
        {
            if (_destroy)
            {
                foreach (T item in pool)
                    Object.Destroy(item as Object);
            }

            pool.Clear();
        }
    } 
}