using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState { InInventory, InContainer, Dropped}
public class PlayerInventory : MonoBehaviour
{
    //[SerializeField] private Transform dropPos;

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
        PlayerCombatController.Instance.CheckAmmoInInventory();
        SaveSystem.SaveData(SaveType.Inventory);
    }

    public void AddItemInInventory(ItemInfo item, int count)
    {
        if (GetItemByInfo(item) != null)
            GetItemByInfo(item).Count += count;
        else
            ItemsInInventory.Add(new ItemInInventory(item, count, ItemState.InInventory));

        PlayerCombatController.Instance.CheckAmmoInInventory();
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

    public void RemoveItemFromInventory(ItemInfo itemInfo, int count)
    {
        int currentCount = GetItemByInfo(itemInfo).Count -= count;

        if (currentCount > 0)
        {
            GetItemByInfo(itemInfo).Count = currentCount;
        }
        else
        {
            ItemsInInventory.Remove(GetItemByInfo(itemInfo));
        }
        PlayerInventoryUI.Instance.UpdateInventoryUI();
    }

    public ItemInInventory GetItemByInfo(ItemInfo info)
    {
        for (int i = 0; i < ItemsInInventory.Count; i++)
        {
            if (ItemsInInventory[i].ItemInfo == info)
                return ItemsInInventory[i];
        }

        return null;
    }

    /*public void DropItem(ItemInInventory item)
    {
        DroppedItem dropped = Instantiate(item.ItemInfo.ItemModel, 
            dropPos.position, Quaternion.identity).AddComponent<DroppedItem>();
        ItemInInventory dropItem = new ItemInInventory();
        dropItem.ItemInfo = GetItemByInfo(item.ItemInfo).ItemInfo;
        dropItem.Count = 1;
        dropItem.State = ItemState.Dropped;
        dropped.Item = dropItem;
        GetItemByInfo(item.ItemInfo).Count--;
        if (GetItemByInfo(item.ItemInfo).Count == 0)
        {
            RemoveItemFromInventory(item);
        }

        PlayerInventoryUI.Instance.UpdateInventoryUI();
    }*/

    public void SortInventory()
    {

    }

    public int GetItemAmount(ItemInfo itemInfo)
    {
        if (GetItemByInfo(itemInfo) == null)
            return 0;

        return GetItemByInfo(itemInfo).Count;
    }

    public int GetItemAmount(ItemInInventory item)
    {
        return ItemsInInventory[ItemsInInventory.IndexOf(item)].Count;
    }
}
