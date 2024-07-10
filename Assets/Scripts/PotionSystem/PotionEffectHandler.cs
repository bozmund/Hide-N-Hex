using Items;
using Scriptable_Objects;
using UnityEngine;
using Utilities;

namespace PotionSystem
{
    public class PotionEffectHandler : Singleton<PotionEffectHandler>
    {
        [SerializeField]public CatCalled CatCalled;
        // ReSharper disable Unity.PerformanceAnalysis
        public PotionInHand potionInHand;
        public MainInventory MainInventoryData;
        public InventoryUIManager inventoryUIManager;
        public void Handle(string potionName)
        {
            var potionEffects = gameObject.AddComponent<PotionEffects>();
            switch (potionName)
            {
                case "CatcallingPotion":
                    potionEffects.ApplyCatcalling(CatCalled);
                    deletePotionFromInventory(potionName);
                    break;
                case "ConfusionPotion":
                    potionEffects.ApplyConfusion();
                    deletePotionFromInventory(potionName);
                    break;
                case "HealingPotion":
                    PotionEffects.ApplyHealing();
                    deletePotionFromInventory(potionName);
                    break;
                case "HolyGrailPotion":
                    potionEffects.ApplyHolyGrail();
                    deletePotionFromInventory(potionName);
                    break;
                case "InvisibilityPotion":
                    potionEffects.ApplyInvisibility();
                    deletePotionFromInventory(potionName);
                    break;
                case "LevitationPotion":
                    potionInHand.potionName = null;
                    potionEffects.ApplyLevitation();
                    deletePotionFromInventory(potionName);
                    break;
                case "LiquidFlamePotion":
                    PotionEffects.ApplyLiquidFlame();
                    deletePotionFromInventory(potionName);
                    break;
                case "LowerSusPotion":
                    PotionEffects.ApplyLowerSus();
                    deletePotionFromInventory(potionName);
                    break;
                case "MightPotion":
                    potionEffects.ApplyMight();
                    deletePotionFromInventory(potionName);
                    break;
                case "ParalyticGasPotion":
                    potionEffects.ApplyParalyticGas();
                    deletePotionFromInventory(potionName);
                    break;
                case "PurificationPotion":
                    potionEffects.ApplyPurification();
                    deletePotionFromInventory(potionName);
                    break;
                case "RecallPotion":
                    PotionEffects.ApplyRecall();
                    deletePotionFromInventory(potionName);
                    break;
                case "StrengthPotion":
                    potionEffects.ApplyStrength();
                    deletePotionFromInventory(potionName);
                    break;
                case "SwiftnessPotion":
                    potionEffects.ApplySwiftness();
                    deletePotionFromInventory(potionName);
                    break;
                case "UsefulnessPotion":
                    potionEffects.ApplyUsefulness();
                    deletePotionFromInventory(potionName);
                    break;
                case "WeaknessPotion":
                    potionEffects.ApplyWeakness();
                    deletePotionFromInventory(potionName);
                    break;
                default:
                    Debug.LogWarning("Unknown potion: " + potionName);
                    break;
            }
        }

        void deletePotionFromInventory(string potionName)
        {
            var potionCount = MainInventoryData.GetSlotAndCountForItem(potionName, out var itemNumber);
            potionCount -= 1;

            if (potionCount == 0)
            {
                potionInHand.potionName = null;
                MainInventoryData.UpdateMainInventory(itemNumber, "", potionCount);
                inventoryUIManager.ClearUIElement(itemNumber);
            }

            else
            {
                MainInventoryData.UpdateMainInventory(itemNumber, potionName, potionCount);
            }
            
            inventoryUIManager.LoadInventorySprites();
            
        }
    }
}