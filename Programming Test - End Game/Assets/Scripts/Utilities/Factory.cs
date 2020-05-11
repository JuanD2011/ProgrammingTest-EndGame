using UnityEngine;

namespace Utilities.Factory
{
    public static class Factory<T> where T : Object
    {
        public static T Create(T _prefab)
        {
            T newInstance = Object.Instantiate(_prefab);
            return newInstance;
        }

        public static T Create(T _prefab, Transform _parent)
        {
            T newInstance = Object.Instantiate(_prefab, _parent);
            return newInstance;
        }

        public static T Create(T _prefab, Vector3 _position, Quaternion _rotation)
        {
            T newInstance = Object.Instantiate(_prefab, _position, _rotation);
            return newInstance;
        }

        public static T Create(T _prefab, Vector3 _position, Quaternion _rotation, Transform _parent)
        {
            T newInstance = Object.Instantiate(_prefab, _position, _rotation, _parent);
            return newInstance;
        }
    } 
}
