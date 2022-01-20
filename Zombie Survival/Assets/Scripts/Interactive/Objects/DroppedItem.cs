using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : Interactive
{
    public ItemInInventory Item;

    public override string Name { get => Item.ItemInfo.Name; }

    public override void OnInteractive()
    {
        PlayerInventory.Instance.AddItemInInventory(Item);
        Destroy(gameObject);
    }
}
