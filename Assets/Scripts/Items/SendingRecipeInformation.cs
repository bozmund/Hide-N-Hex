using CraftingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Items
{
    public class SendingRecipeInformation : MonoBehaviour
    {
        public GameObject firstRecipe; // Reference to the parent object
        public RecipeData recipeData;

        public void OnButtonClick()
        {
            // Clear previous data
            recipeData.potionName = "";
            recipeData.potionSpriteName = "";
            recipeData.firstIngredientSpriteName = "";
            recipeData.secondIngredientSpriteName = "";
            recipeData.thirdIngredientSpriteName = "";

            // Iterate through the children of firstRecipe
            foreach (Transform child in firstRecipe.transform)
            {
                // Check if the child is a Unity UI Image
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    // Check if the child is not the button
                    if (child.GetComponent<Button>() == null)
                    {
                        // Check if the sprite name contains "Potion"
                        if (image.sprite.name.Contains("Potion"))
                        {
                            recipeData.potionSpriteName = image.sprite.name;
                        }
                        else
                        {
                            // Check which ingredient slot this is and assign the sprite name accordingly
                            if (string.IsNullOrEmpty(recipeData.firstIngredientSpriteName))
                            {
                                recipeData.firstIngredientSpriteName = image.sprite.name;
                            }
                            else if (string.IsNullOrEmpty(recipeData.secondIngredientSpriteName))
                            {
                                recipeData.secondIngredientSpriteName = image.sprite.name;
                            }
                            else if (string.IsNullOrEmpty(recipeData.thirdIngredientSpriteName))
                            {
                                recipeData.thirdIngredientSpriteName = image.sprite.name;
                            }
                        }
                    }
                }

                // Check if the child is a TextMeshPro object
                TextMeshProUGUI textMeshPro = child.GetComponent<TextMeshProUGUI>();
                if (textMeshPro != null)
                {
                    // Store the text of the TextMeshPro object as potionName
                    recipeData.potionName = textMeshPro.text;
                }
            }

            // Log the gathered information
            // Debug.Log("Potion Name: " + recipeData.potionName);
            // Debug.Log("Potion Sprite Name: " + recipeData.potionSpriteName);
            // Debug.Log("First Ingredient Sprite Name: " + recipeData.firstIngredientSpriteName);
            // Debug.Log("Second Ingredient Sprite Name: " + recipeData.secondIngredientSpriteName);
            // Debug.Log("Third Ingredient Sprite Name: " + recipeData.thirdIngredientSpriteName);

            SceneManager.LoadScene("CriclelFill");
        }
    }
}
