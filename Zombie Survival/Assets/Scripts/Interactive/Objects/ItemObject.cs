using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : Interactive
{
    [SerializeField] private ItemEntity item;

    public override string Name { get => item.ItemInfo.Name; }

    public override void OnInteractive()
    {
        ItemsHander.AddItemInPlayer(item.ItemInfo, item.Count);
        Destroy(gameObject);
    }
}
