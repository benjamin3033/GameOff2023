using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyAI EnemyPrefab;
    public List<EnemyAI> enemies = new();

    public void SpawnNewEnemy(EnemySO enemyType, Vector3 spawnPosition)
    {
        EnemyAI enemyInstance = Instantiate(EnemyPrefab);
        enemies.Add(enemyInstance);
        enemyInstance.transform.position = spawnPosition;
        enemyInstance.enemySO = enemyType;
        enemyInstance.GetComponent<Enemy>().EnemyDied += OnEnemyDeath;
    }

    private void OnEnemyDeath(EnemyAI aI)
    {
        aI.GetComponent<Enemy>().EnemyDied -= OnEnemyDeath;
        enemies.Remove(aI);
    }

    
}
