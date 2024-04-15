var personId;
var currentCollectionId;

$(document).ready(function () {
    $('#noCollectionsMessage').hide();
    $('.carousel').carousel();

    // Call the function to get the current person
    getPerson();

    function getPerson() {
        $.ajax({
            url: '/api/Person/GetCurrentPerson',
            type: 'GET',
            success: function (data) {
                // If person data is received, show welcome-user section
                $('.welcome-user').show();
                // Hide welcome-section if it was initially visible
                $('.welcome-section').hide();
                // Process the received person data
                personId = data.id;
                // Get the collections, recipes, and popular recipes
                getCollection(personId);
                getRecipes(personId);
                searchPopular("popular")
            },
            error: function (xhr, status, error) {
                // If error occurs (person not found), show welcome-section
                $('.welcome-section').show();
                // Hide welcome-user section if it was initially visible
                $('.welcome-user').hide();
            }
        });
    }

    // Get recipes
    function getRecipes(personId) {
        $.ajax({
            url: '/api/Recipe/ByPersonId/' + personId,
            type: 'GET',
            success: function (data) {
                console.log("Got recipes:", data);
                var numberOfRecipes = data.length;
                console.log("Number of recipes:", numberOfRecipes);

                // Clone the template
                var recipeCountTemplate = $('#recipeCountTemplate').html();
                var $recipeCard = $(recipeCountTemplate);
                // Populate recipe count in the card
                $recipeCard.find('#recipeCount').text(numberOfRecipes);
                // Append the card to the container
                $('#recipeCardContainer').empty().append($recipeCard);

                // Populate user recipes
                populateUserRecipes(data);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching recipes:", error);
            }
        });
    }

    // Populate user recipes
    function populateUserRecipes(data) {
        $('userRecipesContainer').empty();
        data.forEach(recipe => {
            var templateClone = document.getElementById('templateRecipes').content.cloneNode(true);
            templateClone.querySelector('.RecipeName').textContent = `${recipe.name}`;
            // templateClone.querySelector('.RecipeDescription').textContent = `${recipe.description}`;
            templateClone.querySelector('.viewRecipeBtn').addEventListener('click', () => viewRecipe(recipe));
            document.getElementById('userRecipesContainer').appendChild(templateClone);
        });
    }

    // Handle "View" button click to view the recipe
    function viewRecipe(recipe) {
        // Update modal content with recipe details
        $('#modalRecipeName').text(recipe.name);
        $('#modalRecipeDescription').text(recipe.description);
        // Show modal
        $('#recipeModal').css('display', 'block');
    }

    // Close the modal when the close button is clicked
    $('.close').on('click', function () {
        $('#recipeModal').css('display', 'none');
    });

    // Close the modal when clicking outside of it
    $(window).on('click', function (event) {
        if (event.target == $('#recipeModal')[0]) {
            $('#recipeModal').css('display', 'none');
        }
    });

    //get collections
    function getCollection() {
        $.ajax({
            url: '/api/Collection/' + personId,
            type: 'GET',
            success: function (data) {
                console.log("got collections");
                var numberOfCollections = data.length;
                console.log(numberOfCollections);
                console.log(data);

                // Clone the template
                var collectionCountTemplate = $('#collectionCountTemplate').html();
                var $collectionCard = $(collectionCountTemplate);

                // Populate collection count in the card
                $collectionCard.find('#collectionCount').text(numberOfCollections);

                // Replace the existing content with the collection card
                $('#collectionCardContainer').empty().append($collectionCard);

                // Populate user collections
                populateUserCollections(data);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching collections:", error);
            }
        });
    }

    // Populate user Collections
    function populateUserCollections(data) {
        $('userCollectionsContainer').empty();
        data.forEach(collection => {
            var templateClone = document.getElementById('templateCollections').content.cloneNode(true);
            templateClone.querySelector('.CollectionName').textContent = `${collection.name}`;
            // templateClone.querySelector('.CollectionDescription').textContent = `${collection.description}`;
             // Check if there is a valid image URL
        if (collection.collectionImg) {
            // Set the image source if available
            templateClone.querySelector('.collectionImg').src = collection.collectionImg;
        } else {
            // Hide the image element if there is no image URL
            templateClone.querySelector('.collectionImg').style.display = 'none';
        }
            templateClone.querySelector('.viewCollectionBtn').addEventListener('click', () => viewCollection(collection));
            document.getElementById('userCollectionsContainer').appendChild(templateClone);
        });
    }

    // Handle "View" button click to view the collection
    function viewCollection(collection) {
        // Update modal content with collection details
        $('#modalCollectionName').text(collection.name);
        $('#modalCollectionDescription').text(collection.description);
        // Check if there is a valid image URL
    if (collection.collectionImg) {
        // Set the image source using the .attr() method
        $('#modalCollectionImg').attr('src', collection.collectionImg);
        // Show the image element
        $('#modalCollectionImg').show();
    } else {
        // Hide the image element if there is no image URL
        $('#modalCollectionImg').hide();
    }
        // Show modal
        $('#collectionModal').css('display', 'block');
    }

    // Close the modal when the close button is clicked
    $('.close').on('click', function () {
        $('#collectionModal').css('display', 'none');
    });

    // Close the modal when clicking outside of it
    $(window).on('click', function (event) {
        if (event.target == $('#collectionModal')[0]) {
            $('#collectionModal').css('display', 'none');
        }
    });

    //get popular recipes
    function searchPopular(query) {
        const url = `/api/RecipeSearch/search?q=${encodeURIComponent(query)}`;

        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                displayPopularRecipes(data);
            })
            .catch(error => {
                console.error('Fetch error:', error);
                document.getElementById('searchResults').textContent = 'Failed to load recipes.';
            });
    }

    function displayPopularRecipes(data) {
        // Display only the first 20 recipes
        const recipesToDisplay = data.hits.slice(0, 10);
        $('popularRecipesContainer').empty();
        // Loop through the first 20 recipes and display them
        recipesToDisplay.forEach(hit => {
            // Add your code here to display each popular recipe
            var templateClone = document.getElementById('templatePopular').content.cloneNode(true);
            templateClone.querySelector('.PopularName').textContent = `${hit.recipe.label}`;
            templateClone.querySelector('.PopularCuisineType').textContent = `Cuisine: ${hit.recipe.cuisineType}`;
            templateClone.querySelector('.PopularMealType').textContent = `Type: ${hit.recipe.mealType}`;
            templateClone.querySelector('.PopularImg').src = hit.recipe.image;
            document.getElementById('popularRecipesContainer').appendChild(templateClone);
        });
    }
});
