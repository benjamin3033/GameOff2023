using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


public class EnemyWaveController : MonoBehaviour
{
    private List<WaveOption> waveOptions = new List<WaveOption>();
    [SerializeField] EnemySpawner enemySpawner;
    private LevelSO levelSO;

    public void StartWaves()
    {
        levelSO = GameController.Instance.levelSO;
        waveOptions.AddRange(levelSO.waveOptions);
    }

    private void Update()
    {
        SpawnerCheck();
    }

    private void SpawnerCheck()
    {
        for (int i = 0; i < waveOptions.Count; i++)
        {
            if (LevelTimer.Instance.GetCurrentTime() > waveOptions[i].time)
            {
                waveOptions[i].SpawnWave(enemySpawner);
                waveOptions.RemoveAt(i);
            }
        }
    }

    public static bool IsRoomToSpawn(Vector3 position, float radius)
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
