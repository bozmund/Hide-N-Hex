using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class RemoveSlotContent : MonoBehaviour, IPointerClickHandler
{
    // Reference to the item image within the slot
    public Image itemImage;

    // Reference to the TextMeshPro component within the item
    public TextMeshProUGUI itemText;

    // Reference to the PlantDisapears script
    private PlantDisapears plantDisapearsScript;
    private InventoryManager inventoryManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign the PlantDisapears script
        plantDisapearsScript = FindObjectOfType<PlantDisapears>();
        if (plantDisapearsScript == null)
        {
            Debug.LogError("PlantDisapears script not found in the scene.");
        }

        inventoryManagerScript = FindObjectOfType<InventoryManager>();
        if (inventoryManagerScript == null)
        {
            Debug.LogError("InventoryManager script not found in the scene.");
        }
    }

    // Method called when the slot is right-clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if it's a right-click (assuming mouse)
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Sprite name: " + (itemImage.sprite != null ? itemImage.sprite.name : "None"));

            if (inventoryManagerScript != null)
            {
                // Get the item name corresponding to the sprite name
                string itemName = inventoryManagerScript.GetItemNameFromSprite(itemImage.sprite != null ? itemImage.sprite.name : "");

                Debug.Log("Item name: " + itemName);
                plantDisapearsScript.ResetItemCount(itemName);
            }

            // Clear the sprite of the item image
            itemImage.sprite = null;

            // Clear the text of the TextMeshPro component
            itemText.text = "";

            // Check if the PlantDisapears script is found
            if (plantDisapearsScript != null)
            {
                // Get all item counts as a dictionary
                Dictionary<string, int> itemCounts = plantDisapearsScript.GetAllItemCounts();

                // Log the values of the dictionary
                foreach (var kvp in itemCounts)
                {
                    Debug.Log(kvp.Key + " is " + kvp.Value);
                }
            }
        }
    }
}
