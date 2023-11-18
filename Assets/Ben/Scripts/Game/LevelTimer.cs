using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static event Action<float> OnTimerTick;

    [SerializeField] TMP_Text timerText;

    [SerializeField] private float timer;

    private void Update()
    {
        if(!GameController.Instance.LevelStarted || GameController.Instance.GamePaused) { return; }

        timer += Time.deltaTime;

        OnTimerTick?.Invoke(timer);

        TimeSpan time = TimeSpan.FromSeconds(timer);

        if(timer < 60)
        {
            timerText.text = ((int)timer).ToString();
        }
        else if(timer < 600)
        {
            timerText.text = time.ToString("m':'ss");
        }
        else
        {
            timerText.text = time.ToString("mm':'ss");
        }
        
    }

    public float GetCurrentTime()
    {
        return timer;
    }
}
