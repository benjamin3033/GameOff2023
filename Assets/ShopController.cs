using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] GameObject ShopScreen;
    [SerializeField] float ShopTimeOne;
    [SerializeField] float ShopTimeTwo;

    private void OnEnable()
    {
        LevelTimer.OnTimerTick += OnTimerTick;
    }

    private void OnDisable()
    {
        LevelTimer.OnTimerTick -= OnTimerTick;
    }

    private void OnTimerTick(float timer)
    {
        
    }

    public void ShowShop()
    {
        ShopScreen.SetActive(true);
    }
}
