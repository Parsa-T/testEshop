function countShopCart() {
    $.get("/api/shop", function (res) {
        $("#countShopCart").text(res);
    });
}

$(document).ready(function () {

    countShopCart();

    $(document).on("click", ".add-to-cart-btn", function () {

        let id = $(this).data("id");

        let colorInput = $("input[name='color']:checked");

        let url = "/api/shop/" + id;

        if (colorInput.length > 0) {
            let color = colorInput.val();
            url += "?color=" + encodeURIComponent(color);
        }

        $.get(url, function (res) {
            $("#countShopCart").text(res);
        });

    });

});






// تابع برای بروزرسانی سبد خرید و subtotal
function updateCartTotals() {
    let subtotal = 0;

    document.querySelectorAll('[data-item-total]').forEach(item => {
        let priceText = item.textContent.replace(/[\s,تومان]/g, '');
        let price = parseInt(priceText) || 0;

        // گرفتن تعداد از input
        let count = parseInt(item.closest('.cart-item').querySelector('input').value) || 1;

        subtotal += price * count;
    });

    const subtotalElem = document.querySelector('[data-cart-subtotal]');
    const grandtotalElem = document.querySelector('[data-cart-grandtotal]');

    if (subtotalElem) subtotalElem.textContent = subtotal.toLocaleString() + ' تومان';
    if (grandtotalElem) grandtotalElem.textContent = subtotal.toLocaleString() + ' تومان';
}

// حذف آیتم از سبد
$(document).on('click', '[data-action="remove-cart-item"]', function () {
    const cartItem = $(this).closest('.cart-item');
    const productId = parseInt(cartItem.data('cart-item-id')); // بهتره تو HTML data-cart-item-id اضافه کنی

    $.ajax({
        url: '/api/shop/' + productId,
        type: 'DELETE',
        success: function (res) {
            // حذف آیتم از DOM
            cartItem.remove();

            // بروزرسانی قیمت کل
            updateCartTotals();
        },
        error: function () {
            alert('حذف آیتم با مشکل مواجه شد');
        }
    });
});
// بروزرسانی قیمت هر آیتم و کل سبد
function updateCartTotals() {
    let subtotal = 0;

    document.querySelectorAll('.cart-item').forEach(item => {
        const unitPrice = parseInt(item.dataset.unitPrice) || 0;
        const qtyInput = item.querySelector('input');
        const count = parseInt(qtyInput.value) || 1;

        // آپدیت قیمت آیتم
        const itemTotalElem = item.querySelector('[data-item-total]');
        itemTotalElem.textContent = (unitPrice * count).toLocaleString() + ' تومان';

        subtotal += unitPrice * count;
    });

    // آپدیت جمع کل
    const subtotalElem = document.querySelector('[data-cart-subtotal]');
    const grandtotalElem = document.querySelector('[data-cart-grandtotal]');
    if (subtotalElem) subtotalElem.textContent = subtotal.toLocaleString() + ' تومان';
    if (grandtotalElem) grandtotalElem.textContent = subtotal.toLocaleString() + ' تومان';
}

$(document).on('click', '.qty-plus', function () {
    const input = $(this).siblings('input');
    input.val(parseInt(input.val()) + 1);
    updateCartTotals();
});

$(document).on('click', '.qty-minus', function () {
    const input = $(this).siblings('input');
    let current = parseInt(input.val());
    if (current > 1) {
        input.val(current - 1);
        updateCartTotals();
    }
});


