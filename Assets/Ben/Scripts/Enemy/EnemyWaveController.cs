using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class EnemyWaveController : MonoBehaviour
{
    public List<WaveOption> waveOptions = new List<WaveOption>();
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] float timer = 0;
    bool levelStarted = false;

    public void StartWaves()
    {
        levelStarted = true;
    }

    private void Update()
    {
        if (!levelStarted || GameController.Instance.GamePaused) { return; }

        WaveTimer();
        SpawnerCheck();
    }

    private void SpawnerCheck()
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

    private void WaveTimer()
    {
        timer += Time.deltaTime;
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
