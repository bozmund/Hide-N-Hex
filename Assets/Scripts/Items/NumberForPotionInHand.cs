using UnityEngine;

public class NumberForPotionInHand : MonoBehaviour
{
    public PotionInHand potionInHand; // Public reference to the PotionInHand ScriptableObject
    public MainInventory mainInventory; // Public reference to the MainInventory ScriptableObject

    void Update()
    {
        // Check if a number key from 1 to 8 is pressed
        for (int i = 1; i <= 8; i++)
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i)))
            {
                SetPotionInHand(i - 1); // Subtract 1 because list is 0-based
                break;
            }
        }
    }

    void SetPotionInHand(int index)
    {
        if (index < mainInventory.slots.Count)
        {
            string itemName = mainInventory.slots[index].itemName;

            // Check if the itemName contains "Potion"
            if (itemName.Contains("Potion"))
            {
                potionInHand.potionName = itemName;
                Debug.Log("Potion in hand set to: " + potionInHand.potionName);
            }
            else
            {
                potionInHand.potionName = "";
            }
        }
        else
        {
            Debug.LogWarning("Index out of range for MainInventory slots.");
        }
    }
}