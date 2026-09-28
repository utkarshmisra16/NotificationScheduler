// SUCCESS
function showSuccess(message, title = "Success") {
    Swal.fire({
        icon: 'success',
        title: title,
        text: message,
        confirmButtonColor: '#28a745'
    });
}

// ERROR
function showError(message, title = "Error") {
    Swal.fire({
        icon: 'error',
        title: title,
        text: message,
        confirmButtonColor: '#dc3545'
    });
}

// WARNING
function showWarning(message, title = "Warning") {
    Swal.fire({
        icon: 'warning',
        title: title,
        text: message,
        confirmButtonColor: '#ffc107'
    });
}

// INFO / NORMAL
function showInfo(message, title = "Info") {
    Swal.fire({
        icon: 'info',
        title: title,
        text: message,
        confirmButtonColor: '#0d6efd'
    });
}