using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Rigidbody rb;
    [SerializeField] float MovementSpeed;


    private Vector2 previousInput;

    private void OnEnable()
    {
        inputHandler.moveEvent += MovementInput;
    }

    private void OnDisable()
    {
        inputHandler.moveEvent -= MovementInput;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(previousInput == Vector2.zero || !GameController.Instance.CanPlayerMove) { return; }

        Vector3 movementVector = new Vector3(previousInput.x, 0, previousInput.y);

        rb.velocity = movementVector * MovementSpeed;
    }

    private void MovementInput(Vector2 vector)
    {
        previousInput = vector;
    }




}
