document.addEventListener("DOMContentLoaded", function () {
    var agreeCheckbox = document.getElementById("Agree");
    var submitButton = document.querySelector("input[type='submit']");

    submitButton.addEventListener("click", function (event) {
        if (!agreeCheckbox.checked) {
            alert("必須勾選同意使用者條款");
            event.preventDefault();  /*擋住表單的送出*/
        }
    });
});






