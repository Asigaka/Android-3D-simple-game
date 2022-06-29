using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe", fileName = "RecipeInfo")]
public class RecipeInfo : ScriptableObject
{
    public List<ItemWithAmount> Ingredients;
    public ItemInfo FinishedItem;
    public int FinishedItemCount = 1;
}
