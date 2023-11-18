using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    [SerializeField] GameObject ShopScreen;
    [SerializeField] float ShopTimeOne;
    [SerializeField] float ShopTimeTwo;

    [SerializeField] List<UpgradeSO> upgradeSOs = new List<UpgradeSO>();

    [SerializeField] TMP_Text upgradeName;
    [SerializeField] TMP_Text upgradeDescription;
    [SerializeField] Transform LayoutGrid;
    [SerializeField] ShopButton ButtonPrefab;
    [SerializeField] SaveGameSO saveGameSO;

    [Header("Upgrade Values")]
    public List<int> MaxHealthValues = new();

    private bool shopOneShown;

    public enum Upgrades
    {
        MaxHealth
    }

    private void OnEnable()
    {
        LevelTimer.OnTimerTick += OnTimerTick;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDisable()
    {
        LevelTimer.OnTimerTick -= OnTimerTick;
    }

    private void OnTimerTick(float timer)
    {
        if(timer > ShopTimeOne && !shopOneShown)
        {
            shopOneShown = true;
            ShowShop();
        }
    }

    private void ShowShop()
    {
        ShopScreen.SetActive(true);

        for (int i = 0; i < upgradeSOs.Count; i++)
        {
            ShopButton button = Instantiate(ButtonPrefab, LayoutGrid);

            button.upgradeSO = upgradeSOs[i];

            
        }
    }

    public void ChangeHighlightedUpgrade(UpgradeSO upgradeSO)
    {
        upgradeName.text = upgradeSO.LongUpgradeName;
        upgradeDescription.text = upgradeSO.UpgradeDescription;
    }

    public void ButtonPressed(UpgradeSO upgradeSO, ShopButton button)
    {
        int cookies = GameController.Instance.CookiesCollected;

        switch(upgradeSO.upgrade)
        {
            case Upgrades.MaxHealth:

                // Are there more to purchase
                if (saveGameSO.MaxHealthPurchased == MaxHealthValues.Count) { return; }

                // Can we afford to purchase the next upgrade
                if (upgradeSO.Costs[saveGameSO.MaxHealthPurchased + 1] > cookies) { return; }

                saveGameSO.MaxHealthPurchased++;
                saveGameSO.Save();
                GameController.Instance.LoadPlayerStats();
                button.UpdateVisuals();

                break;
        }

        
    }
}
