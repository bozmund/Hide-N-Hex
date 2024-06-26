using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCaveHall : MonoBehaviour
{
    private bool playerInTrigger = false;

    void Update()
    {
        // Check if the player is in the trigger and the F key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // Load the BearCave scene
            SceneManager.LoadScene("BearCave");
        }
    }

    // Called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    // Called when another collider exits the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
