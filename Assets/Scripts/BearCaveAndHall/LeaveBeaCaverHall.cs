using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveBearCaveHall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Find the GameObject named "BackgroundMusicCutscene"
            GameObject musicObject = GameObject.Find("BackgroundMusicForCave");

            // Check if the object exists
            if (musicObject != null)
            {
                // Destroy the GameObject
                Destroy(musicObject);
            }

            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load the scene
            SceneManager.LoadScene("OutsideTheCabin");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the desired scene
        if (scene.name == "OutsideTheCabin")
        {
            // Find the player object by its tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // Move the player to the desired position
                player.transform.position = new Vector3(66.40603f, 27.983f, player.transform.position.z);
            }

            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
