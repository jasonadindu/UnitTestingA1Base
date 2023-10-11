using SendGrid.Helpers.Errors.Model;
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

        public HashSet<Recipe> GetRecipesByDiet(int? id, string? name)
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

        public void DeleteIngredient(int? id, string? name)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Either ID or Name must be provided.");
            }

            Ingredient ingredientToDelete = FindIngredient(id, name);

            if (ingredientToDelete == null)
            {
                throw new ArgumentException("Ingredient not found.");
            }

            List<RecipeIngredient> associatedRecipes = _appStorage.RecipeIngredients
                .Where(ri => ri.IngredientId == ingredientToDelete.Id)
                .ToList();

            if (associatedRecipes.Count > 1)
            {
                throw new ForbiddenException("Multiple recipes use this ingredient. Cannot delete.");
            }
            else if (associatedRecipes.Count == 1)
            {
                int recipeId = associatedRecipes[0].RecipeId;

                // Remove the associated recipe and recipe ingredient
                RemoveRecipeAndRecipeIngredient(recipeId);
            }

            _appStorage.Ingredients.Remove(ingredientToDelete);
        }


        private Ingredient FindIngredient(int? id, string? name)
{
    return id != null
        ? _appStorage.Ingredients.FirstOrDefault(i => i.Id == id)
        : _appStorage.Ingredients.FirstOrDefault(i => i.Name.Contains(name));
}

private void RemoveRecipeAndRecipeIngredient(int recipeId)
{
    Recipe recipeToRemove = _appStorage.Recipes.FirstOrDefault(r => r.Id == recipeId);
    RecipeIngredient recipeIngredientToRemove = _appStorage.RecipeIngredients.FirstOrDefault(ri => ri.RecipeId == recipeId);

    if (recipeToRemove != null)
    {
        _appStorage.Recipes.Remove(recipeToRemove);
    }

    if (recipeIngredientToRemove != null)
    {
        _appStorage.RecipeIngredients.Remove(recipeIngredientToRemove);
    }
}

        internal void DeleteRecipe(int? id, string name)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetRecipesByIngredient(string ingredientName)
        {
            throw new NotImplementedException();
        }
    }
}
