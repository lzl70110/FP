// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring and bundling static web assets.

// Live clock
document.addEventListener("DOMContentLoaded", () => {
    const clock = document.getElementById("live-clock");
 
if (!clock) {
    return;
}

const timeZone = clock.dataset.timezone || "Europe/Sofia";

const updateClock = () => {
    const now = new Date();

    const time = new Intl.DateTimeFormat("bg-BG", {
        timeZone: timeZone,
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
        hour12: false
    }).format(now);

    clock.textContent = time;
};

updateClock();
setInterval(updateClock, 1000);
 

});
