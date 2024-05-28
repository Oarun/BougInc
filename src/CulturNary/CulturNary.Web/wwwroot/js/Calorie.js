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
        fetchLogEntries(personId,0);
    });

    $('#weekly-logs-btn').click(function () {
        fetchLogEntries(personId,1);
    });

    $('#monthly-logs-btn').click(function () {
        fetchLogEntries(personId,2);
    });

    $('#yearly-logs-btn').click(function () {
        fetchLogEntries(personId,3);
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
            fetchYearlyLogEntriesForGraph(personId);
            fetchMonthlyLogEntriesForGraph(personId);
            fetchWeeklyLogEntriesForGraph(personId)
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
            console.log(calorieTracker); // Handle the data as needed
            if (calorieTracker && calorieTracker.length > 0) {
                // Get the last entry in the list
                var lastEntry = calorieTracker[calorieTracker.length - 1];
                // Update the h5 element with the last entry's calories logged
                $('#currentColoricCalc').text('Daily Caloric Need: ' + lastEntry.personCalories);
            }
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
            fetchCalorieTracker(personId)
            console.log("CalorieTracker created successfully:", response);
        },
        error: function (error) {
            // Handle errors during API call
            console.error("Error creating CalorieTracker:", error);
        }
    });
}

// Function for making the AJAX POST request
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
            fetchYearlyLogEntriesForGraph(personId);
            fetchMonthlyLogEntriesForGraph(personId);
            fetchWeeklyLogEntriesForGraph(personId)
        },
        error: function (error) {
            // Handle errors during API call
            console.error("Error logging CalorieLog:", error);
        }
    });
}

// Function to fetch log entries via AJAX
async function fetchLogEntries(personId, number) {
    $.ajax({
        type: 'GET',
        url: '/api/CalorieLog/' + personId,
        success: function (data) {
            displayLogEntries(data, number);
        },
        error: function () {
            alert('Failed to fetch log entries.');
        }
    });
}

// Function to display log entries in the modal
function displayLogEntries(entries, number) {
    // Show modal
    $('#logModal').css('display', 'block');
    var logEntriesTable = $('#logEntriesTable');
    var averageCaloriesDiv = $('#averageCalories');
    logEntriesTable.empty(); // Clear previous entries
    averageCaloriesDiv.empty(); // Clear previous average

    if (entries.length > 0) {
        var filteredEntries = filterEntries(entries, number);

        if (filteredEntries.length > 0) {
            var table = $('<table class="table"></table>');
            var tableHead = $('<thead><tr><th>Date</th><th>Calories</th></tr></thead>');
            var tableBody = $('<tbody></tbody>');

            filteredEntries.forEach(function (entry) {
                var row = $('<tr></tr>');
                row.append('<td>' + entry.logDate + '</td>');
                row.append('<td>' + entry.caloriesLogged + '</td>');
                tableBody.append(row);
            });

            table.append(tableHead);
            table.append(tableBody);
            logEntriesTable.append(table);

            // Calculate the average calories
            var totalCalories = filteredEntries.reduce(function (sum, entry) {
                return sum + entry.caloriesLogged;
            }, 0);
            var averageCalories = (totalCalories / filteredEntries.length).toFixed(2);

            // Append the average to the modal
            averageCaloriesDiv.text('Average Calories: ' + averageCalories);
        } else {
            logEntriesTable.text('No log entries found for the selected period.');
        }
    } else {
        logEntriesTable.text('No log entries found.');
    }
}

