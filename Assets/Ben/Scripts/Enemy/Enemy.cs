using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float Health = 1;
    [SerializeField] EnemySO enemySO;
    [SerializeField] Transform Visual;
    public Action<EnemyAI> EnemyDied;

    private void Start()
    {
        Health = enemySO.Health;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0) { KillEnemy(); }
    }

    private void KillEnemy()
    {
        EnemyDied?.Invoke(GetComponent<EnemyAI>());
        Visual.localRotation = Quaternion.Euler(0, -90, 90);
        Visual.parent = null;

        GameController.Instance.SpawnCookie(transform.position);

        Destroy(gameObject);
    }
}
