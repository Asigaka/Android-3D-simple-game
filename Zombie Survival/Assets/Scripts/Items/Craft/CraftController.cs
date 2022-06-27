using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftController : MonoBehaviour
{
    [SerializeField] private RecipiesInfo recipes;

    private PlayerInventory playerInventory;

    public RecipiesInfo Recipes { get => recipes; private set => recipes = value; }

    private void Start()
    {
        playerInventory = PlayerInventory.Instance;
    }

    public void CraftItem(RecipeInfo recipe)
    {
        if (CanCraft(recipe))
        {
            foreach (RecipeInfo.IngredientRecipe ingredient in recipe.Ingredients)
            {
                playerInventory.RemoveItemFromInventory(ingredient.Info, ingredient.NeededCount);
            }

            playerInventory.AddItemInInventory(recipe.FinishedItem, recipe.FinishedItemCount);
        }
    }

    public bool CanCraft(RecipeInfo recipe)
    {
        foreach (RecipeInfo.IngredientRecipe ingredient in recipe.Ingredients)
        {
            int aviableCount = playerInventory.GetItemAmount(ingredient.Info);
            int neededCount = ingredient.NeededCount;

            if (aviableCount < neededCount)
                return false;
        }

        return true;
    }
}
