using System.Collections;
using UnityEngine;
using Player; // Ensure this namespace is accessible for PlayerMovement

namespace PotionSystem
{
    public class PotionEffectHandler : MonoBehaviour
    {
        public static PotionEffectHandler Instance { get; private set; }
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            // Ensure that PlayerMovement is found after loading a new scene
            UpdatePlayerReference();
        }

        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            UpdatePlayerReference();
        }

        private void UpdatePlayerReference()
        {
            // Find the PlayerMovement component in the scene
            _playerMovement = FindObjectOfType<PlayerMovement>();
            if (_playerMovement == null)
            {
                Debug.LogError("PlayerMovement component not found in the scene.");
            }
        }

        public void Handle(string potionName)
        {
            // Match the potion name with its effect
            switch (potionName)
            {
                case "CrimsonPotion":
                    StartCoroutine(ApplySpeed());
                    break;
                case "HealthPotion":
                    ApplyHealth();
                    break;
                case "SpeedPotion":
                    StartCoroutine(ApplySpeed());
                    break;
                // Add more cases for different potions
                default:
                    Debug.LogWarning("Potion not recognized.");
                    break;
            }
        }

        private void ApplyHealth()
        {
            if (_playerMovement)
            {
                // Apply health effect to the player
                Debug.Log("Health potion consumed. Health restored!");
                // Implement health restoration logic here, for example:
                // _playerMovement.Health += 50; // Assuming you have a Health attribute in PlayerMovement
            }
        }

        private IEnumerator ApplySpeed()
        {
            if (!_playerMovement) yield break;
            // Apply speed effect to the player for a duration
            Debug.Log("Speed potion consumed. Player is now faster!");
                
            _playerMovement._movementSpeed *= 2; // Double the player's speed
            yield return new WaitForSeconds(10); // Effect lasts for 10 seconds
                
            _playerMovement._movementSpeed /= 2; // Revert speed increase
            Debug.Log("Speed effect has worn off.");
        }

        // Add more methods for different potion effects
    }
}
