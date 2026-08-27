using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableObject : MonoBehaviour
{
    protected IObjectPool<PoolableObject> objectPool;
    public IObjectPool<PoolableObject> ObjectPool { set => objectPool = value; }
}
