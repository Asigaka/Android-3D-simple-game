using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemWithAmount
{
    [SerializeField] private ItemInfo item;
    [SerializeField] private int count;

    public ItemInfo Info { get => item; }
    public int Count { get => count; }
}
