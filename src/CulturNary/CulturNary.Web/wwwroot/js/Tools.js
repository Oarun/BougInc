var timer;
var isPaused = false;
var totalTime = 0;
var alertDisplayed = false;
var displayStack = [];

document.addEventListener("DOMContentLoaded", function() {

    var weightButton = document.getElementById("weight-button");
    var volumeButton = document.getElementById("volume-button");

    $('#volume-tab').hide();

    weightButton.addEventListener("click", function() {
        weightButton.classList.add("active");
        volumeButton.classList.remove("active");
        
        $('#weight-tab').show();
        $('#volume-tab').hide();
    });

    volumeButton.addEventListener("click", function() {
        volumeButton.classList.add("active");
        weightButton.classList.remove("active");

        $('#volume-tab').show();
        $('#weight-tab').hide();
    });

    var weightUnitButtonsInput = document.getElementById("weight-unit-buttons-input");
    var weightUnitButtonsOutput = document.getElementById("weight-unit-buttons-output");

    var volumeUnitButtonsInput = document.getElementById("volume-unit-buttons-input");
    var volumeUnitButtonsOutput = document.getElementById("volume-unit-buttons-output");

    var weightUnits = ["Grams", "Ounces", "Pounds"];
    var volumeUnits = ["Milliliters (mL)", "Liters (L)", "Teaspoons (tsp)", "Tablespoons (tbsp)", "Fluid Ounces (fl oz)","Cups", "Pints", "Quarts", "Gallons"];

    var weightInput = document.getElementById("weight-input");
    var volumeInput = document.getElementById("volume-input");

    var weightOutput = document.getElementById("weight-output");
    var volumeOutput = document.getElementById("volume-output");
    
    // Add event listeners to unit buttons for weight input
    weightUnits.forEach(function(unit) {
        var button = document.createElement("button");
        button.classList.add("unit-btn");
        button.classList.add("profile-btn")
        button.textContent = unit;
        button.addEventListener("click", handleUnitButtonClickWeightInput);
        weightUnitButtonsInput.appendChild(button);
    });

    // Add event listeners to unit buttons for weight output
    weightUnits.forEach(function(unit) {
        var button = document.createElement("button");
        button.classList.add("unit-btn");
        button.classList.add("profile-btn")
        button.textContent = unit;
        button.addEventListener("click", handleUnitButtonClickWeightOutput);
        weightUnitButtonsOutput.appendChild(button);
    });
    
    // Add event listeners to unit buttons for weight input
    volumeUnits.forEach(function(unit) {
        var button = document.createElement("button");
        button.classList.add("unit-btn");
        button.classList.add("profile-btn")
        button.textContent = unit;
        button.addEventListener("click", handleUnitButtonClickVolumeInput);
        volumeUnitButtonsInput.appendChild(button);
    });

    // Add event listeners to unit buttons for weight output
    volumeUnits.forEach(function(unit) {
        var button = document.createElement("button");
        button.classList.add("unit-btn");
        button.classList.add("profile-btn")
        button.textContent = unit;
        button.addEventListener("click", handleUnitButtonClickVolumeOutput);
        volumeUnitButtonsOutput.appendChild(button);
    });

    weightUnitButtonsInput.querySelectorAll("button").forEach(function(button) {
        button.addEventListener("click", handleUnitButtonClickWeightInput);
    });
    
    // Add event listeners to unit buttons for weight output
    weightUnitButtonsOutput.querySelectorAll("button").forEach(function(button) {
        button.addEventListener("click", handleUnitButtonClickWeightOutput);
    });
    
    // Add input event listener for weight input
    weightInput.addEventListener("input", handleConversionWeight);

    // Add event listeners to unit buttons for volume input
    volumeUnitButtonsInput.querySelectorAll("button").forEach(function(button) {
        button.addEventListener("click", handleUnitButtonClickVolumeInput);
    });

    // Add event listeners to unit buttons for volume output
    volumeUnitButtonsOutput.querySelectorAll("button").forEach(function(button) {
        button.addEventListener("click", handleUnitButtonClickVolumeOutput);
    });

    // Add input event listener for volume input
    volumeInput.addEventListener("input", handleConversionVolume);

    function handleUnitButtonClickWeightInput(event) {
        // Set the clicked button to active
        var clickedButton = event.target;
        weightUnitButtonsInput.querySelectorAll("button").forEach(function(button) {
            button.classList.remove("activeButton");
        });
        clickedButton.classList.add("activeButton");
    
        // Perform conversion
        handleConversionWeight();
    }
    
    function handleUnitButtonClickWeightOutput(event) {
        // Set the clicked button to active
        var clickedButton = event.target;
        weightUnitButtonsOutput.querySelectorAll("button").forEach(function(button) {
            button.classList.remove("activeButton");
        });
        clickedButton.classList.add("activeButton");
    
        // Perform conversion
        handleConversionWeight();
    }
    
    function handleConversionWeight() {
        var inputUnitButton = weightUnitButtonsInput.querySelector(".activeButton");
        var outputUnitButton = weightUnitButtonsOutput.querySelector(".activeButton");

        if (!inputUnitButton || !outputUnitButton || !weightInput.value) {
            // Clear the output value
            weightOutput.value = "";
            return;
        }

        var inputUnit = inputUnitButton.textContent;
        var outputUnit = outputUnitButton.textContent;
    
        // Set input value
        var inputValue = parseFloat(weightInput.value);
    
        // Perform conversion
        var outputValue;
        switch (outputUnit) {
            case "Grams":
                outputValue = inputValue * conversionFactorsWeight[inputUnit].grams;
                break;
            case "Ounces":
                outputValue = inputValue * conversionFactorsWeight[inputUnit].ounces;
                break;
            case "Pounds":
                outputValue = inputValue * conversionFactorsWeight[inputUnit].pounds;
                break;
            default:
                outputValue = inputValue; // No conversion needed
        }
    
        // Display the result
        weightOutput.value = outputValue.toFixed(4);
    }
    
    // Conversion factors for weight units
    var conversionFactorsWeight = {
        "Grams": {
            grams: 1,
            ounces: 0.03527396,
            pounds: 0.00220462
        },
        "Ounces": {
            grams: 28.3495,
            ounces: 1,
            pounds: 0.0625
        },
        "Pounds": {
            grams: 453.592,
            ounces: 16,
            pounds: 1
        }
    };
    
    function handleUnitButtonClickVolumeInput(event) {
        // Set the clicked button to active
        var clickedButton = event.target;
        volumeUnitButtonsInput.querySelectorAll("button").forEach(function(button) {
            button.classList.remove("activeButton");
        });
        clickedButton.classList.add("activeButton");
    
        // Perform conversion
        handleConversionVolume();
    }
    
    function handleUnitButtonClickVolumeOutput(event) {
        // Set the clicked button to active
        var clickedButton = event.target;
        volumeUnitButtonsOutput.querySelectorAll("button").forEach(function(button) {
            button.classList.remove("activeButton");
        });
        clickedButton.classList.add("activeButton");
    
        // Perform conversion
        handleConversionVolume();
    }
    
    function handleConversionVolume() {
        var inputUnitButton = volumeUnitButtonsInput.querySelector(".activeButton");
        var outputUnitButton = volumeUnitButtonsOutput.querySelector(".activeButton");

        if (!inputUnitButton || !outputUnitButton || !volumeInput.value) {
            // Clear the output value
            volumeOutput.value = "";
            return;
        }

        var inputUnit = inputUnitButton.textContent;
        var outputUnit = outputUnitButton.textContent;
    
        // Set input value
        var inputValue = parseFloat(volumeInput.value);
    
        // Perform conversion
        var outputValue;
        switch (outputUnit) {
            case "Milliliters (mL)":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].milliliters;
                break;
            case "Liters (L)":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].liters;
                break;
            case "Teaspoons (tsp)":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].teaspoons;
                break;
            case "Tablespoons (tbsp)":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].tablespoons;
                break;
            case "Fluid Ounces (fl oz)":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].fluidOunces;
                break;
            case "Cups":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].cups;
                break;
            case "Pints":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].pints;
                break;
            case "Quarts":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].quarts;
                break;
            case "Gallons":
                outputValue = inputValue * conversionFactorsVolume[inputUnit].gallons;
                break;
            default:
                outputValue = inputValue; // No conversion needed
        }
    
        // Display the result
        volumeOutput.value = outputValue.toFixed(4);
    }
    
    var conversionFactorsVolume = {
        "Milliliters (mL)": {
            milliliters: 1,
            liters: 0.001,
            teaspoons: 0.202884,
            tablespoons: 0.067628,
            fluidOunces: 0.033814,
            cups: 0.00422675,
            pints: 0.00211338,
            quarts: 0.00105669,
            gallons: 0.000264172
        },
        "Liters (L)": {
            milliliters: 1000,
            liters: 1,
            teaspoons: 202.884,
            tablespoons: 67.628,
            fluidOunces: 33.814,
            cups: 4.22675,
            pints: 2.11338,
            quarts: 1.05669,
            gallons: 0.264172
        },
        "Teaspoons (tsp)": {
            milliliters: 4.92892,
            liters: 0.00492892,
            teaspoons: 1,
            tablespoons: 0.333333,
            fluidOunces: 0.166667,
            cups: 0.0205372,
            pints: 0.0102536,
            quarts: 0.00512681,
            gallons: 0.00130208
        },
        "Tablespoons (tbsp)": {
            milliliters: 14.7868,
            liters: 0.0147868,
            teaspoons: 3,
            tablespoons: 1,
            fluidOunces: 0.5,
            cups: 0.0625,
            pints: 0.03125,
            quarts: 0.015625,
            gallons: 0.00390625
        },
        "Fluid Ounces (fl oz)": {
            milliliters: 29.5735,
            liters: 0.0295735,
            teaspoons: 6,
            tablespoons: 2,
            fluidOunces: 1,
            cups: 0.125,
            pints: 0.0625,
            quarts: 0.03125,
            gallons: 0.0078125
        },
        "Cups": {
            milliliters: 236.588,
            liters: 0.236588,
            teaspoons: 48,
            tablespoons: 16,
            fluidOunces: 8,
            cups: 1,
            pints: 0.5,
            quarts: 0.25,
            gallons: 0.0625
        },
        "Pints": {
            milliliters: 473.176,
            liters: 0.473176,
            teaspoons: 96,
            tablespoons: 32,
            fluidOunces: 16,
            cups: 2,
            pints: 1,
            quarts: 0.5,
            gallons: 0.125
        },
        "Quarts": {
            milliliters: 946.353,
            liters: 0.946353,
            teaspoons: 192,
            tablespoons: 64,
            fluidOunces: 32,
            cups: 4,
            pints: 2,
            quarts: 1,
            gallons: 0.25
        },
        "Gallons": {
            milliliters: 3785.41,
            liters: 3.78541,
            teaspoons: 768,
            tablespoons: 256,
            fluidOunces: 128,
            cups: 16,
            pints: 8,
            quarts: 4,
            gallons: 1
        }
    };

    // Checklist


});

