var billModule = angular.module('bill', []);

billModule.controller('billController', function ($scope, $http) {
  $scope.bill = {};

  var id = localStorage.getItem('userID');

  $http.get('https://localhost:7118/api/Bill/GetAllBillByCustomerID/' + id)
    .then(function (response) {
      $scope.bill = response.data;
      console.log('ht');
    })
    .catch(function (error) {
      console.error('Lỗi khi lấy dữ liệu', error);
    });
  
  

  $scope.showCTHD = function (maHD) {
    $http.get('https://localhost:7118/api/Bill/GetAllBillInfo/' + maHD)
      .then(function (response) {
        $scope.billInfo = response.data;
        console.log('ht2');
      })
      .catch(function (error) {
        console.error('Lỗi khi lấy dữ liệu', error);
      });
  }


  $scope.confirmDelete = function (bill) {
    if (confirm('Bạn có chắc chắn muốn xóa hóa đơn  này không?')) {
      // debugger;
      $scope.deleteCmt(bill);

    }
  };

  $scope.deleteCmt = function (bill) {
    $http.delete('https://localhost:7118/api/Bill/Delete/' + bill)
      .then(function () {
        alert('Xóa thành công hóa đơn  !');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa hóa đơn !');
        console.error('Lỗi khi xóa hóa đơn ', error);
      });
  };
})