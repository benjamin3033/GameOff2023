using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyAI EnemyPrefab;
    public List<EnemyAI> enemies = new();

    public void SpawnNewEnemy(EnemySO enemyType, Vector3 spawnPosition, WaveOption waveOption)
    {
        EnemyAI enemyInstance = Instantiate(EnemyPrefab);
        GameObject enemyVisual = Instantiate(enemyType.Visual, enemyInstance.transform);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();

        enemies.Add(enemyInstance);

        enemyInstance.transform.position = spawnPosition;
        enemyVisual.transform.SetLocalPositionAndRotation(new Vector3(0, enemyType.VisualHeight, 0), Quaternion.Euler(enemyType.VisualRotation));

        enemyInstance.enemySO = enemyType;
        enemy.enemySO = enemyType;
        enemy.EnemyDied += OnEnemyDeath;
        enemy.UpdateStats(waveOption);
        
        enemy.Visual = enemyVisual.transform;


        enemy.GetComponent<SkinnedOcclusion>().renderers.Add(enemyVisual.GetComponentInChildren<SkinnedMeshRenderer>());
        enemy.GetComponent<SkinnedOcclusion>().ShowRenderers(false);
    }

    private void OnEnemyDeath(EnemyAI aI)
    {
        aI.GetComponent<Enemy>().EnemyDied -= OnEnemyDeath;
        enemies.Remove(aI);
    }

    
}
