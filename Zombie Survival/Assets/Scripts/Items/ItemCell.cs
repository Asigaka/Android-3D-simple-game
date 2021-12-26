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
            PlayerInventory.Instance.AddItemInInventory(itemInCell);
            ContainerInventory.Instance.RemoveItemFromContainer(itemInCell);
            Destroy(this);
        }
        else
        {
            //ContainerInventory.Instance.AddItemInContainer(itemInCell);
            //PlayerInventory.Instance.RemoveItemFromInventory(itemInCell);
            PlayerInventory.Instance.DropItem(itemInCell);
            //Destroy(this);
        }
    }
}
