var personId;

$(document).ready(function() {
    getPerson(); // get the personId.

    // Add JavaScript here to handle form submissions and calculations
    document.getElementById('calculate-calories-form').addEventListener('submit', async function(event) {
        event.preventDefault();
        // Calculate daily caloric intake logic here
        // Get form input values
    const weight = parseFloat(document.getElementById('weight').value);
    const height = parseFloat(document.getElementById('height').value);
    const age = parseInt(document.getElementById('age').value);
    const gender = document.getElementById('gender').value;
    const activityLevel = parseFloat(document.getElementById('activity-level').value);

    // Call the function to calculate daily caloric intake
    const dailyCaloricIntake = calculateDailyCaloricIntake(weight, height, age, gender, activityLevel);

    // Log the result to the console
    console.log("Daily Caloric Intake:", dailyCaloricIntake.toFixed(0), "calories");
    var CalorieTracker = {
        personId: personId,
        personCalories: dailyCaloricIntake.toFixed(0)
    };

    await postDailyCaloricIntake(CalorieTracker);
    });

    document.getElementById('log-calories-form').addEventListener('submit', function(event) {
        event.preventDefault();
        // Log daily caloric intake logic here
    });

    $("#CaloricIntakeCalc").show();

    $('#log-calories-form').submit(function (event) {
        event.preventDefault();
        
        var formData = {
            PersonId: personId, // Assuming you retrieve the PersonId from somewhere
            CaloriesLogged: $('#calories').val(),
            LogDate: $('#date').val()
        };

        console.log(formData)
        // Call the function to make the AJAX POST request
        postCalorieLog(formData);
    });

    $('#view-logs-btn').click(function () {
        fetchLogEntries(personId);
    });

    // Close the modal when the close button is clicked
    $('.close').on('click', function () {
        $('#logModal').css('display', 'none');
    });

    // Close the modal when clicking outside of it
    $(window).on('click', function (event) {
        if (event.target == $('#logModal')[0]) {
            $('#logModal').css('display', 'none');
        }
    });
});

function getPerson() {
    $.ajax({
        url: '/api/Person/GetCurrentPerson',
        type: 'GET',
        success: function (data) {
            personId = data.id;
            fetchCalorieTracker(personId);
        }
    });
}

function fetchCalorieTracker(id) {
    console.log(id)
    $.ajax({
        url: "/api/CalorieTracker/" + id,
        type: "GET",
        contentType: "application/json",
        success: function (calorieTracker) {
            //$("#CaloricIntakeCalc").hide();
            console.log(calorieTracker); // Handle the data as needed
        },
        error: function (error) {
            // Handle errors during retrieving restaurants
            console.error("Error retrieving CalorieTracker:", error);
        }
    });
}

function calculateDailyCaloricIntake(weight, height, age, gender, activityLevel) {
    // Constants for calculating BMR
    const maleConstant = 66;
    const femaleConstant = 655;

    // Convert weight to kilograms (1 pound = 0.453592 kilograms)
    const weightKg = weight * 0.453592;

    // Convert height to centimeters (1 inch = 2.54 centimeters)
    const heightCm = height * 2.54;

    // Calculate BMR based on gender
    let bmr;
    if (gender === 'male') {
        bmr = (10 * weightKg) + (6.25 * heightCm) - (5 * age) + maleConstant;
    } else if (gender === 'female') {
        bmr = (10 * weightKg) + (6.25 * heightCm) - (5 * age) + femaleConstant;
    } else {
        console.error('Invalid gender');
        return null;
    }

    // Calculate daily caloric intake based on activity level
    let dailyCaloricIntake;
    switch (activityLevel) {
        case 1.2:
            dailyCaloricIntake = bmr * 1.2; // Sedentary
            break;
        case 1.375:
            dailyCaloricIntake = bmr * 1.375; // Lightly active
            break;
        case 1.55:
            dailyCaloricIntake = bmr * 1.55; // Moderately active
            break;
        case 1.725:
            dailyCaloricIntake = bmr * 1.725; // Very active
            break;
        case 1.9:
            dailyCaloricIntake = bmr * 1.9; // Super active
            break;
        default:
            console.error('Invalid activity level');
            return null;
    }

    return dailyCaloricIntake;
}
// Function to post daily caloric intake to the API
async function postDailyCaloricIntake(CalorieTracker) {
    $.ajax({
        url: "/api/CalorieTracker/",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(CalorieTracker),
        success: function (response) {
            // Handle successful response
            console.log("CalorieTracker created successfully:", response);
        },
        error: function (error) {
            // Handle errors during API call
            console.error("Error creating CalorieTracker:", error);
        }
    });
}

// Define a function for making the AJAX POST request
async function postCalorieLog(CalorieLog) {
    console.log(CalorieLog)
    $.ajax({
        url: "/api/CalorieLog/",
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(CalorieLog),
        success: function (response) {
            // Handle successful response
            console.log("CalorieLog logged successfully:", response);
        },
        error: function (error) {
            // Handle errors during API call
            console.error("Error logging CalorieLog:", error);
        }
    });
}

// Function to fetch log entries via AJAX
async function fetchLogEntries(personId) {
    $.ajax({
        type: 'GET',
        url: '/api/CalorieLog/' + personId,
        success: function (data) {
            displayLogEntries(data);
        },
        error: function () {
            alert('Failed to fetch log entries.');
        }
    });
}

// Function to display log entries in the modal
function displayLogEntries(entries) {
    // Show modal
    $('#logModal').css('display', 'block');
    var modalBody = $('#logModal').find('.modal-body');
    modalBody.empty(); // Clear previous entries
    if (entries.length > 0) {
        var table = $('<table class="table"></table>');
        var tableHead = $('<thead><tr><th>Date</th><th>Calories</th></tr></thead>');
        var tableBody = $('<tbody></tbody>');
        entries.forEach(function (entry) {
            console.log(entry)
            var row = $('<tr></tr>');
            row.append('<td>' + entry.logDate + '</td>');
            row.append('<td>' + entry.caloriesLogged + '</td>');
            tableBody.append(row);
        });
        table.append(tableHead);
        table.append(tableBody);
        modalBody.append(table);
    } else {
        modalBody.text('No log entries found.');
    }
}