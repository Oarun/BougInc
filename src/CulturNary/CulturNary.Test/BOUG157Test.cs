// using System;
// using NUnit.Framework;
// using Moq;

// namespace CulturNary.Tests
// {
//     [TestFixture]
//     public class SuggestedRecipeTests
//     {
//         private Mock<RecipeSearchService> _recipeSearchServiceMock;
//         private Collections collection;

//         [SetUp]
//         public void Setup()
//         {

//             _recipeSearchServiceMock = new Mock<RecipeSearchService>();
//         }

//         [Test]
//         //<summary> this test will return the tags that are in a collection</summary>//
//         public void GetSearchTermsFromCollection_ReturnCollectionTags()
//         {
//             // Arrange
//             collection = new collection(tags:"soup, spicy, dairy free");//create a collection with tags

//             // Act
//             var tags = _recipeSearchServiceMock.GetCollectionTags(collection);

//             // Assert
//             //should return the tags for the created collection
//         }

//         [Test]
//        //<summary> this test will return "None" because this collection has no tags</summary>//
//         public void GetSearchTermsFromCollection_ShouldReturnNone()
//         {
//             // Arrange
//             collection = new collection();//create a collection with tags

//             // Act
//             var tags = _recipeSearchServiceMock.GetCollectionTags(collection);

//             // Assert
//             //should return "None" because this collection has no tags
//         }

//          [Test]
//        //<summary> this test will return recipes from the _recipeSearchServiceMock when pasing the tags as a param</summary>//
//         public void GetRecipes_ShouldReturnRecipesUsingTags()
//         {
//             // Arrange
//             collection = new collection(tags:"soup, spicy, dairy free");//create a collection with tags

//             // Act
//             var tags = _recipeSearchServiceMock.GetCollectionTags(collection);
//             var recipes = _recipeSearchServiceMock.SearchRecipesAsync(tags);
//             // Assert
//             //should return recipes using the collection tags.
//         }
//     }
// }
