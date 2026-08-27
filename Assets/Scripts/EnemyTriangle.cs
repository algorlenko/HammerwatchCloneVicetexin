using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTriangle : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHp = 20;
    float currentHp;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    public void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(FlashRed(.2f));
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashRed(float delaySeconds)
    {
        mySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(delaySeconds);
        mySpriteRenderer.color = Color.white;
    }
}
