using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    private void OnEnable()
    {
        GameController.Instance.PlayerHealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        GameController.Instance.PlayerHealthChanged -= HealthChanged;
    }

    private void HealthChanged(int newValue)
    {
        healthSlider.value = newValue;
    }
}
