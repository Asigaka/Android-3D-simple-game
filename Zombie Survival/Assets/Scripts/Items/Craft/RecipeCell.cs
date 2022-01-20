using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCell : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private RecipeInfo recipe;

    public void SetValues(Sprite sprite, string name, int count, RecipeInfo recipe)
    {
        itemImage.sprite = sprite;
        itemNameText.text = name;
        itemCountText.text = count.ToString();
        this.recipe = recipe;
    }

    public void OnClickShow()
    {
        CraftUI.Instance.OnRecipeCellClick(recipe);
    }
}
