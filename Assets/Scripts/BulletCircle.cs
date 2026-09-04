using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletCircle : PoolableObject
{
    [SerializeField] float BulletDurationSeconds = 2f;
    [SerializeField] private Rigidbody2D myBody;
    float damage;
    string bulletOwner; //Maybe change this to something better later, like marking the owning object or have like faction IDs.

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
    public void InitBullet(Vector2 moveVector, float initialDamage, Unit myBulletOwner)
    {
        bulletOwner = myBulletOwner.tag;
        damage = initialDamage;
        SetRbVelocity(moveVector);
    }
    void SetRbVelocity(Vector2 moveVector)
    {
        myBody.velocity = moveVector;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagedTarget))
        {
            if(!collision.gameObject.CompareTag(bulletOwner))
            {
                damagedTarget.TakeDamage(damage);
                objectPool.Release(this);
            }

        }
    }

}


