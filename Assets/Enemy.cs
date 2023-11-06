using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 1;

    public Action<EnemyAI> EnemyDied;

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0) { KillEnemy(); }
    }

    private void KillEnemy()
    {
        EnemyDied?.Invoke(GetComponent<EnemyAI>());
        Destroy(gameObject);
    }
}
