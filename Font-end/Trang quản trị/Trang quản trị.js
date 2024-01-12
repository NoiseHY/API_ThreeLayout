var myApp  = angular.module('myApp', ['ngRoute', 'Home', 'product', 'customer', 'account'], );

myApp.factory('AuthInterceptor', function ($q, $window) {
  return {
    request: function (config) {
      config.headers = config.headers || {};
      var token = $window.localStorage.getItem('token');
      if (token) {
        config.headers.Authorization = 'Bearer ' + token;
      }
      return config;
    },
    responseError: function (response) {
      if (response.status === 401 || response.status === 403) {

        $window.location.href = '/Trang đăng nhập/Đăng nhập.html';
      }
      return $q.reject(response);
    }
  };
});

myApp.config(function ($httpProvider) {
  $httpProvider.interceptors.push('AuthInterceptor');
});


myApp.config(function ($routeProvider) {
  $routeProvider
    .when('/quan-ly-khach-hang', {
      templateUrl: '/Trang quản trị/Quản trị/Customer/Customer.html',
      controller: 'QuanLyKhachHangController',
    })
    .when('/quan-ly-san-pham', {
      templateUrl: '/Trang quản trị/Quản trị/Product/Product.html',
      controller: 'QuanLySanPhamController',
    })
    .when('/quan-ly-tai-khoan', {
      templateUrl: '/Trang quản trị/Quản trị/Account/Account.html',
      controller: 'QuanLyTaiKhoanController',
    })
    .when('/admin', {
      templateUrl: 'Quản trị/Home/Home.html',
      controller: 'QuanLyAdminController',

    })
    .otherwise({
      redirectTo: '/admin'
    });
});


myApp.controller('QuanLySanPhamController', function ($scope, $location) {
  $scope.navigateToPageSP = function () {
    // console.log('Clicked navigateToPageSP');
    $location.path('/quan-ly-san-pham');
  };
});

myApp.controller('QuanLyKhachHangController', function ($scope, $location) {
  $scope.navigateToPageKH = function () {
    // console.log('Clicked navigateToPageKH');
    $location.path('/quan-ly-khach-hang');
  };
});

myApp.controller('QuanLyTaiKhoanController', function ($scope, $location) {
  $scope.navigateToPageTK = function () {
    
    $location.path('/quan-ly-tai-khoan');
  };
});

myApp.controller('QuanLyAdminController', function ($scope, $location) {
  $scope.navigateToAdmin = function () {
    // console.log('Clicked navigateToAdmin');
    $location.path('/quan-ly-admin');
  };
});
