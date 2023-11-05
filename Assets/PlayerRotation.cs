using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] Transform PlayerVisual;
    [SerializeField] float rotationSpeed = 5f;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    void FixedUpdate()
    {
        
        Vector3 mousePosition = Input.mousePosition;

        
        Ray ray = cam.ScreenPointToRay(mousePosition);

        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            Vector3 cursorDirection = hit.point - transform.position;
            cursorDirection.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(cursorDirection);
            PlayerVisual.localRotation = Quaternion.Slerp(PlayerVisual.localRotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
