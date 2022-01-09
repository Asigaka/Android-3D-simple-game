using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Objects/TerminalInfo", fileName = "TerminalInfo")]
public class TerminalInfo : ScriptableObject
{
    public List<OptionWithMessage> OptionsWithMessage;

    [System.Serializable]
    public class OptionWithMessage
    {
        public string OptionLabel;
        public string OptionMessage;
    }
}
