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

    private ItemEntity itemInCell;

    public ItemEntity ItemInCell { get => itemInCell; }

    public void SetValues(ItemEntity item)
    {
        itemInCell = item;

        if (itemInCell.ItemInfo.Sprite != null)
            itemImg.sprite = itemInCell.ItemInfo.Sprite;

        itemCountText.text = itemInCell.Count.ToString();
    }

    public void OnClickUp()
    {
        //PlayerInventoryUI.Instance.TurnOnItemPanel(itemInCell, this);
    }
}
