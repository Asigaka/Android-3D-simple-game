using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class InventorySaveData : ISaveData
{
    private List<SerializableItemInInventory> serialItems;

    public InventorySaveData(List<ItemInInventory> items)
    {
        SerializeItems(items);
    }

    public List<ItemInInventory> GetItems()
    {
        List<ItemInInventory> desirializeItems = new List<ItemInInventory>();

        foreach (SerializableItemInInventory serialItem in serialItems)
        {
            ItemInInventory item = new ItemInInventory(SaveSystem.GetItemsInfoByAssetLabel(serialItem.ItemName), serialItem.Count, serialItem.State);
            desirializeItems.Add(item);
        }

        return desirializeItems;
    }

    private void SerializeItems(List<ItemInInventory> items)
    {
        serialItems = new List<SerializableItemInInventory>();

        foreach (ItemInInventory item in items)
        {
            SerializableItemInInventory serializableItem;
            serializableItem = new SerializableItemInInventory(item.ItemInfo.Name, item.Count, item.State);
            serialItems.Add(serializableItem);
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
