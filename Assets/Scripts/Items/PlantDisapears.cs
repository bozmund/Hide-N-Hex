using System.Collections.Generic;
using UnityEngine;

public class PlantDisapears : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to InventoryManager for updating the UI
    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    private void Awake()
    {
        // Initialize the dictionary with default values
        itemCounts.Add("firebloom", 0);
        itemCounts.Add("icecap", 0);
        itemCounts.Add("sorrowmoss", 0);
        itemCounts.Add("blindweed", 0);
        itemCounts.Add("sungrass", 0);
        itemCounts.Add("earthroot", 0);
        itemCounts.Add("fadeleaf", 0);
        itemCounts.Add("rotberry", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DisappearObject"))
        {
            string itemName = GetItemNameFromCollision(collision.gameObject.name);

            if (!string.IsNullOrEmpty(itemName))
            {
                IncrementItemCounter(itemName);
                inventoryManager.AddItemSprite(itemName); // Add sprite to inventory if not already there
                inventoryManager.UpdateItemCount(itemName, GetItemCount(itemName)); // Update item count in UI

                // Make the object disappear after collecting it
                Destroy(collision.gameObject);
            }
        }
    }

    private string GetItemNameFromCollision(string collisionName)
    {
        if (collisionName.Contains("FirebloomPlant"))
            return "firebloom";
        if (collisionName.Contains("IcecapPlant"))
            return "icecap";
        if (collisionName.Contains("SorrowmossPlant"))
            return "sorrowmoss";
        if (collisionName.Contains("BlindweedPlant"))
            return "blindweed";
        if (collisionName.Contains("SungrassPlant"))
            return "sungrass";
        if (collisionName.Contains("EarthrootPlant"))
            return "earthroot";
        if (collisionName.Contains("FadeleafPlant"))
            return "fadeleaf";
        if (collisionName.Contains("RotberryPlant"))
            return "rotberry";

        return string.Empty; // Return empty if no match found
    }

    private void IncrementItemCounter(string itemName)
    {
        if (itemCounts.ContainsKey(itemName))
        {
            itemCounts[itemName]++;
        }
    }

    public int GetItemCount(string itemName)
    {
        if (itemCounts.ContainsKey(itemName))
        {
            return itemCounts[itemName];
        }
        else
        {
            return 0;
        }
    }

    public Dictionary<string, int> GetAllItemCounts()
    {
        return itemCounts;
    }

    public void ResetItemCount(string itemName)
    {
        if (itemCounts.ContainsKey(itemName))
        {
            itemCounts[itemName] = 0; // Set the value of the specified key to 0
            Debug.Log($"Reset count for '{itemName}' to 0.");
        }
        else
        {
            Debug.LogWarning($"Item '{itemName}' not found in the dictionary.");
        }
    }
}
