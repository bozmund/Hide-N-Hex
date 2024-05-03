using UnityEngine;

public class PlantDisapears : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to InventoryManager for updating the UI

    private int firebloom = 0;
    private int icecap = 0;
    private int sorrowmoss = 0;
    private int blindweed = 0;
    private int sungrass = 0;
    private int earthroot = 0;
    private int fadeleaf = 0;
    private int rotberry = 0;

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
        switch (itemName)
        {
            case "firebloom":
                firebloom++;
                break;
            case "icecap":
                icecap++;
                break;
            case "sorrowmoss":
                sorrowmoss++;
                break;
            case "blindweed":
                blindweed++;
                break;
            case "sungrass":
                sungrass++;
                break;
            case "earthroot":
                earthroot++;
                break;
            case "fadeleaf":
                fadeleaf++;
                break;
            case "rotberry":
                rotberry++;
                break;
        }
    }

    private int GetItemCount(string itemName)
    {
        switch (itemName)
        {
            case "firebloom":
                return firebloom;
            case "icecap":
                return icecap;
            case "sorrowmoss":
                return sorrowmoss;
            case "blindweed":
                return blindweed;
            case "sungrass":
                return sungrass;
            case "earthroot":
                return earthroot;
            case "fadeleaf":
                return fadeleaf;
            case "rotberry":
                return rotberry;
            default:
                return 0;
        }
    }
}
