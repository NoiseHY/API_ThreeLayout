var app = angular.module('myApp', []);

app.controller('ProductDetailController', function ($scope, $http, $window) {
  
  var maSP = $window.localStorage.getItem('maSP');
  var getProductByID = function (id) {
      $http.get('https://localhost:7118/api/InfoProduct/GetProductByID?id=' + id)
          .then(function (response) {
              $scope.productDetail = response.data;
          })
          .catch(function (error) {
              console.error('Lỗi', error);
          });
  };

  getProductByID(maSP);
});
