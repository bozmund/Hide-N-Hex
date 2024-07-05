using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCaveHall : MonoBehaviour
{
    public GameObject objectToToggle;
    private bool playerInTrigger = false;

    void Update()
    {
        // Check if the player is in the trigger and the F key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // Load the BearCave scene
            GameObject musicObject = GameObject.Find("BackgroundMusicMain");
            if (musicObject != null)
            {
                // Destroy the GameObject
                Destroy(musicObject);
            }
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
            playerInTrigger = false;
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(false); // Set the object to inactive when the player is not in range
            }
        }
    }
}
