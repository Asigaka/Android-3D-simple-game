using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField] private GameObject recipePrefab;
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private Transform ingredietPanel;
    [SerializeField] private Transform recipiesPanel;
    [SerializeField] private Button craftBtn; 

    [Space(7)]
    [SerializeField] private GameObject aboutItemPanel;
    [SerializeField] private TextMeshProUGUI itemNameText; 
    [SerializeField] private TextMeshProUGUI itemDescriptionText; 

    private CraftController craftController;
    private RecipeInfo currentRecipeInfo;

    public static CraftUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        craftController = CraftController.Instance;
        UpdateUI();
    }

    public void TurnOnCraftUI()
    {
        UIManager.Instance.ToogleUI(UIObjectType.Craft);
        UpdateUI();
    }

    public void TurnOnInventoryUI()
    {
        UIManager.Instance.ToogleUI(UIObjectType.Inventory);
    }

    public void OnCraftBtnClick()
    {
        CraftController.Instance.CraftItem(currentRecipeInfo);
        UpdateUI();
    }

    public void UpdateUI()
    {
        craftBtn.gameObject.SetActive(false);
        aboutItemPanel.gameObject.SetActive(false);
        ClearRecipies();
        ClearIngredientsUI();
        SpawnRecipes();
    }

    public void ClearRecipies()
    {
        for (int i = 0; i < recipiesPanel.childCount; i++)
            Destroy(recipiesPanel.GetChild(i).gameObject);
    }

    private void ClearIngredientsUI()
    {
        for (int i = 0; i < ingredietPanel.childCount; i++)
            Destroy(ingredietPanel.GetChild(i).gameObject);

        ingredietPanel.gameObject.SetActive(false);
    }

    private void TurnOnAboutItem(string name, string description)
    {
        aboutItemPanel.SetActive(true);
        itemNameText.text = name;
        itemDescriptionText.text = description;
    }

    public void SpawnRecipes()
    {
        List<RecipeInfo> recipesLocal = craftController.Recipes.Recipes;

        foreach (RecipeInfo recipe in recipesLocal)
        {
            GameObject cellObj = Instantiate(recipePrefab, recipiesPanel);
            RecipeCell cell = cellObj.GetComponent<RecipeCell>();
            cell.SetValues(recipe.FinishedItem.Sprite, recipe.FinishedItem.Name, recipe.FinishedItemCount, recipe);
        }
    }

    public void OnRecipeCellClick(RecipeInfo recipe)
    {
        currentRecipeInfo = recipe;
        ClearIngredientsUI();
        ingredietPanel.gameObject.SetActive(true);
        TurnOnAboutItem(currentRecipeInfo.FinishedItem.Name, currentRecipeInfo.FinishedItem.Description);
        SpawnIngredients(currentRecipeInfo);
    }

    private void SpawnIngredients(RecipeInfo recipe)
    {
        craftBtn.gameObject.SetActive(true);
        craftBtn.interactable = CraftController.Instance.CanCraft(recipe);

        foreach (RecipeInfo.IngredientRecipe ingredient in recipe.Ingredients)
        {
            int aviableCount = PlayerInventory.Instance.GetItemAmount(ingredient.Info);
            int neededCount = ingredient.NeededCount;

            GameObject cellObj = Instantiate(ingredientPrefab, ingredietPanel);
            IngredientCell cell = cellObj.GetComponent<IngredientCell>();
            cell.SetValues(ingredient.Info.Sprite, ingredient.Info.Name,
                aviableCount, neededCount);
        }
    }
}
