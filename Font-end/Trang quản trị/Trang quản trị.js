var app = angular.module('myApp', ['ngRoute']);

app.config(function ($routeProvider) {
  $routeProvider
    .when('/quan-ly-khach-hang', {
      templateUrl: 'Quản trị/Customer/Customer.html',
      controller: 'QuanLyKhachHangController',
      resolve: {
        css: function() {

          var head = document.getElementsByTagName('head')[0];
          var link = document.createElement('link');
          link.rel = 'stylesheet';
          link.type = 'text/css';
          link.href = 'Quản trị/Customer/Customer.css'; 
          head.appendChild(link);
        }
      }
    })
    .when('/quan-ly-san-pham', {
      templateUrl: 'Quản trị/Product/Product.html',
      controller: 'QuanLySanPhamController',
      resolve: {
        css: function() {

          var head = document.getElementsByTagName('head')[0];
          var link = document.createElement('link');
          link.rel = 'stylesheet';
          link.type = 'text/css';
          link.href = 'Quản trị/Product/Product.css'; 
          head.appendChild(link);
        }
      }
    })
    .when('/', {
      templateUrl: 'Quản trị/Home/Home.html', 
      controller: 'QuanLyAdminController', 
      resolve: {
        css: function() {

          var head = document.getElementsByTagName('head')[0];
          var link = document.createElement('link');
          link.rel = 'stylesheet';
          link.type = 'text/css';
          link.href = 'Quản trị/Home/Home.css'; 
          head.appendChild(link);
        }
      }
    })
    .otherwise({
      redirectTo: '/'
    });
});


app.controller('AdminContentController', function ($scope) {
  // Controller logic for chart view
  $scope.chartMonths = [
    { name: 'Tháng 1', value: 50 },
    { name: 'Tháng 2', value: 70 },
    { name: 'Tháng 3', value: 90 },
    { name: 'Tháng 4', value: 100 },
    { name: 'Tháng 5', value: 60 },
    { name: 'Tháng 6', value: 120 },
    { name: 'Tháng 7', value: 80 },
    { name: 'Tháng 8', value: 87 },
    { name: 'Tháng 9', value: 54 },
    { name: 'Tháng 10', value: 50 },
    { name: 'Tháng 11', value: 70 },
    { name: 'Tháng 12', value: 90 },
  ];
});

app.controller('QuanLySanPhamController', function ($scope, $location) {
  $scope.navigateToPageSP = function() {
    console.log('Clicked navigateToPageSP');
    $location.path('/quan-ly-san-pham');
  };
});

app.controller('QuanLyKhachHangController', function ($scope, $location) {
  $scope.navigateToPageKH = function() {
    console.log('Clicked navigateToPageKH');
    $location.path('/quan-ly-khach-hang');
  };
});

app.controller('QuanLyAdminController', function($scope, $location) {
  $scope.navigateToAdmin = function() {
    console.log('Clicked navigateToAdmin');
    $location.path('/quan-ly-admin');
  };
});




