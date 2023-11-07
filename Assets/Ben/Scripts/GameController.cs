using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int CookiesCollected = 0;

    [Header("Prefabs")]
    [SerializeField] GameObject Cookie;

    [Header("References")]
    [SerializeField] Transform Player;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] LevelTileController levelTileController;
    [SerializeField] EnemyWaveController enemyWaveController;
    [SerializeField] NavMeshSurface navMesh;

    private void OnEnable()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        levelTileController.GenerateGrid();
        navMesh.BuildNavMesh();
        enemyWaveController.SpawnWave();
        InvokeRepeating(nameof(UpdateEnemiesPlayerPosition), 0, 0.5f);
    }

    public void IncreaseCookies()
    {
        CookiesCollected++;
    }

    public void SpawnCookie(Vector3 position)
    {
        GameObject cookieInstance = Instantiate(Cookie);
        cookieInstance.transform.position = position;
    }

    private void UpdateEnemiesPlayerPosition()
    {
        for (int i = 0; i < enemySpawner.enemies.Count; i++)
        {
            enemySpawner.enemies[i].UpdateDestination(Player.position);
        }
    }
}
