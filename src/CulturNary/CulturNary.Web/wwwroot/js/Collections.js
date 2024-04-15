var personId;
var currentCollectionId;
var recipeTitles;
var similarRecipeList;

$(document).ready(function () {
    $('#noCollectionsMessage').hide();
    getPerson();
    $('.carousel').carousel();

    const similarRecipesButton = document.getElementById('similarRecipesButton');

    similarRecipesButton.addEventListener('click', function() {
        PopulateRecipeModal(recipeTitles);
    });

    // Bind event listener for form submission to add new collection
    $('#createCollectionForm').on('submit', function (event) {
        event.preventDefault();
        var collectionName = $('#collectionName').val();
        var collectionDescription = $('#collectionDescription').val();
        var collectionImage = $('#collectionImage')[0].files[0];
        addCollection({ name: collectionName, description: collectionDescription, collectionImg: collectionImage });
        $('#collectionName').val('');
        $('#collectionDescription').val('');
        $('#collectionImage').val('');
    });

    // Bind event listener for form submission to edit collection
    $('#editCollectionForm').on('submit', function (event) {
        event.preventDefault();
        var collectionId = $('#editCollectionId').val();
        var collectionName = $('#editCollectionName').val();
        var collectionDescription = $('#editCollectionDescription').val();
        var collectionImage = $('#editCollectionImage')[0].files[0];
        editCollection({ id: collectionId, name: collectionName, description: collectionDescription, collectionImg: collectionImage });
    });

    function editCollection(collectionData) {
        // Get the image file from the file input field
        var imageFile = $('#editCollectionImage')[0].files[0];

        // Check if a file is selected
        if (imageFile) {
            // Create a new FileReader
            var reader = new FileReader();

            // Define onload function to handle the file reading
            reader.onload = function () {
                // Assign the base64 string to the image property
                collectionData.collectionImg = reader.result;
                console.log(collectionData);
                // Make a fetch request with JSON.stringify for the body
                fetch(`/api/Collection/${collectionData.id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(collectionData)
                })
                    // .then(response => {
                    //     console.log(response)
                    //     if (!response.ok) {
                    //         throw new Error('Failed to update collection');
                    //     }
                    //     console.log(collectionData);
                    //     getCollection();
                    //     $('#editCollectionFormContainer').hide();
                    //     $('#createCollectionFormContainer').show();
                    //     return response.json();
                    // })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Failed to update collection');
                        }
                        // Check if response body is empty
                        const contentType = response.headers.get('content-type');
                        if (contentType && contentType.includes('application/json')) {
                            return response.json();
                        } else {
                            return {}; // Return empty object if response body is empty or not JSON
                        }
                    })
                    .then(data => {
                        console.log("Inside then block");
                        console.log(collectionData);
                        getCollection(); // Ensure this function is being called without any errors
                        $('#editCollectionFormContainer').hide(); // Ensure these jQuery operations are working correctly
                        $('#createCollectionFormContainer').show();
                    })
                    .catch(error => {
                        console.error('Error updating collection:', error);
                    });

            };

            // Read the image file as data URL
            reader.readAsDataURL(imageFile);
        } else {
            // If no file is selected, set collectionImg property to null
            collectionData.collectionImg = null;

            // Make a fetch request without including the image property
            fetch(`/api/Collection/${collectionData.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(collectionData)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Failed to update collection');
                    }
                    // Check if response body is empty
                    const contentType = response.headers.get('content-type');
                    if (contentType && contentType.includes('application/json')) {
                        return response.json();
                    } else {
                        return {}; // Return empty object if response body is empty
                    }
                })
                .then(() => {
                    console.log(collectionData);
                    getCollection();
                    $('#editCollectionFormContainer').hide();
                    $('#createCollectionFormContainer').show();
                })
                .catch(error => {
                    console.error('Error updating collection:', error);
                })
        }
    };

    // Event listener for clicking the "Add a Collection" icon
    $('.add-collection-icon').click(function () {
        $('#editCollectionFormContainer').hide();
        $('#collectionDetailsContainer').hide();
        $('#createCollectionFormContainer').show();
    });

    // Event listener for clicking the "Add a Recipe" icon
    $('#collectionDetailsContainer').on('click', '.add-recipe-icon', function () {
        // Get the collection ID from the data attribute of the clicked card
        var collectionId = $(this).closest('.collection-card').data('collection-id');
        // Hide other forms
        $('#editCollectionFormContainer').hide();
        $('#createCollectionFormContainer').hide();
        // Set the collection ID in the add recipe form
        $('#collectionId').val(collectionId);
        // Show add recipe form
        $('#addRecipeFormContainer').show();
        console.log(currentCollectionId);
    });

    // Event listener for clicking the "Back" button in the add recipe form
    $('#backButton').on('click', function () {
        // This line hides the form
        $('#addRecipeFormContainer').hide();
    });

    // Event listener for clicking the "Show Tags" button
    $('#showTags').on('click', function () {
        $('#addRecipeFormContainer').hide();
        $('#addTagsFormContainer').hide();
        $('#displayTagsContainer').show();
        $('#hideTags').show();
        $('#showTags').hide();
    });

    $('#hideTags').on('click', function () {
        $('#addRecipeFormContainer').hide();
        $('#addTagsFormContainer').hide();
        $('#displayTagsContainer').hide();
        $('#hideTags').hide();
        $('#showTags').show();
    });

    // Event Listener for clicking the "Add Tags" icon
    $('#addTagIcon').on('click', function () {
        $('#addRecipeFormContainer').hide();
        $('#displayTagsContainer').hide();
        $('#addTagsFormContainer').show();
        $('#hideTags').hide();
        $('#showTags').show();
    });

    $('#addRecipeForm').on('submit', function (event) {
        event.preventDefault();
        var recipeName = $('#recipeName').val();
        var recipeDescription = $('#recipeDescription').val();

        // Get the selected collections
        var selectedCollections = [];
        $('input[name="collection"]:checked').each(function () {
            selectedCollections.push($(this).val());
        });

        // Create a JSON object with the recipe data
        var recipeData = {
            personId: personId,
            name: recipeName,
            description: recipeDescription
        };

        // Iterate over each selected collection and make a separate AJAX POST request
        selectedCollections.forEach(function (collectionId) {
            // Add the collectionId to the recipe data
            recipeData.collectionId = collectionId;

            // Make an AJAX POST request to add the recipe to the current collection
            $.ajax({
                url: '/api/Recipe',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(recipeData),
                success: function (data) {
                    // Recipe added successfully
                    console.log('Recipe added successfully to collection ' + collectionId + ':', data);
                    // Regenerate the recipe list for the current collection
                    displayRecipes(collectionId);
                },
                error: function (xhr, status, error) {
                    // Error handling code
                    console.error('Error adding recipe to collection ' + collectionId + ':', error);
                    // Display an error message to the user or handle the error in any other way
                }
            });
        });

        // Hide the add recipe form
        $('#addRecipeFormContainer').hide();

        // Clear the form fields after submitting the recipe
        $('#recipeName').val('');
        $('#recipeDescription').val('');
    });

    $('#addTagsForm').on('submit', function (event) {
        event.preventDefault();
        var collectionTagsNew = $('#collectionTagsEdit').val();

        putTags(collectionTagsNew, currentCollectionId);
        
    });

    });

