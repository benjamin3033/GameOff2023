using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] Transform Player;
    [SerializeField] EnemySpawner enemySpawner;

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
        InvokeRepeating(nameof(UpdateEnemiesPlayerPosition), 0, 0.5f);
    }

    private void UpdateEnemiesPlayerPosition()
    {
        for (int i = 0; i < enemySpawner.enemies.Count; i++)
        {
            enemySpawner.enemies[i].UpdateDestination(Player.position);
        }
    }
}
