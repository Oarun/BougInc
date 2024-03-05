document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('searchForm').addEventListener('submit', function (e) {
        e.preventDefault();

        const ingredients = document.getElementById('ingredients').value;
        searchRecipes(ingredients);
    });
});

function searchRecipes(query) {
    const url = `/api/RecipeSearch/search?q=${encodeURIComponent(query)}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            displayResults(data);
        })
        .catch(error => {
            console.error('Fetch error:', error);
            document.getElementById('searchResults').textContent = 'Failed to load recipes.';
        });
}

function displayResults(data) {
    console.log(data);
    const resultsDiv = document.getElementById('searchResults');
    resultsDiv.innerHTML = '';

    if (data.hits && data.hits.length > 0) {
        data.hits.forEach(hit => {
            const card = document.createElement('div');
            card.className = 'card';
            card.style.color = '#353F2D';
            card.style.marginBottom = '20px';
            resultsDiv.appendChild(card);

            if (hit.recipe.image) {
                const img = document.createElement('img');
                img.src = hit.recipe.image;
                img.className = 'card-img-top';
                img.alt = hit.recipe.label;
                img.style.width = '100%';
                img.style.margin = 'auto';
                img.style.padding = '10px';
                card.appendChild(img);
            }

            const cardBody = document.createElement('div');
            cardBody.className = 'card-body';
            card.appendChild(cardBody);

            const title = document.createElement('h5');
            title.className = 'card-title';
            title.textContent = hit.recipe.label;
            cardBody.appendChild(title);

            const calories = document.createElement('p');
            calories.className = 'card-text';
            calories.textContent = `${hit.recipe.calories} cals`;
            cardBody.appendChild(calories);

            const ingredients = document.createElement('p');
            ingredients.className = 'card-text';
            hit.recipe.ingredients.length = hit.recipe.ingredients.length ? hit.recipe.ingredients.length : 'N/A';
            ingredients.textContent = `${hit.recipe.ingredients.length} Ingredients`;
            cardBody.appendChild(ingredients);

            if (hit.recipe.totalTime && hit.recipe.totalTime > 0) {
                const totalTime = document.createElement('p');
                totalTime.className = 'card-text';
                totalTime.textContent = `${hit.recipe.totalTime} min`;
                cardBody.appendChild(totalTime);
            }


            //if (hit.recipe.dietLabels.length > 0 || hit.recipe.healthLabels.length > 0) {
            //    const text = document.createElement('p');
            //    text.className = 'card-text';
            //    text.textContent = `${hit.recipe.dietLabels.join(', ')}`;
            //    cardBody.appendChild(text);
            //}

            //const listGroup = document.createElement('ul');
            //listGroup.className = 'list-group list-group-flush';
            //hit.recipe.ingredientLines.forEach(ingredient => {
            //    const item = document.createElement('li');
            //    item.className = 'list-group-item';
            //    item.textContent = ingredient;
            //    listGroup.appendChild(item);
            //});
            //card.appendChild(listGroup);

            //const cardBodyLinks = document.createElement('div');
            //cardBodyLinks.className = 'card-body';
            //const recipeLink = document.createElement('a');
            //recipeLink.href = hit.recipe.url;
            //recipeLink.className = 'card-link';
            //recipeLink.textContent = hit.recipe.source;
            ////recipeLink.target = "_blank"; // Opens in new tab
            //cardBodyLinks.appendChild(recipeLink);
            //card.appendChild(cardBodyLinks);

            const cardBodyLinks = document.createElement('div');
            cardBodyLinks.className = 'card-body';

            const recipeLink = document.createElement('button');
            recipeLink.className = 'btn btn-primary';
            recipeLink.style.backgroundColor = '#407A3B';
            recipeLink.style.borderColor = '#407A3B';
            recipeLink.style.color = '#DCEDCF';
            recipeLink.textContent = 'View Recipe';
            recipeLink.onclick = function () {
                updateAndShowModal(hit.recipe);
            };
            cardBodyLinks.appendChild(recipeLink);
            card.appendChild(cardBodyLinks);
        });
    } else {
        resultsDiv.innerHTML = '<p>No recipes found.</p>';
    }
}

function updateAndShowModal(recipe) {
    // Update the modal title
    document.getElementById('recipeModalLabel').textContent = recipe.label;

    // Update the modal body
    const modalBody = document.querySelector('#recipeModal .modal-body');
    modalBody.style.color = '#353F2D';
    modalBody.innerHTML = `
        <img src="${recipe.image}" class="img-fluid mb-3">
        <p><strong>Ingredients:</strong> ${recipe.ingredientLines.join(', ')}</p>
    `;

    // Update the link in the modal footer
    const modalRecipeLink = document.getElementById('modalRecipeLink');
    modalRecipeLink.href = recipe.url;

    // Show the modal
    var recipeModal = new bootstrap.Modal(document.getElementById('recipeModal'));
    recipeModal.show();
}