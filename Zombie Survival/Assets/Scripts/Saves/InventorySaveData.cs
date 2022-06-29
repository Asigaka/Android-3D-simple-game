using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class InventorySaveData : ISaveData
{
    private List<SerializableItemInInventory> serialItems;

    public InventorySaveData(List<ItemEntity> items)
    {
        SerializeItems(items);
    }

    public List<ItemEntity> GetItems()
    {
        List<ItemEntity> desirializeItems = new List<ItemEntity>();

        foreach (SerializableItemInInventory serialItem in serialItems)
        {
            //ItemEntity item = new ItemEntity(SaveSystem.GetItemsInfoByAssetLabel(serialItem.ItemName), serialItem.Count, serialItem.State);
            //desirializeItems.Add(item);
        }

        return desirializeItems;
    }

    private void SerializeItems(List<ItemEntity> items)
    {
        serialItems = new List<SerializableItemInInventory>();

        foreach (ItemEntity item in items)
        {
            //SerializableItemInInventory serializableItem;
            //serializableItem = new SerializableItemInInventory(item.ItemInfo.Name, item.Count, item.State);
            //serialItems.Add(serializableItem);
        }
    }

    [System.Serializable]
    public class SerializableItemInInventory
    {
        public string ItemName;
        public int Count;
        public ItemState State;

        public SerializableItemInInventory(string itemName, int count, ItemState state)
        {
            ItemName = itemName;
            Count = count;
            this.State = state;
        }
    }
}
