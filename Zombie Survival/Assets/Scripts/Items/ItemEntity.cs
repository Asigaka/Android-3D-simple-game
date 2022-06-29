using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEntity 
{
    public ItemInfo ItemInfo;
    public int Count;

    public ItemEntity(ItemInfo itemInfo, int count)
    {
        ItemInfo = itemInfo;
        Count = count;
    }
}
