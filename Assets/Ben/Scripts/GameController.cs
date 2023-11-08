using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int CookiesCollected = 0;
    public float CurrentMilk = 0;

    [Header("Prefabs")]
    [SerializeField] GameObject Cookie;
    [SerializeField] Milk Milk;

    [Header("References")]
    [SerializeField] Transform Player;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] LevelTileController levelTileController;
    [SerializeField] EnemyWaveController enemyWaveController;
    [SerializeField] NavMeshSurface navMesh;
    [SerializeField] Slider MilkMeterSlider;

    [Header("Stats")]
    [SerializeField] int MilkChance = 10;
    [SerializeField] float MilkValueMax = 10;
    [SerializeField] float MilkValueMin = 1;
    [SerializeField] float MaxMilk = 100;

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

        MilkMeterSlider.maxValue = MaxMilk;
    }

    public void IncreaseCookies(int value)
    {
        CookiesCollected += value;
    }

    public void IncreaseMilk(float value)
    {
        CurrentMilk += value;
        MilkMeterSlider.value = CurrentMilk;
    }

    public void SpawnCookie(Vector3 position)
    {
        GameObject cookieInstance = Instantiate(Cookie);
        cookieInstance.transform.position = position;
    }

    public void SpawnMilk(Vector3 position)
    {
        int randomNumber = Random.Range(1, MilkChance);
        float randomValue = Random.Range(MilkValueMin, MilkValueMax);

        if(randomNumber == 1)
        {
            Milk milkInstance = Instantiate(Milk);
            milkInstance.transform.position = position;
            milkInstance.MilkValue = randomValue;
        }
    }

    private void UpdateEnemiesPlayerPosition()
    {
        for (int i = 0; i < enemySpawner.enemies.Count; i++)
        {
            enemySpawner.enemies[i].UpdateDestination(Player.position);
        }
    }
}
