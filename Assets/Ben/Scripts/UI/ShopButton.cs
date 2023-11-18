using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : HighlitedButton
{
    public SaveGameSO saveGameSO;
    public UpgradeSO upgradeSO;
    public Button Button;
    public Slider Slider;
    public TMP_Text CostText;

    private void Start()
    {
        Button.onClick.AddListener(ButtonPressed);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ShopController.Instance.ChangeHighlightedUpgrade(upgradeSO);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void UpdateVisuals()
    {
        saveGameSO.Load();
        
        switch(upgradeSO.upgrade)
        {
            case ShopController.Upgrades.MaxHealth:

                Slider.maxValue = ShopController.Instance.MaxHealthValues[^1];
                Slider.minValue = 100;
                Slider.value = GameController.Instance.MaxHealth;

                if(saveGameSO.MaxHealthPurchased == -1)
                {
                    CostText.text = upgradeSO.Costs[0].ToString();
                }
                else
                {
                    CostText.text = upgradeSO.Costs[saveGameSO.MaxHealthPurchased].ToString();
                }

                break;
        }
    }

    private void ButtonPressed()
    {
        ShopController.Instance.ButtonPressed(upgradeSO, this);
    }
}
