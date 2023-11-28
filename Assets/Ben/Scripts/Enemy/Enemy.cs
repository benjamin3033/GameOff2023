using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    private float Health = 1;
    public int Damage = 1;
    public EnemySO enemySO;
    public Transform Visual;
    public TileOcclusion occlusion;

    public Action<EnemyAI> EnemyDied;

    [SerializeField] List<Texture2D> bloods = new();

    [SerializeField] MeshRenderer BloodPrefab;

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
        Destroy(Visual.GetComponentInChildren<Animator>());
        Visual.parent = null;

        MeshRenderer blood = Instantiate(BloodPrefab);
        blood.material.SetTexture("_Blood_Image", bloods[UnityEngine.Random.Range(0, bloods.Count)]);
        blood.transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);

        GameController.Instance.EnemyDied(transform.position);
        
        Destroy(gameObject);
    }
}
