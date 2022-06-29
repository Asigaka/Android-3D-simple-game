using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsHander
{
    public static List<ItemEntity> GetPlayerInventory => Session.Instance.Player.Inventory.ItemsInInventory;

    public static void AddItem(List<ItemEntity> inventory, ItemInfo info, int count)
    {
        ItemEntity item = GetItemByInfo(inventory, info);

        if (item != null)
        {
            item.Count += count;
        }
        else
        {
            item = new ItemEntity(info, count);
            inventory.Add(item);
        }
    }

    public static void AddItemInPlayer(ItemInfo info, int count)
    {
        ItemEntity item = GetItemByInfoInPlayer(info);

        if (item != null)
        {
            item.Count += count;
        }
        else
        {
            item = new ItemEntity(info, count);
            GetPlayerInventory.Add(item);
        }
    }

    public static void AddItems(List<ItemEntity> inventory, List<ItemWithAmount> items)
    {
        foreach (ItemWithAmount item in items)
        {
            AddItem(inventory, item.Info, item.Count);
        }
    }

    public static void AddItemsInPlayer(List<ItemWithAmount> items)
    {
        foreach (ItemWithAmount item in items)
        {
            AddItemInPlayer(item.Info, item.Count);
        }
    }

    public static int RemoveItem(List<ItemEntity> inventory, ItemInfo info, int count = -1)
    {
        ItemEntity item = GetItemByInfo(inventory, info);

        if (item == null) return 0;

        int currentCount = item.Count - count;

        if (currentCount > 0)
        {
            item.Count = currentCount;
            return currentCount;
        }
        else
        {
            GetPlayerInventory.Remove(item);
            return 0;
        }
    }

    public static int RemoveItemInPlayer(ItemInfo info, int count)
    {
        ItemEntity item = GetItemByInfoInPlayer(info);

        if (item == null) return 0;

        int currentCount = item.Count - count;

        if (currentCount > 0)
        {
            item.Count = currentCount;
            return currentCount;
        }
        else
        {
            GetPlayerInventory.Remove(item);
            return 0;
        }
    }

    public static void RemoveItems(List<ItemEntity> inventory, List<ItemWithAmount> items)
    {
        foreach (ItemWithAmount item in items)
        {
            RemoveItem(inventory, item.Info, item.Count);
        }
    }

    public static void RemoveItemsInPlayer(List<ItemWithAmount> items)
    {
        foreach (ItemWithAmount item in items)
        {
            RemoveItemInPlayer(item.Info, item.Count);
        }
    }

    public static bool HasItems(List<ItemEntity> inventory, ItemInfo info, int count)
    {
        ItemEntity item = GetItemByInfo(inventory, info);

        if (item == null) return false;

        return item != null;
    }

    public static bool HasItems(List<ItemEntity> inventory, List<ItemWithAmount> items)
    {
        foreach (ItemWithAmount item in items)
        {
            if (!HasItems(inventory, item.Info, item.Count))
            {
                return false;
            }
        }

        return true;
    }

    public static bool HasItemsInPlayer(List<ItemWithAmount> items)
    {
        foreach (ItemWithAmount item in items)
        {
            if (!HasItemsInPlayer(item.Info, item.Count))
            {
                return false;
            }
        }

        return true;
    }

    public static bool HasItemsInPlayer(ItemInfo info, int count)
    {
        ItemEntity item = GetItemByInfoInPlayer(info);

        if (item == null) return false;

        return false;
    }

    public static ItemEntity GetItemByInfo(List<ItemEntity> inventory, ItemInfo info)
    {
        foreach (ItemEntity item in inventory)
        {
            if (item.ItemInfo == info)
            {
                return item;
            }
        }

        return null;
    }

    public static ItemEntity GetItemByInfoInPlayer(ItemInfo info)
    {
        foreach (ItemEntity item in GetPlayerInventory)
        {
            if (item.ItemInfo == info)
            {
                return item;
            }
        }

        return null;
    }

    public static int GetItemAmount(List<ItemEntity> inventory, ItemInfo info)
    {
        ItemEntity item = GetItemByInfo(inventory, info);

        if (item != null)
            return item.Count;
        else
            return 0;
    }

    public static int GetItemAmountInPlayer(ItemInfo info)
    {
        ItemEntity item = GetItemByInfoInPlayer(info);

        if (item != null)
            return item.Count;
        else
            return 0;
    }
}
