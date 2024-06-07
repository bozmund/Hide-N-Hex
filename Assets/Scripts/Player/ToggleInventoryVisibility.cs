using UnityEngine;
using UnityEngine.UI;

public class ToggleInventoryVisibility : MonoBehaviour
{
    // Reference to the EntireInventory Image
    public Image entireInventory;

    // Keeps track of whether the EntireInventory is currently visible or not
    private bool isVisible;

    void Start()
    {
        // Ensure the EntireInventory is not visible at the start
        entireInventory.gameObject.SetActive(isVisible);
    }

    void Update()
    {
        // Check if the 'i' key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Toggle the visibility
            isVisible = !isVisible;

            // Set the active state of the EntireInventory image
            entireInventory.gameObject.SetActive(isVisible);
        }
    }
}
