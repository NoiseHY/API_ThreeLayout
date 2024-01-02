document.addEventListener('DOMContentLoaded', function () {
  function updateCartDetails() {
    const cartItems = localStorage.getItem('allProductsJSON'); // Thay đổi tên key để lấy dữ liệu từ localStorage
    const productList = JSON.parse(cartItems) || []; // Đổi tên biến để phản ánh dữ liệu sản phẩm

    const cartList = document.getElementById('cartList');

    // Xóa nội dung cũ của giỏ hàng
    cartList.innerHTML = '';

    let totalPrice = 0;

    productList.forEach(product => {
      const item = document.createElement('li');
      const itemInfo = document.createElement('div');
      const itemName = document.createElement('h6');
      const itemPrice = document.createElement('a');
      const itemTotalPrice = document.createElement('span');

      // Cập nhật lại tên biến để phản ánh dữ liệu từ localStorage
      itemName.textContent = product.name.trim();
      itemPrice.textContent = `${product.sales} x ${product.quantity}`;
      itemTotalPrice.textContent = `${parseFloat(product.sales.replace('đ', '').replace(',', '').trim()) * product.quantity}đ`;

      itemInfo.appendChild(itemName);
      itemInfo.appendChild(itemPrice);
      item.appendChild(itemInfo);
      item.appendChild(itemTotalPrice);

      cartList.appendChild(item);

      totalPrice += parseFloat(product.sales.replace('đ', '').replace(',', '').trim()) * product.quantity;
    });

    // Hiển thị tổng thành tiền
    const totalPriceElement = document.createElement('li');
    totalPriceElement.innerHTML = `<span>Tổng thành tiền</span><strong>${totalPrice.toFixed(2)}đ</strong>`;
    cartList.appendChild(totalPriceElement);
  }

  // Gọi hàm cập nhật thông tin giỏ hàng khi trang được load
  updateCartDetails();

  var imgTrangChu = document.getElementById('imgTrangChu');
  imgTrangChu.addEventListener('click', function () {
    window.location.href = 'http://127.0.0.1:5500/L%E1%BA%ADp%20tr%C3%ACnh%20Web%20B%C3%A1ch%20H%C3%B3a%20Xanh/HTML/Trang%20ch%E1%BB%A7/TrangChu.html';
  })
});
