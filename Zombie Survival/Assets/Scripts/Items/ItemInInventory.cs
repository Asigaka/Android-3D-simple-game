using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInInventory 
{
    public ItemInfo ItemInfo;
    public int Count;
    public ItemState State;

    public ItemInInventory(ItemInfo itemInfo, int count, ItemState state)
    {
        ItemInfo = itemInfo;
        Count = count;
        State = state;
    }
}
