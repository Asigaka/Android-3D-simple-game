using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventory : MonoBehaviour
{
    public ContainerObject SelectedContainer;

    public static ContainerInventory Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void AddItemInContainer(ItemInInventory item)
    {
        if (GetItemByInfo(item.ItemInfo) != null)
            GetItemByInfo(item.ItemInfo).Count += item.Count;
        else
            SelectedContainer.ItemsInContainer.Add(item);

        item.State = ItemState.InContainer;
        ContainerInventoryUI.Instance.UpdateInventoryUI();
    }

    public void AddItemsInContainer(List<ItemInInventory> items)
    {
        foreach (ItemInInventory item in items)
        {
            AddItemInContainer(item);
        }
    }

    public void RemoveItemFromContainer(ItemInInventory item)
    {
        SelectedContainer.ItemsInContainer.Remove(item);
        ContainerInventoryUI.Instance.UpdateInventoryUI();
    }

    private ItemInInventory GetItemByInfo(ItemInfo info)
    {
        for (int i = 0; i < SelectedContainer.ItemsInContainer.Count; i++)
            if (SelectedContainer.ItemsInContainer[i].ItemInfo == info)
                return SelectedContainer.ItemsInContainer[i];

        return null;
    }

    public void OpenContainer(ContainerObject container)
    {
        SelectedContainer = container;
        ContainerInventoryUI.Instance.UpdateInventoryUI();
        UIManager.Instance.ToogleUI(UIObjectType.Container);
    }

    public void CloseContainer()
    {
        if (SelectedContainer.ItemsInContainer.Count == 0)
            SelectedContainer.IsEmpty = true;

        SelectedContainer = null;
    }

    public void SortInventory()
    {

    }

    public void OnContainerClose()
    {
        SelectedContainer.IsEmpty = SelectedContainer.ItemsInContainer.Count == 0;

        SelectedContainer = null;
    }

    public int GetItemAmount(ItemInInventory item)
    {
        return SelectedContainer.ItemsInContainer[SelectedContainer.ItemsInContainer.IndexOf(item)].Count;
    }
}
