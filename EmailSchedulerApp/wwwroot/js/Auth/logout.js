$(document).ready(function() {
    $('#logoutBtn').on('click', logOut);
});

function logOut(e) {
    e.preventDefault();
    $.ajax({
        url: '/Auth/Auth/Logout',
        type: 'POST',
        data : { 
            __RequestVerificationToken:
                $("#logoutForm input[name='__RequestVerificationToken']").val()
        },
        success: function() {
            window.location.href = '/Auth/Auth/Index';
        },

        error: function(xhr, status, error) {
            console.error('Logout failed:' + xhr.responseText);
        }
    })
}
