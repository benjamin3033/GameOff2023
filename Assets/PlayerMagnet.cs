using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    public float CollectionSpeed = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.MoveTowardsPlayer(transform, CollectionSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.StopMovingTowardsPlayer();
        }
    }
}
