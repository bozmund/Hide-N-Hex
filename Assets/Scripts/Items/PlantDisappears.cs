using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Player;

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
            PlayerPrefs.SetInt("canColectFire", 0);
            PlayerPrefs.SetInt("canColectFrozen", 0);
            PlayerPrefs.SetInt("canMultiply", 0);
            _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DisappearObject"))
            {
                string itemName = collision.gameObject.name;
                itemName = Regex.Replace(itemName, @" \(\d+\)$", "").Trim();

                int canColectFire = PlayerPrefs.GetInt("canColectFire");
                int canColectFrozen = PlayerPrefs.GetInt("canColectFrozen");
                int canMultiply = PlayerPrefs.GetInt("canMultiply");
                
                if (canColectFire == 0 && (itemName == "firebloom" || itemName == "stormvine"))
                {
                    healthValue.fillAmount -= 0.1f;
                }

                if (canColectFrozen == 0 && (itemName == "icecap" || itemName == "mageroyal"))
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
                    //Debug.Log(_player.strength);
                    /*
                     if (_player.strength == 0 && itemName != "starflower" && itemName != "stormvine" && itemName != "goldenLotus" && itemName != "blandfruit" && itemName != "starflower" && itemName != "starflower")
                     {
                         MainInventoryData.UpdateMainInventory(itemNumber, itemName, itemCount);

                         // Refresh the inventory UI
                         inventoryUIManager.LoadInventorySprites();

                         // Make the object disappear after collecting it
                         collision.gameObject.SetActive(false);
                     }

                     else if (_player.strength == 1 && itemName != "starflower" && itemName != "goldenLotus")
                     {
                         MainInventoryData.UpdateMainInventory(itemNumber, itemName, itemCount);

                         // Refresh the inventory UI
                         inventoryUIManager.LoadInventorySprites();

                         // Make the object disappear after collecting it
                         collision.gameObject.SetActive(false);
                     }

                     else 
                     {
                         MainInventoryData.UpdateMainInventory(itemNumber, itemName, itemCount);

                         // Refresh the inventory UI
                         inventoryUIManager.LoadInventorySprites();

                         // Make the object disappear after collecting it
                         collision.gameObject.SetActive(false);
                     }
                    */
                    MainInventoryData.UpdateMainInventory(itemNumber, itemName, itemCount);

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
