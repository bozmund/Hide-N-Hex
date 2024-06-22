using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class StartGameButtonHandler : MonoBehaviour
    {
        public PlayerPosition playerPositionData;

        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var startGameButton = root.Q<Button>("StartGame");
            startGameButton.clicked += StartGame;
        }

        private void StartGame()
        {
            // Check if the playerPositionData has valid scene information
            if (playerPositionData != null && !string.IsNullOrEmpty(playerPositionData.sceneName))
            {
                // Register a callback to handle scene loaded event
                SceneManager.sceneLoaded += OnSceneLoaded;

                // Load the scene stored in playerPositionData
                SceneManager.LoadScene(playerPositionData.sceneName);
            }
            else
            {
                // Load default scene if no playerPositionData or no sceneName is set
                SceneManager.LoadScene("OutsideTheCabin");
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Unregister the callback to prevent it from being called multiple times
            SceneManager.sceneLoaded -= OnSceneLoaded;

            // Log the loaded scene name for debugging
            Debug.Log($"Loaded scene: {scene.name}");

            // Find the Player GameObject in the newly loaded scene
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                // Log that the Player GameObject is found
                Debug.Log("Player GameObject found.");

                // Check if the loaded scene matches the stored sceneName in playerPositionData
                if (scene.name == playerPositionData.sceneName)
                {
                    // Set the position of the Player GameObject
                    player.transform.position = playerPositionData.position;

                    // Log the position to confirm it's being set correctly
                    Debug.Log($"Player position set to: {playerPositionData.position}");
                }
                else
                {
                    Debug.LogWarning($"Scene {scene.name} does not match stored sceneName {playerPositionData.sceneName}. Player position not set.");
                }
            }
            else
            {
                Debug.LogWarning("Player GameObject not found in the loaded scene.");
            }
        }
    }
}
