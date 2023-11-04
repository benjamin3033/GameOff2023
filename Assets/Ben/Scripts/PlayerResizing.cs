using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResizing : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Vector3 SlimScale = Vector3.one;
    [SerializeField] Vector3 FatScale = new(2, 2, 2);

    [SerializeField] private bool ScaleIsSlim = true;

    private void OnEnable()
    {
        inputHandler.resize += ResizeInput;
    }

    private void OnDisable()
    {
        inputHandler.resize -= ResizeInput;
    }

    private void ResizeInput()
    {
#if UNITY_EDITOR
        ResizePlayer();
#endif
    }

    public void ResizePlayer()
    {
        ScaleIsSlim = !ScaleIsSlim;
        transform.localScale = ScaleIsSlim ? SlimScale : FatScale;
    }
}
