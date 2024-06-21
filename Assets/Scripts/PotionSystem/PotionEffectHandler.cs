

using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace PotionSystem
{
    public class PotionEffectHandler : Singleton<PotionEffectHandler>
    {
        private PlayerMovement _playerMovement;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UpdatePlayerReference();
        }

        private void UpdatePlayerReference()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            if (_playerMovement == null)
            {
                Debug.LogError("PlayerMovement component not found in the scene.");
            }
        }

        public void Handle(string potionName)
        {
            switch (potionName)
            {
                
            }
        }
    }
}
