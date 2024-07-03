using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class CheckIngredientCount : MonoBehaviour
    {
        // Public variables to be set in the Inspector
        public string itemName;
        public int count;

        // Reference to the MainInventory ScriptableObject
        public MainInventory mainInventory;

        // Reference to the Image component
        private Image _imageComponent;

        // Sprites
        private Sprite _checkSymbol;
        private Sprite _xSymbol;

        // Start is called before the first frame update
        private void Start()
        {
            // Load the sprites from the Resources folder
            _checkSymbol = Resources.Load<Sprite>("Icons/checksymbol");
            _xSymbol = Resources.Load<Sprite>("Icons/xsymbol");

            // Get the Image component attached to the GameObject
            _imageComponent = GetComponent<Image>();

            if (_imageComponent == null)
            {
                Debug.LogError("No Image component found on this GameObject.");
                return;
            }

            // Check the ingredient count at the start
            CheckIngredient();
        }

        // Update is called once per frame
        private void Update()
        {
            // Continuously check the ingredient count
            CheckIngredient();
        }

        // Method to check the ingredient count
        private void CheckIngredient()
        {
            if (mainInventory != null)
            {
                int itemCount = mainInventory.GetSlotAndCountForItem(itemName, out _);

                if (itemCount >= count)
                {
                    // Debug.Log($"Sufficient {itemName} found. Count: {itemCount}");
                    _imageComponent.sprite = _checkSymbol;
                }
                else
                {
                    Debug.LogWarning($"Not enough {itemName}. Required: {count}, Available: {itemCount}");
                    _imageComponent.sprite = _xSymbol;
                }
            }
            else
            {
                Debug.LogError("MainInventory ScriptableObject is not assigned.");
                _imageComponent.sprite = _xSymbol;
            }
        }
    }
}
