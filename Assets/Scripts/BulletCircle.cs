using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletCircle : MonoBehaviour
{
    [SerializeField] float BulletDurationSeconds = 2f;
    [SerializeField] private Rigidbody2D myBody;
    private IObjectPool<BulletCircle> objectPool;
    public IObjectPool<BulletCircle> ObjectPool { set => objectPool = value; }

    void OnEnable()
    {
        StartCoroutine(BulletTimeOutAfterDelay(BulletDurationSeconds));
    }

    /// <summary>
    /// Waits for the given duration, then destroys this bullet's GameObject.
    /// </summary>
    private IEnumerator BulletTimeOutAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        myBody.velocity = new Vector2(0f, 0f);
        myBody.angularVelocity = 0;
        objectPool.Release(this);
    }

    public void SetRbVelocity(Vector2 moveVector)
    {
        myBody.velocity = moveVector;
    }

}


