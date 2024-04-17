using Moq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using CulturNary.DAL.Concrete;
using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using System.Collections.Generic;
using CulturNary.Web.Models;
[TestFixture]
public class FavoriteRecipeRepositoryTests
{
    private IFavoriteRecipeRepository _repository;
    private Mock<CulturNaryDbContext> _mockContext;
    private Mock<IPersonRepository> _mockPersonRepository;

    [SetUp]
    public void SetUp()
    {
        _mockContext = new Mock<CulturNaryDbContext>();
        _mockPersonRepository = new Mock<IPersonRepository>();

        var expectedRecipe = new FavoriteRecipe
        {
            Id = 1,
            PersonId = 1,
            RecipeId = "1",
            FavoriteDate = DateTime.Now,
            ImageUrl = "http://example.com/image.jpg",
            Label = "Test Label",
            Uri = "http://example.com",
            Tags = "test"
        };

        var data = new List<FavoriteRecipe> { expectedRecipe }.AsQueryable();

        var mockSet = new Mock<DbSet<FavoriteRecipe>>();
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        _mockContext.Setup(c => c.Set<FavoriteRecipe>()).Returns(mockSet.Object);

        _repository = new FavoriteRecipeRepository(_mockContext.Object, _mockPersonRepository.Object);
    }

    [Test]
    public void TestGetFavoriteRecipeForPersonID()
    {
        // Arrange
        var expectedRecipe = new FavoriteRecipe
        {
            Id = 1,
            PersonId = 1,
            RecipeId = "1",
            FavoriteDate = DateTime.Now,
            ImageUrl = "http://example.com/image.jpg",
            Label = "Test Label",
            Uri = "http://example.com",
            Tags = "test"
        };

        // Act
        var result = _repository.GetFavoriteRecipeForPersonID(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        var firstResult = result[0];
        Assert.That(firstResult.Id, Is.EqualTo(expectedRecipe.Id));
        Assert.That(firstResult.PersonId, Is.EqualTo(expectedRecipe.PersonId));
        Assert.That(firstResult.RecipeId, Is.EqualTo(expectedRecipe.RecipeId));
        Assert.That(firstResult.FavoriteDate.Date, Is.EqualTo(expectedRecipe.FavoriteDate.Date));
        Assert.That(firstResult.ImageUrl, Is.EqualTo(expectedRecipe.ImageUrl));
        Assert.That(firstResult.Label, Is.EqualTo(expectedRecipe.Label));
        Assert.That(firstResult.Uri, Is.EqualTo(expectedRecipe.Uri));
        Assert.That(firstResult.Tags, Is.EqualTo(expectedRecipe.Tags));
    }
    [Test]
    public void TestGetFavoriteRecipeForInvalidPersonID()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<FavoriteRecipe>().Find(It.IsAny<int>())).Returns((FavoriteRecipe)null);

        // Act
        var result = _repository.GetFavoriteRecipeForPersonID(-1);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestGetFavoriteRecipeForPersonIDAndRecipeID()
    {
        // Arrange
        var expectedRecipe = new FavoriteRecipe
        {
            Id = 1,
            PersonId = 1,
            RecipeId = "1",
            FavoriteDate = DateTime.Now,
            ImageUrl = "http://example.com/image.jpg",
            Label = "Test Label",
            Uri = "http://example.com",
            Tags = "test"
        };

        // Act
        var result = _repository.GetFavoriteRecipeForPersonIDAndRecipeID(1, 1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(expectedRecipe.Id));
        Assert.That(result.PersonId, Is.EqualTo(expectedRecipe.PersonId));
        Assert.That(result.RecipeId, Is.EqualTo(expectedRecipe.RecipeId));
        Assert.That(result.FavoriteDate.Date, Is.EqualTo(expectedRecipe.FavoriteDate.Date));
        Assert.That(result.ImageUrl, Is.EqualTo(expectedRecipe.ImageUrl));
        Assert.That(result.Label, Is.EqualTo(expectedRecipe.Label));
        Assert.That(result.Uri, Is.EqualTo(expectedRecipe.Uri));
        Assert.That(result.Tags, Is.EqualTo(expectedRecipe.Tags));
    }

