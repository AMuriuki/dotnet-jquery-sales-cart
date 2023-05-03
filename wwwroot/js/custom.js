var products = []
var totalPrice = 0.0;
var subtotal = 0.0;

$(function () {
    $.ajax({
        url: '/Sales/GetProducts',
        method: 'GET',
        success: function (response) {
            products = response;

            var productNames = $.map(products, function (product) {
                return product['name'];
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

    var selectedProduct = products.find(function (product) {
        return product.name === selectedValue;
    });

    var quantityInput = $("<input>").attr("type", "text").attr("class", "quantity").val("1");

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
        $("<td>").
            append(
                $("<div>").addClass("quantity").append(
                    $("<button>").attr("type", "button").text("-").click(decreaseQuantity),
                    quantityInput,
                    $("<button>").attr("type", "button").text("+").click(increaseQuantity)
                )
            )
    );
    newRow.append(
        $("<td>").text(insertCommas(selectedProduct.price.toFixed(2))));
    newRow.append(
        $("<td>").text(insertCommas(selectedProduct.price.toFixed(2))));
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
    var deletedProductPrice = convertToNumber($(this).closest("tr").find("td:eq(4)").text());
    totalPrice -= deletedProductPrice;
    updateTotalPrice();
})

$(document).on('change', '.quantity input', function (event) {
    var price = convertToNumber($(event.currentTarget).closest("tr").find("td:eq(3)").text());
    var prevSubTotal = convertToNumber($(event.currentTarget).closest("tr").find("td:eq(4)").text());
    var quantity = parseInt($(this).val())
    subtotal = price * quantity;
    totalPrice -= prevSubTotal;
    totalPrice += subtotal
    updateSubtotal($(event.currentTarget));
})


function updateTotalPrice() {
    $(".total h5").text("Ksh. " + insertCommas(totalPrice.toFixed(2)));
}

function decreaseQuantity() {
    var input = $(this).closest('.quantity').find('input');
    var value = parseInt(input.val());
    if (value > 1) {
        input.val(value - 1);

        var quantity = parseInt(input.val());
        var price = convertToNumber(input.closest("tr").find("td:eq(3)").text());

        subtotal = price * quantity;
        totalPrice -= price

        updateSubtotal(input);
    }
}

function increaseQuantity() {
    var input = $(this).closest('.quantity').find('input');
    var value = parseInt(input.val());
    input.val(value + 1);

    var quantity = parseInt(input.val());
    var price = convertToNumber(input.closest("tr").find("td:eq(3)").text());

    subtotal = price * quantity;
    totalPrice += price

    updateSubtotal(input);
}

function updateSubtotal(input) {
    input.closest("tr").find("td:eq(4)").text(insertCommas(subtotal.toFixed(2)));
    updateTotalPrice();
}


function insertCommas(number) {
    if (number !== null || number !== undefined) {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    } else {
        return "0.00";
    }
}

function convertToNumber(a) {
    number = a.replace(/\,/g, '');
    return parseFloat(number)
}