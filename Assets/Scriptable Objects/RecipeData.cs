using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(fileName = "RecipeData", menuName = "Crafting/Recipe Data", order = 1)]
    public class RecipeData : ScriptableObject
    {
        public string potionName;
        public string potionSpriteName;
        public string firstIngredientSpriteName;
        public string secondIngredientSpriteName;
        public string thirdIngredientSpriteName;
    }
}
