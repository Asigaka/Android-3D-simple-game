using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionLabelText;

    private TerminalInfo.OptionWithMessage optionWithMessage;
    private TerminalController.OptionWithEvent optionWithEvent;

    public TerminalInfo.OptionWithMessage OptionWithMessage { get => optionWithMessage; set => optionWithMessage = value; }
    public TerminalController.OptionWithEvent OptionWithEvent { get => optionWithEvent; set => optionWithEvent = value; }

    public void UpdateValuse()
    {
        if (optionWithMessage != null)
            optionLabelText.text = optionWithMessage.OptionLabel;
        else
            optionLabelText.text = optionWithEvent.OptionLabel;
    }

    public void OnClick()
    {
        /*if (optionWithMessage != null)
            TerminalScreen.Instance.ShowMessageText(optionWithMessage.OptionMessage);
        else
            optionWithEvent.OptionEvent.Invoke();*/
    }
}
