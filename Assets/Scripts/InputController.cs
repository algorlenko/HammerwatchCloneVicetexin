using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] InputActionReference fireAction;
    [SerializeField] InputActionReference moveAction;
    public PlayerSquare myCharachter;
   void OnEnable()
    {

        fireAction.action.Enable();
        fireAction.action.performed += HandleFireInput;
        moveAction.action.Enable();
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;
    }

   void OnDisable()
    {
        fireAction.action.Disable();
        fireAction.action.performed -= HandleFireInput;
        moveAction.action.Disable();
        moveAction.action.performed -= HandleMoveInput;
    }


   void HandleFireInput(InputAction.CallbackContext ctx)
    {
        myCharachter.fireBullets(ctx); 
    }
    void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        myCharachter.move(ctx);
    }
}
