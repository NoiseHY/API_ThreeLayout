var myApp  = angular.module('myApp', ['ngRoute', 'Home', 'product'], );

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
    console.log('Clicked navigateToPageSP');
    $location.path('/quan-ly-san-pham');
  };
});

myApp.controller('QuanLyKhachHangController', function ($scope, $location) {
  $scope.navigateToPageKH = function () {
    console.log('Clicked navigateToPageKH');
    $location.path('/quan-ly-khach-hang');
  };
});

myApp.controller('QuanLyAdminController', function ($scope, $location) {
  $scope.navigateToAdmin = function () {
    console.log('Clicked navigateToAdmin');
    $location.path('/quan-ly-admin');
  };
});
