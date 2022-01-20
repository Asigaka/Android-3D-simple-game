using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IngredientCell : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;

    public void SetValues(Sprite sprite, string name, int aviableCount, int neededCount)
    {
        itemImage.sprite = sprite;
        itemNameText.text = name;

        if (aviableCount >= neededCount)
            itemCountText.color = Color.green;
        else
            itemCountText.color = Color.red;

        itemCountText.text = aviableCount.ToString() + "/" + neededCount.ToString();
    }
}
