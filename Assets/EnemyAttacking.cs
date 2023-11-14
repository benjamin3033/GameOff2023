using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    bool CanDamage = true;
    [SerializeField] Enemy enemy;

    private void OnCollisionEnter(Collision collision)
    {
        if(!CanDamage) { return; }

        if(collision.collider.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            StartCoroutine(DamageCooldown());
            playerHealth.TakeDamage(enemy.Damage);
        }
    }

    IEnumerator DamageCooldown()
    {
        CanDamage = false;
        yield return new WaitForSeconds(1);
        CanDamage = true;
    }
}
