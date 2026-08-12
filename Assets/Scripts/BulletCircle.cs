using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletCircle : MonoBehaviour
{
    private const float BulletDurationSeconds = 2f;
    [SerializeField] private Rigidbody2D myBody;

    void Awake()
    {
        StartCoroutine(BulletTimeOutAfterDelay(BulletDurationSeconds));
    }

    /// <summary>
    /// Waits for the given duration, then destroys this bullet's GameObject.
    /// </summary>
    private IEnumerator BulletTimeOutAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        Destroy(gameObject);
    }

    public void SetRbVelocity(Vector2 moveVector)
    {
        myBody.velocity = moveVector;
    }

}


