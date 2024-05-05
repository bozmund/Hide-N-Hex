using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using System.Collections.Generic;

[System.Serializable]
public class ItemSpriteMapping
{
    public string itemName; // The item name key
    public Sprite itemSprite; // The sprite value
}


[System.Serializable]
public class InventorySlot
{
    public Image slotImage; // The image for the slot
    public TextMeshProUGUI slotText; // Updated to TextMeshProUGUI
}

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> inventorySlots; // List of inventory slots
    public List<ItemSpriteMapping> itemSpriteMappings; // List of item-to-sprite mappings

    private int currentSlot = 0; // Tracks which slot to use next

    public string GetItemNameFromSprite(string spriteName)
    {
        switch (spriteName)
        {
            case "plants_0":
                return "firebloom";
            case "plants_1":
                return "icecap";
            case "plants_2":
                return "sorrowmoss";
            case "plants_3":
                return "blindweed";
            case "plants_4":
                return "sungrass";
            case "plants_5":
                return "earthroot";
            case "plants_6":
                return "fadeleaf";
            case "plants_7":
                return "rotberry";
            default:
                return string.Empty; // Return empty if no match is found
        }
    }

    public void AddItemSprite(string itemName)
    {
        if (currentSlot >= inventorySlots.Count)
        {
            Debug.Log("No available slots in inventory");
            return;
        }

        // Find the sprite corresponding to the item name
        Sprite itemSprite = null;
        foreach (var mapping in itemSpriteMappings)
        {
            if (mapping.itemName == itemName)
            {
                itemSprite = mapping.itemSprite;
                break;
            }
        }

        if (itemSprite == null)
        {
            Debug.LogWarning($"Sprite not found for item: {itemName}");
            return;
        }

        // Check if the sprite is already in any slot
        foreach (var slot in inventorySlots)
        {
            if (slot.slotImage.sprite == itemSprite) // Sprite already exists in a slot
            {
                Debug.LogWarning($"Sprite for '{itemName}' is already in the inventory.");
                return; // Avoid adding duplicate
            }
        }

        // Assign the sprite to the next available slot
        if (currentSlot < inventorySlots.Count)
        {
            Debug.Log($"Adding sprite for '{itemName}' to slot {currentSlot}");
            inventorySlots[currentSlot].slotImage.sprite = itemSprite; // Assign the sprite
            currentSlot++; // Move to the next slot
        }
    }

    public void UpdateItemCount(string itemName, int count)
    {
        Debug.Log($"Updating count for '{itemName}' to {count}");

        bool slotFound = false;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            var slot = inventorySlots[i];

            if (slot.slotImage.sprite != null)
            {
                string spriteName = slot.slotImage.sprite.name;
                string expectedItemName = GetItemNameFromSprite(spriteName);

                Debug.Log($"Comparing: slot {i} sprite name '{spriteName}' with expected item name '{expectedItemName}'");

                if (expectedItemName == itemName)
                {
                    slotFound = true;
                    if (slot.slotText != null)
                    {
                        slot.slotText.text = count.ToString(); // Update the text with the item count
                        Debug.Log($"Updated text for '{itemName}' in slot {i}.");
                    }
                    else
                    {
                        Debug.LogWarning($"Slot text is null for '{itemName}' in slot {i}.");
                    }
                    break; // Exit the loop once the correct slot is found
                }
            }
        }

        if (!slotFound)
        {
            Debug.LogWarning($"No slot found for '{itemName}' to update count.");
        }
    }
}
