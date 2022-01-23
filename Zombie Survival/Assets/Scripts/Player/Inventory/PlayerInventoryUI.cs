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
        UIInGameManager.Instance.ToogleUI(UIInGameObjectType.Inventory);
    }

    public void TurnOffIventory()
    {
        UIInGameManager.Instance.ToogleMainUI();
        ContainerInventory.Instance.CloseContainer();
    }             

    public void UpdateInventoryUI()
    {
        if (ContainerInventory.Instance.SelectedContainer != null)
            containerNameText.text = ContainerInventory.Instance.SelectedContainer.Name;
        else
            containerNameText.text = "";

        containerContent.gameObject.SetActive(ContainerInventory.Instance.SelectedContainer != null);
        ClearItemsUI();
        SpawnItemsUI();
        aboutItemPanel.SetActive(false);
        buttonsItemPanel.SetActive(false);
    }

    public void TakeSelectedItem()
    {
        PlayerHands.Instance.RemoveItemInHandIfTransferHis(itemCell.ItemInCell.ItemInfo);

        if (itemCell.ItemInCell.State == ItemState.InContainer)
        {
            PlayerInventory.Instance.AddItemInInventory(itemCell.ItemInCell);
            ContainerInventory.Instance.RemoveItemFromContainer(itemCell.ItemInCell);
            Destroy(itemCell.gameObject);
            itemCell = null;//Нужно ли?
        }
        else if (itemCell.ItemInCell.State == ItemState.InInventory)
        {
            ContainerInventory.Instance.AddItemInContainer(itemCell.ItemInCell);
            PlayerInventory.Instance.RemoveItemFromInventory(itemCell.ItemInCell);
            Destroy(itemCell.gameObject);
            itemCell = null;
        }

        transferItemBtn.SetActive(false);
    }

    public void EquipSelectedItem()
    {
        PlayerHands.Instance.TakeInHand(itemCell.ItemInCell.ItemInfo);
        if (itemCell.ItemInCell.State == ItemState.InContainer)
        {
            PlayerInventory.Instance.AddItemInInventory(itemCell.ItemInCell);
            ContainerInventory.Instance.RemoveItemFromContainer(itemCell.ItemInCell);
            Destroy(itemCell.gameObject);
            itemCell = null;//Нужно ли?
        }
    }

    public void TurnOnItemPanel(ItemInInventory item, ItemCell itemCell)
    {
        transferItemBtn.SetActive(ContainerInventory.Instance.SelectedContainer);
        useItemBtn.SetActive(itemCell.ItemInCell.ItemInfo.Type == ItemType.Food);
        equipItemBtn.SetActive(itemCell.ItemInCell.ItemInfo.CanEquip);

        this.itemCell = itemCell;
        aboutItemPanel.SetActive(true);
        buttonsItemPanel.SetActive(true);
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
