

const RecipeSearch = {
    searchRecipes: function (query) {
        const url = `/api/RecipeSearch/search?q=${encodeURIComponent(query)}`;

        return fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .catch(error => {
                console.error('Fetch error:', error);
                throw new Error('Failed to load recipes.');
            });
    },

    displayResults: function (data) {
        console.log(data);
        const resultsDiv = document.getElementById('searchResults');
        resultsDiv.innerHTML = '';

        if (data.hits && data.hits.length > 0) {
            data.hits.forEach(product => {
                const cardHTML = `
                <div class="card mb-3" style="max-width: 560px;">
                    <div class="row m-0 p-2" style="brackground-color: #353F2D;">
                        <div class="col-md-4 p-0" style="background-color: white;">
                            <img src="${product.recipe.image}" class="img-fluid" alt="${product.recipe.label}">
                        </div>
                        <div class="col-md-8 p-0">
                            <div class="card-text-body" style="background-color: #353f2dbf; color: white; height: 100%; width: 100%;">
                                <p class="card-title label" style="text-transform: capitalize;"><strong>${product.recipe.label}</strong></h5>
                                <p class="card-text" style="line-height: 3.5px; color: #DCEDCF;">${Math.round(product.recipe.calories)} kcal</p>
                                <p class="card-text" style="line-height: 3.5px; color: #DCEDCF;">${product.recipe.ingredients.length} Ingredients</p>
                                <button class="btn btn-primary" style="line-height: 4.5px; border: none;" onclick="updateAndShowModal(${JSON.stringify(product.recipe).replace(/"/g, '&quot;')})">View More ></button>
                                <button class="btn btn-primary" style="line-height: 4.5px; border: none;" onclick="RecipeSearch.addToFavorites(${JSON.stringify(product.recipe).replace(/"/g, '&quot;')})"><i class="fas fa-star"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                `;
                resultsDiv.innerHTML += cardHTML;
            });
        } else {
            resultsDiv.innerHTML = '<p>No products found.</p>';
        }
    },

    updateAndShowModal: function (recipe) {
        let digestHtml = '';
        recipe.digest.forEach((nutrient) => {
            digestHtml += `
        <tr>
            <th scope="row">${nutrient.label}</th>
            <td>${nutrient.total.toFixed(2)} ${nutrient.unit}</td>
        </tr>
    `;
        });
        document.getElementById('recipeModalLabel').textContent = recipe.label;
        const modalBody = document.querySelector('#recipeModal .modal-body');
        modalBody.innerHTML = `
    <img src="${recipe.image}" alt="${recipe.label}" class="img-fluid mb-3" style="border-radius: 200px; border: 2px solid #353F2D;">
        <table class="table" style="color: #DCEDCF; background-color: #353F2D;">
            <tbody>
                <tr>
                    <th scope="row">Ingredients</th>
                    <td style="text-transform: capitalize;">${recipe.ingredientLines.join('<br>')}</td>
                </tr>
                <tr>
                    <th scope="row">Health Labels</th>
                    <td style="text-transform: capitalize;">${recipe.healthLabels.join(', ')}</td>
                </tr>
                <tr>
                    <th scope="row">Diet Labels</th>
                    <td style="text-transform: capitalize;">${recipe.dietLabels.join(', ')}</td>
                </tr>
                <tr>
                    <th scope="row">Cuisine Type</th>
                    <td style="text-transform: capitalize;">${recipe.cuisineType.join(', ')}</td>
                </tr>
                <tr>
                    <th scope="row">Dish Type</th>
                    <td style="text-transform: capitalize;">${recipe.dishType.join(', ')}</td>
                </tr>
                <tr>
                    <th scope="row">Meal Type</th>
                    <td style="text-transform: capitalize;">${recipe.mealType.join(', ')}</td>
                </tr>
                <tr>
                    <th scope="row" class="h2">Nutrition Facts: </th>
                    <td></td>
                </tr>
                <tr>
                    <th scope="row" class="h4">Calories</th>
                    <td style="text-transform: capitalize;">${Math.round(recipe.calories)} kcal</td>
                </tr>
                </tr>
                ${digestHtml}
            </tbody>
        </table>
        <a href="${recipe.url}" type="button" id="modalRecipeLink" target="_blank" class="btn btn-primary w-100 my-2">View Full Recipe</a>
    `;
        const modalRecipeLink = document.getElementById('modalRecipeLink');
        var recipeModal = new bootstrap.Modal(document.getElementById('recipeModal'));
        recipeModal.show();
    },
    getPerson: function () {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/Person/GetCurrentPerson',
                type: 'GET',
                success: function (data) {
                    console.log("Received data for person")
                    resolve(data); // Resolve with the entire Person object
                },
                error: function (xhr, status, error) {
                    console.log("Didn't receive data for person")
                    reject(error);
                }
            });
        });
    },

    addToFavorites: (recipe) => {
        const url = '/api/FavoriteRecipesApi'; 

        RecipeSearch.getPerson().then(person => {
            console.log(person);
            const favoriteRecipe = {
                PersonId: person.id, // Use person.Id
                RecipeId: recipe.uri, // Assuming the recipe's URI is its ID
                FavoriteDate: new Date().toISOString(),
                Person: person
            };

            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(favoriteRecipe)
            })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                console.log('Added to favorites:', recipe);
            })
            .catch(error => {
                console.error('Fetch error:', error);
            });
        });
    }
};

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('searchForm').addEventListener('submit', function (e) {
        e.preventDefault();

        const ingredients = document.getElementById('ingredients').value;
        RecipeSearch.searchRecipes(ingredients)
            .then(data => RecipeSearch.displayResults(data));
    });
});