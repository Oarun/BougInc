function makeAdmin(userId) {
    return $.ajax({
        url: '/Admin/MakeAdmin/' + userId,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userId: userId }),
    });
}

function handleSuccess() {
    alert('User has been made admin');
    location.reload();
}

function handleError() {
    alert('Error making user admin');
}

// MakeAdminBtn.js
function setupClickHandler() {
    $('.make-admin-button').click(function() {
        var userId = $(this).data('user-id');
        makeAdmin(userId)
            .then(handleSuccess)
            .catch(handleError);
    });
}

function initialize() {
    $(document).ready(setupClickHandler);
}

module.exports = { makeAdmin, handleSuccess, handleError, setupClickHandler };