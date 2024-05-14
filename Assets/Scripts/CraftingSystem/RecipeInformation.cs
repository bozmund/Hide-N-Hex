using UnityEngine;

namespace CraftingSystem
{
    public class RecipeInformation : MonoBehaviour
    {
        public RecipeData recipeData; // Reference to the RecipeData scriptable object

        private void Start()
        {
            // Ensure that the RecipeData is assigned before using it
            if (recipeData == null)
            {
                Debug.LogError("RecipeData is not assigned. Please assign it in the Inspector.");
                return;
            }

            // Use the retrieved information as needed
            Debug.Log("Recipe Information Retrieved:");
            Debug.Log("Potion Name: " + recipeData.potionName);
            Debug.Log("Potion Sprite Name: " + recipeData.potionSpriteName);
            Debug.Log("First Ingredient Sprite Name: " + recipeData.firstIngredientSpriteName);
            Debug.Log("Second Ingredient Sprite Name: " + recipeData.secondIngredientSpriteName);
            Debug.Log("Third Ingredient Sprite Name: " + recipeData.thirdIngredientSpriteName);

            // You can use this information to initialize objects or perform other actions in the CriclelFill scene
        }
    }
}
