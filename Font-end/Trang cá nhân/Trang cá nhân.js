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
    $scope.nameCustomer = $scope.customer.tenKH || '';
    $scope.address = $scope.customer.diachiKH || '';
    $scope.phone = $scope.customer.sdt || '';

    $scope.birthday = $scope.customer.ngaysinh ? new Date($scope.customer.ngaysinh) : null;

  };


  $scope.getCustomer();


});

app.controller('updateCustomer', function ($scope, $http, $window) {
  $scope.updateCustomer = function () {
    var name = $scope.nameCustomer;

    var address = $scope.address;

    var phone = $scope.phone;

    var birthday = new Date($scope.birthday); 
    birthday.setDate(birthday.getDate() + 1)

    var maKH = $window.localStorage.getItem('userID');

    var updatedInfo = {
      maKH: maKH,
      tenKH: name,
      diachiKH: address,
      sdt: phone,
      ngaysinh: birthday
    };

    debugger;

    // Gửi yêu cầu cập nhật thông tin lên server
    $http.put('https://localhost:7118/api/InfoCustomer/Update', updatedInfo)
      .then(function (response) {
        console.log('Đã cập nhật thông tin khách hàng:', response.data);
        alert("Cập nhật thành công !");
      })
      .catch(function (error) {
        console.error('Lỗi khi cập nhật thông tin khách hàng:', error);
        alert('Lỗi khi cập nhật thông tin khách hàng');
      });
  };
});


