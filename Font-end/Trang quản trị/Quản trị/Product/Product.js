var app = angular.module('myApp', []);

app.controller('myCtrl', function($scope, $http) {
  $http.get('https://localhost:7117/api/category/GetAll')
    .then(function(response) {
      $scope.categories = response.data;
    })
    .catch(function(error) {
      console.error('Error fetching categories', error);
    });
});
