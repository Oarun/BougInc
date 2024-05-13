// Define AJAX request function
function deleteUser(userId) {
    return $.ajax({
        url: '/Admin/DeleteUser/' + userId,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userId: userId }),
    });
}

// Define success and error handlers
function handleSuccess() {
    alert('User has been deleted');
    location.reload();
}

function handleError() {
    alert('Error deleting user');
}

// Define click handler setup function
function setupClickHandler() {
    $('.delete-user-button').on('click', function() {
        const userId = $(this).data('user-id');
        deleteUser(userId)
            .then(handleSuccess)
            .catch(handleError);
    });
}

// Initialize on document ready
$(document).ready(setupClickHandler);