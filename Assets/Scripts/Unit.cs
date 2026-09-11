using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour, IDamagable
{

    [SerializeField] float maxHp = 20;
    float currentHp;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] HealthBar healthBar;

    [SerializeField] BulletCircle bulletPrefab;
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float bulletSpeed = 2f;
    public bool isShooting = false;
    [SerializeField] float _fireRate = 0.3f;
    [SerializeField] float bulletOffsetMagnitude = 1;
    [SerializeField] float bulletPower = 2;
    public Vector2 rbVelocity { get => myRigidBody.velocity; }
    float FireRate
    {
        get { return _fireRate; }
        set
        {
            _fireRate = value;
            OnFireRateChanged?.Invoke(_fireRate);
        }
    }
    public Action<float> OnFireRateChanged;
    [SerializeField] float fireCoolDown;
    [SerializeField] ObjectPool playerBulletPool;
    [SerializeField] public SpriteRenderer mySprite;
    public void Start()
    {
        currentHp = maxHp;
        float tempFire = _fireRate;
        FireRate = 1;
        FireRate = tempFire;
    }

    public virtual void Update()
    {
        fireCoolDown -= Time.deltaTime;
        if (isShooting && fireCoolDown <= 0)
        {
            FireBullet(getAimingVector());
            fireCoolDown = _fireRate;
        }
    }
   public virtual Vector2 getAimingVector()
    {
        return Vector2.zero;
    }

    public void FireBullet(Vector2 aimingVector)
    {
        // BulletCircle currentBullet = Instantiate(bulletPrefab, transform.position, quaternion.identity);
        // above is the old non object pooled way of making a bullet
        OnShootingChanged?.Invoke(aimingVector, true);
        BulletCircle currentBullet = (BulletCircle)playerBulletPool.objectPool.Get();
        Vector2 bulletOffset = aimingVector * bulletOffsetMagnitude;
        currentBullet.transform.position = transform.position + new Vector3(bulletOffset.x, bulletOffset.y, 0);
        currentBullet.InitBullet(aimingVector * bulletSpeed, bulletPower, this);
    }

    public Action<Vector2, bool> OnShootingChanged;
    public void StartShooting()
    {
        isShooting = true;
    }
    public void CancelShooting()
    {
        isShooting = false;
        OnShootingChanged?.Invoke(Vector2.zero, false);
    }

    public Action<bool> OnMovementChanged;

    public void Move(Vector2 moveVector)
    {
        myRigidBody.velocity = moveVector.normalized * moveSpeed;
        OnMovementChanged?.Invoke(!Mathf.Approximately(moveVector.magnitude, 0f));
        // if (!isShooting) mySprite.flipX = moveVector.x < 0; 
        //mySprite.flipY = moveVector.y < 0; // the HOMM3 spritesheet does not play nicely with flipping y

    }

    public void TakeDamage(float damage)
    {
        FloatingNumber damageText = (FloatingNumber)PoolManager.Instance.textPool.objectPool.Get();
        damageText.initText(damage.ToString(), transform.position);
        StartCoroutine(FlashRed(.2f));
        currentHp -= damage;
        healthBar.AdjustHealthBar(currentHp, maxHp);
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