function setTimer() {
    var hours = parseInt(document.getElementById("hours").value) || 0;
    var minutes = parseInt(document.getElementById("minutes").value) || 0;
    var seconds = parseInt(document.getElementById("seconds").value) || 0;

    totalTime = hours * 3600 + minutes * 60 + seconds;

    if (totalTime <= 0 || isNaN(totalTime)) {
        alert("Please enter a valid time.");
        return;
    }

    var timerDisplay = document.getElementById("timer");
    timerDisplay.textContent = formatTime(totalTime);

    timer = setInterval(function() {
        if (!isPaused) {
            totalTime--;
            timerDisplay.textContent = formatTime(totalTime);

            if (totalTime <= 0 && !alertDisplayed) {
                clearInterval(timer);
                alertDisplayed = true;
                showAlert();
            }
        }
    }, 1000);

    document.getElementById("pauseResumeBtn").disabled = false;
    document.getElementById("pauseResumeBtn").textContent = "Pause";
}

function showAlert() {
    document.getElementById("alarmSound").loop = true;
    document.getElementById("alarmSound").play();
    var confirmDismiss = confirm("Time's up! Click OK to dismiss the alarm.");
    if (confirmDismiss) {
        document.getElementById("alarmSound").pause();
        alertDisplayed = false;
        clearInterval(timer);
        document.getElementById("timer").textContent = "00:00:00";
    }
}

