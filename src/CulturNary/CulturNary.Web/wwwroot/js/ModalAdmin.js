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
                $('#emailConfirmed').text(user.emailConfirmed);
                $('#phoneNumber').text(user.phoneNumber || "N/A" );
                $('#twoFactorEnabled').text(user.twoFactorEnabled);

                // Fetch person details
                return fetch('/api/Person/Identity/' + userId);
            })
            .then(response => response.json())
            .then(person => {
                // Check if person.id is defined before fetching collections
                if (person.id) {
                    return fetch('/api/Collection/' + person.id);
                } else {
                    throw new Error('PersonId is undefined');
                }
            })
            .then(response => response.json())
            .then(collections => {
                // Check if collections is an array before iterating over it
                if (Array.isArray(collections)) {
                    // Populate the collections
                    var collectionsList = $('#collections');
                    collectionsList.empty();
                    var ul = $('<ul>');
                    collections.forEach(collection => {
                        var listItem = $('<li>');
                        listItem.text(collection.name + ': ' + (collection.description || 'N/A'));
                        ul.append(listItem);
                    });
                    collectionsList.append(ul);
                } else {
                    throw new Error('collections is not an array');
                }
            
                // Show the modal
                $('#myModal').modal('show');
            })
            .catch(error => console.error('Error:', error));
    });
});