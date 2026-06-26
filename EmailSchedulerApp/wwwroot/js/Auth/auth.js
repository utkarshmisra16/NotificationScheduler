document.addEventListener("DOMContentLoaded", () => {
    loadPartial("Login");
})
async function loadPartial(action) {
    const response = await fetch('/Auth/Auth/' + action);
    const html = await response.text();
    document.getElementById('auth-right').innerHTML = html;
}
