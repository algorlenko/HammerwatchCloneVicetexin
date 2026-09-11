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
        CheckAndSetFacingDirection(Vector2.zero, false);
        myAnimator.SetBool("isMoving", state);
    }

    private void ToggleShootingAnim(Vector2 directionVector, bool state)
    {
        myAnimator.SetBool("isShooting", state);
        CheckAndSetFacingDirection(directionVector, state);
    }

    private void CheckAndSetFacingDirection(Vector2 directionVector, bool isAiming)
    {
        if (animatedUnit.isShooting && !isAiming) { return; }
        directionVector = animatedUnit.isShooting ? directionVector : animatedUnit.rbVelocity;
        if(animatedUnit.isShooting)
        {
            myAnimator.SetFloat("aimY", (directionVector.y >= 0 ? 1 : -1) * Vector2.Angle(new Vector2(directionVector.x, 0), directionVector));
        }
        myAnimator.SetFloat("aimX", directionVector.x < 0 ? -1 : 1);
        
        //Vector2 directionVector = charachterSquare.myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;

    }
}
