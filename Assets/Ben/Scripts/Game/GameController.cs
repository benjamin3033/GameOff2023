using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public LevelSO levelSO;

    [Header("Current Values")]
    public int CookiesCollected = 0;
    public float CurrentMilk = 0;
    public int CurrentHealth = 100;

    [Header("Player Stats")]
    public SaveGameSO saveGameSO;
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

    [SerializeField] GameObject Overlay;
    [SerializeField] Image FadeToWhite;

    [Header("Game Over")]
    [SerializeField] LevelTimer timer;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] TMP_Text GameOverTitle;
    [SerializeField] TMP_Text CookiesCollectedText;
    [SerializeField] TMP_Text TimeSurvivedText;

    [Header("Start Level Refs")]
    [SerializeField] GameObject SpawnArea;
    [SerializeField] Texture2D cursor;
    [SerializeField] GameObject DirectionalLight;

    [Header("Milk")]
    [SerializeField] int MilkChance = 10;
    [SerializeField] float MilkValueMax = 10;
    [SerializeField] float MilkValueMin = 1;
    [SerializeField] float MaxMilk = 100;
    [SerializeField] GameObject MilkText;

    [HideInInspector] public bool CanPlayerMove = false;
    [HideInInspector] public bool LevelStarted = false;
    [HideInInspector] public bool GamePaused = false;

    public Action<int> PlayerHealthChanged;

    private float length;
    public bool CanTakeDamage = true;

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

        LevelTimer.OnTimerTick += OnTimerTick;
    }

    private void OnDisable()
    {
        LevelTimer.OnTimerTick -= OnTimerTick;
    }

    private void OnTimerTick(float time)
    {
        if(time >= levelSO.length)
        {
            GameOverTitle.text = "You survived!";
            PlayerDied();
        }
    }

    private void Start()
    {
        MilkMeterSlider.maxValue = MaxMilk;
        length = levelSO.length;


        
        saveGameSO.Load();

        // Load Player Stats
        LoadPlayerStats();
    }

    public void LoadPlayerStats()
    {
        if (saveGameSO.MaxHealthPurchased != -1)
        {
            

            MaxHealth = ShopController.Instance.MaxHealthValues[saveGameSO.MaxHealthPurchased];
        }
    }

    public void ChangeHealth(int amount)
    {
        if (CanTakeDamage)
        {
            CurrentHealth += amount;

        }        

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

        CookiesCollectedText.text = "You collected " + CookiesCollected.ToString() + " cookies!";

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
        float timeSurvived = timer.GetCurrentTime();
        TimeSpan time = TimeSpan.FromSeconds(timeSurvived);

        if (timeSurvived < 60)
        {
            TimeSurvivedText.text = "You survived for " + ((int)timeSurvived).ToString() + " seconds";
        }
        else if (timeSurvived < 600)
        {
            TimeSurvivedText.text = "You survived for " + time.ToString("m' minutes and 'ss' seconds'");
        }
        else
        {
            TimeSurvivedText.text = "You survived for " + time.ToString("m' minutes and 'ss' seconds'");
        }
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
        LevelStarted = true;
        ChangeCamera();
        StartEnemies();
        Overlay.SetActive(true);
        PlayerAnimation.animator.SetLayerWeight(1, 1);
        Cursor.SetCursor(cursor, new Vector2(50, 50), CursorMode.Auto);
        DirectionalLight.SetActive(true);
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
        Player.GetComponent<PlayerShooting>().ChangeWeapon(chosenWeapon);
        
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

    public void ResetMilk()
    {
        float valueStart = CurrentMilk;

        DOTween.To(() => valueStart, x => valueStart = x, 0, 0.5f)
            .OnUpdate(() => { MilkMeterSlider.value = valueStart; })
            .OnComplete(() => { MilkMeterSlider.value = 0; CurrentMilk = 0; });

        MilkText.SetActive(false);

    }

    public void IncreaseMilk(float value)
    {
        float valueStart = CurrentMilk;

        CurrentMilk += value;

        DOTween.To(() => valueStart, x => valueStart = x, valueStart + value, 0.5f)
            .OnUpdate(() => { MilkMeterSlider.value = valueStart; })
            .OnComplete(() => { MilkMeterSlider.value = CurrentMilk; });

        if(CurrentMilk == MaxMilk)
        {
            MilkText.SetActive(true);
        }
    }

    private void SpawnCookie(Vector3 position)
    {
        GameObject cookieInstance = Instantiate(Cookie);
        position.y = 1;
        cookieInstance.transform.position = position;
    }

    public void EnemyDied(Vector3 position)
    {
        int randomNumber = UnityEngine.Random.Range(1, MilkChance);
        float randomValue = UnityEngine.Random.Range(MilkValueMin, MilkValueMax);

        if (randomNumber == 1)
        {
            Milk milkInstance = Instantiate(Milk);
            position.y = 1;
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
