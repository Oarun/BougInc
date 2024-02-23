document.getElementById('registerForm').addEventListener('submit', function(event) {
    event.preventDefault();
    grecaptcha.ready(function() {
        grecaptcha.execute('6LeE2nwpAAAAAEXBLHPq-3aJX9uYs3cOijVxFWsf', {action: 'submit'}).then(function(token) {
            document.getElementById('recaptchaResponse').value = token;
            event.target.submit();
        });
    });
});