function bindEditAndDeleteEventListeners() {
    // Bind event listener for edit icon click
    $('#collectionContainer').on('click', '.pencil-icon', function (event) {
        event.stopPropagation();
        $('#createCollectionFormContainer').hide();
        $('#collectionDetailsContainer').hide();
        var collectionId = $(this).data('collection-id');
        $('#addTagsForm').on('submit', function (event) {
            event.preventDefault();
            var collectionTagsNew = $('#collectionTagsEdit').val();

            // Store tags in temporary JSON object
            var tagsData = {
                id: currentCollectionId,
                name: $('#collectionName').val(),
                description: $('#collectionDescription').val(),
                tags: collectionTagsNew
            };

            // Make an AJAX Post request for the current collection
            $.ajax({
                url: '/api/Collection/Tags/' + currentCollectionId,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(tagsData),
                success: function (data) {
                    // Tags added successfully
                    console.log('Tags added successfully to collection:' + currentCollectionId + ':', data);
                    updateTags(collectionTagsNew);
                    getCollection();
                    $('#addTagsFormContainer').hide();
                    $('#collectionTagsEdit').empty();
                    $('#displayTagsContainer').show();
                    $('#showTags').hide();
                    $('#hideTags').show();
                },
                error: function (xhr, status, error) {
                    // Error handling code
                    console.error('Error adding tags to collection ' + currentCollectionId + ':', error);
                    // Display an error message to the user or handle the error in any other way
                }
            });
        });
    })
}

