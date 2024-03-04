document.getElementById('registerForm').addEventListener('submit', function(e) {
    var recaptchaResponse = grecaptcha.getResponse();
    if (recaptchaResponse.length == 0) {
        e.preventDefault();
        alert("Please verify the reCAPTCHA");
    } else {
        document.getElementById('Input_RecaptchaResponse').value = recaptchaResponse;
    }
});