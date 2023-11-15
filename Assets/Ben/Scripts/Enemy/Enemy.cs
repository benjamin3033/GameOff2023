using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float Health = 1;
    public int Damage = 1;
    public EnemySO enemySO;
    public Transform Visual;
    public TileOcclusion occlusion;

    public Action<EnemyAI> EnemyDied;

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0) { KillEnemy(); }
    }

    public void UpdateStats(WaveOption stats)
    {
        Health = UnityEngine.Random.Range(stats.HealthRange.x, stats.HealthRange.y);
        Damage = UnityEngine.Random.Range((int)stats.DamageRange.x, (int)stats.DamageRange.y);
    }

    private void KillEnemy()
    {
        EnemyDied?.Invoke(GetComponent<EnemyAI>());
        Visual.localRotation = Quaternion.Euler(0, -90, 90);
        Visual.parent = null;

        GameController.Instance.SpawnCookie(transform.position);
        GameController.Instance.SpawnMilk(transform.position);

        Destroy(gameObject);
    }
}
