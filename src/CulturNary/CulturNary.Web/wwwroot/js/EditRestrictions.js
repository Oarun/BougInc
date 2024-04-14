document.addEventListener("DOMContentLoaded", function() {

    document.querySelectorAll(".toggle-buttons .form-check-res-input").forEach(function(checkbox) {
        var label = document.querySelector("label[for='" + checkbox.id + "']");
        if (checkbox.checked) {
            label.classList.add('active');
        } 
        else {
            label.classList.remove('active');
        }
    });

    // Attach click event listener to all labels within the toggle-buttons container
    document.querySelectorAll(".toggle-buttons .form-check-res-label").forEach(function(label) {

        label.addEventListener("click", function() {
            var checkboxId = this.getAttribute('for');
            var checkbox = document.getElementById(checkboxId);

            this.classList.toggle('active', !checkbox.checked); // Toggle the active class on the label
        });
    });
});