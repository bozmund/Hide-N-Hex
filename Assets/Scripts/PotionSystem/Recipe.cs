using System;
using System.Collections.Generic;
using System.Linq;
using PotionSystem.Helpers;

namespace PotionSystem
{
    public class Recipe
    {
        private readonly Ingredient[] _ingredients;
    
        public Recipe(List<Ingredient> ingredients)
        {
            if (ingredients.Count >= 3)
            {
                _ingredients = ingredients.ToArray();
            }
            else
            {
                for (var i = ingredients.Count; i < 4; i++)
                {
                    ingredients.Add(HelperMethods.GetRandomIngredient());
                }
    
                _ingredients = ingredients.ToArray();
            }
        }

        public Recipe()
        {
            HelperMethods.GetRandomRecipe();
        }
    
        public List<Recipe> GetRandomizedRecipes()
        {
            if (_ingredients.Length < 3)
                return new List<Recipe>
                {
                    new(_ingredients.ToList())
                };
    
            var numberOfRecipes = (int)Math.Ceiling((double)_ingredients.Length / 3);
    
            var shuffledIngredients = _ingredients.OrderBy(_ => Guid.NewGuid()).ToList();
    
            return CreateRandomRecipes(numberOfRecipes, shuffledIngredients);
        }
    
        private static List<Recipe> CreateRandomRecipes(int numberOfRecipes, IReadOnlyCollection<Ingredient> shuffledIngredients)
        {
            var randomRecipes = new List<Recipe>();
            for (var i = 0; i < numberOfRecipes; i++)
            {
                var currentIngredients = shuffledIngredients.Skip(i * 3).Take(3).ToList();
    
                var randomRecipe = new Recipe(currentIngredients);
                randomRecipes.Add(randomRecipe);
            }
    
            return randomRecipes;
        }
    }
}

