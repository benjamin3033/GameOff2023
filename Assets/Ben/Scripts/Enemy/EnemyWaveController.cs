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

    private void OnEnable()
    {
        LevelTimer.OnTimerTick += SpawnerCheck;
    }

    private void OnDisable()
    {
        LevelTimer.OnTimerTick -= SpawnerCheck;
    }

    private void SpawnerCheck(float timer)
    {
        for (int i = 0; i < waveOptions.Count; i++)
        {
            if (timer > waveOptions[i].time)
            {
                waveOptions[i].SpawnWave(enemySpawner);
                waveOptions.RemoveAt(i);
            }
        }
    }

    

}
