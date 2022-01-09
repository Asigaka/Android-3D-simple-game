using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTerminalsMessage;
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
        UIManager.Instance.ToogleUI(UIObjectType.Terminal);
        this.currentTerminal = currentTerminal;
        UpdateTerminalUI();
    }

    public void TurnOffTerminalUI()
    {
        UIManager.Instance.ToogleMainUI();
        currentTerminal = null;
    }

    public void ShowMessageText(string message)
    {
        textTerminalsMessage.text = message;
    }

    public void UpdateTerminalUI()
    {
        textTerminalsMessage.text = "";
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
