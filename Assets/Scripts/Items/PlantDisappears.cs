using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Player;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Items
{
    public class PlantDisappears : MonoBehaviour
    {
        public MainInventory MainInventoryData; // Reference to the ScriptableObject
        public InventoryUIManager inventoryUIManager; // Reference to the InventoryUIManager script
        public HealthValue healthValue;
        private PlayerMovement _player;

        private void Start()
        {
            PlayerPrefs.SetInt("canColect", 0);
            PlayerPrefs.SetInt("canMultiply", 0);
            _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DisappearObject"))
            {
                string itemName = collision.gameObject.name;
                itemName = Regex.Replace(itemName, @" \(\d+\)$", "").Trim();

                int canColect = PlayerPrefs.GetInt("canColect");
                int canMultiply = PlayerPrefs.GetInt("canMultiply");

                if (canColect == 0 && (itemName == "firebloom" || itemName == "stormvine" || itemName == "sorrowmoss"))
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

                    if (_player.strength == 2)   // 0,1(strength) and 2(might)  
                    {
                        Debug.Log("STRONG");
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
