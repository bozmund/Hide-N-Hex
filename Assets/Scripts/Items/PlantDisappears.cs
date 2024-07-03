using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Items
{
    public class PlantDisappears : MonoBehaviour
    {
        public MainInventory MainInventoryData; // Reference to the ScriptableObject
        public InventoryUIManager inventoryUIManager; // Reference to the InventoryUIManager script

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DisappearObject"))
            {
                string itemName = collision.gameObject.name;
                itemName = Regex.Replace(itemName, @" \(\d+\)$", "").Trim();

                if (!string.IsNullOrEmpty(itemName))
                {
                    string itemNumber;
                    int itemCount = MainInventoryData.GetSlotAndCountForItem(itemName, out itemNumber);
                    itemCount += 1;
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
