using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    Camera cam;
    public float rotationSpeed = 5.0f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        // Get the cursor position in screen coordinates
        Vector3 cursorPosition = Input.mousePosition;

        // Convert the screen position to a ray in the world
        Ray ray = cam.ScreenPointToRay(cursorPosition);

        // Create a plane at the object's position with a normal pointing out
        Plane plane = new Plane(Vector3.up, transform.position);

        // Calculate the intersection point between the ray and the plane
        if (plane.Raycast(ray, out float distance))
        {
            // Get the point of intersection
            Vector3 targetPoint = ray.GetPoint(distance);

            // Calculate the direction to the target point
            Vector3 direction = targetPoint - transform.position;

            // Rotate the object to face the target point
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
