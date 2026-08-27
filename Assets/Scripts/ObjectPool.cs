using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public IObjectPool<PoolableObject> objectPool { get; private set; }
    // Throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] PoolableObject objectPrefab;
    [SerializeField] int defaultCapacity = 20;
    [SerializeField] int maxSize = 100;

    private void Awake()
    {
        objectPool = new ObjectPool<PoolableObject>(CreateObject,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    public PoolableObject CreateObject()
    {
        PoolableObject objectInstance = Instantiate(objectPrefab, gameObject.transform);
        objectInstance.ObjectPool = objectPool;
        return objectInstance;
    }
    // Invoked when returning an item to the object pool
    private void OnReleaseToPool(PoolableObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // Invoked when retrieving the next item from the object pool
    private void OnGetFromPool(PoolableObject pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // Invoked when the maximum number of pooled items is exceeded (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(PoolableObject pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}
