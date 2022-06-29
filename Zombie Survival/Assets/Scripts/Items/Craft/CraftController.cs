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
        playerInventory = Session.Instance.Player.Inventory;
    }

    public void CraftItem(RecipeInfo recipe)
    {
        if (ItemsHander.HasItemsInPlayer(recipe.Ingredients))
        {
            foreach (ItemWithAmount ingredient in recipe.Ingredients)
            {
                ItemsHander.RemoveItemInPlayer(ingredient.Info, ingredient.Count);
            }

            ItemsHander.AddItemInPlayer(recipe.FinishedItem, recipe.FinishedItemCount);
        }
    }
}
