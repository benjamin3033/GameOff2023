using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public LevelSO levelSO;

    [Header("Current Values")]
    public int CookiesCollected = 0;
    public float CurrentMilk = 0;
    public int CurrentHealth = 100;

    [Header("Player Stats")]
    public int MaxHealth = 100;

    [Header("Prefabs")]
    [SerializeField] GameObject Cookie;
    [SerializeField] Milk Milk;

    [Header("References")]
    public Transform Player;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] LevelTileController levelTileController;
    [SerializeField] EnemyWaveController enemyWaveController;
    [SerializeField] NavMeshSurface navMesh;
    [SerializeField] Slider MilkMeterSlider;
    [SerializeField] TMP_Text CookiesText;
    [SerializeField] GameObject TopDownCamera;
    [SerializeField] GameObject PlayMenuCamera;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject Overlay;
    [SerializeField] Image FadeToWhite;

    [Header("Start Level Refs")]
    [SerializeField] GameObject SpawnArea;

    [Header("Milk")]
    [SerializeField] int MilkChance = 10;
    [SerializeField] float MilkValueMax = 10;
    [SerializeField] float MilkValueMin = 1;
    [SerializeField] float MaxMilk = 100;

    public bool CanPlayerMove = false;
    public bool GamePaused = false;

    public Action<int> PlayerHealthChanged;

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

    public void ChangeHealth(int amount)
    {
        //Debug.Log("Amount: " + " | " + "Changed: " + (CurrentHealth + amount)) ;

        CurrentHealth += amount;

        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if(CurrentHealth <= 0)
        {
            PlayerDied();
        }

        PlayerHealthChanged?.Invoke(CurrentHealth);
    }

    private void PlayerDied()
    {
        GameOverMenu.SetActive(true);
        CanPlayerMove = false;
        GamePaused = true;
        Overlay.SetActive(false);
    }

    public IEnumerator StartLevel()
    {
        FadeToWhite.DOFade(1, 0.5f).OnComplete(() => {
            SpawnArea.SetActive(false);
            SetupLevel();
        });

        yield return new WaitForSeconds(1f);

        FadeToWhite.DOFade(0, 0.5f);

        Player.GetComponent<Rigidbody>().isKinematic = false;
        CanPlayerMove = true;

        ChangeCamera();
        StartEnemies();
        Overlay.SetActive(true);
    }

    private void ChangeCamera()
    {
        if(PlayMenuCamera.activeSelf)
        {
            PlayMenuCamera.SetActive(false);
            TopDownCamera.SetActive(true);
        }
        else
        {
            PlayMenuCamera.SetActive(true);
            TopDownCamera.SetActive(false);
        }
    }

    public void ChooseWeapon(WeaponSO chosenWeapon)
    {
        Player.GetComponent<PlayerShooting>().currentWeapon = chosenWeapon;
        
    }

    public void ChooseLevel(LevelSO level)
    {
        levelSO = level;
        StartCoroutine(StartLevel());
    }

    private void SetupLevel()
    {
        levelTileController.GenerateGrid();
        navMesh.BuildNavMesh();
    }

    private void StartEnemies()
    {
        enemyWaveController.StartWaves();
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

    private void SpawnCookie(Vector3 position)
    {
        GameObject cookieInstance = Instantiate(Cookie);
        cookieInstance.transform.position = position;
    }

    public void EnemyDied(Vector3 position)
    {
        int randomNumber = UnityEngine.Random.Range(1, MilkChance);
        float randomValue = UnityEngine.Random.Range(MilkValueMin, MilkValueMax);

        if (randomNumber == 1)
        {
            Milk milkInstance = Instantiate(Milk);
            milkInstance.transform.position = position;
            milkInstance.MilkValue = randomValue;
        }
        else
        {
            SpawnCookie(position);
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
