const uri = 'api/movietest';
let movies = null;
function getCount(data) {
    const el = $('#counter');
    let name = 'to-do';
    if (data) {
        if (data > 1) {
            name = 'movies';
        }
        el.text(data + ' ' + name);
    } else {
        el.html('No ' + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            $('#movies').empty();
            getCount(data.length);
            $.each(data, function (key, movie) {
                const checked = movie.isComplete ? 'checked' : '';

                $('<tr><td><input disabled="true" type="checkbox" ' + checked + '></td>' +
                    '<td>' + movie.name + '</td>' +
                    '<td><button onclick="editItem(' + movie.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteItem(' + movie.id + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#movies'));
            });

            movies = data;
        }
    });
}

function addMovie() {
    const movie = {
        'name': $('#add-name').val(),
        'isComplete': false
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(movie),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#add-name').val('');
        }
    });
}

function deleteMovie(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

function editMovie(id) {
    $.each(movies, function (key, mv) {
        if (mv.id === id) {
            $('#edit-name').val(mv.name);
            $('#edit-id').val(mv.id);
            $('#edit-isComplete')[0].checked = mv.isComplete;
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const mv = {
        'name': $('#edit-name').val(),
        'isComplete': $('#edit-isComplete').is(':checked'),
        'id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(mv),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}