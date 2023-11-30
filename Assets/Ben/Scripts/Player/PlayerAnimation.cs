using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static Animator animator;
    [SerializeField] Rigidbody rb;

    float magnitude;

    private void Start()
    {
        animator = GetComponent<Animator>();
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


    public void HoldGun()
    {
        animator.SetLayerWeight(2, 1);
    }

    public void ChangeLayer(int layerIndex, float weight)
    {
        animator.SetLayerWeight(layerIndex, weight);
    }

    public void TriggerAnimation(string name)
    {
        animator.SetTrigger(name);
    }

    public void SmashAttack()
    {
        PlayerMelee.Instance.SmashAttack();
    }

    public void StompAttack()
    {
        PlayerMelee.Instance.StompAttack();
    }

}
