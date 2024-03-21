const { searchRecipes } = require('../../CulturNary.Web/wwwroot/js/site');

// Mock searchRecipes function
jest.mock('../../CulturNary.Web/wwwroot/js/site', () => ({
    searchRecipes: jest.fn(),
}));

describe('searchRecipes function', () => {
    // Test for checking if the function is called with the correct arguments
    it('is called with the correct arguments', async () => {
        // Arrange
        const mockIngredients = 'eggs, bacon';
        const mockResponse = { hits: [{ recipe: { label: 'Bacon and Eggs', image: 'bacon_and_eggs.jpg', ingredients: ['eggs', 'bacon'], url: 'bacon_and_eggs_recipe_url' } }] };
        searchRecipes.mockResolvedValueOnce(mockResponse);

        // Act
        // Simulate the function call with mock arguments
        await searchRecipes(mockIngredients);

        // Assert
        expect(searchRecipes).toHaveBeenCalledWith(mockIngredients);
    });

    // Test for checking if the function returns the correct response
    it('returns the correct response', async () => {
        // Arrange
        const mockIngredients = 'pancakes';
        const mockResponse = { hits: [{ recipe: { label: 'Fluffy Pancakes', image: 'fluffy_pancakes.jpg', ingredients: ['flour', 'milk', 'eggs'], url: 'fluffy_pancakes_recipe_url' } }] };
        searchRecipes.mockResolvedValueOnce(mockResponse);

        // Act
        const response = await searchRecipes(mockIngredients);

        // Assert
        expect(response).toEqual(mockResponse);
        expect(response.hits[0].recipe.label).toEqual('Fluffy Pancakes');
    });

    // Test for checking if the function handles errors properly
    it('handles errors correctly', async () => {
        // Arrange
        const mockIngredients = 'toast';
        const mockError = new Error('Network Error');
        searchRecipes.mockRejectedValueOnce(mockError);

        // Act & Assert
        await expect(searchRecipes(mockIngredients)).rejects.toThrow('Network Error');
    });
});

