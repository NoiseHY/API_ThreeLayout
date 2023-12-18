var app = angular.module('myApp', []);

app.controller('categoryCtrl', function ($scope, $http) {
  $http.get('https://localhost:7117/api/category/GetAll')
    .then(function (response) {
      $scope.categories = response.data;
    })
    .catch(function (error) {
      console.error('Error fetching categories', error);
    });
});

app.controller('productCtrl', function($scope, $http) {
  $scope.product = {};

  // Hàm để lấy giá trị từ input file và gán cho $scope.imageFile
  $scope.setImageFile = function(event) {
    var files = event.target.files;
    if (files.length > 0) {
      $scope.imageFile = files[0];
    }
  };
  

  $scope.createProduct = function() {
    var productId = parseInt($scope.product.id);
  
    if (isNaN(productId) || productId <= 0) {
      alert('Invalid product ID');
      return;
    }
  
    if (!$scope.imageFile) {
      alert('No image selected');
      return;
    }
  
    var url = 'https://localhost:7117/api/product/uploadImage?productID=' + productId;
    var formData = new FormData();
    formData.append('file', $scope.imageFile);
  
    $http.post(url, formData, {
      transformRequest: angular.identity,
      headers: { 'Content-Type': undefined }
    })
    .then(function(response) {
      alert('Upload ảnh thành công !!');
    })
    .catch(function(error) {
      alert('Lỗi khi Upload ảnh', error.data);
    });
  };  
});

app.controller('productController', function($scope, $http) {
  $scope.productList = [];

  // Gọi API để lấy danh sách sản phẩm
  $http.get('https://localhost:7117/api/product/GetAll')
    .then(function(response) {
      // Trích xuất thông tin cần thiết từ dữ liệu trả về và gán vào productList
      $scope.productList = response.data.map(function(product) {
        return {
          maSP: product.maSP,
          tenSP: product.tenSP,
          mota: product.mota,
          soLuong: product.soLuong,
          dongia: product.dongia,
          maTL: product.maTL,
          // Gán thuộc tính img với chuỗi base64 để hiển thị hình ảnh
          img: 'data:image/jpeg;base64,' + product.img // Thay 'jpeg' bằng định dạng thích hợp
        };
      });
    })
    .catch(function(error) {
      console.error('Lỗi khi lấy danh sách sản phẩm !', error);
    });
});