function getPerson() {
    $.ajax({
        url: '/api/Person/GetCurrentPerson',
        type: 'GET',
        success: function (data) {
            personId = data.id;
            getCollection(personId);
            getCarousel(personId);
        }
    });
}

function getCarousel() {
    $.ajax({
        url: '/api/Collection/' + personId,
        type: 'GET',
        success: function (data) {
            if (data == null || data.length === 0) {
                $('#noCollectionsMessage').show();
            } else {
                $('#noCollectionsMessage').hide();
            }
            $('#carousel-inner').empty(); // Clear existing carousel items
            // Loop through collections data and create carousel items
            data.forEach(function (collection, index) {
                var activeClass = index === 0 ? 'active' : ''; // Apply 'active' class to first item
                var carouselItem = `
                <div class="carousel-item ${activeClass}">
                    <div class="card mb-3 mx-auto collection-card" data-collection-id="${collection.id}">
                        ${collection.collectionImg !== null ? `<img class="collection-image" src="${collection.collectionImg}" alt="Collection Image">` : ''}
                        <div class="card-body text-center">
                            <h5 class="card-title">${collection.name}</h5>
                            <p class="card-text">${collection.description}</p>
                            <i class="fas fa-trash-alt icon-margin delete-collection" data-collection-id="${collection.id}"></i>
                            <i class="fas fa-pencil-alt pencil-icon" data-collection-id="${collection.id}"></i>
                        </div>
                    </div>
                </div>`;
                $('#carousel-inner').append(carouselItem);
            });
            // Unbind previous event handlers to avoid multiple bindings
            $('#carousel-inner').off('click', '.pencil-icon');
            $('#carousel-inner').off('click', '.delete-collection');
            // Bind event listeners for edit and delete actions
            bindEditAndDeleteEventListeners();
        }
    });
}

function getCollection() {
    $.ajax({
        url: '/api/Collection/' + personId,
        type: 'GET',
        success: function (data) {
            if (data == null || data.length === 0) {
                $('#noCollectionsMessage').show();
            } else {
                $('#noCollectionsMessage').hide();
            }
            $('#collectionContainer').empty();
            var collectionContainer = $('#collectionContainer');
            // Iterate over data and create collection cards
            data.forEach(collection => {
                var collectionCard = $(`<div class="card mb-3 mx-auto collection-card" data-collection-id="${collection.id}"></div>`);
                var cardBody = $('<div class="card-body text-center"></div>');
                cardBody.append(`<h5 class="card-title">${collection.name}</h5>`);
                cardBody.append(`<p class="card-text">${collection.description}</p>`);

                // Conditionally append image if collectionImg is not null
                if (collection.collectionImg !== null) {
                    cardBody.append(`<img class="collection-image" src="${collection.collectionImg}" alt="Collection Image">`);
                }

                cardBody.append(`<p class="card-tags" style="display:none">${collection.tags}</p>`);
                var trashIcon = $(`<i class="fas fa-trash-alt icon-margin delete-collection" data-collection-id="${collection.id}"></i>`);
                var pencilIcon = $(`<i class="fas fa-pencil-alt pencil-icon" data-collection-id="${collection.id}"></i>`);
                cardBody.append(trashIcon);
                cardBody.append(pencilIcon);
                collectionCard.append(cardBody);
                collectionContainer.append(collectionCard);
            });

            // Unbind previous event handlers to avoid multiple bindings
            collectionContainer.off('click', '.pencil-icon');
            collectionContainer.off('click', '.delete-collection');
            // Bind event listeners for edit and delete actions
            bindEditAndDeleteEventListeners();
        }
    });

    // Bind event listener for delete icon click
    $('#collectionContainer').on('click', '.delete-collection', function (event) {
        event.stopPropagation();
        var collectionId = $(this).data('collection-id');
        var confirmDelete = confirm('Are you sure you want to delete this collection?');
        if (confirmDelete) {
            deleteCollection(collectionId);
        }
    });
}

