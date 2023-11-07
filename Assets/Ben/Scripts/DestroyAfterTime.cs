using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float time = 5f;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
