var app = angular.module('myApp', []);

app.controller('categoryCtrl', function($scope, $http) {
  $http.get('https://localhost:7117/api/category/GetAll')
    .then(function(response) {
      $scope.categories = response.data;
    })
    .catch(function(error) {
      console.error('Error fetching categories', error);
    });
});

app.controller('productCtrl', function($scope, $http) {
  $scope.product = {};
  $scope.imageFile = null;

  $scope.createProduct = function() {
    var formData = new FormData();
    formData.append('file', $scope.imageFile);

    $http.post('https://localhost:7117/api/product/Create', $scope.product)
      .then(function(response) {
        $http.post('https://localhost:7117/api/product/UploadImage', formData, {
          transformRequest: angular.identity,
          headers: { 'Content-Type': undefined }
        })
        .then(function(imageResponse) {
          console.log('Image uploaded successfully:', imageResponse);
        })
        .catch(function(imageError) {
          console.error('Error uploading image:', imageError);
        });
      })
      .catch(function(error) {
        console.error('Error updating product:', error);
      });
  };
});
