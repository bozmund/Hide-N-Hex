using UnityEngine;
using Utilities;

namespace PotionSystem
{
    public class PotionEffectHandler : Singleton<PotionEffectHandler>
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public void Handle(string potionName)
        {
            var potionEffects = gameObject.AddComponent<PotionEffects>();
            switch (potionName)
            {
                case "CatcallingPotion":
                    potionEffects.ApplyCatcalling(GetComponent<CatCalled>());
                    break;
                case "ConfusionPotion":
                    potionEffects.ApplyConfusion();
                    break;
                case "HealingPotion":
                    PotionEffects.ApplyHealing();
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
                    PotionEffects.ApplyLiquidFlame();
                    break;
                case "LowerSusPotion":
                    PotionEffects.ApplyLowerSus();
                    break;
                case "MightPotion":
                    potionEffects.ApplyMight();
                    break;
                case "ParalyticGasPotion":
                    potionEffects.ApplyParalyticGas();
                    break;
                case "PurificationPotion":
                    potionEffects.ApplyPurification();
                    break;
                case "RecallPotion":
                    PotionEffects.ApplyRecall();
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