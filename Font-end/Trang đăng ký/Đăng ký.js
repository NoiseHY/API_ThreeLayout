var app = angular.module('myApp', []);

app.controller('myCtrl', function($scope, $http, $window) {
  $scope.inputType = 'password';
  $scope.imgSrc = 'Anh/hide.png';
  $scope.userData = {}; 

  $scope.togglePassword = function() {
    if ($scope.inputType === 'password') {
      $scope.inputType = 'text';
      $scope.imgSrc = 'Anh/show.png';
    } else {
      $scope.inputType = 'password';
      $scope.imgSrc = 'Anh/hide.png';
    }
  };

  $scope.registerForm = function() {  

    var accountData = {
      tenTK: $scope.accountName,
      mkTK: $scope.accountPassword,
      email: $scope.accountEmail,
      maPQ: 3
    };

    $http.post('https://localhost:7117/api/account/Create', accountData)
      .then(function () {
        alert('Thêm tài khoản thành công!');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi thêm tài khoản !');
        console.error('Lỗi khi tạo tài khoản ', error);
      });
  };

  $scope.viewHome = function () {
    $window.location.href = '/Trang chủ/TrangChu.html';
    console.log('ht');
  };
  
});
