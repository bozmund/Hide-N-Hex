using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Items
{
    public class PlantDisappears : MonoBehaviour
    {
        public MainInventory MainInventoryData; // Reference to the ScriptableObject
        public InventoryUIManager inventoryUIManager; // Reference to the InventoryUIManager script
        public HealthValue healthValue;

        private void Start()
        {
            PlayerPrefs.SetInt("canColect", 0);
            PlayerPrefs.SetInt("canMultiply", 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DisappearObject"))
            {
                string itemName = collision.gameObject.name;
                itemName = Regex.Replace(itemName, @" \(\d+\)$", "").Trim();

                int canColect = PlayerPrefs.GetInt("canColect");
                int canMultiply = PlayerPrefs.GetInt("canMultiply");

                if (canColect == 0 && (itemName == "sungrass" || itemName == "dewcatcher"))
                {
                    healthValue.fillAmount -= 0.1f;
                }

                if (!string.IsNullOrEmpty(itemName))
                {
                    string itemNumber;
                    int itemCount = MainInventoryData.GetSlotAndCountForItem(itemName, out itemNumber);

                    if (canMultiply == 1)
                    {
                        itemCount += 2;
                    }

                    else
                    {
                        itemCount += 1;
                    }

                    MainInventoryData.UpdateMainInventory(itemNumber, itemName, itemCount);

                    // Log the entire dictionary
                    LogMainInventory();

                    // Refresh the inventory UI
                    inventoryUIManager.LoadInventorySprites();

                    // Make the object disappear after collecting it
                    collision.gameObject.SetActive(false);
                }
            }
        }

        private void LogMainInventory()
        {
            Dictionary<string, MainInventory.InventorySlot> inventory = MainInventoryData.GetMainInventory();
            foreach (var kvp in inventory)
            {
                // Debug.Log($"Slot: {kvp.Key}, Item: {kvp.Value.itemName}, Count: {kvp.Value.count}");
            }
        }
    }
}
