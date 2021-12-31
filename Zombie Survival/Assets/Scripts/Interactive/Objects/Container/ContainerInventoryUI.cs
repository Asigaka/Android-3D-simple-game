using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContainerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private Transform itemModelPos;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private ItemCell itemCell;

    [Space(7)]
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private Transform inventoryContent;

    private ContainerInventory containerInventory;

    public static ContainerInventoryUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        containerInventory = ContainerInventory.Instance;
    }

    public void TurnOnInventory()
    {
        UpdateInventoryUI();
        UIManager.Instance.ToogleUI(UIObjectType.Inventory);
    }

    public void TurnOffIventory()
    {
        UIManager.Instance.ToogleMainUI();
    }

    public void UpdateInventoryUI()
    {
        itemCell = null;
        ClearItemsUI();
        SpawnItemsUI();
        itemPanel.SetActive(false);
    }

    public void TurnOnItemPanel(ItemInInventory item, ItemCell itemCell)
    {
        if (itemModelPos.childCount != 0)
            Destroy(itemModelPos.GetChild(0).gameObject);

        Instantiate(item.ItemInfo.ItemModel, itemModelPos).layer = 5;
        this.itemCell = itemCell;
        itemPanel.SetActive(true);
        itemNameText.text = item.ItemInfo.Name;
        itemDescriptionText.text = item.ItemInfo.Description;
    }

    public void TakeSelectedItem()
    {
        PlayerInventory.Instance.AddItemInInventory(itemCell.ItemInCell);
        ContainerInventory.Instance.RemoveItemFromContainer(itemCell.ItemInCell);
        Destroy(itemCell.gameObject);
    }                                      

    private void SpawnItemsUI()
    {
        for (int i = 0; i < containerInventory.SelectedContainer.ItemsInContainer.Count; i++)
        {
            GameObject cellObj = Instantiate(itemCellPrefab, inventoryContent);
            ItemCell cell = cellObj.GetComponent<ItemCell>();
            cell.SetValues(containerInventory.SelectedContainer.ItemsInContainer[i]);
        }
    }

    private void ClearItemsUI()
    {
        for (int i = 0; i < inventoryContent.childCount; i++)
            Destroy(inventoryContent.GetChild(i).gameObject);
    }
}
