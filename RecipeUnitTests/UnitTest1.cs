using Microsoft.AspNetCore.Mvc;
using UnitTestingA1Base.Data;
using UnitTestingA1Base.Models;

namespace RecipeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private BusinessLogicLayer _initializeBusinessLogic()
        {
            return new BusinessLogicLayer(new AppStorage());
        }
        [TestMethod]
        public void GetRecipesByIngredient_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange


            // act


            // assert

        }
        [TestMethod]
        public void GetRecipesByIngredient_InvalidId_ReturnsNull()
        {
            // arrange


            // act


            // assert
        }

        [TestMethod]
        public void GetRecipesByDiet_ReturnsRecipesWithDiet()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int validDietId = 1; 
            int expectedRecipeCount = 5; 

            // Act
            HashSet<Recipe> result = bll.GetRecipesByDiet(validDietId, null);

            // Assert
            Assert.AreEqual(expectedRecipeCount, result.Count);
        }

        [TestMethod]
        public void GetRecipes_ReturnsRecipesWithCriteria()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string validCriteria = "Grilled Salmon"; 
            int expectedRecipeCount = 12; 

            // Act
            HashSet<Recipe> result = bll.GetRecipes(null, validCriteria);

            // Assert
            Assert.AreEqual(expectedRecipeCount, result.Count);
        }



    }
}