function pauseResumeTimer() {
    if (!isPaused) {
        clearInterval(timer);
        document.getElementById("pauseResumeBtn").textContent = "Resume";
    } else {
        timer = setInterval(function() {
            if (!isPaused) {
                totalTime--;
                document.getElementById("timer").textContent = formatTime(totalTime);
                if (totalTime <= 0 && !alertDisplayed) {
                    clearInterval(timer);
                    alertDisplayed = true;
                    showAlert();
                }
            }
        }, 1000);
        document.getElementById("pauseResumeBtn").textContent = "Pause";
    }
    isPaused = !isPaused;
}

function formatTime(seconds) {
    var hours = Math.floor(seconds / 3600);
    var minutes = Math.floor((seconds % 3600) / 60);
    var secs = seconds % 60;

    return (
        (hours < 10 ? "0" : "") + hours + ":" +
        (minutes < 10 ? "0" : "") + minutes + ":" +
        (secs < 10 ? "0" : "") + secs
    );
}

function addToDisplay(value) {
    if (value === 'Back') { // If the value is "Back", remove the last item from the display stack
        displayStack.pop();
    } else if (value === '.') { // If the value is a decimal point
        var lastValue = displayStack[displayStack.length - 1];
        // Check if the last value is a number
        if (!lastValue || isNaN(parseFloat(lastValue))) { // If last value is empty or not a number
            displayStack.push('0.'); // Add "0." to the display stack
        } else if (!lastValue.includes('.')) { // If the last value doesn't already contain a decimal point
            displayStack[displayStack.length - 1] += '.'; // Append decimal point to the last value
        }
    } else { // For other values, push them onto the display stack
        displayStack.push(value);
    }

    document.getElementById("display").value = displayStack.join('');
}

