using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFPS;

    private float fps;

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        textFPS.text = fps.ToString();    
    }
}