function addCollection(collectionData) {
    collectionData.personId = personId;

    // Get the image file from the file input field
    var imageFile = $('#collectionImage')[0].files[0];

    // Check if a file is selected
    if (imageFile) {
        // Create a new FileReader
        var reader = new FileReader();

        // Define onload function to handle the file reading
        reader.onload = function () {
            // Assign the base64 string to the image property
            collectionData.collectionImg = reader.result;
            console.log(collectionData);
            // Make a fetch request with JSON.stringify for the body
            fetch('/api/Collection', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(collectionData)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Failed to add collection');
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(collectionData);
                    getCollection();
                })
                .catch(error => {
                    console.error('Error adding collection:', error);
                });
        };

        // Read the image file as data URL
        reader.readAsDataURL(imageFile);
    } else {
        // If no file is selected, set collectionImg property to null
        collectionData.collectionImg = null;

        // Make a fetch request without including the image property
        fetch('/api/Collection', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(collectionData)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to add collection');
                }
                return response.json();
            })
            .then(data => {
                console.log(collectionData);
                getCollection();
            })
            .catch(error => {
                console.error('Error adding collection:', error);
            });
    }
}

function deleteCollection(collectionId) {
    fetch(`/api/Collection/${collectionId}`, {
        method: 'DELETE'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to delete collection');
            }
            getCollection();
        })
        .catch(error => {
            console.error('Error deleting collection:', error);
        });
}

// Add event listener for collection card click
$('#collectionContainer').on('click', '.collection-card', function () {
    // Get collection details from the clicked card
    var collectionId = $(this).data('collection-id');
    var collectionName = $(this).find('.card-title').text();
    currentCollectionId = collectionId; //store for use when creating recipe
    console.log(collectionName)
    console.log('you are here')
    var collectionDescription = $(this).find('.collection-description').text();
    var collectionTags = $(this).find('.card-tags').text();
    //console.log(collectionTags)

    //Run tag update
    $('#collectionTagsEdit').empty();
    updateTags(collectionTags);

    // Hide the create and edit collection form and recipe form
    $('#createCollectionFormContainer').hide();
    $('#editCollectionFormContainer').hide();
    $('#addRecipeFormContainer').hide();
    $('#addTagsFormContainer').hide();
    $('#displayTagsContainer').hide();
    $('#hideTags').hide();
    $('#showTags').show();
    // Populate and show collection details
    $('#collectionTitle').text(collectionName + " Collection");
    $('#collectionDescription').text(collectionDescription);
    $('#collectionDetailsContainer').show();

    // Call function to fetch and display recipes for this collection
    displayRecipes(collectionId);
});

$('#collectionSearch').on('input', function () {
    var searchValue = $(this).val().toLowerCase().trim();

    $('.collection-card').each(function () {
        var tagsText = $(this).find('.card-tags').text().toLowerCase();
        if (tagsText.includes(searchValue)) {
            $(this).show(); // Show the collection card if the search term matches the tags
        } else {
            $(this).hide(); // Hide the collection card if there's no match
        }
    });
});

