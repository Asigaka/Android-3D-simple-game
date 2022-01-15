using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFPS;

    private int fps;

    private void Update()
    {
        fps = (int)(1 / Time.deltaTime);
        textFPS.text = fps.ToString();    
    }
}
