using System.Collections;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] bool CanDamage = true;
    [SerializeField] bool NearPlayer = false;
    [SerializeField] Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(CanDamage)
        
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
        CanDamage = false;
        playerHealth.TakeDamage(enemy.Damage);
        yield return new WaitForSeconds(1);
        CanDamage = true;

        if(NearPlayer)
        {
            StartCoroutine(DamageCooldown(playerHealth));
        }
    }
}
