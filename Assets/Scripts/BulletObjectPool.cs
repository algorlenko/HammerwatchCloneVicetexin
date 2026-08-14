using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletObjectPool : MonoBehaviour
{
    public IObjectPool<BulletCircle> objectPool { get; private set; }
    // Throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] BulletCircle projectilePrefab;
    [SerializeField] int defaultCapacity = 20;
    [SerializeField] int maxSize = 100;

    private void Awake()
    {
        objectPool = new ObjectPool<BulletCircle>(CreateProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    public BulletCircle CreateProjectile()
    {
        BulletCircle projectileInstance = Instantiate(projectilePrefab, gameObject.transform);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }
    // Invoked when returning an item to the object pool
    private void OnReleaseToPool(BulletCircle pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // Invoked when retrieving the next item from the object pool
    private void OnGetFromPool(BulletCircle pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // Invoked when the maximum number of pooled items is exceeded (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(BulletCircle pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}
