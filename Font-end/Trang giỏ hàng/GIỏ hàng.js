var app = angular.module('myApp', []);

app.controller('CartController', function ($scope, $http, $window) {
  var maSP = $window.localStorage.getItem('maSP');
  var maTK = $window.localStorage.getItem('userID');

  var getProductByID = function () {
    $http.get('https://localhost:7118/api/Cart/GetAll/' + maTK)
      .then(function (response) {
        $scope.cartItems = response.data;
      })
      .catch(function (error) {
        console.error('Lỗi', error);
      });
  };

  getProductByID();

  $scope.confirmDelete = function (maGiohang) {
    if (confirm('Bạn có chắc chắn muốn xóa sản phẩm này không?')) {
      // debugger;
      $scope.deleteProduct(maGiohang);
    }
  };

  $scope.deleteProduct = function (maGiohang) {
    $http.delete('https://localhost:7118/api/Cart/Delete/' + maGiohang)
      .then(function (response) {
        alert('Xóa thành công sản phẩm !');
        console.log(response.data);
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa sản phẩm!');
        console.error('Lỗi khi xóa sản phẩm:', error);
      });
  };
  $scope.increase = function (cartItem) {
    if (!cartItem.quantity) {
      cartItem.quantity = 0;
    }
    cartItem.quantity++; // Tăng số lượng của sản phẩm khi nhấn nút "+"
  };

  $scope.decrease = function (cartItem) {
    if (cartItem.quantity > 0) {
      cartItem.quantity--; // Giảm số lượng của sản phẩm khi nhấn nút "-"
    }
  };
  

  
});

app.controller('PaymentController', function ($scope) {
  // Hàm tính tổng tiền hàng dựa trên số lượng và giá tiền của mỗi sản phẩm
  $scope.calculateTotalAmount = function () {
    let total = 0;
    const productElements = document.querySelectorAll('.cart-container-product-info'); // Thay #product-sales bằng class thích hợp
    for (let i = 0; i < productElements.length; i++) {
      const quantityElement = productElements[i].querySelector('.ipNumbers'); // Selector cho số lượng sản phẩm
      const priceElement = productElements[i].querySelector('#product-sales'); // Selector cho giá tiền của sản phẩm

      const quantity = parseInt(quantityElement.innerText.trim()); // Số lượng sản phẩm
      const priceText = priceElement.innerText.trim();
      const price = parseFloat(priceText.replace('đ', '').replace(',', '.')); // Giá tiền của sản phẩm

      total += price * quantity; // Tính tổng tiền hàng cho mỗi sản phẩm và số lượng tương ứng
    }
    return total;
  };

  // Hàm tính tổng thanh toán
  $scope.calculateTotalPayment = function () {
    return $scope.calculateTotalAmount() + 50000; // Thêm phí vận chuyển thực tế nếu có
  };

  // Hàm để thực hiện đặt hàng
  $scope.placeOrder = function () {
    // Thực hiện đặt hàng - Có thể thêm logic xử lý khi nhấn nút Đặt hàng
    // ...
  };
});




