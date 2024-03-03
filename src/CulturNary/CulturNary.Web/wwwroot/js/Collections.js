var personId;
var currentCollectionId;

$(document).ready(function () {
    $('#noCollectionsMessage').hide();
    getPerson();


    // Bind event listener for form submission to add new collection
    $('#createCollectionForm').on('submit', function (event) {
        event.preventDefault();
        var collectionName = $('#collectionName').val();
        var collectionDescription = $('#collectionDescription').val();
        addCollection({ name: collectionName, description: collectionDescription });
        $('#collectionName').val('');
        $('#collectionDescription').val('');
    });

    // Bind event listener for form submission to edit collection
    $('#editCollectionForm').on('submit', function (event) {
        event.preventDefault();
        var collectionId = $('#editCollectionId').val();
        var collectionName = $('#editCollectionName').val();
        var collectionDescription = $('#editCollectionDescription').val();
        $.ajax({
            url: '/api/Collection/' + collectionId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                id: collectionId,
                name: collectionName,
                description: collectionDescription
            }),
            success: function () {
                getCollection();
                $('#editCollectionFormContainer').hide();
                $('#createCollectionFormContainer').show();
            },
            error: function (xhr, status, error) {
                console.error('Error updating collection:', error);
            }
        });
    });

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
});

function getPerson() {
    $.ajax({
        url: '/api/Person/GetCurrentPerson',
        type: 'GET',
        success: function (data) {
            personId = data.id;
            getCollection(personId);
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
            data.slice(0, 5).forEach(collection => {
                var collectionCard = $(`<div class="card mb-3 mx-auto collection-card" data-collection-id="${collection.id}"></div>`);
                var cardBody = $('<div class="card-body text-center"></div>');
                cardBody.append(`<h5 class="card-title">${collection.name}</h5>`);
                cardBody.append(`<p class="card-text">${collection.description}</p>`);
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
}

function bindEditAndDeleteEventListeners() {
    // Bind event listener for edit icon click
    $('#collectionContainer').on('click', '.pencil-icon', function (event) {
        event.stopPropagation();
        $('#createCollectionFormContainer').hide();
        $('#collectionDetailsContainer').hide();
        var collectionId = $(this).data('collection-id');
        $.ajax({
            url: '/api/Collection/ById/' + collectionId,
            type: 'GET',
            success: function (collection) {
                $('#editCollectionId').val(collection.id);
                $('#editCollectionName').val(collection.name);
                $('#editCollectionDescription').val(collection.description);
                $('#editCollectionFormContainer').show();
            },
            error: function (xhr, status, error) {
                console.error('Error fetching collection details:', error);
            }
        });
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
            getCollection();
        })
        .catch(error => {
            console.error('Error adding collection:', error);
        });
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

    // Hide the create and edit collection form and recipe form
    $('#createCollectionFormContainer').hide();
    $('#editCollectionFormContainer').hide();
    $('#addRecipeFormContainer').hide();
    // Populate and show collection details
    $('#collectionTitle').text(collectionName + " Collection");
    $('#collectionDescription').text(collectionDescription);
    $('#collectionDetailsContainer').show();

    // Call function to fetch and display recipes for this collection
    displayRecipes(collectionId);
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
                $('#collectionRecipes').append('<ul>No recipes found for this collection. You can add some!</ul>');
            } else {
                // Add each recipe to the list
                data.forEach(function (recipe) {
                    var recipeCard = $(`<div class="card mx-auto recipe-card"></div>`);
                    var cardBody = $('<div class="card-body"></div>');
                    cardBody.append(`<h5 class="card-title">${recipe.name}</h5>`);
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
                });
            }
            // Call the function to display available collections when the page loads

            displayCollectionsForRecipe();
        },
        error: function (xhr, status, error) {
            console.error('Error fetching recipes:', error);
        }
    });
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