function calculate() {
    var expression = document.getElementById("display").value;
    var result;
    try {
        result = evaluateExpression(expression);
        document.getElementById("display").value = result;
    } catch (error) {
        document.getElementById("display").value = "Error";
        displayStack = [];
    }
}

function evaluateExpression(expression) {
    // Using regular expression to extract numbers, operators, and parentheses
    var tokens = expression.match(/\d+(\.\d+)?|\+|\-|\*|\/|\(|\)/g); // Updated regular expression to match decimal numbers

    // Implementing basic order of operations
    var output = [];
    var operatorStack = [];
    var precedence = {
        '+': 1,
        '-': 1,
        '*': 2,
        '/': 2
    };

    tokens.forEach(function(token) {
        if (/^\d+(\.\d+)?$/.test(token)) { // Check if token is a number or a decimal
            output.push(parseFloat(token));
        } else if ('+-*/'.indexOf(token) !== -1) {
            while (operatorStack.length > 0 && precedence[operatorStack[operatorStack.length - 1]] >= precedence[token]) {
                output.push(operatorStack.pop());
            }
            operatorStack.push(token);
        } else if (token === '(') {
            operatorStack.push(token);
        } else if (token === ')') {
            while (operatorStack[operatorStack.length - 1] !== '(') {
                output.push(operatorStack.pop());
            }
            operatorStack.pop(); // Discard the '('
        }
    });

    while (operatorStack.length > 0) {
        output.push(operatorStack.pop());
    }

    // Evaluating the postfix expression
    var stack = [];
    output.forEach(function(token) {
        if (typeof token === 'number') {
            stack.push(token);
        } else {
            var b = stack.pop();
            var a = stack.pop();
            switch (token) {
                case '+':
                    stack.push(a + b);
                    break;
                case '-':
                    stack.push(a - b);
                    break;
                case '*':
                    stack.push(a * b);
                    break;
                case '/':
                    if (b === 0) {
                        throw new Error('Division by zero');
                    }
                    stack.push(a / b);
                    break;
            }
        }
    });

    if (stack.length !== 1) {
        throw new Error('Invalid expression');
    }

    displayStack = [stack[0]];
    return stack[0];
}

