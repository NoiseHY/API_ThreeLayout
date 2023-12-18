var app = angular.module('myApp', ['ngRoute']);

app.config(function ($routeProvider) {
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


app.controller('QuanLySanPhamController', function ($scope, $location) {
  $scope.navigateToPageSP = function () {
    console.log('Clicked navigateToPageSP');
    $location.path('/quan-ly-san-pham');
  };
});

app.controller('QuanLyKhachHangController', function ($scope, $location) {
  $scope.navigateToPageKH = function () {
    console.log('Clicked navigateToPageKH');
    $location.path('/quan-ly-khach-hang');
  };
});

app.controller('QuanLyAdminController', function ($scope, $location) {
  $scope.navigateToAdmin = function () {
    console.log('Clicked navigateToAdmin');
    $location.path('/quan-ly-admin');
  };
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

