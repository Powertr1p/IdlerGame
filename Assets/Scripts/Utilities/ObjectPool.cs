using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly T _prefab;
        private readonly Transform _parent;

        public ObjectPool(int initialSize, T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                var instance = Object.Instantiate(_prefab, _parent);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var obj = Object.Instantiate(_prefab, _parent);
                return obj;
            }
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}