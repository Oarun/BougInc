document.addEventListener('DOMContentLoaded', function() {
    var buttons = document.querySelectorAll('button');

   buttons.forEach(function(button) {
    button.addEventListener('click', function() {
        var buttonText = button.textContent;
        var userId = button.getAttribute('data-user-id');

        if(buttonText === 'Send Friend Request') {
            // Make an AJAX call to the SendFriendRequest endpoint
            fetch(`/api/FriendApi/SendFriendRequest/${userId}`, { // Send the user ID in the URL
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                if (!response.ok && response.status === 400) {
                    throw new Error("Unable to send friend request. Please try again later.");
                }
                return response.json();
            })
            .then(data => {
                console.log('Friend request sent successfully');
                // Update the button text and style
                button.textContent = 'Friend Request Pending';
                button.className = 'btn btn-secondary';
                button.disabled = true;
            })
            .catch(error => {
                console.error('Error:', error);
                // Update the button text and style
                button.textContent = 'Unable to Send Friend Request';
                button.className = 'btn btn-danger';
            });
        } else if(buttonText === 'Friended') {
            // Add your logic for "Friended" here
            console.log('Friended button clicked for user with id: ' + userId);
        }
    });
});
});