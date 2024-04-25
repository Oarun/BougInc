var timer;
var isPaused = false;
var totalTime = 0;
var alertDisplayed = false;

$(document).ready(function () {



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