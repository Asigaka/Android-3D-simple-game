using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuikOutline))]
public abstract class Interactive : MonoBehaviour
{
    [SerializeField] private bool isLocked;

    [SerializeField] private List<ItemEntity> itemsForInteract;

    private QuikOutline outline;
    private Color outlineColor = Color.white;

    private void OnEnable()
    {
        outline = GetComponent<QuikOutline>();
        outline.OutlineColor = outlineColor;
        outline.enabled = false;
    }

    public void OnFocused()
    {
        outline.enabled = true;
    }

    public void OnDisfocused()
    {
        outline.enabled = false;
    }

    public abstract string Name { get; }
    public bool IsLocked { get => isLocked; private set => isLocked = value; }

    public abstract void OnInteractive();

    public void Lock() => IsLocked = true;
    public void Unlock() => IsLocked = false;
}
