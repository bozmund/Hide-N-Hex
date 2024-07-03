using UnityEngine;
using UnityEngine.UI;

namespace CraftingSystem
{
    public class RecipeInformation : MonoBehaviour
    {
        public RecipeData recipeData;

        public Image craftingSlotQImage;
        public Image craftingSlotWImage;
        public Image craftingSlotEImage;

        public Image ResultImage;
        public Image FirstIngridientImage;
        public Image SecondIngridientImage;
        public Image ThirdIngridientImage;

        private void Start()
        {
            // Ensure that the RecipeData is assigned before using it
            if (!recipeData)
            {
                Debug.LogError("RecipeData is not assigned. Please assign it in the Inspector.");
                return;
            }


            // Use the retrieved information as needed
            //Debug.Log("Recipe Information Retrieved:");
            //Debug.Log("Potion Name: " + recipeData.potionName);
            //Debug.Log("Potion Sprite Name: " + recipeData.potionSpriteName);
            //Debug.Log("First Ingredient Sprite Name: " + recipeData.firstIngredientSpriteName);
            //Debug.Log("Second Ingredient Sprite Name: " + recipeData.secondIngredientSpriteName);
            //Debug.Log("Third Ingredient Sprite Name: " + recipeData.thirdIngredientSpriteName);

            craftingSlotQImage.sprite = Resources.Load<Sprite>("Plants/" + recipeData.firstIngredientSpriteName);
            craftingSlotWImage.sprite = Resources.Load<Sprite>("Plants/" + recipeData.secondIngredientSpriteName);
            craftingSlotEImage.sprite = Resources.Load<Sprite>("Plants/" + recipeData.thirdIngredientSpriteName);

            ResultImage.sprite = Resources.Load<Sprite>("Potions/" + recipeData.potionSpriteName);
            FirstIngridientImage.sprite = Resources.Load<Sprite>("Plants/" + recipeData.firstIngredientSpriteName);
            SecondIngridientImage.sprite = Resources.Load<Sprite>("Plants/" + recipeData.secondIngredientSpriteName);
            ThirdIngridientImage.sprite = Resources.Load<Sprite>("Plants/" + recipeData.thirdIngredientSpriteName);

        }
    }
}