function displayRecipes(collectionId) {
    // Make AJAX request to fetch recipes for the given collectionId
    console.log(currentCollectionId)
    $.ajax({
        url: '/api/Recipe/' + collectionId,
        type: 'GET',
        success: function (data) {
            // Clear existing recipe list
            $('#collectionRecipes').empty();
            if (data.length === 0) {
                // If no recipes found, display a message
                similarRecipesButton.style.display = 'none';
                $('#collectionRecipes').append('<ul>No recipes found for this collection. You can add some!</ul>');
            } else {
                recipeTitles = [];
                // Add each recipe uri to the list
                similarRecipeList = [];
                data.forEach(function (recipe) {
                    similarRecipesButton.style.display = 'block';
                    var recipeCard = $(`<div class="card mx-auto recipe-card"></div>`);
                    var cardBody = $('<div class="card-body"></div>');
                    cardBody.append(`<h5 class="card-title">${recipe.name}</h5>`);
                    recipeTitles.push(recipe.name); 
                    cardBody.append(`<p class="card-text">${recipe.description}</p>`);
                    var deleteIcon = $(`<i class="fas fa-trash-alt delete-recipe-icon" data-recipe-id="${recipe.id}"></i>`);
                    cardBody.append(deleteIcon);
                    recipeCard.append(cardBody);
                    $('#collectionRecipes').append(recipeCard);
                    // Bind event listener for delete icon click
                    deleteIcon.on('click', function () {
                        var recipeId = $(this).data('recipe-id');
                        deleteRecipe(recipeId, collectionId);
                    });
                    if(recipe.uri!=null){
                        similarRecipeList.push(recipe.uri)
                    }
                });
                console.log(recipeTitles)
            }
            // Call the function to display available collections when the page loads

            displayCollectionsForRecipe();
        },
        error: function (xhr, status, error) {
            console.error('Error fetching recipes:', error);
        }
    });
}

function putTags(updatedTags, currentCollectionId) {

    var collectionName = $('#collectionName').val();
    var collectionDescription = $('#collectionDescription').val();


    var tagsData = {
        id: currentCollectionId,
        name: collectionName,
        description: collectionDescription,
        tags: updatedTags
    };

    // Make an AJAX Post request for the current collection
    $.ajax({
        url: '/api/Collection/Tags/' + currentCollectionId,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(tagsData),
        success: function (data) {
            // Tags added successfully
            console.log('Tags added successfully to collection:' + currentCollectionId + ':', data);
            updateTags(updatedTags);
            getCollection();
            $('#addTagsFormContainer').hide();
            $('#collectionTagsEdit').empty();
            $('#displayTagsContainer').show();
            $('#showTags').hide();
            $('#hideTags').show();
        },
        error: function (xhr, status, error) {
            // Error handling code
            console.error('Error adding tags to collection ' + currentCollectionId + ':', error);
            // Display an error message to the user or handle the error in any other way
        }
    });
}

function updateTags(tags) {

    if (tags == null || tags == "" || tags == "null") {
        $('#collectionTagsDisplay').text("No tags found for this collection.");
        $('#collectionTagsEdit').empty();
    }
    else {
        //console.log('tags found')
        $('#collectionTagsEdit').empty();
        $('#collectionTagsEdit').text(tags);
        var tagsSplit = tags.split(',');
        var tagList = "";
        tagsSplit.forEach(tagsSplit => {
            tagList += `<p class="tag-item">${tagsSplit}</p>`;
        });
        //console.log(tagList);
        $('#collectionTagsDisplay').html(tagList);
    }
}

function deleteRecipe(recipeId, collectionId) {
    // AJAX request to delete the recipe
    $.ajax({
        url: '/api/Recipe/' + recipeId,
        type: 'DELETE',
        success: function () {
            // Recipe deleted successfully, regenerate the recipe list
            displayRecipes(collectionId);
        },
        error: function (xhr, status, error) {
            console.error('Error deleting recipe:', error);
        }
    });
}

