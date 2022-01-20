using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : Interactive
{
    [SerializeField] private ContainerInfo containerInfo;

    public List<ItemInInventory> ItemsInContainer;

    public override string Name => containerInfo.Name;

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        for (int i = 0; i < containerInfo.ItemsInContainer.Count; i++)
        {
            int itemCount = containerInfo.ItemsInContainer[i].GetCount();
            if (itemCount != 0)
            {
                ItemInInventory item = new ItemInInventory(containerInfo.ItemsInContainer[i].Info, itemCount, ItemState.InContainer);
                ItemsInContainer.Add(item);
            }
        }
    }

    public void Open()
    {
        ContainerInventory.Instance.OpenContainer(this);
    }

    public override void OnInteractive()
    {
        Open();
    }
}