function clearDisplay() {
    displayStack = [];
    document.getElementById("display").value = "";
}

function addItem() {
    var input = document.getElementById("addItemInput");
    var itemText = input.value.trim();
    if (itemText === "") return;

    var newItem = document.createElement("li");
    newItem.className = "checklist-item";
    newItem.style.listStyleType = "none";
    newItem.draggable = true;
    newItem.innerHTML = `
        <label class="checklist-toggle-container">
        <input type="checkbox">
        <span class="checklist-toggle" onchange="toggleItem(this)"></span>
        </label>
        <input class="checklist-text"  type="text" value="${itemText}" style="font-size: 20px; width: 300px;" disabled>
        <button class="profile-btn unit-btn"  onclick="editItem(this)">Edit</button>
        <button class="profile-btn unit-btn" onclick="deleteItem(this)">Delete</button>
    `;

    newItem.querySelector(".checklist-toggle").addEventListener("click", function() {
        console.log("toggle");
        toggleItem(this);
    });

    // newItem.addEventListener("dragstart", handleDragStart);
    // newItem.addEventListener("dragenter", handleDragEnter);
    // newItem.addEventListener("dragover", handleDragOver);
    // newItem.addEventListener("dragleave", handleDragLeave);
    // newItem.addEventListener("drop", handleDrop);

    document.getElementById("checklistItems").appendChild(newItem);
    input.value = "";
}

function editItem(button) {
    var item = button.parentNode;
    var textInput = item.querySelector("input[type='text']");
    var buttonText = button.textContent;

    if (buttonText === "Edit") {
        textInput.disabled = false;
        button.textContent = "Save";
    } else {
        textInput.disabled = true;
        button.textContent = "Edit";
    }
}

function deleteItem(button) {
    var item = button.parentNode;
    item.parentNode.removeChild(item);
}

function toggleItem(checkbox) {
    var textInput = checkbox.parentNode.nextElementSibling;
    console.log(textInput);
    if (textInput.style.textDecoration !== "line-through") {
        textInput.style.textDecoration = "line-through";
    } else {
        textInput.style.textDecoration = "none";
    }
}

// function handleDragStart(event) {
//     event.dataTransfer.setData("text/plain", event.target.id);
//     event.target.classList.add("dragging");
// }

// function handleDragEnter(event) {
//     event.preventDefault();
//     event.target.classList.add("dragover");
// }

// function handleDragOver(event) {
//     event.preventDefault();
// }

// function handleDragLeave(event) {
//     event.target.classList.remove("dragover");
// }

// function handleDrop(event) {
//     event.preventDefault();
//     var draggedItem = document.querySelector('.dragging');
//     var newList = this.parentNode;

//     var draggedIndex = Array.from(newList.children).indexOf(draggedItem);
//     var dropIndex = Array.from(newList.children).indexOf(this);

//     // If the dragged item is already above the drop target, insert it before the drop target.
//     if (draggedIndex < dropIndex) {
//         newList.insertBefore(draggedItem, this.nextSibling);
//     } 
//     // If the dragged item is below the drop target, or the drop target is the last item,
//     // insert it before the drop target.
//     else if (draggedIndex > dropIndex || dropIndex === -1) {
//         newList.insertBefore(draggedItem, this);
//     }
// }