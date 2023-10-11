
$(document).ready(function () {
    $(".addQty").each(function () {
        $(this).on("click", function () {
            var self = $(this);

            var productId = self.attr("data-id");
            var qty = parseInt(self.attr("data-qty")) + 1;

            $.get(
                "/Cart/UpdateItem?productId=" + productId + "&qty=" + qty,
                null,
                function (result) {
                    location.reload();
                }
            );
        });
    });

    $(".delQty").each(function () {
        $(this).on("click", function () {
            var self = $(this);

            var productId = self.attr("data-id");
            var qty = parseInt(self.attr("data-qty")) - 1;
            console.log(productId);
            console.log(qty);

            $.get(
                "/Cart/UpdateItem?productId=" + productId + "&qty=" + qty,
                null,
                function (result) {
                    location.reload();
                }
            );
        });
    });
});