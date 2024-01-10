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

  $scope.submitForm = function() {
    const data = {
      TenTk: $scope.userData.username,
      MkTk: $scope.userData.password
    };

    $http({
      method: 'POST',
      url: 'https://localhost:7117/api/login/login', 
      headers: {
        'Content-Type': 'application/json'
      },
      data: data
    }).then(function(response) {

      // console.log(response.data);

      if (response.data && response.data.maPQ === 1 && response.data.token) {
        var jwtToken = response.data.token;
        
        $window.localStorage.setItem('token', jwtToken);
        

        $window.location.href = '/Trang quản trị/Trang quản trị.html';
      } else {
        
        var userID = response.data.maKH;

        $window.location.href = '/Trang chủ/TrangChu.html';

        $window.localStorage.setItem('userID', userID);

        $window.localStorage.removeItem('token');
      }

      alert('Đăng nhập thành công!');
    }).catch(function(error) {
      // Xử lý lỗi nếu có
      console.error('There was an error!', error);
      alert('Đăng nhập thất bại!');
    });
  };
});
