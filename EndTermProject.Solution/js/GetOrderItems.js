document.addEventListener("DOMContentLoaded", function () {
    initForm();
});

function initForm() {
    var tdElements = document.querySelectorAll("td[data-id]");

    tdElements.forEach(function (td) {
        td.addEventListener("click", function () {
            var id = td.getAttribute("data-id");

            var url = "/Orders/OrderItem?orderId=" + id;

            $.get(url, function (result) {
                
                window.location.href = url;
            });
        });
    });
}
