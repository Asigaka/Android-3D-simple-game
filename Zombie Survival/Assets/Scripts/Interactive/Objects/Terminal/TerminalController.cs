using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerminalController : Interactive
{
    [SerializeField] private TerminalInfo info;

    [Space(7)]
    [SerializeField] private List<OptionWithEvent> optionsWithEvent;

    public TerminalInfo Info { get => info; private set => info = value; }
    public List<OptionWithEvent> OptionsWithEvent { get => optionsWithEvent; private set => optionsWithEvent = value; }

    public override void OnInteractive()
    {
        TerminalUI.Instance.TurnOnTerminalUI(this);
    }

    [System.Serializable]
    public class OptionWithEvent
    {
        public string OptionLabel;
        public UnityEvent OptionEvent;
    }
}
