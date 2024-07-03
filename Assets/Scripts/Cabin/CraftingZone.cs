using UnityEngine;
using UnityEngine.SceneManagement;

public class CraftingZone: MonoBehaviour
{
    public GameObject objectToToggle;

    // Called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(true); // Set the object to active when the player is in range
            }
        }
    }

    // Called when another collider exits the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(false); // Set the object to inactive when the player is not in range
            }
        }
    }
}
