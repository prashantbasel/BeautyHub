$(function () {
    $(".sidebar .nav-item .nav-link").on("click", function () {
        $(this).toggleClass(".nav-item-active");
    });
});