using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    [SerializeField] private Image cellBG;
    [SerializeField] private Image itemImg;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private ItemInInventory itemInCell;

    public ItemInInventory ItemInCell { get => itemInCell; set => itemInCell = value; }

    public void SetValues(ItemInInventory item)
    {
        itemInCell = item;

        if (itemInCell.ItemInfo.Sprite != null)
            itemImg.sprite = itemInCell.ItemInfo.Sprite;

        itemCountText.text = itemInCell.Count.ToString();
    }

    public void OnClickUp()
    {
        if (itemInCell.State == ItemState.InContainer)
        {
             ContainerInventoryUI.Instance.TurnOnItemPanel(itemInCell, this);
        }
        else
        {
             PlayerInventoryUI.Instance.TurnOnItemPanel(itemInCell, this);         
        }
    }
}
