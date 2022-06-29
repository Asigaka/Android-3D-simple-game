using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Enums;

public class TerminalScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI terminalMessageText;
    [SerializeField] private TextMeshProUGUI terminalNameText;
    [SerializeField] private Transform optionsContent;
    [SerializeField] private TerminalOption optionsPrefab;
}
