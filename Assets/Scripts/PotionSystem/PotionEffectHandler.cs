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
            var potionEffects = new PotionEffects(_playerMovement);
            switch (potionName)
            {
                case "CatcallingPotion":
                    potionEffects.ApplyCatcalling();
                    break;
                case "ClothingPotion":
                    potionEffects.ApplyClothing();
                    break;
                case "ConfusionPotion":
                    potionEffects.ApplyConfusion();
                    break;
                case "HealingPotion":
                    potionEffects.ApplyHealing();
                    break;
                case "HolyGrailPotion":
                    potionEffects.ApplyHolyGrail();
                    break;
                case "InvisibilityPotion":
                    potionEffects.ApplyInvisibility();
                    break;
                case "LevitationPotion":
                    potionEffects.ApplyLevitation();
                    break;
                case "LiquidFlamePotion":
                    potionEffects.ApplyLiquidFlame();
                    break;
                case "LowerSusPotion":
                    potionEffects.ApplyLowerSus();
                    break;
                case "MightPotion":
                    potionEffects.ApplyMight();
                    break;
                case "MindVisionPotion":
                    potionEffects.ApplyMindVision();
                    break;
                case "ParalyticGasPotion":
                    potionEffects.ApplyParalyticGas();
                    break;
                case "PurificationPotion":
                    potionEffects.ApplyPurification();
                    break;
                case "RecallPotion":
                    potionEffects.ApplyRecall();
                    break;
                case "StrengthPotion":
                    potionEffects.ApplyStrength();
                    break;
                case "SwiftnessPotion":
                    potionEffects.ApplySwiftness();
                    break;
                case "UsefulnessPotion":
                    potionEffects.ApplyUsefulness();
                    break;
                case "WeaknessPotion":
                    potionEffects.ApplyWeakness();
                    break;
                default:
                    Debug.LogError("Unknown potion: " + potionName);
                    break;
            }
        }
    }
}