var products = [], customers = [];

var totalPrice = 0.00, subtotal = 0.00, tax = 0.00, tax_percentage = 0.00, discount = 0.00, discount_percentage = 0.00, shipping = 0.0;

$(function () {
    $.ajax({
        url: '/Sales/GetProductConfig',
        method: 'GET',
        success: function (response) {
            console.log(response);
            tax_percentage = response.taxRate
            $(".tax-input").val(tax_percentage);
        },
        error: function () {
            console.log("Failed to get product configurations");
        }
    })
})

$(document).ready(function () {
    $('.js-example-basic-single').select2();
});

$(document).ready(function () {
    $("#selectProduct").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Sales/GetProducts",
                data: { term: request.term, page: 1, pageSize: 10 },
                type: "GET",
                success: function (data) {
                    response(data);
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText)
                }
            });
        },
        minLength: 1,

        // when user selects a product
        select: function (event, ui) {
            var selectedProduct = ui.item;

            var sameProductRow = null;

            var rows = $("#cart-table-body tr");

            for (var i = 0; i < rows.length; i++) {
                var row = rows.eq(1);
                var productInfo = row.find(".product")
                if (productInfo.text() === selectedProduct.name) {
                    sameProductRow = row;
                    break;
                }
            }

            if (sameProductRow) {
                // if the same product is already in the cart increase the quantity for that row
                var quantityInput = sameProductRow.find(".quantity input");
                var quantity = parseInt(quantityInput.val()) + 1;
                quantityInput.val(quantity);
                updateSubtotal(quantityInput);
            }

            var quantityInput = $("<input>").attr("type", "text").attr("class", "quantity").val("1");

            // create a new row for the selected product
            var newRow = $("<tr>");

            // add product info to the new row
            newRow.append($("<td>").text($("#cart-table-body tr").length + 1));
            newRow.append(
                $("<td>").append(
                    $("<a>").addClass("product").attr("href", "javascript:void(0);").attr("id", selectedProduct.value).text(selectedProduct.name)));
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

            return false;
        }
    });

    $("#selectCustomer").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Sales/GetCustomers",
                data: { term: request.term, page: 1, pageSize: 10 },
                type: "GET",
                success: function (data) {
                    response(data);
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText)
                }
            })
        }
    })


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

    $(".tax-value").text(insertCommas(value * (tax_percentage / 100)))
    $(".discount-value").text(insertCommas(value * (discount_percentage / 100)));

    console.log(value, tax_percentage);

    calculateTotalPrice({
        subtotal: value,
        tax: value * (tax_percentage / 100),
        discount: value * (discount_percentage / 100),
        shipping: shipping,
    })
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

    shipping = $(this).val() ? parseFloat($(this).val()) : 0.00;
    console.log($(this).val())

    $(".shipping-value").text(insertCommas(shipping));

    calculateTotalPrice({
        subtotal: value,
        tax: tax,
        discount: discount,
        shipping: shipping,
    })
})


function calculateTotalPrice(options) {
    console.log(options)
    if (options.discount > 0.00) {
        tax = (options.subtotal - options.discount) * (tax_percentage / 100)
        console.log(options.subtotal, options.discount, options.subtotal - options.discount, tax)
        $(".tax-value").text(insertCommas(tax.toFixed(2)));
        totalPrice = (options.subtotal - options.discount) + (tax + options.shipping);
    } else {
        totalPrice = (options.subtotal - options.discount) + (options.tax + options.shipping);
    }
    updateTotalPrice()
}

// post sales order
$("#submitBtn").click(function () {
    var customerId = $("#selectCustomer").val();
    var date = $(".date").val();
    var products = [];

    $("#cart-table-body tr").each(function () {
        var productId = $(this).find(".product").attr("id");
        var productName = $(this).find(".product").text()

        var productQuantity = $(this).find("input[type='text']").val();
        products.push({
            ProductId: parseInt(productId),
            ProductName: productName,
            Quantity: parseInt(productQuantity)
        });
    })

    var taxValue = $(".tax-value").text();
    var taxPercentage = $(".tax-percentage").text();
    var discountValue = $(".discount-value").text();
    var discountPercentage = $(".discount-percentage").text();
    var shipping = $(".shipping-value").text();
    var grandTotal = $(".total").text();

    var data = {
        "customerId": customerId,
        "date": date,
        "products": products,
        "taxValue": taxValue,
        "taxPercentage": taxPercentage,
        "discountValue": discountValue,
        "discountPercentage": discountPercentage,
        "shipping": shipping,
        "grandTotal": grandTotal
    }

    console.log(data);

    $.ajax({
        type: "POST",
        url: "/Sales/Create",
        data: JSON.stringify(data),
        contentType: "application/json; charset=UTF-8",
        dataType: "text",
        success: function (response) {
            alert("Order posted successfully");
            window.location.href = "/Sales/Invoices";
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Error: " + jqXHR.responseText);
        }
    })
});