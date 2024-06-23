using UnityEngine;
using UnityEngine.UI;

public class CheckIngredientCount : MonoBehaviour
{
    // Public variables to be set in the Inspector
    public string itemName;
    public int count;

    // Reference to the MainInventory ScriptableObject
    public MainInventory mainInventory;

    // Reference to the Image component
    private Image imageComponent;

    // Sprites
    private Sprite checkSymbol;
    private Sprite xSymbol;

    // Start is called before the first frame update
    void Start()
    {
        // Load the sprites from the Resources folder
        checkSymbol = Resources.Load<Sprite>("Icons/checksymbol");
        xSymbol = Resources.Load<Sprite>("Icons/xsymbol");

        // Get the Image component attached to the GameObject
        imageComponent = GetComponent<Image>();

        if (imageComponent == null)
        {
            Debug.LogError("No Image component found on this GameObject.");
            return;
        }

        // Check the ingredient count at the start
        CheckIngredient();
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously check the ingredient count
        CheckIngredient();
    }

    // Method to check the ingredient count
    void CheckIngredient()
    {
        if (mainInventory != null)
        {
            string itemNumber;
            int itemCount = mainInventory.GetSlotAndCountForItem(itemName, out itemNumber);

            if (itemCount >= count)
            {
                Debug.Log($"Sufficient {itemName} found. Count: {itemCount}");
                imageComponent.sprite = checkSymbol;
            }
            else
            {
                Debug.LogWarning($"Not enough {itemName}. Required: {count}, Available: {itemCount}");
                imageComponent.sprite = xSymbol;
            }
        }
        else
        {
            Debug.LogError("MainInventory ScriptableObject is not assigned.");
            imageComponent.sprite = xSymbol;
        }
    }
}
