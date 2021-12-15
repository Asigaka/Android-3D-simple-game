using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNeedsUI : MonoBehaviour
{
    [SerializeField] private PlayerNeedsController controller;

    [Space(7)]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI satietyText;
    [SerializeField] private TextMeshProUGUI thirstText;

    public static PlayerNeedsUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    public void ShowAllNeeds(float health, float satiety, float thirst)
    {
        healthText.text = "Health: " + health;
        satietyText.text = "Satiety: " + satiety;
        thirstText.text = "Thirst: " + thirst;
    }
}
