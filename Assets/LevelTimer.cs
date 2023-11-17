using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;

    private float timer;

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(!GameController.Instance.LevelStarted || GameController.Instance.GamePaused) { return; }

        timer += Time.deltaTime;
    }

    public float GetCurrentTime()
    {
        return timer;
    }
}
