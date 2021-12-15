using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private GameObject movingObject;
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

    public void OnInventorySwitch()
    {
        if (!inventoryPanel.activeSelf)
            TurnOnInventory();
        else
            TurnOffIventory();
    }

    public void TurnOnInventory()
    {
        UpdateInventoryUI();
        inventoryPanel.SetActive(true);
    }

    public void TurnOffIventory()
    {
        inventoryPanel.SetActive(false);
        UIManager.Instance.ToogleMainUI();
    }

    public void UpdateInventoryUI()
    {
        ClearItemsUI();
        SpawnItemsUI();
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
