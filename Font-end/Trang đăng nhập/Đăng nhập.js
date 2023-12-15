var app = angular.module('myApp', []);

app.controller('myCtrl', function($scope, $http) {
  $scope.inputType = 'password';
  $scope.imgSrc = 'Anh/hide.png';
  $scope.userData = {}; // Tạo đối tượng userData để chứa thông tin đăng nhập

  $scope.togglePassword = function() {
    if ($scope.inputType === 'password') {
      $scope.inputType = 'text';
      $scope.imgSrc = 'Anh/show.png';
    } else {
      $scope.inputType = 'password';
      $scope.imgSrc = 'Anh/hide.png';
    }
  };

  $scope.submitForm = function() {
    const data = {
      TenTk: $scope.userData.username,
      MkTk: $scope.userData.password
    };

    $http({
      method: 'POST',
      url: 'https://localhost:7117/api/login/login', // Đổi endpoint thành '/api/login/login'
      headers: {
        'Content-Type': 'application/json'
      },
      data: data
    }).then(function(response) {
      // Xử lý phản hồi từ server
      console.log(response.data);
      alert('Đăng nhập thành công!');
    }).catch(function(error) {
      // Xử lý lỗi nếu có
      console.error('There was an error!', error);
      alert('Đăng nhập thất bại!');
    });
  };
});
