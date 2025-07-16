using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Inputs")]
public class InputReader : ScriptableObject, InputManager.IPlayActions
{

    public static InputManager inputActions;


    private void OnEnable()
    {
        if (inputActions == null) 
        {
            inputActions = new InputManager();

            inputActions.Play.SetCallbacks(this);

            inputActions.Play.Enable();
        }

    }


    public static event Action<Vector2> S_OnPusherMove;
    public static event Action S_OnPusherAttack;
    public static event Action S_OnPusherDash;
    
    public static event Action<Vector2> S_OnPullerMove;
    public static event Action S_OnPullerAttack;
    public static event Action S_OnPullerDash;



    public void OnPullerAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) 
        {
            S_OnPullerAttack?.Invoke();
        }
    }

    public void OnPullerDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            S_OnPullerDash?.Invoke();
        }
    }

    public void OnPullerMovement(InputAction.CallbackContext context)
    {
        S_OnPullerMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPusherAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            S_OnPusherAttack?.Invoke();
        }
    }

    public void OnPusherDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            S_OnPusherDash?.Invoke();
        }
    }

    public void OnPusherMovement(InputAction.CallbackContext context)
    {
        S_OnPusherMove?.Invoke(context.ReadValue<Vector2>());
    }
}
