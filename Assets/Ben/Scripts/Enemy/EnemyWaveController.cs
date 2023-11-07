using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] List<EnemySO> enemyTypes = new();

    [SerializeField] int AmountToSpawn = 20;
    [SerializeField] float radius = 5f;

    public void SpawnWave()
    {
        for (int i = 0; i < AmountToSpawn; i++)
        {
            float angle = i * 360f / AmountToSpawn;

            float x = 0 + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = 0 + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 spawnPosition = new Vector3(x, 1, z);

            if (IsRoomToSpawn(spawnPosition, 0.5f))
            {
                enemySpawner.SpawnNewEnemy(enemyTypes[0], spawnPosition);
            }
        }
    }

    private bool IsRoomToSpawn(Vector3 position, float radius)
    {
        if (Physics.CheckSphere(position, radius))
        {
            
            return false;
        }
        else
        {

            return true;
        }
    }

}
