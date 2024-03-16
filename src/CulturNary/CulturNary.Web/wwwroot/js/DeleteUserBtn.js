function deleteUser(userId, ajax) {
    return ajax({
        url: '/Admin/DeleteUser/' + userId,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userId: userId }),
    });
}

function handleSuccess(reload) {
    alert('User has been deleted');
    reload();
}

function handleError() {
    alert('Error deleting user');
}

function setupClickHandler(getUserId, deleteUser, handleSuccess, handleError) {
    return function() {
        var userId = getUserId(this);
        deleteUser(userId)
            .then(() => handleSuccess(location.reload))
            .catch(handleError);
    };
}

function initialize(setupClickHandler) {
    $(document).ready(() => {
        $('.delete-user-button').click(setupClickHandler(
            element => $(element).data('user-id'),
            deleteUser.bind(null, $),
            handleSuccess,
            handleError
        ));
    });
}

module.exports = { deleteUser, handleSuccess, handleError, setupClickHandler, initialize };