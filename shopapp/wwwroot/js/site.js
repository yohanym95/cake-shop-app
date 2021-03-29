// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    //$('#searchBox').autocomplete({
    //    source: '/api/pie/search'
    //});
    AOS.init();
});


$(document).addEventListener("scroll", function () {
    const header = document.querySelector(".navsticky");
    header.classList.toggle("sticky", window.scrollY > 0);
});
