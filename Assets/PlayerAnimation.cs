using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;

    float magnitude;

    private void Start()
    {
        StartCoroutine(RandomIdleAnimation());
    }

    private void Update()
    {
        Vector3 normalizedVelocity = rb.velocity.normalized;
        magnitude = normalizedVelocity.magnitude;

        animator.SetFloat("RunSpeed", magnitude);
    }

    IEnumerator RandomIdleAnimation()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);

            if(magnitude <= 0)
            {
                int randomNumber = Random.Range(0, 3);

                if (randomNumber == 2)
                {
                    animator.SetTrigger("Idle1");
                }
                else if (randomNumber == 1)
                {
                    animator.SetTrigger("Idle2");
                }
            }
        }
    }
}