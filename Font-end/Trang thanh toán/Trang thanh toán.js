var app = angular.module('myApp', []);

app.controller('BillController', function ($http, $window, $scope) {
  $scope.customer = {};

  var maTK = $window.localStorage.getItem('userID');

  $scope.getCustomer = function () {

    if (maTK) {
      $http.get('https://localhost:7118/api/InfoCustomer/GetCustomerByID/' + maTK)
        .then(function (response) {
          console.log("Hiển thị ");
          $scope.customer = response.data;

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

  var getBillByID = function () {
    $http.get('https://localhost:7118/api/Bill/GetAllBill/' + maTK)
      .then(function (response) {
        $scope.BillInfoItems = response.data;
        var maHDB = $scope.BillInfoItems[0].maHDB;
        $scope.getBillInfoByID(maHDB);
      })
      .catch(function (error) {
        console.error('Lỗi', error);
      });
  };

  $scope.getBillInfoByID = function (maHDB) {
    $http.get('https://localhost:7118/api/Bill/GetAllBillInfo/' + maHDB)
      .then(function (response) {
        $scope.BillItems = response.data;
        
      })
      .catch(function (error) {
        console.error('Lỗi', error);
      });
  };

  getBillByID(); 



})

app.controller('home', function ($window, $scope) {
  $scope.viewHome = function () {
      $window.location.href = '/Trang chủ/TrangChu.html';
      console.log('ht');
  };
})