    [Test]
    public void TestGetFavoriteRecipeByRecipeId()
    {
        // Arrange
        var expectedRecipe = new FavoriteRecipe
        {
            Id = 1,
            PersonId = 1,
            RecipeId = "1",
            FavoriteDate = DateTime.Now,
            ImageUrl = "http://example.com/image.jpg",
            Label = "Test Label",
            Uri = "http://example.com",
            Tags = "test"
        };

        // Act
        var result = _repository.GetFavoriteRecipeByRecipeId("1");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(expectedRecipe.Id));
        Assert.That(result.PersonId, Is.EqualTo(expectedRecipe.PersonId));
        Assert.That(result.RecipeId, Is.EqualTo(expectedRecipe.RecipeId));
        Assert.That(result.FavoriteDate.Date, Is.EqualTo(expectedRecipe.FavoriteDate.Date));
        Assert.That(result.ImageUrl, Is.EqualTo(expectedRecipe.ImageUrl));
        Assert.That(result.Label, Is.EqualTo(expectedRecipe.Label));
        Assert.That(result.Uri, Is.EqualTo(expectedRecipe.Uri));
        Assert.That(result.Tags, Is.EqualTo(expectedRecipe.Tags));
    }

    [Test]
    public void TestSearchFavoriteRecipesForPersonID()
    {
        // Arrange
        var expectedRecipe = new FavoriteRecipe
        {
            Id = 1,
            PersonId = 1,
            RecipeId = "1",
            FavoriteDate = DateTime.Now,
            ImageUrl = "http://example.com/image.jpg",
            Label = "Test Label",
            Uri = "http://example.com",
            Tags = "test"
        };

        // Act
        var result = _repository.SearchFavoriteRecipesForPersonID(1, "Test");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        var firstResult = result[0];
        Assert.That(firstResult.Id, Is.EqualTo(expectedRecipe.Id));
        Assert.That(firstResult.PersonId, Is.EqualTo(expectedRecipe.PersonId));
        Assert.That(firstResult.RecipeId, Is.EqualTo(expectedRecipe.RecipeId));
        Assert.That(firstResult.FavoriteDate.Date, Is.EqualTo(expectedRecipe.FavoriteDate.Date));
        Assert.That(firstResult.ImageUrl, Is.EqualTo(expectedRecipe.ImageUrl));
        Assert.That(firstResult.Label, Is.EqualTo(expectedRecipe.Label));
        Assert.That(firstResult.Uri, Is.EqualTo(expectedRecipe.Uri));
        Assert.That(firstResult.Tags, Is.EqualTo(expectedRecipe.Tags));
    }
    [Test]
    public void TestGetFavoriteRecipeForNonExistingPersonID()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<FavoriteRecipe>().Find(It.IsAny<int>())).Returns((FavoriteRecipe)null);

        // Act
        var result = _repository.GetFavoriteRecipeForPersonID(9999);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestGetFavoriteRecipeForPersonIDAndNonExistingRecipeID()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<FavoriteRecipe>().Find(It.IsAny<int>())).Returns((FavoriteRecipe)null);

        // Act
        var result = _repository.GetFavoriteRecipeForPersonIDAndRecipeID(1, 9999);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void TestGetFavoriteRecipeByNonExistingRecipeId()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<FavoriteRecipe>().Find(It.IsAny<int>())).Returns((FavoriteRecipe)null);

        // Act
        var result = _repository.GetFavoriteRecipeByRecipeId("9999");

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void TestSearchFavoriteRecipesForPersonIDWithNonExistingKeyword()
    {
        // Arrange
        _mockContext.Setup(c => c.Set<FavoriteRecipe>().Find(It.IsAny<int>())).Returns((FavoriteRecipe)null);

        // Act
        var result = _repository.SearchFavoriteRecipesForPersonID(1, "NonExistingKeyword");

        // Assert
        Assert.That(result, Is.Empty);
    }

}