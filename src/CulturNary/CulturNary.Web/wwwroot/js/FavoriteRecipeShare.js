document.addEventListener('DOMContentLoaded', function() {
    // When the Share button is clicked, fetch the friends and populate the dropdown
    document.querySelectorAll('.share-btn').forEach(function(button) {
        button.addEventListener('click', function() {
            var recipeId = this.getAttribute('data-recipe-id');
            document.getElementById('shareRecipeBtn').setAttribute('data-recipe-id', recipeId);

            fetch('/api/FriendApi/GetFriends')
                .then(response => response.json())
                .then(data => {
                    var dropdown = document.getElementById('friendSelect');
                    dropdown.innerHTML = '';
                    if (Array.isArray(data)) {
                        if (data.length === 0) {
                            var option = document.createElement('option');
                            option.textContent = 'No friends to share with';
                            dropdown.appendChild(option);
                        } else {
                            data.forEach(function(friend) {
                                var option = document.createElement('option');
                                option.value = friend.id;
                                var userName = friend.userName.split('@')[0]; // Split the email by the @ symbol and take the first part
                                option.textContent = userName;
                                dropdown.appendChild(option);
                            });
                        }
                    } else {
                        console.error('data is not an array', data);
                    }

                    // Show the modal after the dropdown has been populated
                    $('#shareModal').modal('show');
                });
        });
    });

    // When the Share button in the modal is clicked, make an API call to share the recipe
    document.getElementById('shareRecipeBtn').addEventListener('click', function() {
        var recipeId = this.getAttribute('data-recipe-id');
        var friendId = document.getElementById('friendSelect').value;

        fetch(`/api/FriendApi/SendRecipeToFriend/${friendId}/${recipeId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then(() => {
            $('#shareModal').modal('hide');
        });
    });
});