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

    private void ToggleShootingAnim()
    {
        myAnimator.SetTrigger("isShooting");
    }
}
