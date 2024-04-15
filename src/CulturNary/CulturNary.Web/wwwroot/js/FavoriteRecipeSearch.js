$(document).ready(function () {
    $('#searchFavorites').on('keyup', function () {
        var search = $(this).val();
        var url = $(this).data('url');
        $.ajax({
            url: url,
            type: 'GET',
            data: { search: search },
            success: function (response) {
                $('#recipeTable').html(response);
            }
        });
    });
});