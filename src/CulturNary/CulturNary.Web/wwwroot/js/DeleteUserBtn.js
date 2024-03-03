$(document).ready(function() {
    $('.delete-user-button').click(function() {
        var userId = $(this).data('user-id');
        $.ajax({
            url: '/Admin/DeleteUser/' + userId, // Include the userId in the URL
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ userId: userId }), // You can still include the userId in the request body if needed
            success: function() {
                alert('User has been deleted');
                location.reload();
            },
            error: function() {
                alert('Error deleting user');
            }
        });
    });
});