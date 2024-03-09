$(document).ready(function() {
    $('tr[data-user-id]').on('click', function() {
        var userId = $(this).data('userId');

        // Fetch user details from your API
        fetch('/Admin/Users/' + userId)
            .then(response => response.json())
            .then(user => {
                // Populate the modal with the user's details
                $('#userId').text(user.id);
                $('#userName').text(user.userName);
                $('#normalizedUserName').text(user.normalizedUserName);
                $('#email').text(user.email);
                $('#normalizedEmail').text(user.normalizedEmail);
                $('#emailConfirmed').text(user.emailConfirmed ? 'Yes' : 'No');
                $('#phoneNumber').text(user.phoneNumber ? user.phoneNumber : 'N/A');
                $('#twoFactorEnabled').text(user.twoFactorEnabled ? 'Yes' : 'No');

                // Show the modal
                $('#myModal').modal('show');
            })
            .catch(error => console.error('Error:', error));
    });
});