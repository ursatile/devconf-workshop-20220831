// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(connectToSignalR);

function connectToSignalR() {
    console.log("Connecting to SignalR...");
    var conn = new signalR.HubConnectionBuilder().withUrl("/hub").build();
    conn.on("DisplayNotification", displayNotification);
    conn.start().then(function () {
        console.log("SignalR has started.");
    }).catch(function (err) {
        console.log(err);
    });
}

function displayNotification(user, message) {
    console.log(`Message from ${user}`);
    var data = JSON.parse(message);
    var html = `<div>New vehicle! 
${data.ManufacturerName} ${data.ModelName}, 
${data.Color}, ${data.Year}. Price ${data.Price}${data.CurrencyCode}<br />
<a href="/vehicles/details/${data.Registration}">click for more...</a></div>`;
    const $div = $(html);
    $div.css("background-color", data.Color);
    const $target = $("div#signalr-notifications");
    $target.prepend($div);
    window.setTimeout(function () {
        $div.fadeOut(500, function () { $div.remove() });
    }, 10000);
}
