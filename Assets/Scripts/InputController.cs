using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] InputActionReference fireAction;
    [SerializeField] InputActionReference moveAction;
    [SerializeField] PlayerSquare myCharachter;
   void OnEnable()
    {

        fireAction.action.Enable();
        fireAction.action.performed += OnFireInputPressed;
        fireAction.action.canceled += OnFireInputReleased;

        moveAction.action.Enable();
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;
    }

   void OnDisable()
    {
        fireAction.action.Disable();
        fireAction.action.performed -= OnFireInputPressed;
        fireAction.action.canceled -= OnFireInputReleased;
        moveAction.action.Disable();
        moveAction.action.performed -= HandleMoveInput;
        moveAction.action.canceled -= HandleMoveInput;
    }


   void OnFireInputPressed(InputAction.CallbackContext ctx)
    {
        myCharachter.StartShooting(); 
    }
    void OnFireInputReleased(InputAction.CallbackContext ctx)
    {
        myCharachter.CancelShooting();
    }

    void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        Vector2 myMoveVector = ctx.ReadValue<Vector2>();
        myCharachter.Move(myMoveVector);
    }
}
