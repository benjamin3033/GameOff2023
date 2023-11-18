using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input Handler", menuName = "Input Handler")]
public class InputHandler : ScriptableObject, PlayerInput.IPlayerActions
{
    public Action<Vector2> moveEvent;
    public Action resize;
    public Action shoot;

    private PlayerInput input;

    private void OnEnable()
    {
        if(input == null)
        {
            input = new PlayerInput();
            input.Player.SetCallbacks(this);
        }

        input.Player.Enable();
    }

    private void OnDisable()
    {
        if(input != null)
        {
            input.Player.Disable();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnResize(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            resize?.Invoke();
        }
        
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            shoot?.Invoke();
        }
    }
}
