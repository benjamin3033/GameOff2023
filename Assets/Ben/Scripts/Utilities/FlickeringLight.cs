using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] Light flickeringLight;
    [SerializeField] float minIntensity = 0.5f;
    [SerializeField] float maxIntensity = 1.5f;
    [SerializeField] float flickerSpeed = 0.1f;

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);

            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
