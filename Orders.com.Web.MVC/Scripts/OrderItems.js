$(function () {
    var quantity = $("#Quantity");
    var price = $("#Price");
    var priceDiv = $("#price");
    var amount = $("#amount");
    var categories = $("#CategoryID");
    var products = $("#ProductID");
    var inStock = $("#inStock");

    if (priceDiv.text()) {
        priceDiv.text(USD(parseFloat(priceDiv.text())));
    }

    if (amount.text()) {
        amount.text(USD(parseFloat(amount.text())));
    }

    quantity.keyup(function() {
        amount.text(USD(quantity.val() * price.val()));
    });

    categories.change(function () {
        var categoryID = categories.val();
        $.getJSON("/products/category/" + categoryID, function (data) {
            var items = '<option>-- select one --</option>';
            $.each(data, function (i, state) {
                console.log(state);
                items += "<option value='" + state.ID + "'>" + state.Name + "</option>";
            });
            products.html(items);
        });
    });

    products.change(function () {
        var productID = products.val();
        $.getJSON("/inventoryitems/product/" + productID, function (data) {
            inStock.text(data.QuantityOnHand);
        });
        $.getJSON("/products/product/" + productID, function (data) {
            price.val(data.Price);
            priceDiv.html(USD(data.Price));
            amount.text(USD(quantity.val() * price.val()));
        });
    });

    function USD(value) {
        return value.toLocaleString("en-US", {style: "currency", currency: "USD", minimumFractionDigits: 2});
    }
});