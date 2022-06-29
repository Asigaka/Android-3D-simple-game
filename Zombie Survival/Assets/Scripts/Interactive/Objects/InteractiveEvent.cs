using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveEvent : Interactive
{
    [SerializeField] private string objectName;

    [Space]
    public UnityEvent onInteractiveEvent;

    public override string Name => objectName;

    public override void OnInteractive()
    {
        onInteractiveEvent.Invoke();
    }
}
