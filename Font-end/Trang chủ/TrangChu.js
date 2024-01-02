var app = angular.module('myApp', []);

app.controller('GetNewProductsController', function ($scope, $http, $window) {

  var getNewProducts = function () {
    $http.get('https://localhost:7118/api/InfoProduct/GetNewProductsAll?pageNumber=1&pageSize=10')
      .then(function (response) {
        $scope.products = response.data;
      })
      .catch(function (error) {
        console.error('Lỗi', error);
      });
  };

  getNewProducts();

  $scope.viewProductDetail = function (maSP) {
    $window.localStorage.setItem('maSP', maSP);
    $window.location.href = '/Chi tiết sản phẩm/Chi tiết sản phẩm.html';
  };

  $scope.addToCart = function (product) {
    var maTK = $window.localStorage.getItem('userID');
    var cart = {
      MaKH: maTK,
      MaSP: product.maSP,
      Soluong: 1,
      Dongia: product.dongia
    };
    
    $http.post('https://localhost:7118/api/Cart/Create', cart)
      .then(function () {
        alert('Thêm sản phẩm vào giỏ hàng thành công !');
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi thêm vào giỏ hàng!');
        console.error('Lỗi khi tạo sản phẩm:', error);
      });
  };


});


app.controller('SearchController', function ($scope, $http) {
  $scope.productName = '';

  $scope.searchProduct = function () {
    $http.get('https://localhost:7118/api/InfoProduct/SearchProductByName', {
      params: {
        Name: $scope.productName,
        pageNumber: 1,
        pageSize: 5
      }
    })
      .then(function (response) {

        $scope.products = response.data;
      })
      .catch(function (error) {

        console.error('Lỗi khi gọi API:', error);
      });
  };
});

app.controller('UserController', function ($scope, $window) {
  $scope.showButtons = false;

  $scope.toggleButtons = function () {
    var userID = $window.localStorage.getItem('userID');
    $scope.isLoggedIn = !!userID;

    if ($scope.isLoggedIn) {
      $scope.showButtons = !$scope.showButtons;
      if ($scope.showButtons) {
        document.getElementById('buttonDialog').style.display = 'block'; // Hiển thị phần button-dialog
      } else {
        document.getElementById('buttonDialog').style.display = 'none'; // Ẩn phần button-dialog
      }
    } else {

      $window.location.href = '/Trang đăng nhập/Đăng nhập.html';
    }
  };

  $scope.goToProfile = function () {

    $window.location.href = '/Trang cá nhân/Trang cá nhân.html';
  };

  $scope.logout = function () {

    $window.localStorage.removeItem('userID');
    $window.localStorage.removeItem('maSP');
    $scope.showButtons = false;
    $window.location.href = '/Trang chủ/TrangChu.html';
  };

  $scope.viewCart = function (maSP) {
    $window.location.href = '/Trang giỏ hàng/Giỏ hàng.html';
  };

  $scope.viewHome = function (maSP) {
    $window.location.href = '/Trang chủ/TrangChu.html';
  };
});





