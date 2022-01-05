using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject aboutItemPanel;
    [SerializeField] private GameObject transferItem;
    [SerializeField] private Transform itemModelPos;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private ItemCell itemCell;

    [Space(7)]
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform containerContent;

    private PlayerInventory playerInventory;

    public static PlayerInventoryUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        playerInventory = PlayerInventory.Instance;
        UpdateInventoryUI();
    }

    public void TurnOnInventory()
    {
        UpdateInventoryUI();
        UIManager.Instance.ToogleUI(UIObjectType.Inventory);
    }

    public void TurnOffIventory()
    {
        UIManager.Instance.ToogleMainUI();
        ContainerInventory.Instance.CloseContainer();
    }             

    public void UpdateInventoryUI()
    {
        containerContent.gameObject.SetActive(ContainerInventory.Instance.SelectedContainer != null);
        ClearItemsUI();
        SpawnItemsUI();
        aboutItemPanel.SetActive(false);
    }

    public void TakeSelectedItem()
    {
        if (itemCell.ItemInCell.State == ItemState.InContainer)
        {
            PlayerInventory.Instance.AddItemInInventory(itemCell.ItemInCell);
            ContainerInventory.Instance.RemoveItemFromContainer(itemCell.ItemInCell);
            Destroy(itemCell.gameObject);
            itemCell = null;//����� ��?
        }
        else if (itemCell.ItemInCell.State == ItemState.InInventory)
        {
            ContainerInventory.Instance.AddItemInContainer(itemCell.ItemInCell);
            PlayerInventory.Instance.RemoveItemFromInventory(itemCell.ItemInCell);
            Destroy(itemCell.gameObject);
            itemCell = null;
        }
        transferItem.SetActive(false);
    }

    public void TurnOnItemPanel(ItemInInventory item, ItemCell itemCell)
    {
        transferItem.SetActive(ContainerInventory.Instance.SelectedContainer);

        if (itemModelPos.childCount != 0)
            Destroy(itemModelPos.GetChild(0).gameObject);

        Instantiate(item.ItemInfo.ItemModel, itemModelPos).layer = 5;
        this.itemCell = itemCell;
        aboutItemPanel.SetActive(true);
        itemNameText.text = item.ItemInfo.Name;
        itemDescriptionText.text = item.ItemInfo.Description;
    }

    private void SpawnItemsUI()
    {
        for (int i = 0; i < playerInventory.ItemsInInventory.Count; i++)
        {
            GameObject cellObj = Instantiate(itemCellPrefab, inventoryContent);
            ItemCell cell = cellObj.GetComponent<ItemCell>();
            cell.SetValues(playerInventory.ItemsInInventory[i]);
        }

        if (ContainerInventory.Instance.SelectedContainer != null)
        {
            for (int i = 0; i < ContainerInventory.Instance.SelectedContainer.ItemsInContainer.Count; i++)
            {
                GameObject cellObj = Instantiate(itemCellPrefab, containerContent);
                ItemCell cell = cellObj.GetComponent<ItemCell>();
                cell.SetValues(ContainerInventory.Instance.SelectedContainer.ItemsInContainer[i]);
            }
        }
    }

    private void ClearItemsUI()
    {
        for (int i = 0; i < inventoryContent.childCount; i++)
            Destroy(inventoryContent.GetChild(i).gameObject);

        for (int i = 0; i < containerContent.childCount; i++)
            Destroy(containerContent.GetChild(i).gameObject);
    }
}
