using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite pressedSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mySpriteRenderer.sprite = pressedSprite;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mySpriteRenderer.sprite = defaultSprite;
        }
    }
}
