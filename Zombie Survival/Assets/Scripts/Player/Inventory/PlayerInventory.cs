using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState { InInventory, InContainer, Dropped}
public class PlayerInventory : MonoBehaviour
{
    public List<ItemInInventory> ItemsInInventory;

    public static PlayerInventory Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void AddItemInInventory(ItemInInventory item)
    {
        if (GetItemByInfo(item.ItemInfo) != null)
            GetItemByInfo(item.ItemInfo).Count += item.Count;
        else
            ItemsInInventory.Add(item);

        item.State = ItemState.InInventory;
        PlayerInventoryUI.Instance.UpdateInventoryUI();
    }

    public void AddItemsInInventory(List<ItemInInventory> items)
    {
        foreach (ItemInInventory item in items)
        {
            AddItemInInventory(item);
        }
    }

    public void RemoveItemFromInventory(ItemInInventory item)
    {
        ItemsInInventory.Remove(item);
        PlayerInventoryUI.Instance.UpdateInventoryUI();
    }

    private ItemInInventory GetItemByInfo(ItemInfo info)
    {
        for (int i = 0; i < ItemsInInventory.Count; i++)
            if (ItemsInInventory[i].ItemInfo == info)
                return ItemsInInventory[i];

        return null;
    }

    public void SortInventory()
    {

    }

    public int GetItemAmount(ItemInInventory item)
    {
        return ItemsInInventory[ItemsInInventory.IndexOf(item)].Count;
    }
}
