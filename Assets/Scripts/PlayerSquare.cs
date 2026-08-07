using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSquare : MonoBehaviour
{
   [SerializeField] GameObject bulletPrefab;
    public Camera myCamera;
    public Rigidbody2D myRigidBody;
    public float moveSpeed = 1f;
    public void fireBullets(InputAction.CallbackContext ctx)
    {
      BulletCircle currentBullet = Instantiate(bulletPrefab).GetComponent<BulletCircle>();
        //currentBullet.transform.position = new Vector2(Random.Range(0, 4), Random.Range(0, 4));
        currentBullet.transform.position = this.transform.position;
        currentBullet.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position, 2);
        //currentBullet.GetComponent<Rigidbody2D>().velocity = ctx.action.ReadValue<Vector2>();
    }

    public void move(InputAction.CallbackContext ctx)
    {
        Vector2 myMoveVector = ctx.ReadValue<Vector2>();
        // gameObject.transform.position += new Vector3(myMoveVector.x, myMoveVector.y, 0);
        myRigidBody.velocity = myMoveVector.normalized * moveSpeed;
    }
}
