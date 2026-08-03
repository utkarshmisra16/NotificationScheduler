$(document).ready(function () {
    loadTemplates();
});

function loadTemplates() {

    $.ajax({
        url: '/Template/Template/GetTemplates',
        type: 'GET',

        success: function (html) {
            $('#templateTableBody').html(html);
        },

        error: function (xhr) {
            console.error('Failed to load templates:', xhr);
        }
    });
}