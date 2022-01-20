using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe", fileName = "RecipeInfo")]
public class RecipeInfo : ScriptableObject
{
    public List<IngredientRecipe> Ingredients;
    public ItemInfo FinishedItem;
    public int FinishedItemCount = 1;

    [System.Serializable] 
    public class IngredientRecipe
    {
        public ItemInfo Info;
        public int NeededCount;
    }
}
