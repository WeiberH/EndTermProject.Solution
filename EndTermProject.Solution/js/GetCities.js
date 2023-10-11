$(document).ready(function () {
    $("#cityDropdown").change(function () {
        var selectedCityId = $(this).val();
        if (selectedCityId !== "") {
            $.ajax({
                url: "/Cart/GetDistricts", // 替换成你的控制器方法
                type: "GET",
                data: { cityId: selectedCityId },
                dataType: "json",
                success: function (data) {
                    $("#districtDropdown").empty();
                    $("#districtDropdown").append($('<option>', {
                        value: "",
                        text: "請選擇"
                    }));
                    $.each(data, function (key, item) {
                        $("#districtDropdown").append($('<option>', {
                            value: item.Id, // 请确保DistrictVm中有Id属性
                            text: item.District // 请确保DistrictVm中有District属性
                        }));
                    });
                }

            });
        } else {
            $("#districtDropdown").empty();
            $("#districtDropdown").append($('<option>', {
                value: "",
                text: "請選擇"
            }));
        }
    });
});