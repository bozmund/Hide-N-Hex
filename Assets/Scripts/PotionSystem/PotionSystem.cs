using System.Collections.Generic;
using UnityEngine;

namespace PotionSystem
{
    public class PotionSystem
    {
        private readonly List<Potion> _potions;

        public PotionSystem()
        {
            _potions = new List<Potion>();
            InitializePotions();
        }

        private void InitializePotions()
        {
            _potions.Add(new Potion(
                new PotionEffect("Weakness", "Makes player weak."),
                PotionType.UsableAndThrowable,
                new Recipe(new List<Ingredient> { Ingredient.Blandfruit }).GetRandomizedRecipes(),
                new Color32()));
        }

        public List<Potion> GetAllPotions()
        {
            return _potions;
        }
    }
}