var app = angular.module('myApp', []);

app.controller('CustomerController', function ($scope, $http) {
  $scope.customer = {}; // Dữ liệu khách hàng từ API

  // Hàm lấy thông tin khách hàng từ API dựa trên ID từ localStorage
  $scope.getCustomer = function () {
    var id = localStorage.getItem('userID'); // Lấy ID từ localStorage
    if (id) {
      $http.get('https://localhost:7118/api/InfoCustomer/GetCustomerByID/' + id)
        .then(function (response) {
          console.log("Hiển thị ");
          $scope.customer = response.data; // Lưu thông tin khách hàng từ API

          // Gán thông tin khách hàng vào các trường input
          $scope.fillForm();
        })
        .catch(function (error) {
          console.error('Lỗi khi lấy dữ liệu', error);
        });
    } else {
      console.error('Không tìm thấy ID trong localStorage');
    }
  };

  $scope.fillForm = function () {
    $scope.customer = $scope.customer || {}; // Đảm bảo $scope.customer không null
    // Gán thông tin từ $scope.customer vào các trường input
    document.getElementById('input-name').value = $scope.customer.tenKH || '';
    document.getElementById('input-address').value = $scope.customer.diachiKH || '';
    document.getElementById('input-phone').value = $scope.customer.sdt || '';
    document.getElementById('input-birthday').value = $scope.customer.ngaysinh || '';
  };

  // Gọi hàm để lấy thông tin khách hàng khi cần thiết
  $scope.getCustomer();


  // Hàm cập nhật thông tin khách hàng
  $scope.updateCustomer = function () {
    $http.put('URL của API Update', $scope.customer)
      .then(function (response) {
        console.log('Đã cập nhật khách hàng:', response.data);
      })
      .catch(function (error) {
        console.error('Lỗi khi cập nhật khách hàng:', error);
      });
  };
});
