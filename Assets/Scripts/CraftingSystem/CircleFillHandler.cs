using Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CraftingSystem
{
    public class CircleFillHandler : MonoBehaviour
    {
        public Image circleFillImage;
        public RectTransform handlerEdgeImage;
        public RectTransform fillHandler;
        public Image Q;
        public Image W;
        public Image E;
        public Image Heat;
        public Image OptimalHeat;
        public RecipeData recipeData;
        public MainInventory MainInventoryData;
        private InventoryUIManager inventoryUI;

        private float fillSpeed = 0.3f;
        private float targetFillAmount = 1.0f;
        private float decreaseRate = 0.1f;
        private float increaseRate = 0.4f;
        private float optimalHeatPercentage;
        private float tolerance = 0.1f;

        private void Start()
        {
            circleFillImage.fillAmount = 0.0f;
            Heat.fillAmount = 0;
            // Randomly place the OptimalHeat image within 0 to 80% of the HeatBar
            float optimalPositionY = Random.Range(-25, 25);
            optimalHeatPercentage = (optimalPositionY + 25) / 50;
            OptimalHeat.rectTransform.anchoredPosition =
                new Vector2(OptimalHeat.rectTransform.anchoredPosition.x, optimalPositionY);
            inventoryUI = InventoryUIManager.Instance;
            inventoryUI.gameObject.SetActive(false);
        }

        private void Update()
        {
            circleFillImage.fillAmount += fillSpeed * Time.deltaTime;

            if (circleFillImage.fillAmount >= targetFillAmount)
            {
                circleFillImage.fillAmount = 0;
            }

            if (Mathf.Abs(Heat.fillAmount - optimalHeatPercentage) <= tolerance)
            {
                if (Input.GetKeyDown(KeyCode.Q) && circleFillImage.fillAmount is >= 0.1f and <= 0.25f)
                {
                    Q.color = Color.white;
                }

                if (Input.GetKeyDown(KeyCode.W) && circleFillImage.fillAmount is >= 0.75f and <= 0.9f)
                {
                    W.color = Color.white;
                }

                if (Input.GetKeyDown(KeyCode.E) && circleFillImage.fillAmount is >= 0.4f and <= 0.6f)
                {
                    E.color = Color.white;
                }
            }

            Heat.fillAmount -= decreaseRate * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                Heat.fillAmount += increaseRate * Time.deltaTime;
            }

            Heat.fillAmount = Mathf.Clamp(Heat.fillAmount, 0, 1);

            if (!E.color.Equals(Color.white) || !Q.color.Equals(Color.white) || !W.color.Equals(Color.white)) return;
            Craft(recipeData.potionSpriteName, recipeData.firstIngredientSpriteName, recipeData.secondIngredientSpriteName, recipeData.thirdIngredientSpriteName);
        }

        private void Craft(string potionSpriteName, string firstIngredient, string secondIngredient, string thirdIngredient)
        {
            var potionCount = MainInventoryData.GetSlotAndCountForItem(potionSpriteName, out var itemNumber);
            potionCount += 1;
            MainInventoryData.UpdateMainInventory(itemNumber, potionSpriteName, potionCount);
            SubtractIngredient(firstIngredient);
            SubtractIngredient(secondIngredient);
            SubtractIngredient(thirdIngredient);
            inventoryUI.LoadInventorySprites();
            SceneManager.LoadScene("OutsideTheCabin");
            inventoryUI.gameObject.SetActive(true);
        }

        private void SubtractIngredient(string ingredient)
        {
            var countForItem = MainInventoryData.GetSlotAndCountForItem(ingredient, out var itemNumber);
            countForItem -= 1;
            MainInventoryData.UpdateMainInventory(itemNumber, ingredient, countForItem);
        }
    }
}