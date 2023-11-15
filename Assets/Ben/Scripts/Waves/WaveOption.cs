using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveOption
{
    public float time;
    public int amount = 1;
    public float radius = 30f;
    public Vector2 HealthRange = new Vector2(1,1);
    public Vector2 DamageRange = new Vector2(1,1);
    public EnemySO enemySO;

    public WaveOptions Option;

    public enum WaveOptions
    {
        Circle,
        Cluster
    }

    /*
    public WaveOption(WaveOptions Option, float time, int amount, float radius, Vector2 HealthRange, Vector2 DamageRange, EnemySO enemySO)
    {
        this.Option = Option;
        this.time = time;
        this.amount = amount;
        this.radius = radius;
        this.HealthRange = HealthRange;
        this.DamageRange = DamageRange;
        this.enemySO = enemySO;
    }*/

    public void SpawnWave(EnemySpawner enemySpawner)
    {
        switch(Option)
        {
            case WaveOptions.Circle:
                CircleWave(enemySpawner);
                break;

            case WaveOptions.Cluster:
                ClusterWave(enemySpawner);
                break;
        }
    }

    private void ClusterWave(EnemySpawner enemySpawner)
    {
        float angle = Random.Range(0f, 360f);

        float x = GameController.Instance.Player.transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float z = GameController.Instance.Player.transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

        Vector3 spawnPosition = new Vector3(x, 1, z);

        for (int i = 0; i < amount; i++)
        {
            enemySpawner.SpawnNewEnemy(enemySO, spawnPosition, this);
        }
    }

    private void CircleWave(EnemySpawner enemySpawner)
    {
        Vector3 playerPosition = GameController.Instance.Player.transform.position;

        for (int i = 0; i < amount; i++)
        {
            float angle = i * 360f / amount;

            float x = playerPosition.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = playerPosition.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 spawnPosition = new Vector3(x, 1, z);

            // Try to find a valid spawn position
            while (!EnemyWaveController.IsRoomToSpawn(spawnPosition, 0.5f))
            {
                // Adjust the spawn position if there is no room
                float newAngle = Random.Range(0f, 360f);
                float newX = playerPosition.x + radius * Mathf.Cos(Mathf.Deg2Rad * newAngle);
                float newZ = playerPosition.z + radius * Mathf.Sin(Mathf.Deg2Rad * newAngle);

                spawnPosition = new Vector3(newX, 1, newZ);
            }

            // Spawn the enemy at the final valid position
            enemySpawner.SpawnNewEnemy(enemySO, spawnPosition, this);
        }
    }
}
