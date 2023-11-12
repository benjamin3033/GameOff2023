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
        Circle
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
        }
    }

    private void CircleWave(EnemySpawner enemySpawner)
    {
        for (int i = 0; i < amount; i++)
        {
            float angle = i * 360f / amount;

            float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 spawnPosition = new Vector3(x, 1, z);

            if (EnemyWaveController.IsRoomToSpawn(spawnPosition, 0.5f))
            {
                enemySpawner.SpawnNewEnemy(enemySO, spawnPosition);
            }
        }
    }
}
