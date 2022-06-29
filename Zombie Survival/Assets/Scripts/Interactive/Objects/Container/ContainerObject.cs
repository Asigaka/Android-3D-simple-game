using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : Interactive
{
    [SerializeField] private ContainerInfo containerInfo;

    [SerializeField] private List<ItemEntity> itemsInContainer;

    public override string Name => containerInfo.Name;
    public List<ItemEntity> ItemsInContainer { get => itemsInContainer; }

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
                ItemsHander.AddItem(itemsInContainer, containerInfo.ItemsInContainer[i].Info, itemCount);
            }
        }
    }

    public void Open()
    {

    }

    public override void OnInteractive()
    {
        Open();
    }
}
