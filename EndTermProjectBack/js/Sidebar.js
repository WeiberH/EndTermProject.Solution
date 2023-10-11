$(document).ready(function () {
    $("nav ul li a").on("click", function (event) {
        $("nav ul li ul").removeClass("show");

        if ($(this).next("ul").length) {
            event.preventDefault();

            $(this).next("ul").toggleClass("show");
        }
    });

    $("#collapse").on("click", function () {
        $("#sidebar").toggleClass("active");
        $(".fa-align-left").toggleClass("fa-circle-right");
    });
});
