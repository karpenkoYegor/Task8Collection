// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var html = document.getElementById("html");
var toggle = document.getElementById("toggleTheme");

toggle.addEventListener("change", (e) => {
    if (toggle.value === "dark")
        document.cookie = "theme=" + "dark" + ";path=/";
    else
        document.cookie = "theme=" + "light" + ";path=/";
    fetch("/Account/ChangeTheme?theme="+toggle.value, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
    console.log();
    window.location.reload();
});
