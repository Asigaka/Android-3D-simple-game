using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI terminalMessageText;
    [SerializeField] private TextMeshProUGUI terminalNameText;
    [SerializeField] private Transform optionsContent;
    [SerializeField] private GameObject optionsPrefab;

    public static TerminalUI Instance;

    private TerminalController currentTerminal;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void TurnOnTerminalUI(TerminalController currentTerminal)
    {
        UIInGameManager.Instance.ToogleUI(UIInGameObjectType.Terminal);
        this.currentTerminal = currentTerminal;
        terminalNameText.text = currentTerminal.Info.Name;
        UpdateTerminalUI();
    }

    public void TurnOffTerminalUI()
    {
        UIInGameManager.Instance.ToogleMainUI();
        currentTerminal = null;
    }

    public void ShowMessageText(string message)
    {
        terminalMessageText.text = message;
    }

    public void UpdateTerminalUI()
    {
        terminalMessageText.text = "";
        ClearOptions();
        SpawnOptions();
    }

    public void SpawnOptions()
    {
        if (currentTerminal.Info != null)
        {
            foreach (TerminalInfo.OptionWithMessage option in currentTerminal.Info.OptionsWithMessage)
            {
                TerminalsOption terminalsOption = Instantiate(optionsPrefab, optionsContent).GetComponent<TerminalsOption>();
                terminalsOption.OptionWithMessage = option;
                terminalsOption.UpdateValuse();
            }
        }

        foreach (TerminalController.OptionWithEvent option in currentTerminal.OptionsWithEvent)
        {
            TerminalsOption terminalsOption = Instantiate(optionsPrefab, optionsContent).GetComponent<TerminalsOption>();
            terminalsOption.OptionWithEvent = option;
            terminalsOption.UpdateValuse();
        }
    }

    public void ClearOptions()
    {
        for (int i = 0; i < optionsContent.childCount; i++)
            Destroy(optionsContent.GetChild(i).gameObject);
    }
}
