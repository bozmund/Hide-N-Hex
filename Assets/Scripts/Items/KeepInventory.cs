using UnityEngine;

public class LoadInventoryCanvas : MonoBehaviour
{
    public string inventoryUIObjectName = "InventoryRecipeUI"; // Name of the InventoryUIManager GameObject

    void Start()
    {
        Debug.Log("LoadInventoryCanvas Start");

        // Check if the InventoryUIManager needs to be created
        if (!PlayerPrefs.HasKey("InventoryUIManagerCreated"))
        {
            Debug.Log("InventoryUIManager not found in PlayerPrefs. Creating new instance.");

            // If it hasn't been created, instantiate it from the scene
            GameObject inventoryUI = GameObject.Find(inventoryUIObjectName);

            // If it's not found, you can instantiate it here
            if (inventoryUI == null)
            {
                Debug.LogWarning("InventoryUIManager GameObject not found. Creating a new instance.");
                inventoryUI = new GameObject(inventoryUIObjectName);
            }

            // Optionally, you can parent it to a specific GameObject if needed
            // inventoryUI.transform.parent = transform;

            // Set the PlayerPrefs flag to indicate it has been created
            PlayerPrefs.SetInt("InventoryUIManagerCreated", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("InventoryUIManager found in PlayerPrefs.");
        }
    }
}
