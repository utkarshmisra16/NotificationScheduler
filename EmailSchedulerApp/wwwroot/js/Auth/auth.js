document.addEventListener("DOMContentLoaded", () => {
    loadPartial("Login");
})
async function loadPartial(action) {
    const response = await fetch('/Auth/Auth/' + action);
    const html = await response.text();
    document.getElementById('auth-right').innerHTML = html;
}

$(document).on("submit", "#loginForm", function (e) {
    e.preventDefault();
    login();
});

$(document).on("submit", "#registerForm", function (e) {
    e.preventDefault();
    register();
});

function login() {
    $("#loginError").addClass("d-none").text("");
    let data = $("#loginForm").serialize();
    $.ajax({
        url: "/Auth/Auth/Login",
        type: "POST",
        data: data,
        success: function (response) {
            if (response.success) 
                window.location.href = "/Dashboard/Dashboard/Index";
            else 
                $("#loginError").removeClass("d-none").text(response.message);
        },
        error: function () {
            $("#loginError")
                .removeClass("d-none")
                .text("Something went wrong. Please try again.");
        }
    });
}


function register() {
    $("#loginError").addClass("d-none").text("");
    let data = $("#registerForm").serialize();
    $.ajax({
        url: "/Auth/Auth/Register",
        type: "POST",
        data: data,
        success: function (response) {
            if (response.success) 
                window.location.href = "/Auth/Auth/Index?message=" + encodeURIComponent(response.message);
            else 
                $("#loginError").removeClass("d-none").text(response.message);
        },
        error: function () {
            $("#loginError").removeClass("d-none").text("Something went wrong. Please try again.");
        }
    });
}
