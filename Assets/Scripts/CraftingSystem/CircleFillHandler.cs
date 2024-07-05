using Items;
using PotionSystem;
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
        private bool _hasCrafted;

        private void Start()
        {
            circleFillImage.fillAmount = 0.0f;
            Heat.fillAmount = 0;
            // Randomly place the OptimalHeat image within 0 to 80% of the HeatBar
            float optimalPositionY = Random.Range(-25, 25);
            optimalHeatPercentage = (optimalPositionY + 25) / 50;
            OptimalHeat.rectTransform.anchoredPosition =
                new Vector2(OptimalHeat.rectTransform.anchoredPosition.x, optimalPositionY);
            var mainInventory = GameObject.Find("MainInventory");
            var entireInventory = GameObject.Find("EntireInventory");
            mainInventory.SetActive(false);
            entireInventory.SetActive(false);
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
                    Q.color = Color.white;
                else if (Input.GetKeyDown(KeyCode.Q) && circleFillImage.fillAmount is < 0.1f or > 0.25f)
                    PotionEffects.ApplyMoreSus(true);

                if (Input.GetKeyDown(KeyCode.W) && circleFillImage.fillAmount is >= 0.75f and <= 0.9f)
                    W.color = Color.white;
                else if (Input.GetKeyDown(KeyCode.W) && circleFillImage.fillAmount is < 0.75f or > 0.9f)
                    PotionEffects.ApplyMoreSus(true);

                if (Input.GetKeyDown(KeyCode.E) && circleFillImage.fillAmount is >= 0.4f and <= 0.6f)
                    E.color = Color.white;
                else if (Input.GetKeyDown(KeyCode.E) && circleFillImage.fillAmount is < 0.4f or > 0.6f)
                    PotionEffects.ApplyMoreSus(true);
            }

            Heat.fillAmount -= decreaseRate * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                Heat.fillAmount += increaseRate * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                ChangeScene();
            }

            Heat.fillAmount = Mathf.Clamp(Heat.fillAmount, 0, 1);

            if (!E.color.Equals(Color.white) || !Q.color.Equals(Color.white) || !W.color.Equals(Color.white)) return;
            Craft(recipeData.potionSpriteName, recipeData.firstIngredientSpriteName,
                recipeData.secondIngredientSpriteName, recipeData.thirdIngredientSpriteName);
        }

        private void Craft(string potionSpriteName, string firstIngredient, string secondIngredient,
            string thirdIngredient)
        {
            if (_hasCrafted) return;
            
            var potionCount = MainInventoryData.GetSlotAndCountForItem(potionSpriteName, out var itemNumber);
            potionCount += 1;
            MainInventoryData.UpdateMainInventory(itemNumber, potionSpriteName, potionCount);
            SubtractIngredient(firstIngredient);
            SubtractIngredient(secondIngredient);
            SubtractIngredient(thirdIngredient);

            _hasCrafted = true;
            
            ChangeScene();
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene("Cabin");
            inventoryUI.gameObject.SetActive(true);
        }

        private void SubtractIngredient(string ingredient)
        {
            var countForItem = MainInventoryData.GetSlotAndCountForItem(ingredient, out var itemNumber);
            countForItem -= 1;

            if (countForItem == 0)
            {
                Debug.Log("Is zero");
                Debug.Log(itemNumber);
                MainInventoryData.UpdateMainInventory(itemNumber, "", countForItem);
                inventoryUI.ClearUIElement(itemNumber);
            }
            else
            {
                MainInventoryData.UpdateMainInventory(itemNumber, ingredient, countForItem);
            }
        }
    }
}