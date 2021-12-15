using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private GameObject movingObject;
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

    public void OnInventorySwitch()
    {
        if (!inventoryPanel.activeSelf)
            TurnOnInventory();
        else
            TurnOffIventory();
    }

    private void TurnOnInventory()
    {
        UpdateInventoryUI();
        UIManager.Instance.ToogleUI(UIObjectType.Inventory);
    }

    private void TurnOffIventory()
    {
        UIManager.Instance.ToogleMainUI();
    }

    public void UpdateInventoryUI()
    {
        ClearItemsUI();
        SpawnItemsUI();
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
