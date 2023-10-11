$(".btnHideItems").hide();


$("tr[data-is-detail=true]").hide();
$(".btnShowItems").click(function () {
    let self = $(this);
    let orderId = self.data("order-id");

    $("tr[data-order-id=" + orderId + "]").show();
    $(".btnHideItems[data-order-id=" + orderId + "]").show();
    self.hide();
});

$(".btnHideItems").click(function () {
    let self = $(this);
    let orderId = self.data("order-id");

    $("tr[data-order-id=" + orderId + "]").hide();
    $(".btnShowItems[data-order-id=" + orderId + "]").show();
    self.hide();
});