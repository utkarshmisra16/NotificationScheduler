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
