using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle : MonoBehaviour
{
    private const float BulletDurationSeconds = 2f;

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
        Destroy(this.gameObject);
    }
}