function displayCollectionsForRecipe() {
    console.log('displayCollectionsForRecipe')
    console.log(personId)
    $.ajax({
        url: '/api/Collection/' + personId,
        type: 'GET',
        success: function (data) {
            $('#collections').empty();
            if (data.length === 0) {
                $('#collections').append('<p>No collections found.</p>');
            } else {
                data.forEach(function (collection) {
                    var checkboxId = 'collectionCheckbox_' + collection.id; // Generate unique id for each checkbox
                    var checkbox = $('<input type="checkbox" class="form-check-input" name="collection" id="' + checkboxId + '" value="' + collection.id + '">');
                    var label = $('<label class="form-check-label" for="' + checkboxId + '">' + collection.name + '</label><br>'); // Associate label with checkbox
                    $('#collections').append(checkbox);
                    $('#collections').append(label);
                });
            }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching collections for recipe:', error);
        }
    });
}

// Function to populate the recipe modal
function PopulateRecipeModal(query) {
    // Your code to populate the recipe modal goes here
    console.log("Populating recipe modal...");
    var num = query.length;
    var randomIndex = Math.floor(Math.random() * num);
    console.log(query)
    console.log(num)
    console.log(query[randomIndex])
    const url = `/api/RecipeSearch/search?q=${encodeURIComponent(query[randomIndex])}`;
    console.log(similarRecipeList)

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log("Response data:", data);
            const similarRecipesToDisplay = data.hits.slice(0, 20);
            similarRecipesToDisplay.forEach(hit => {
                // Add your code here to display each similar recipe
                if (!similarRecipeList.includes(hit.recipe.uri)) {
                var templateClone = document.getElementById('templateSimilarRecipes').content.cloneNode(true);
                templateClone.querySelector('.SimilarRecipeName').textContent = `${hit.recipe.label}`;
                templateClone.querySelector('.CuisineType').textContent = `Cuisine: ${hit.recipe.cuisineType}`;
                templateClone.querySelector('.MealType').textContent = `Type: ${hit.recipe.mealType}`;
                templateClone.querySelector('.SimilarRecipeImg').src = hit.recipe.image;

                // Add an event listener to the "Add to Collection" button within the cloned template
                templateClone.querySelector('.addSimilarRecipeBtn').addEventListener('click', function() {
                    handleAddToCollection(hit.recipe); // Pass the recipe data to the event handler
                });

                document.querySelector('.modal-content').appendChild(templateClone);
            }
            });
            document.getElementById('similarRecipeModal').style.display = 'block';
        })
        .catch(error => {
            console.error('Fetch error:', error);
            document.getElementById('searchResults').textContent = 'Failed to load recipes.';
        });
}

// Function to handle adding a similar recipe to the collection
function handleAddToCollection(recipe) {
    console.log("Recipe Name:", recipe.label);
    // console.log("Cuisine Type:", recipe.cuisineType[0]);
    // console.log("Meal Type:", recipe.mealType[0]);
    // console.log("Image:", recipe.image);

    // Create a JSON object with the recipe data
    var similarRecipeData = {
        personId: personId,
        name: recipe.label,
        description: recipe.cuisineType[0],
        collectionId: currentCollectionId,
        img: recipe.image,
        uri: recipe.uri
    };
    console.log(similarRecipeData)
    // Make an AJAX POST request to add the recipe to the current collection
    $.ajax({
        url: '/api/Recipe',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(similarRecipeData),
        success: function (data) {
            // Recipe added successfully
            console.log('Recipe added successfully to collection ' + collectionId + ':', data);
        },
        error: function (xhr, status, error) {
            // Error handling code
            console.error('Error adding recipe to collection ' + collectionId + ':', error);
            // Display an error message to the user or handle the error in any other way
        }
    });
}

// Function to close the modal
function closeModal() {
    document.getElementById('similarRecipeModal').style.display = 'none';
    while (modalContent.firstChild) {
        modalContent.removeChild(modalContent.firstChild);
    }
    // Call the function to update the recipe list
    displayRecipes(currentCollectionId)
}

// Add event listener to the close button within the modal
document.querySelector('.close').addEventListener('click', function() {
    closeModal();
});



// module.exports = { getPerson, getCollection, addCollection, deleteCollection, displayRecipes, putTags, updateTags, deleteRecipe, displayCollectionsForRecipe };
