using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState { InInventory, InContainer, Dropped}
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemEntity> itemsInInventory;
    [SerializeField] private ContainerObject selectedContainer;

    public List<ItemEntity> ItemsInInventory { get => itemsInInventory; }
    public ContainerObject SelectedContainer { get => selectedContainer; }

    public void OpenContainer(ContainerObject container)
    {
        selectedContainer = container;
    }
}
