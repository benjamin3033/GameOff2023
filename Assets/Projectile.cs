using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public void SendProjectileInDirection(Vector3 direction)
    {
        rb.AddForce(direction);
    }
}
