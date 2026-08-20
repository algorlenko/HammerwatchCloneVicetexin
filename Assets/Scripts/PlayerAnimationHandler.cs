using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    private PlayerSquare charachterSquare;
    [SerializeField] AnimationClip shootingAnimation;
    private void Awake()
    {
        if(myAnimator == null)
        {
            myAnimator = GetComponent<Animator>();
        }
        charachterSquare = GetComponent<PlayerSquare>();
    }

    private void OnEnable()
    {
        charachterSquare.OnMovementChanged += ToggleMovementAnim;
        charachterSquare.OnShootingChanged += ToggleShootingAnim;
        charachterSquare.OnFireRateChanged += ChangeAnimationSpeed;
    }

    private void OnDisable()
    {
        charachterSquare.OnMovementChanged -= ToggleMovementAnim;
        charachterSquare.OnShootingChanged -= ToggleShootingAnim;
        charachterSquare.OnFireRateChanged -= ChangeAnimationSpeed;
    }

    private void ChangeAnimationSpeed(float newSpeed)
    {
        myAnimator.SetFloat("shootSpeedMultiplier", shootingAnimation.length / newSpeed);
    }

    private void ToggleMovementAnim(bool state)
    {
        myAnimator.SetBool("isMoving", state);
    }

    private void ToggleShootingAnim(Vector2 directionVector)
    {
        charachterSquare.mySprite.flipX = directionVector.x < 0;
        myAnimator.SetFloat("aimY", (directionVector.y >= 0 ? 1 : -1) * Vector2.Angle(new Vector2(directionVector.x, 0), directionVector));
        myAnimator.SetTrigger("isShooting");
    }

    private void CheckAndSetMovementDirection()
    {
        Vector2 directionVector = charachterSquare.myRigidBody.velocity;
        //Vector2 directionVector = charachterSquare.myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        charachterSquare.mySprite.flipX = directionVector.x < 0;
    }
}
