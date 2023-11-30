using System.Collections;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] bool NearPlayer = false;
    [SerializeField] Enemy enemy;

    public Animator animator;

    private void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            NearPlayer = true;

            StartCoroutine(DamageCooldown(playerHealth));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            NearPlayer = false;
            StopAllCoroutines();
        }
    }


    IEnumerator DamageCooldown(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(enemy.Damage);
        PlayAnimation();
        yield return new WaitForSeconds(1);

        if(NearPlayer)
        {
            StartCoroutine(DamageCooldown(playerHealth));
        }
    }

    private void PlayAnimation()
    {
        animator.SetTrigger(enemy.enemySO.AnimationTriggers[Random.Range(0, enemy.enemySO.AnimationTriggers.Count)]);
    }
}
