using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveCabin : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject objectToToggle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(true); // Set the object to active when the player is in range
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(false); // Set the object to inactive when the player is not in range
            }
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // Load the new scene
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("OutsideTheCabin");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the one we expect
        if (scene.name == "OutsideTheCabin")
        {
            // Find the Player game object
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                // Set the player's position
                player.transform.position = new Vector3(0.3726141f, -0.7669999f, player.transform.position.z);
            }

            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
