var products = []
var totalPrice = 0.00, subtotal = 0.00, tax = 0.00, tax_percentage = 0.00, discount = 0.00, discount_percentage = 0.00, shipping = 0.0;

$(document).ready(function () {
    $('.js-example-basic-single').select2();
});

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
    $(".extras").removeClass("d-none");

    // update the total price
    totalPrice += selectedProduct.price
    subtotal = selectedProduct.price
    updateSubtotal(quantityInput);
    updateTotalPrice();

    return false;
});

$(document).on('click', '.delete-set', function () {
    var value = 0.00;

    $("#cart-table-body tr").each(function () {
        value += convertToNumber($(this).find("td:eq(4)").text());
    })

    $(this).closest('tr').remove();

    // update total price
    var deletedProductPrice = convertToNumber($(this).closest("tr").find("td:eq(4)").text());

    console.log(value, deletedProductPrice)

    value -= deletedProductPrice;

    calculateTotalPrice({
        subtotal: value,
        tax: value * (tax_percentage / 100),
        discount: value * (discount_percentage / 100),
        shipping: shipping,
    })
})

$(document).on('change', '.quantity input', function (event) {
    var price = convertToNumber($(event.currentTarget).closest("tr").find("td:eq(3)").text());
    var quantity = parseInt($(this).val())
    subtotal = price * quantity;
    updateSubtotal($(event.currentTarget));
})


function updateTotalPrice() {
    $(".total").text(insertCommas(totalPrice.toFixed(2)));
}

function decreaseQuantity() {
    var input = $(this).closest('.quantity').find('input');
    var value = parseInt(input.val());
    if (value > 1) {
        input.val(value - 1);

        var quantity = parseInt(input.val());
        var price = convertToNumber(input.closest("tr").find("td:eq(3)").text());

        subtotal = price * quantity;
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
    updateSubtotal(input);

}

function updateSubtotal(input) {
    input.closest("tr").find("td:eq(4)").text(insertCommas(subtotal.toFixed(2)));

    var value = 0.00

    $("#cart-table-body tr").each(function () {
        value += convertToNumber($(this).find("td:eq(4)").text());
    })

    console.log(value)

    calculateTotalPrice({
        subtotal: value,
        tax: value * (tax_percentage / 100),
        discount: value * (discount_percentage / 100),
        shipping: shipping,
    })

    $(".tax-value").text(insertCommas(value * (tax_percentage / 100)))
    $(".discount-value").text(insertCommas(value * (discount_percentage / 100)));
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



$('.tax-input, .discount-input, .shipping-input').on('input keypress', function (event) {
    var value = $(this).val();

    if (value > 100) {
        $(this).siblings('span').removeClass("d-none");
    }
    else {
        $(this).siblings('span').addClass("d-none");
    }

    if (value.indexOf(".") !== -1 && value.split(".")[1].length === 2) {
        event.preventDefault();
    }
})


$('.tax-input').on('input', function () {
    var value = 0.00;

    $("#cart-table-body tr").each(function () {
        value += convertToNumber($(this).find("td:eq(4)").text());
    })

    tax_percentage = $(this).val();
    tax = value * (tax_percentage / 100);

    $(".tax-value").text(insertCommas(tax.toFixed(2)));
    $(".tax-percentage").text(insertCommas(tax_percentage));

    calculateTotalPrice({
        subtotal: value,
        tax: tax,
        discount: discount,
        shipping: shipping,
    })
})

$('.discount-input').on('input', function () {
    var value = 0.00;

    $("#cart-table-body tr").each(function () {
        value += convertToNumber($(this).find("td:eq(4)").text());
    })

    discount_percentage = $(this).val();
    discount = value * (discount_percentage / 100);

    $(".discount-value").text(insertCommas(discount.toFixed(2)));
    $(".discount-percentage").text(insertCommas(discount_percentage));

    calculateTotalPrice({
        subtotal: value,
        tax: tax,
        discount: discount,
        shipping: shipping,
    })
})

$(".shipping-input").on('input', function () {
    var value = 0.00;

    $("#cart-table-body tr").each(function () {
        value += convertToNumber($(this).find("td:eq(4)").text());
    })

    shipping = parseFloat($(this).val());

    $(".shipping-value").text(insertCommas(shipping));

    calculateTotalPrice({
        subtotal: value,
        tax: tax,
        discount: discount,
        shipping: shipping,
    })
})


function calculateTotalPrice(options) {
    totalPrice = (options.subtotal - options.discount) + (options.tax + options.shipping);
    updateTotalPrice()
}