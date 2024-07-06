using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCabin : MonoBehaviour
{
    public GameObject objectToToggle; // Public reference to the game object to be activated/deactivated
    private bool playerInRange = false;

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
            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Cabin");
        }
    }

    // This method will be called when the "Cabin" scene has loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Cabin")
        {
            // Log scene load
            Debug.Log("Cabin scene loaded");

            GameObject minorSounds = GameObject.Find("MinorSounds");

            if (minorSounds != null)
            {
                // Find the "DoorSound" GameObject inside "MinorSounds"
                GameObject doorSound = minorSounds.transform.Find("DoorSound").gameObject;

                if (doorSound != null)
                {
                    // Get the AudioSource component
                    AudioSource audioSource = doorSound.GetComponent<AudioSource>();

                    if (audioSource != null)
                    {
                        // Play the audio
                        audioSource.Play();
                    }
                    else
                    {
                        Debug.LogWarning("DoorSound GameObject does not have an AudioSource component.");
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find GameObject named 'DoorSound' inside 'MinorSounds'.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find GameObject named 'MinorSounds'.");
            }

            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

}
