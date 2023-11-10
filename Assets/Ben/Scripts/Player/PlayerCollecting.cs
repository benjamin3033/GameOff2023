using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.GetCollected();
        }
    }
}
