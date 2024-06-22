using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public PlayerPosition playerPositionData;  // Reference to the ScriptableObject

    public void QuitGame()
    {
        // Find the player GameObject with tag "Player"
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Get the player's transform
            Transform playerTransform = player.transform;

            // Save the player's state before quitting
            string currentSceneName = SceneManager.GetActiveScene().name;
            Vector3 currentPosition = playerTransform.position;
            playerPositionData.SaveState(currentSceneName, currentPosition);
        }
        else
        {
            Debug.LogWarning("Player GameObject not found with tag 'Player'. Player state not saved.");
        }

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
