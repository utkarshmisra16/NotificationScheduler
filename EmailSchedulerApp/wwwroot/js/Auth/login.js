document.addEventListener('DOMContentLoaded', function () {
    $(document).on("click", '#signUp', function() {
        loadPartial("Register");
    });
});

document.addEventListener('DOMContentLoaded', function () {
    // Password visibility toggle
    const toggleButtons = document.querySelectorAll('.toggle-pwd');
    toggleButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            e.preventDefault();
            togglePasswordVisibility(this);
        });
    });

    // Form validation
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', function (e) {
            if (!this.checkValidity()) {
                e.preventDefault();
                e.stopPropagation();
            }
            this.classList.add('was-validated');
        });
    }

    // Optional: Enter key on password field
    const passwordInput = document.getElementById('password');
    if (passwordInput) {
        passwordInput.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                loginForm.dispatchEvent(new Event('submit'));
            }
        });
    }
});

function togglePasswordVisibility(button) {
    const targetId = button.getAttribute('data-toggle-target');
    const input = document.getElementById(targetId);
    const icon = button.querySelector('i');
    const isPassword = input.type === 'password';

    input.type = isPassword ? 'text' : 'password';
    icon.className = isPassword ? 'bi bi-eye-slash' : 'bi bi-eye';
    button.setAttribute('aria-pressed', isPassword.toString());
}

$(document).on("submit", "#forgotPasswordForm", function (e) {
    e.preventDefault();
    const email = $("#forgotEmail").val();
    // AJAX call
     const form = $(this);
    $.ajax({
        url: "/Auth/Auth/ForgotPassword",
        type: "POST",
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                $("#forgotPasswordError").addClass("d-none").text("");
                $("#forgotPasswordSuccess").removeClass("d-none").text(response.message);
                $("#forgotEmail").val("");
            }
            else {
                $("#forgotPasswordSuccess").addClass("d-none").text("");
                $("#forgotPasswordError").removeClass("d-none").text(response.message);
            }
        },

        error: function () {
            $("#forgotPasswordError").removeClass("d-none").text("Something went wrong. Please try again.");
        }
    });
});
