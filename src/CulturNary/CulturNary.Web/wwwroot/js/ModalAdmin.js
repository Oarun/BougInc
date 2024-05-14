// ModalAdmin.js
function getUserIdFromRow(row) {
    return $(row).data('userId');
}

function fetchUserDetails(userId) {
    return fetch('/Admin/Users/' + userId)
        .then(response => response.json());
}

function populateUserDetails(user) {
    $('#userId').text(user.id);
    $('#userName').text(user.userName);
    $('#normalizedUserName').text(user.normalizedUserName);
    $('#email').text(user.email);
    $('#normalizedEmail').text(user.normalizedEmail);
    $('#emailConfirmed').text(user.emailConfirmed);
    $('#phoneNumber').text(user.phoneNumber || "N/A" );
    $('#twoFactorEnabled').text(user.twoFactorEnabled);
}

function fetchPersonDetails(userId) {
    return fetch('/api/Person/Identity/' + userId)
        .then(response => response.json());
}

function fetchCollections(personId) {
    if (personId) {
        return fetch('/api/Collection/' + personId)
            .then(response => response.json());
    } else {
        throw new Error('PersonId is undefined');
    }
}

function populateCollections(collections) {
    if (Array.isArray(collections)) {
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
}

$(document).ready(function() {

});
module.exports = { getUserIdFromRow, fetchUserDetails, populateUserDetails, fetchPersonDetails, fetchCollections, populateCollections };