var products = []
var totalPrice = 0.0;

$(function () {
    $.ajax({
        url: '/Sales/GetProducts',
        method: 'GET',
        success: function (response) {
            products = response;

            var productNames = $.map(products, function (product) {
                return product['name'] + " - " + product['sku'];
            });

            $("#selectProduct").autocomplete({
                source: productNames,
            });
        },
        error: function () {
            console.log("Failed to get products");
        }
    })
})

$("#selectProduct").on("autocompleteselect", function (event, ui) {
    var selectedValue = ui.item.value;
    var skuIndex = selectedValue.lastIndexOf(" - ") + 3;
    var selectedSku = selectedValue.substring(skuIndex);

    var selectedProduct = products.find(function (product) {
        return product.sku === selectedSku;
    });

    // create a new row for selected product
    var newRow = $("<tr>");

    // add the product info to the new row
    newRow.append($("<td>").text($("#cart-table-body tr").length + 1));
    newRow.append(
        $("<td>").addClass("productimgname").
            append(
                $("<a>").addClass("product-img").
                    append($("<img>").attr("src", selectedProduct.imageUrl).attr("alt", selectedProduct.name)),
                $("<a>").attr("href", "javascript:void(0);").text(selectedProduct.name)));
    newRow.append(
        $("<td>").text(selectedProduct.price.toFixed(2)));
    newRow.append(
        $("<td>").
            append(
                $("<a>").attr("href", "javascript:void(0);").addClass("delete-set").append(
                    $("<img>").attr("src", "/images/delete.svg").attr("alt", "svg")
                )
            ));

    // add the new row to the table
    $("#cart-table-body").append(newRow);

    $("#selectProduct").val('');

    // update the total price
    totalPrice += selectedProduct.price
    updateTotalPrice();

    return false;
});

$(document).on('click', '.delete-set', function () {
    $(this).closest('tr').remove();

    // update total price
    var deletedProductPrice = parseFloat($(this).closest("tr").find("td:eq(2)").text());
    totalPrice -= deletedProductPrice;
    updateTotalPrice();
})

function updateTotalPrice() {
    $(".total h5").text("Ksh." + totalPrice.toFixed(2));
}