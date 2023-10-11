using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage)
        {
            _appStorage = appStorage;
        }
        public HashSet<Recipe> GetRecipesByIngredient(int? id, string? name)
        {
            Ingredient ingredient;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null)
            {
                ingredient = _appStorage.Ingredients.First(i => i.Id == id);

                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();

                recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
            }

            return recipes;
        }

        public HashSet<Recipe> GetRecipesByDiet(int? id, string name)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null)
            {
                DietaryRestriction diet = _appStorage.DietaryRestrictions.FirstOrDefault(d => d.Id == id);

                if (diet != null)
                {
                    recipes = _appStorage.Recipes
                        .Where(r => r.Diet.Name == diet.Name)
                        .ToHashSet();
                }
            }

            if (recipes.Count == 0 && !string.IsNullOrWhiteSpace(name))
            {
                recipes = _appStorage.Recipes
                    .Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToHashSet();
            }

            return recipes;
        }

        internal HashSet<Recipe> GetRecipes(int id, string name)
        {
            throw new NotImplementedException();
        }

        public HashSet<Recipe> GetRecipes(object value, string validCriteria)
        {
            throw new NotImplementedException();
        }
    }
}
