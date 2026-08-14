using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSquare : MonoBehaviour
{
    [SerializeField] BulletCircle bulletPrefab;
    [SerializeField] Camera myCamera;
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float bulletSpeed = 2f;
    private bool isShooting = false;
    [SerializeField] float _fireRate = 0.3f;
    float FireRate {
        get { return _fireRate; } 
        set 
        {
            _fireRate = value;
            OnFireRateChanged?.Invoke(_fireRate);
        } 
    }
    public Action<float> OnFireRateChanged;
    [SerializeField] float fireCoolDown;
    [SerializeField] BulletObjectPool playerBulletPool;
    [SerializeField] SpriteRenderer mySprite;
    
    public void FireBullet()
    {
        // BulletCircle currentBullet = Instantiate(bulletPrefab, transform.position, quaternion.identity);
        // above is the old non object pooled way of making a bullet
        OnShootingChanged?.Invoke();
        BulletCircle currentBullet = playerBulletPool.objectPool.Get();
        currentBullet.transform.position = transform.position;
        currentBullet.SetRbVelocity(Vector2.ClampMagnitude(myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position, bulletSpeed));
    }

    public void Start()
    {
        float tempFire = _fireRate;
        FireRate = 1;
        FireRate = tempFire;
    }
    public void Update()
    {
        fireCoolDown -= Time.deltaTime;
        if(isShooting && fireCoolDown <= 0)
        {
            FireBullet();
            fireCoolDown = _fireRate;
        }
    }

    public Action OnShootingChanged;
    public void StartShooting()
    {
        isShooting = true;
    }
    public void CancelShooting()
    {
        isShooting = false;
    }
    public Action<bool> OnMovementChanged;

    public void Move(Vector2 moveVector)
    {
        OnMovementChanged?.Invoke(!Mathf.Approximately(moveVector.magnitude, 0f));
        mySprite.flipX = moveVector.x < 0;
        //mySprite.flipY = moveVector.y < 0; // the HOMM3 spritesheet does not play nicely with flipping y
        myRigidBody.velocity = moveVector.normalized * moveSpeed;
    }
}
