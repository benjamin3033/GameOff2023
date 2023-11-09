using System.Collections;
using DG.Tweening;
using TMPro;
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
    [SerializeField] TMP_Text CookiesText;
    [SerializeField] GameObject TopDownCamera;
    [SerializeField] GameObject MenuCamera;

    [Header("Start Level Refs")]
    [SerializeField] GameObject Wall;
    [SerializeField] GameObject Floor;
    [SerializeField] GameObject SmokeSphere;

    [Header("Stats")]
    [SerializeField] int MilkChance = 10;
    [SerializeField] float MilkValueMax = 10;
    [SerializeField] float MilkValueMin = 1;
    [SerializeField] float MaxMilk = 100;

    public bool CanPlayerMove = false;

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
        MilkMeterSlider.maxValue = MaxMilk;
    }

    public IEnumerator StartLevel()
    {
        Wall.transform.DOMoveY(Wall.transform.position.y + 10, 1)
            .OnComplete(() => 
            { 
                SetupLevel(); 
                
            });

        yield return new WaitForSeconds(2);
        Player.GetComponent<Rigidbody>().isKinematic = false;
        Wall.SetActive(false);
        SmokeSphere.SetActive(false);
        Floor.SetActive(false);
        ChangeCamera();
        StartEnemies();
        CanPlayerMove = true;
    }

    private void ChangeCamera()
    {
        if(MenuCamera.activeSelf)
        {
            MenuCamera.SetActive(false);
            TopDownCamera.SetActive(true);
        }
        else
        {
            MenuCamera.SetActive(true);
            TopDownCamera.SetActive(false);
        }
    }

    private void SetupLevel()
    {
        levelTileController.GenerateGrid();
        navMesh.BuildNavMesh();
    }

    private void StartEnemies()
    {
        enemyWaveController.SpawnWave();
        InvokeRepeating(nameof(UpdateEnemiesPlayerPosition), 0, 0.5f);
    }

    public void IncreaseCookies(int value)
    {
        CookiesCollected += value;
        CookiesText.text = CookiesCollected.ToString();
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
            if (enemySpawner.enemies[i].enabled == false) { return; }
            enemySpawner.enemies[i].UpdateDestination(Player.position);
        }
    }
}
