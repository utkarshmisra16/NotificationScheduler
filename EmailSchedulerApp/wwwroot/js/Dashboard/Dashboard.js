$(document).ready(function(){
    $('#currentPage').text("Dashboard");
    // Recipient popover
    $('[data-bs-toggle="popover"]').each(function () {
        new bootstrap.Popover(this);
    });

    // Hover info tooltip
    $('span[title="Click to view recipients"]').each(function () {
        new bootstrap.Tooltip(this, {
            trigger: 'hover'
        });
    });
});