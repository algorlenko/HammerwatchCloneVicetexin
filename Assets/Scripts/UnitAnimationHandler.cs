using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    private Unit animatedUnit;
    [SerializeField] AnimationClip shootingAnimation;
    private void Awake()
    {
        if(myAnimator == null)
        {
            myAnimator = GetComponent<Animator>();
        }
        animatedUnit = GetComponent<Unit>();
    }

    private void OnEnable()
    {
        animatedUnit.OnMovementChanged += ToggleMovementAnim;
        animatedUnit.OnShootingChanged += ToggleShootingAnim;
        animatedUnit.OnFireRateChanged += ChangeAnimationSpeed;
    }

    private void OnDisable()
    {
        animatedUnit.OnMovementChanged -= ToggleMovementAnim;
        animatedUnit.OnShootingChanged -= ToggleShootingAnim;
        animatedUnit.OnFireRateChanged -= ChangeAnimationSpeed;
    }

    private void ChangeAnimationSpeed(float newSpeed)
    {
        myAnimator.SetFloat("shootSpeedMultiplier", shootingAnimation.length / newSpeed);
    }

    private void ToggleMovementAnim(bool state)
    {
        CheckAndSetMovementDirection();
        myAnimator.SetBool("isMoving", state);
    }

    private void ToggleShootingAnim(Vector2 directionVector)
    {
        animatedUnit.mySprite.flipX = directionVector.x < 0;
        myAnimator.SetFloat("aimY", (directionVector.y >= 0 ? 1 : -1) * Vector2.Angle(new Vector2(directionVector.x, 0), directionVector));
        myAnimator.SetTrigger("isShooting");
    }

    private void CheckAndSetMovementDirection()
    {
        Vector2 directionVector = animatedUnit.rbVelocity;
        //Vector2 directionVector = charachterSquare.myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        animatedUnit.mySprite.flipX = directionVector.x < 0;
    }
}
