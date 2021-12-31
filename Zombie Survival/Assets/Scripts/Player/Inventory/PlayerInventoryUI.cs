using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private Transform itemModelPos;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private ItemCell itemCell;

    [Space(7)]
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private Transform inventoryContent;

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

    public void OnDropItemDown()
    {
        playerInventory.DropItem(itemCell.ItemInCell);
    } 

    private void SpawnItemsUI()
    {
        for (int i = 0; i < playerInventory.ItemsInInventory.Count; i++)
        {
            GameObject cellObj = Instantiate(itemCellPrefab, inventoryContent);
            ItemCell cell = cellObj.GetComponent<ItemCell>();
            cell.SetValues(playerInventory.ItemsInInventory[i]);
        }
    }

    private void ClearItemsUI()
    {
        for (int i = 0; i < inventoryContent.childCount; i++)
            Destroy(inventoryContent.GetChild(i).gameObject);
    }
}
