using System.Collections.Generic;
using UnityEngine;

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
            var instance = CreateObject();
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
            var obj = CreateObject();
            return obj;
        }
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    private T CreateObject()
    {
        return Object.Instantiate(_prefab, _parent);
    }
}