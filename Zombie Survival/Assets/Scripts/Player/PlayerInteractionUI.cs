using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject interactiveCrosshair;
    [SerializeField] private TextMeshProUGUI interactiveNameText;

    public static PlayerInteractionUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void TurnOnInteractiveCrosshair(string interactiveName)
    {
        interactiveCrosshair.SetActive(true);
        interactiveNameText.text = interactiveName;
    }

    public void TurnOffInteractiveCrosshair()
    {
        interactiveCrosshair.SetActive(false);
        interactiveNameText.text = "";
    }
}
