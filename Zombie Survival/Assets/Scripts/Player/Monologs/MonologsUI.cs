using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonologsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMonologs;

    public static MonologsUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void ShowMonolog(string monolog)
    {
        textMonologs.text = monolog;
    }
}
