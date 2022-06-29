using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject aboutItemPanel;
    [SerializeField] private GameObject buttonsItemPanel;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI containerNameText;
    [SerializeField] private ItemCell itemCell;

    [Header("Buttons of panels items")]
    [SerializeField] private GameObject transferItemBtn;
    [SerializeField] private GameObject useItemBtn;
    [SerializeField] private GameObject equipItemBtn;

    [Space(7)]
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform containerContent;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = Session.Instance.Player.Inventory;
    }           

    public void UpdateInventoryUI()
    {
        ClearItemsUI();
        SpawnItemsUI();
        aboutItemPanel.SetActive(false);
        buttonsItemPanel.SetActive(false);
    }

    public void SelectItem(ItemEntity item)
    {

    }

    private void SpawnItemsUI()
    {
        for (int i = 0; i < playerInventory.ItemsInInventory.Count; i++)
        {
            GameObject cellObj = Instantiate(itemCellPrefab, inventoryContent);
            ItemCell cell = cellObj.GetComponent<ItemCell>();
            cell.SetValues(playerInventory.ItemsInInventory[i]);
        }

        ContainerObject container = playerInventory.SelectedContainer;

        if (container)
        {
            for (int i = 0; i < container.ItemsInContainer.Count; i++)
            {
                GameObject cellObj = Instantiate(itemCellPrefab, containerContent);
                ItemCell cell = cellObj.GetComponent<ItemCell>();
                cell.SetValues(container.ItemsInContainer[i]);
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
