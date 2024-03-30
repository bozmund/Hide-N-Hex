using System.Collections.Generic;

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
                new Recipe(new List<Ingredient> { Ingredient.Blandfruit }).GetRandomizedRecipes()));
        }

        public List<Potion> GetAllPotions()
        {
            return _potions;
        }
    }
}

