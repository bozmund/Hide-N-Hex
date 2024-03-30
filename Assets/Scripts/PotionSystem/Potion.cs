using System.Collections.Generic;

namespace PotionSystem
{
    public class Potion
    {
        public Potion(PotionEffect potionEffect, PotionType potionType, List<Recipe> recipes)
        {
            Name = "Potion of " + potionEffect.Name;
            Description = "This potion effect " + potionEffect.Description;
            PotionEffect = potionEffect;
            _potionType = potionType;
            Recipe = recipes;
        }

        public string Name;
        public string Description;
        public PotionEffect PotionEffect;
        private readonly PotionType _potionType;
        public readonly List<Recipe> Recipe;
    }
}