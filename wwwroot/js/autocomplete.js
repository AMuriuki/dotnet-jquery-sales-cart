$(function () {
    var products = []

    $.ajax({
        url: '/Sales/GetProducts',
        method: 'GET',
        success: function (response) {
            products = response;
            console.log(products);
        },
        error: function () {
            console.log("Failed to get products");
        }
    })
})