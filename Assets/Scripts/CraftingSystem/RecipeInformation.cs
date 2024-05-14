using UnityEngine;
using UnityEngine.UI;

namespace CraftingSystem
{
    public class RecipeInformation : MonoBehaviour
    {
        public RecipeData recipeData;
        public Image craftingSlotQImage;
        private void Start()
        {
            // Ensure that the RecipeData is assigned before using it
            if (!recipeData)
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
            var loadedSprite = Resources.Load<Sprite>(recipeData.firstIngredientSpriteName);
            craftingSlotQImage.sprite = loadedSprite;
        }
    }
}
