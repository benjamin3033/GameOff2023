using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input Handler", menuName = "Input Handler")]
public class InputHandler : ScriptableObject, PlayerInput.IPlayerActions
{
    public Action<Vector2> moveEvent;

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
}
