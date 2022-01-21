using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    [SerializeField] private Light flashingLight;
    [SerializeField] private float minFlashingTime;
    [SerializeField] private float maxFlashingTime;

    private float localTime;

    private void Update()
    {
        if (localTime <= 0)
        {
            localTime = Random.Range(minFlashingTime, maxFlashingTime);
            flashingLight.enabled = !flashingLight.enabled;
        }
        else
        {
            localTime -= Time.deltaTime;
        }
    }
}