// Function to filter entries based on the number parameter
function filterEntries(entries, number) {
    var now = new Date();
    var filteredEntries = [];

    entries.forEach(function (entry) {
        var entryDate = new Date(entry.logDate);

        switch (number) {
            case 0: // Show all entries
                filteredEntries.push(entry);
                break;
            case 1: // Show current week entries
                var startOfWeek = new Date(now.setDate(now.getDate() - now.getDay()));
                startOfWeek.setHours(0, 0, 0, 0);
                var endOfWeek = new Date(startOfWeek);
                endOfWeek.setDate(startOfWeek.getDate() + 6);
                endOfWeek.setHours(23, 59, 59, 999);

                if (entryDate >= startOfWeek && entryDate <= endOfWeek) {
                    filteredEntries.push(entry);
                }
                break;
            case 2: // Show current month entries
                var startOfMonth = new Date(now.getFullYear(), now.getMonth(), 1);
                var endOfMonth = new Date(now.getFullYear(), now.getMonth() + 1, 0);

                if (entryDate >= startOfMonth && entryDate <= endOfMonth) {
                    filteredEntries.push(entry);
                }
                break;
            case 3: // Show current year entries
                var startOfYear = new Date(now.getFullYear(), 0, 1);
                var endOfYear = new Date(now.getFullYear(), 11, 31);

                if (entryDate >= startOfYear && entryDate <= endOfYear) {
                    filteredEntries.push(entry);
                }
                break;
        }
    });

    return filteredEntries;
}

// Function to fetch log entries via AJAX
async function fetchYearlyLogEntriesForGraph(personId) {
    $.ajax({
        type: 'GET',
        url: '/api/CalorieLog/' + personId,
        success: function (data) {
            console.log(data)
            renderYearlyGraph(data);
        },
        error: function () {
            alert('Failed to fetch log entries.');
        }
    });
}

// Function to render graph
function renderYearlyGraph(data) {
    // Parse your array into labels and data arrays
    const labels = data.map(entry => entry.logDate);
    const calories = data.map(entry => entry.caloriesLogged);

    // Render the graph using Chart.js
    const ctx = document.getElementById('yearlyCaloriesChart').getContext('2d');
    const caloriesChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Calories Logged',
                data: calories,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

async function fetchMonthlyLogEntriesForGraph(personId) {
    try {
        const today = new Date();
        const year = today.getFullYear();
        const month = today.getMonth() + 1; // Months are zero-indexed

        const data = await $.ajax({
            type: 'GET',
            url: '/api/CalorieLog/' + personId,
        });

        // Filter data for the current month
        const currentMonthData = data.filter(entry => {
            const entryDate = new Date(entry.logDate);
            return entryDate.getFullYear() === year && entryDate.getMonth() + 1 === month;
        });

        // Render graph for current month data
        renderMonthlyGraph(currentMonthData);
    } catch (error) {
        //alert('Failed to fetch log entries for the current month.');
        console.error(error);
    }
}

// Function to render graph
function renderMonthlyGraph(data) {
    // Parse your array into labels and data arrays
    const labels = data.map(entry => entry.logDate);
    const calories = data.map(entry => entry.caloriesLogged);

    // Render the graph using Chart.js
    const ctx = document.getElementById('monthlyCaloriesChart').getContext('2d');
    const caloriesChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Calories Logged',
                data: calories,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

async function fetchWeeklyLogEntriesForGraph(personId) {
    try {
        const today = new Date();
        const startOfWeek = new Date(today.getFullYear(), today.getMonth(), today.getDate() - today.getDay());
        const endOfWeek = new Date(today.getFullYear(), today.getMonth(), today.getDate() - today.getDay() + 6);

        const data = await $.ajax({
            type: 'GET',
            url: '/api/CalorieLog/' + personId,
        });

        // Filter data for the current week
        const currentWeekData = data.filter(entry => {
            const entryDate = new Date(entry.logDate);
            return entryDate >= startOfWeek && entryDate <= endOfWeek;
        });

        // Render graph for current week data
        renderWeeklyGraph(currentWeekData);
    } catch (error) {
       // alert('Failed to fetch log entries for the current week.');
        console.error(error);
    }
}

// Function to render graph
function renderWeeklyGraph(data) {
    // Parse your array into labels and data arrays
    const labels = data.map(entry => entry.logDate);
    const calories = data.map(entry => entry.caloriesLogged);

    // Render the graph using Chart.js
    const ctx = document.getElementById('weeklyCaloriesChart').getContext('2d');
    const caloriesChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Calories Logged',
                data: calories,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}