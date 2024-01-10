var app = angular.module('myApp', ['ngRoute','cmt', 'bill'],);

app.config(function ($routeProvider){
  $routeProvider
    .when('/cmt', {
      templateUrl: '/Trang cá nhân/Bình luận/Bình luận.html',
      controller: 'cmtController',
    })
    .when('/bill', {
      templateUrl: '/Trang cá nhân/Hóa đơn/Hóa đơn.html',
      controller: 'BillController',
    })
    .otherwise({
      redirectTo: '/cmt'
    });
})

app.controller('cmtController', function ($scope, $location) {
  $scope.navigateToCmt = function () {
    $location.path('/cmt');
    console.log('ht');
  };
});

app.controller('BillController', function ($scope, $location) {
  $scope.navigateToBill = function () {
    $location.path('/bill');
    console.log('ht');
  };
});

app.controller('CustomerController', function ($scope, $http) {
  $scope.customer = {};
  
  var id = localStorage.getItem('userID');

  $scope.getCustomer = function () {
    
    if (id) {
      $http.get('https://localhost:7118/api/InfoCustomer/GetCustomerByID/' + id)
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

  $scope.setImageFile = function (event) {
    var files = event.target.files;
    if (files.length > 0) {
      $scope.imageFile = files[0];
    }
  };

  $scope.imgProduct = function () {

    if (!$scope.imageFile) {
      alert('Không hình ảnh được chọn !');
      return;
    }

    var url = 'https://localhost:7118/api/InfoCustomer/uploadImage?customerID=' + id;
    var formData = new FormData();
    formData.append('file', $scope.imageFile);

    $http.post(url, formData, {
      transformRequest: angular.identity,
      headers: { 'Content-Type': undefined }
    })
      .then(function (response) {
        alert('Upload ảnh thành công !!');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Lỗi khi Upload ảnh', error.data);
      });
  };
  
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

    // debugger;

    $http.put('https://localhost:7118/api/InfoCustomer/Update', updatedInfo)
      .then(function () {
        alert("Cập nhật thành công !");
        window.location.reload();
      })
      .catch(function (error) {
        console.error('Lỗi khi cập nhật thông tin khách hàng:', error);
        alert('Lỗi khi cập nhật thông tin khách hàng');
      });
  };


});

app.controller('home', function ($window, $scope) {
  $scope.viewHome = function () {
      $window.location.href = '/Trang chủ/TrangChu.html';
      console.log('ht');
  };
})
