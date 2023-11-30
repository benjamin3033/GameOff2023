using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float Health = 1;
    public int Damage = 1;
    public EnemySO enemySO;
    public Transform Visual;
    public TileOcclusion occlusion;
    private EnemyAI enemyAI;

    public Action<EnemyAI> EnemyDied;

    public List<Texture2D> bloods = new();

    public MeshRenderer BloodPrefab;

    public Vector3 LastHitPosition;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

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

        //Destroy(Visual.gameObject);

        enemyAI.ExplodeAwayFromPlayer(LastHitPosition, 5, 1, 1, true);

        
    }
}
