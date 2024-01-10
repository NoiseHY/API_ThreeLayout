var app = angular.module('myApp', []);

app.controller('CartController', function ($scope, $http, $window) {
  var maKH = $window.localStorage.getItem('userID');

  var getProductByID = function () {
    $http.get('https://localhost:7118/api/Cart/GetAll/' + maKH)
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
    cartItem.quantity++;
  };

  $scope.decrease = function (cartItem) {
    if (cartItem.quantity > 0) {
      cartItem.quantity--;
    }
  };



});

app.controller('PaymentController', function ($scope, $window, $http) {

  $scope.calculateTotalAmount = function () {
    let total = 0;
    for (let i = 0; i < $scope.cartItems.length; i++) {
      const product = $scope.cartItems[i];
      if (product.selected) {
        total += product.dongia * product.quantity;
      }
    }
    return total;
  };



  $scope.calculateTotalPayment = function () {
    return $scope.calculateTotalAmount() + 50000;
  };

  $scope.createBillFromCart = function () {
    if ($scope.calculateTotalAmount() === 0 || !$scope.cartItems.some(item => item.selected)) {
      alert('Vui lòng chọn sản phẩm và số lượng trước khi đặt hàng.');
      return;
    }

    var maKH = $window.localStorage.getItem('userID');
    var date = new Date();
    var Ngayban = date.toISOString().slice(0, 10);

    var tempBills = [];

    debugger;

    angular.forEach($scope.cartItems, function (cartItem) {
      if (cartItem.selected) {
        var tempBill = {
          MaSP: cartItem.maSP,
          Soluong: cartItem.quantity,
          Gia: cartItem.dongia,
          Thanhtien: cartItem.dongia * cartItem.quantity
        };
        tempBills.push(tempBill);
      }
    });

    $http.post('https://localhost:7118/api/Bill/CreateTemp', tempBills)
      .then(function () {
        console.log('Thêm bảng tạm thành công');
        createMainBill();
      })
      .catch(function (error) {
        alert('Lỗi khi tạo bảng tạm', error);
      });

    function createMainBill() {
      var mainBill = {
        Tongtien: $scope.calculateTotalAmount() + 50000,
        MaKH: maKH,
        Ngayban: Ngayban
      };

      $http.post('https://localhost:7118/api/Bill/Create', mainBill)
        .then(function () {
          alert('Thêm hóa đơn thành công!');
          $window.location.href = '/Trang thanh toán/Trang thanh toán.html';
          
        })
        .catch(function (error) {
          alert('Lỗi khi tạo hóa đơn', error);
        });
    }
  };


});




