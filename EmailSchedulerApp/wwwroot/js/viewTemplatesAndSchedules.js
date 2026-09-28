$(document).ready(function () {
    loadTemplates();
    loadSchedules();
});

function loadTemplates() {
    $('#currentPage').text("View Templates");
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

function loadSchedules() {
    $('#currentPage').text("View Schedules");
    $.ajax({
        url: '/Schedule/Schedule/GetSchedules',
        type: 'GET',

        success: function (html) {
            $('#scheduleTableBody').html(html);
        },

        error: function (xhr) {
            console.error('Failed to load schedules:', xhr);
        }
    });
}
