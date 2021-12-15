using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactive : MonoBehaviour
{
    private Outline outline;

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    public void OnFocused()
    {
        outline.OutlineWidth = 6;
    }

    public void OnDisfocused()
    {
        outline.OutlineWidth = 0;
    }

    public abstract void OnInteractive();
}
