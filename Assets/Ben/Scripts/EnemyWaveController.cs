using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] List<EnemySO> enemyTypes = new();

    [SerializeField] int AmountToSpawn = 20;
    [SerializeField] float radius = 5f;

    private void Start()
    {
        for (int i = 0; i < AmountToSpawn; i++)
        {
            float angle = i * 360f / AmountToSpawn;

            float x = 0 + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = 0 + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 spawnPosition = new Vector3(x, 1, z);

            if(!IsObstacleInWay(spawnPosition))
            {
                enemySpawner.SpawnNewEnemy(enemyTypes[0], spawnPosition);
            }            
        }
    }

    bool IsObstacleInWay(Vector3 position)
    {
        // Check if there's an obstacle at the given position using raycasting.
        Ray ray = new Ray(position, Vector3.zero);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            return true;
        }

        return false;
    }
}
