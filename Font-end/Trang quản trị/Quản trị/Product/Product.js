var productModule = angular.module('product', []);

productModule.factory('AuthInterceptor', function ($q, $window) {
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

        $window.location.href = '/login';
      }
      return $q.reject(response);
    }
  };
});

productModule.config(function ($httpProvider) {
  $httpProvider.interceptors.push('AuthInterceptor');
});

productModule.controller('btproductController', function ($scope, $http) {
  $http.get('https://localhost:7117/api/category/GetAll')
    .then(function (response) {
      $scope.categories = response.data;
    })
    .catch(function (error) {
      console.error('Lỗi', error);
    });

  // ---------------------- IMG ---------------------------------
  // Hàm để lấy giá trị từ input file và gán cho $scope.imageFile
  $scope.setImageFile = function (event) {
    var files = event.target.files;
    if (files.length > 0) {
      $scope.imageFile = files[0];
    }
  };

  $scope.imgProduct = function () {
    var productId = parseInt($scope.productId);

    if (isNaN(productId) || productId <= 0) {
      alert('Hãy nhập Mã sản phẩm !');
      return;
    }
    
    if (!$scope.imageFile) {
      alert('Không hình ảnh được chọn !');
      return;
    }

    var url = 'https://localhost:7117/api/product/uploadImage?productID=' + productId;
    var formData = new FormData();
    formData.append('file', $scope.imageFile);

    $http.post(url, formData, {
      transformRequest: angular.identity,
      headers: { 'Content-Type': undefined }
    })
      .then(function (response) {
        alert('Upload ảnh thành công !!');
      })
      .catch(function (error) {
        alert('Lỗi khi Upload ảnh', error.data);
      });
  };


  // ------------- Create -----------------------------
  $scope.createProduct = function () {

    if ($scope.selectedCategory == null) {
      alert('Hãy chọn thể loại');
      return;
    }

    var productData = {
      tenSP: $scope.productName,
      mota: $scope.productDescription,
      soLuong: $scope.productQuantity,
      dongia: $scope.productPrice,
      maTL: $scope.selectedCategory ? $scope.selectedCategory.maLoai : null
    };

    $http.post('https://localhost:7117/api/product/Create', productData)
      .then(function () {
        alert('Thêm sản phẩm thành công!');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi thêm sản phẩm!');
        console.error('Lỗi khi tạo sản phẩm:', error);
      });
  };

  //----------------- Update ----------------------------
  $scope.updateProduct = function () {

    if (isNaN($scope.productId) || $scope.productId <= 0) {
      alert('Hãy nhập Mã sản phẩm !');
      return;
    }

    if ($scope.selectedCategory == null) {
      alert('Hãy chọn thể loại');
      return;
    }

    var productData = {
      maSP: $scope.productId,
      tenSP: $scope.productName,
      mota: $scope.productDescription,
      soLuong: $scope.productQuantity,
      dongia: $scope.productPrice,
      maTL: $scope.selectedCategory ? $scope.selectedCategory.maLoai : null
    };

    $http.put('https://localhost:7117/api/product/Update', productData)
      .then(function () {
        alert('Sửa sản phẩm thành công!');

        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi sửa sản phẩm!');
        console.error('Lỗi khi sửa sản phẩm:', error);
      });
  };


});

productModule.controller('productController', function ($scope, $http) {
  $scope.productList = [];
  $scope.product = {};

  $http.get('https://localhost:7117/api/product/GetAll')
    .then(function (response) {
      $scope.productList = response.data.map(function (product) {
        return {
          maSP: product.maSP,
          tenSP: product.tenSP,
          mota: product.mota,
          soLuong: product.soLuong,
          dongia: product.dongia,
          maTL: product.maTL,

          img: 'data:image/jpeg;base64,' + product.img
        };
      });
    })
    .catch(function (error) {
      console.error('Lỗi khi lấy danh sách sản phẩm !', error);
    });

  // ----------------- Delete ---------------------------
  $scope.confirmDelete = function (productId) {
    if (confirm('Bạn có chắc chắn muốn xóa sản phẩm này không?')) {
      // debugger;
      $scope.deleteProduct(productId);
      
    }
  };

  $scope.deleteProduct = function (productId) {
    $http.delete('https://localhost:7117/api/product/Delete?id=' + productId)
      .then(function () {
        alert('Xóa thành công sản phẩm !');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa sản phẩm!');
        console.error('Lỗi khi xóa sản phẩm:', error);
      });
  };

  $scope.selectItem = function (selectedItem) {
    // debugger;
    $scope.productId = selectedItem.maSP;
    $scope.productName = selectedItem.tenSP;
    $scope.productDescription = selectedItem.mota;
    $scope.productQuantity = selectedItem.soLuong;
    $scope.productPrice = selectedItem.dongia;

    $scope.name = selectedItem.tenSP;

    console.log($scope.productId);
  };

  $scope.pageNumber = 1;
  $scope.pageSize = 10;
  $scope.noData = false; 

  $scope.buttonNext = function () {
    $scope.pageNumber++;
    debugger;
    $http.get('https://localhost:7117/api/product/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
      .then(function (response) {
        if (response.data.length === 0) {
          $scope.noData = true; 
          $scope.pageNumber--; 
        } else {
          $scope.productList = response.data.map(function (product) {
            return {
              maSP: product.maSP,
              tenSP: product.tenSP,
              mota: product.mota,
              soLuong: product.soLuong,
              dongia: product.dongia,
              maTL: product.maTL,
              img: 'data:image/jpeg;base64,' + product.img
            };
          });
        }
      })
      .catch(function (error) {
        console.error('Lỗi khi lấy danh sách sản phẩm!', error);
      });
  };

  $scope.buttonPrev = function () {
    if ($scope.pageNumber > 1) {
      $scope.pageNumber--;

      $http.get('https://localhost:7117/api/product/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
        .then(function (response) {
          $scope.noData = false; 
          $scope.productList = response.data.map(function (product) {
            return {
              maSP: product.maSP,
              tenSP: product.tenSP,
              mota: product.mota,
              soLuong: product.soLuong,
              dongia: product.dongia,
              maTL: product.maTL,
              img: 'data:image/jpeg;base64,' + product.img
            };
          });
        })
        .catch(function (error) {
          console.error('Lỗi khi lấy danh sách sản phẩm!', error);
        });
    }
  };

});