using System;
using System.Collections.Generic;

namespace PotionSystem.Helpers
{
    public static class HelperMethods
    {
        public static Ingredient GetRandomIngredient()
        {
            var random = new Random();
            var allIngredients = (Ingredient[])Enum.GetValues(typeof(Ingredient));
            return allIngredients[random.Next(0, allIngredients.Length)];
        }

        public static Ingredient[] GetRandomRecipe()
        {
            var recipe = new Ingredient[3];
            var random = new Random();
            var allIngredients = (Ingredient[])Enum.GetValues(typeof(Ingredient));
            recipe[0] = allIngredients[random.Next(0, allIngredients.Length)];
            recipe[1] = allIngredients[random.Next(0, allIngredients.Length)];
            recipe[2] = allIngredients[random.Next(0, allIngredients.Length)];
            return recipe;
        }

        public static List<Recipe> RandomizeRecipes(Ingredient[] ingredients)
        {
            throw new NotImplementedException();
        }
    }
}