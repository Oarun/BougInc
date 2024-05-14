var personId;
var allRestaurantTypes = []; // Global variable to store all restaurants
var restaurantElements = []; // Global variable to store jQuery elements of restaurants

$(document).ready(function() {
    // Function to handle file upload validation
    document.getElementById('restaurantMenu').addEventListener('change', function(event) {
        var file = event.target.files[0];
        var allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'application/pdf']; // Include PDF MIME type
        
        if (file && allowedTypes.includes(file.type)) {
            document.getElementById('menuValidationMessage').textContent = '';
        } else {
            document.getElementById('menuValidationMessage').textContent = 'Please select a valid file (JPEG, PNG, GIF, PDF).';
            this.value = ''; // Clear the file input field
        }
    });

    document.addEventListener('click', function(event) {
        // Check if the clicked element is the "Add to my restaurants" button
        if (event.target && event.target.id === 'addToMyRestaurantsButton') {
            // Call addToMyRestaurants function passing the clicked element as an argument
            console.log('clicked')
             // Retrieve information from HTML elements
            const name = document.getElementById('restaurantGoogleName').textContent;
            const address = document.getElementById('address').textContent || 'N/A';
             // Create an object with the captured information
             const restaurantInfo = {
                RestaurantsName: name,
                RestaurantsAddress: address
             };
            console.log(restaurantInfo)
            addToMyRestaurants(restaurantInfo.RestaurantsName, restaurantInfo.RestaurantsAddress);
        }
    });

    function addToMyRestaurants(RestaurantsName, RestaurantsAddress) {
        // Create Restaurant object from the restaurant information
        var restaurant = {
            PersonId: personId,
            RestaurantsName: RestaurantsName,
            RestaurantsAddress: RestaurantsAddress || 'N/A',
            RestaurantsNotes: 'Added using google search', // You can set this to empty or some default value
        };
        
        console.log(restaurant)
        // Send data to API using AJAX
        sendDataToAPI(restaurant);
    }

    // Event listener for clicking on the restaurant menu image
    $(document).on("click", ".restaurant-menu-image", function() {
        var imageUrl = $(this).attr("src");

        // Display the clicked image in a modal
        $("#menuModalImage").attr("src", imageUrl);
        $("#menuModal").modal("show");
    });

    // Handle form submission
    $("#createRestaurantForm").submit(function(event) {
        event.preventDefault();

        // Create Restaurant object from form data
        var restaurant = {
            PersonId: personId,
            RestaurantsName: $("#restaurantName").val(),
            RestaurantsAddress: $("#restaurantAddress").val(),
            RestaurantsWebsite: $("#restaurantWebsite").val(),
            RestaurantsPhoneNumber: $("#restaurantPhoneNumber").val(),
            RestaurantsNotes: $("#restaurantNotes").val(),
            RestaurantType: $("#restaurantType").val(),
        };

        // Check if a file has been uploaded
        var file = document.getElementById('restaurantMenu').files[0];
        if (file) {
            // If a file has been uploaded, read it as a data URL
            var reader = new FileReader();
            reader.onload = function(e) {
                // Convert the image to a Base64 encoded string
                var base64Image = e.target.result;
                // Include the Base64 string in the restaurant object
                restaurant.RestaurantsMenu = base64Image;
                // Send data to API using AJAX
                sendDataToAPI(restaurant);
            };
            // Read the selected file as a data URL
            reader.readAsDataURL(file);
        } else {
            // If no file uploaded, set the RestaurantsMenu property to null or an empty string
            restaurant.RestaurantsMenu = null; // or ""
            // Send data to API using AJAX
            sendDataToAPI(restaurant);
        }
    });

    // Function to send data to API using AJAX
    function sendDataToAPI(restaurant) {
        $.ajax({
            url: "/api/restaurant/",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(restaurant),
            success: function (response) {
                // Handle successful response
                console.log("Restaurant created successfully:", response);
                $("#createRestaurantForm")[0].reset();
                // Refresh the restaurant list
                getRestaurantsForUser(personId);
            },
            error: function (error) {
                // Handle errors during API call
                console.error("Error creating restaurant:", error);
            }
        });
    }

    function getPerson() {
        $.ajax({
            url: '/api/Person/GetCurrentPerson',
            type: 'GET',
            success: function (data) {
                personId = data.id;
                getRestaurantsForUser(personId);
            }
        });
    }
    getPerson(); // get the personId.

    function getRestaurantsForUser(personId) {
        $.ajax({
            url: "/api/Restaurant/" + personId,
            type: "GET",
            contentType: "application/json",
            success: function (restaurants) {
                populateRestaurants(restaurants);
            },
            error: function (error) {
                // Handle errors during retrieving restaurants
                console.error("Error retrieving restaurants:", error);
            }
        });
    }

    function populateRestaurants(restaurants) {
        var userRestaurantsList = $("#UserRestaurantsList");
        userRestaurantsList.empty(); // Clear existing content
    
        // Check if there are no restaurants
        if (restaurants.length === 0) {
            userRestaurantsList.append("<p>You currently have no restaurants added.</p>");
            $("#RestaurantTypeFilter").hide(); // Hide the filter dropdown
            return; // Exit the function
        }
        $("#RestaurantTypeFilter").show(); 
        // Loop through the received restaurants and append them to the list
        restaurants.forEach(function(restaurant) {
            console.log(restaurant)
            var restaurantItem = `
                <div class="card mb-3 col-md-4" style="max-width: 100%;">
                    <div class="card-body">
                        <h2 class="RestaurantsName">${restaurant.restaurantsName}</h2>
                        <h5 class="RestaurantType">${restaurant.restaurantType ? restaurant.restaurantType : ""}</h5>
                        <h5 class="RestaurantsAddress">${restaurant.restaurantsAddress}</h5>
                        <a href="${restaurant.restaurantsWebsite ?? ''}" style="color: #007bff; text-decoration: none; border-bottom: 1px solid #007bff; ${restaurant.restaurantsWebsite ? '' : 'display: none;'}">
                        Visit Website
                        </a>
                        <h5 class="RestaurantsPhoneNumber">${restaurant.restaurantsPhoneNumber ? restaurant.restaurantsPhoneNumber : ""}</h5>
                        ${restaurant.restaurantsMenu ? `<img class="restaurant-menu-image clickable" src="${restaurant.restaurantsMenu}" alt="Restaurant Menu" style="max-width: 100%; height: auto;">` : `<p>No menu uploaded</p>`}
                        <h5 class="RestaurantsNotes">${restaurant.restaurantsNotes ? restaurant.restaurantsNotes : "No notes available"}</h5>
                        <button class="btn btn-danger delete-restaurant" data-restaurant-id="${restaurant.id}">Delete</button>
                    </div>
                </div>
            `;
            if(restaurant.restaurantType != ""){
                allRestaurantTypes.push(restaurant.restaurantType);
            }
            var restaurantElement = $(restaurantItem); // Convert the HTML string to jQuery element
            userRestaurantsList.append(restaurantElement);
            restaurantElements.push(restaurantElement); // Push the jQuery element to global array
        });
        allRestaurantTypes = removeDuplicatesFromArray(allRestaurantTypes);
        generateFilterOptions()
    }
    
    function removeDuplicatesFromArray(array) {
        return [...new Set(array.filter(item => item !== ""))];
    }

    function generateFilterOptions() {
        var filterDropdown = $("#RestaurantTypeFilter");
        filterDropdown.empty(); // Clear existing options
    
        // Add default option for "All"
        filterDropdown.append(`<option value="">All</option>`);
    
        // Iterate over restaurant types and generate options
        allRestaurantTypes.forEach(function(type) {
            var option = `<option value="${type}">${type}</option>`;
            filterDropdown.append(option);
        });
    }

    $(document).on("change", "#RestaurantTypeFilter", function() {
        var selectedType = $(this).val(); // Get the selected restaurant type
    
        // Show or hide restaurants based on the selected type
        restaurantElements.forEach(function(restaurantElement) {
            var restaurantType = restaurantElement.find(".RestaurantType").text();
            if (selectedType === "" || restaurantType === selectedType) {
                restaurantElement.show();
            } else {
                restaurantElement.hide();
            }
        });
    });

    // Event listener for delete button click
    $(document).on("click", ".delete-restaurant", function() {
        var restaurantId = $(this).data("restaurant-id");
        deleteRestaurant(restaurantId);
    });

    function deleteRestaurant(restaurantId) {
        $.ajax({
            url: "/api/Restaurant/" + restaurantId,
            type: "DELETE",
            success: function(response) {
                console.log("Restaurant deleted successfully");
                // Remove the deleted restaurant item from the UI
                var deletedRestaurantElement = restaurantElements.find(function(element) {
                    return element.find(".delete-restaurant").data("restaurant-id") === restaurantId;
                });
                deletedRestaurantElement.remove();
    
                // Remove the corresponding filter option from the filter dropdown
                generateFilterOptions()
            },
            error: function(error) {
                console.error("Error deleting restaurant:", error);
            }
        });
    }

    // Event listener for closing the modal when the "Close" button is clicked
    $("#menuModal").on("click", "button[data-dismiss='modal']", function() {
        $("#menuModal").modal("hide");
    });

    // Event listener for closing the modal when the "x" button is clicked
    $("#menuModal").on("click", ".close", function() {
        $("#menuModal").modal("hide");
    });
});
