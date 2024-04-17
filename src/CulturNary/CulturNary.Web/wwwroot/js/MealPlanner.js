document.addEventListener('DOMContentLoaded', function () {
    console.log('MealPlanner.js loaded!');

    function addCheckboxListeners() {
        var checkboxes = document.querySelectorAll('.meals input[type="checkbox"]');
        checkboxes.forEach(function (checkbox) {
            checkbox.addEventListener('change', function () {
                console.log('Checkbox changed:', checkbox.checked);
                toggleHighlight(checkbox);
            });
        });
    }

    // Function to toggle the highlight class
    function toggleHighlight(checkbox) {
        checkbox.parentElement.classList.toggle('highlighted', checkbox.checked);
    }

    // Add event listener for when the tabs are shown (Bootstrap event)
    $('#mealTabs').on('shown.bs.tab', function () {
        addCheckboxListeners(); // Add event listeners when a tab is shown
    });

    // Add event listeners initially when the page loads
    addCheckboxListeners();

    var micronutrientCheckboxes = document.querySelectorAll('.micronutrients input[type="checkbox"]');
    micronutrientCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            toggleInputsAndColor(checkbox); // Call toggleInputsAndColor function
        });
    });

    // Function to toggle inputs and change color based on checkbox state
    function toggleInputsAndColor(checkbox) {
        var parentDiv = checkbox.closest('.macro');
        var minMaxInputs = parentDiv.querySelector('.min-max-inputs');
        if (parentDiv && minMaxInputs) {
            if (checkbox.checked) {
                minMaxInputs.style.display = 'block'; // Show the min-max inputs
                toggleBackgroundColor(parentDiv); // Change color when checked
            } else {
                minMaxInputs.style.display = 'none'; // Hide the min-max inputs if unchecked
                parentDiv.style.backgroundColor = ''; // Reset background color when unchecked
            }
        }
    }

    // Function to toggle background color
    function toggleBackgroundColor(parentDiv) {
        if (parentDiv) {
            parentDiv.style.backgroundColor = '#c8e6c9'; // Change background color
        }
    }

    var macronutrientCheckboxes = document.querySelectorAll('.macronutrients input[type="checkbox"]');
    macronutrientCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            var parentDiv = checkbox.closest('.macro');
            var minMaxInputs = parentDiv.querySelector('.min-max-inputs');
            if (checkbox.checked) {
                minMaxInputs.style.display = 'block'; // Show the min-max inputs
            } else {
                minMaxInputs.style.display = 'none'; // Hide the min-max inputs if unchecked
            }
        });
    });

    // Event listeners for allergies checkboxes
    var allergyCheckboxes = document.querySelectorAll('.allergies input[type="checkbox"]');
    allergyCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            console.log('Checkbox changed:', checkbox.value, checkbox.checked);
            toggleHighlight(checkbox);
        });
    });

    // Event listeners for diets checkboxes
    var dietCheckboxes = document.querySelectorAll('.diets input[type="checkbox"]');
    dietCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            console.log('Checkbox changed:', checkbox.value, checkbox.checked);
            toggleHighlight(checkbox);
        });
    });

    function toggleHighlight(checkbox) {
        var parentDiv = checkbox.closest('.form-check');
        if (parentDiv) {
            if (checkbox.checked) {
                parentDiv.style.backgroundColor = '#c8e6c9'; // Highlight when checked
            } else {
                parentDiv.style.backgroundColor = ''; // Remove highlight when unchecked
            }
        }
    }

    var mealCheckboxes = document.querySelectorAll('.meals input[type="checkbox"]');
    mealCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            console.log('Checkbox changed:', checkbox.value, checkbox.checked);
            toggleHighlightMeals(checkbox); // Call toggleHighlight function
        });
    });

    // Function to toggle highlight based on checkbox state
    function toggleHighlightMeals(checkbox) {
        var parentDiv = checkbox.closest('.tab-pane');
        if (parentDiv) {
            if (checkbox.checked) {
                parentDiv.classList.add('highlight'); // Add highlight class when checked
            } else {
                parentDiv.classList.remove('highlight'); // Remove highlight class when unchecked
            }
        }
    }

    var addButton = document.querySelectorAll('.macro .add-button');
    addButton.forEach(function (button) {
        button.addEventListener('click', function () {
            var parentDiv = button.closest('.macro');
            var checkbox = parentDiv.querySelector('input[type="checkbox"]');
            if (checkbox.checked) {
                console.log('Checkbox changed:', checkbox.value, checkbox.checked);
                toggleBackgroundColor(parentDiv);
            } else {
                alert('Please check the checkbox before adding minimum and maximum values.');
            }
        });
    });

    function toggleBackgroundColor(parentDiv) {
        if (parentDiv) {
            parentDiv.style.backgroundColor = '#c8e6c9';
        }
    }

    // Collect and handle calorie information
    document.getElementById('addCalories').addEventListener('click', function () {
        var includeCalories = document.getElementById('includeCalories').checked;
        var minCalories = document.getElementById('calorieMin').value;
        var maxCalories = document.getElementById('calorieMax').value;
        var requestPayload = {
            Meals: [],
            Allergies: [],
            Diets: [],
            Macronutrients: {},
            Micronutrients: {}
        };

        if (includeCalories && !isNaN(parseInt(minCalories, 10)) && !isNaN(parseInt(maxCalories, 10))) {
            requestPayload.CalorieMin = parseInt(minCalories, 10);
            requestPayload.CalorieMax = parseInt(maxCalories, 10);
            console.log("Calorie range set to:", requestPayload.CalorieMin, "to", requestPayload.CalorieMax);
        } else {
            console.log("Calorie values are not included or are invalid.");
        }

        console.log(requestPayload); // For debugging
    });

    // Finalize and send the meal plan request
    document.getElementById('searchButton').addEventListener('click', function () {
        console.log('Search button clicked!');

        // Clear previous error messages
        var errorMessage = document.getElementById('errorMessage');
        if (errorMessage) {
            errorMessage.innerText = '';
        }

        var request = {
            "size": 7,
            "plan": {
                "accept": {
                    "all": [
                        {
                            "health": []
                        }
                    ]
                },
                "fit": {
                    "ENERC_KCAL": {
                        "min": "",
                        "max": ""
                    },
                    "SUGAR.added": {
                        "max": ""
                    }
                },
                "sections": {
                    "Breakfast": {
                        "accept": {
                            "all": [
                                {
                                    "dish": []
                                },
                                {
                                    "meal": []
                                }
                            ]
                        },
                        "fit": {
                            "ENERC_KCAL": {
                                "min": "",
                                "max": ""
                            }
                        }
                    },
                    "Lunch": {
                        "accept": {
                            "all": [
                                {
                                    "dish": []
                                },
                                {
                                    "meal": []
                                }
                            ]
                        },
                        "fit": {
                            "ENERC_KCAL": {
                                "min": "",
                                "max": ""
                            }
                        }
                    },
                    "Dinner": {
                        "accept": {
                            "all": [
                                {
                                    "dish": []
                                },
                                {
                                    "meal": []
                                }
                            ]
                        },
                        "fit": {
                            "ENERC_KCAL": {
                                "min": "",
                                "max": ""
                            }
                        }
                    }
                }
            }
        };

        // Initialize arrays in the request object
        request.Allergies = [];
        request.Diets = [];
        request.Macronutrients = {};
        request.Micronutrients = {};

        // Collect allergies
        document.querySelectorAll('.allergies input[type="checkbox"]:checked').forEach(function (checkbox) {
            request.Allergies.push(checkbox.value);
        });

        // Collect diets
        document.querySelectorAll('.diets input[type="checkbox"]:checked').forEach(function (checkbox) {
            request.Diets.push(checkbox.value);
        });

        // Collect macronutrients and micronutrients
        document.querySelectorAll('.macronutrients input[type="checkbox"]:checked').forEach(function (checkbox) {
            request.Macronutrients[checkbox.value]; // Example values, adjust as necessary
        });

        document.querySelectorAll('.micronutrients input[type="checkbox"]:checked').forEach(function (checkbox) {
            request.Micronutrients[checkbox.value]; // Example values, adjust as necessary
        });

        console.log(request); // Debugging to see what is being sent

        // Collect meal selections
        var selectedMeals = [];
        document.querySelectorAll('.meal-checkbox:checked').forEach(function (checkbox) {
            var sectionName = checkbox.value; // Assuming the value of the checkbox corresponds to the section name
            selectedMeals.push(sectionName);
        });

        // Add selected meals to the request payload
        request.Meals = selectedMeals;

        fetch('/MealPlannerApi/plan', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(request)
        })
            .then(response => {
                console.log('Response from server:', response);
                return response.text(); // Read the response body as text
            })
            .then(text => {
                console.log('Response body:', text);
                // Now, try to parse the response body as JSON
                const data = JSON.parse(text);
                console.log('Success:', data);
                alert('Meal plan created successfully!');
            })
            .catch((error) => {
                console.error('Error:', error);
                if (errorMessage) {
                    errorMessage.innerText = 'Failed to create meal plan. Please try again.';
                }
            });
    });
});
