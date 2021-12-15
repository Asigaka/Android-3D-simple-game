using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInInventory : MonoBehaviour
{
    public ItemInfo ItemInfo;
    public int Count;
    public ItemState State;

    public void Use()
    {
        Debug.Log(GetComponent<ItemFoodInfo>());
        switch (ItemInfo.Type)
        {
            case ItemType.Default: break;
            case ItemType.Food:
                break;
            case ItemType.Weapon: break;
        }
    }
}
