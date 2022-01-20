using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipies", fileName = "RecipiesInfo")]
public class RecipiesInfo : ScriptableObject
{
    public List<RecipeInfo> Recipes;
}
