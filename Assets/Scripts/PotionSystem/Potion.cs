using System.Collections.Generic;
using UnityEngine;

namespace PotionSystem
{
    public class Potion
    {
        public Potion(PotionEffect potionEffect, PotionType potionType, List<Recipe> recipes, Color32 color32)
        {
            Name = "Potion of " + potionEffect.Name;
            Description = "This potion effect " + potionEffect.Description;
            PotionEffect = potionEffect;
            _potionType = potionType;
            Recipe = recipes;
            Color32 = color32;
        }

        public string Name;
        public string Description;
        public PotionEffect PotionEffect;
        private readonly PotionType _potionType;
        public readonly List<Recipe> Recipe;
        public readonly Color32 Color32;
    }
}