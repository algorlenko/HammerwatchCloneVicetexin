using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // The static property that other scripts access
    public static PoolManager Instance { get; private set; }
    public ObjectPool bulletPool;
    public ObjectPool textPool;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Delete duplicate GameManager objects
            return;
        }

        // Set the active instance
        Instance = this;

        // Optional: Keep this object alive when switching scenes
        DontDestroyOnLoad(gameObject);
    }




}
