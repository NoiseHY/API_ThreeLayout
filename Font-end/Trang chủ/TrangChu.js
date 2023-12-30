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
        // Xử lý dữ liệu trả về từ API ở đây
        $scope.products = response.data;
      })
      .catch(function (error) {
        // Xử lý lỗi nếu có
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
});





