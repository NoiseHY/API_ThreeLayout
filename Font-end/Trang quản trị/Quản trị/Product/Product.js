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

app.controller('productCtrl', function ($scope, $http) {
  $scope.product = {};

  // Hàm để lấy giá trị từ input file và gán cho $scope.imageFile
  $scope.setImageFile = function (event) {
    var files = event.target.files;
    if (files.length > 0) {
      $scope.imageFile = files[0];
    }
  };

  $scope.imgProduct = function () {
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
      .then(function (response) {
        alert('Upload ảnh thành công !!');
      })
      .catch(function (error) {
        alert('Lỗi khi Upload ảnh', error.data);
      });
  };
});

app.controller('productController', function ($scope, $http) {
  $scope.productList = [];
  $scope.selectedProduct = {}; // Biến lưu trữ hàng được chọn

  // Gọi API để lấy danh sách sản phẩm
  $http.get('https://localhost:7117/api/product/GetAll')
    .then(function (response) {
      // Trích xuất thông tin cần thiết từ dữ liệu trả về và gán vào productList
      $scope.productList = response.data.map(function (product) {
        return {
          maSP: product.maSP,
          tenSP: product.tenSP,
          mota: product.mota,
          soLuong: product.soLuong,
          dongia: product.dongia,
          maTL: product.maTL,
          // Gán thuộc tính img với chuỗi base64 để hiển thị hình ảnh
          img: 'data:image/jpeg;base64,' + product.img
        };
      });
    })
    .catch(function (error) {
      console.error('Lỗi khi lấy danh sách sản phẩm !', error);
    });

  $scope.createProduct = function () {
    var productData = {
      maSP: $scope.product.id,
      tenSP: $scope.product.productName,
      mota: $scope.product.productDescription,
      soLuong: $scope.product.productQuantity,
      dongia: $scope.product.productPrice,
      maTL: $scope.selectedCategory ? $scope.selectedCategory.maLoai : null, // Đảm bảo đã chọn danh mục
      img: $scope.imageFile
    };

    $http.post('https://localhost:7117/api/product/Create', productData)
      .then(function (response) {
        // Xử lý khi tạo sản phẩm thành công
        alert('Thêm sản phẩm thành công!');
        console.log(response.data); // log kết quả từ API
      })
      .catch(function (error) {
        // Xử lý khi có lỗi
        alert('Đã xảy ra lỗi khi thêm sản phẩm!');
        console.error('Lỗi khi tạo sản phẩm:', error);
      });
  };

  $scope.selectRow = function (product) {
    $scope.selectedProduct = product; // Gán hàng được chọn vào biến selectedProduct
  };
  $scope.editProduct = function (product) {
    $scope.selectedProduct = angular.copy(product); // Copy dữ liệu từ hàng được chọn vào selectedProduct
    // Gán dữ liệu từ selectedProduct vào các input
    $scope.product.id = $scope.selectedProduct.maSP;
    $scope.product.productName = $scope.selectedProduct.tenSP;
    $scope.product.productDescription = $scope.selectedProduct.mota;
    $scope.product.productQuantity = $scope.selectedProduct.soLuong;
    $scope.product.productPrice = $scope.selectedProduct.dongia;

    // $scope.selectedCategory = "Thịt 12312321";
    // // Gọi API để lấy tên thể loại
    // $http.get('https://localhost:7117/api/category/GetNameCategoryByID?id=' + $scope.selectedProduct.maTL)
    //   .then(function (response) {
    //     if (Array.isArray(response.data) && response.data.length > 0) {
    //       $scope.selectedCategory = response.data[0].tenLoai;
    //     } else {
    //       alert('Không có dữ liệu thể loại trả về.');
    //     }
    //   })
    //   .catch(function (error) {
    //     alert('Lỗi khi lấy tên thể loại:', error);
    //   });
  };

  $scope.updateProduct = function () {
    var productData = {
      maSP: $scope.product.id,
      tenSP: $scope.product.productName,
      mota: $scope.product.productDescription,
      soLuong: $scope.product.productQuantity,
      dongia: $scope.product.productPrice,
      maTL: $scope.selectedCategory ? $scope.selectedCategory.maLoai : null,
    };

    $http.put('https://localhost:7117/api/product/Update', productData)
      .then(function (response) {
        alert('Sửa sản phẩm thành công!');
        console.log(response.data);
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi sửa sản phẩm!');
        console.error('Lỗi khi sửa sản phẩm:', error);
      });
  };

  $scope.confirmDelete = function (productId) {
    if (confirm('Bạn có chắc chắn muốn xóa sản phẩm này không?')) {
      debugger;
      $scope.deleteProduct(productId);

    }
  };

  $scope.deleteProduct = function (productId) {
    $http.delete('https://localhost:7117/api/product/Delete/' + productId)
      .then(function (response) {
        alert('Xóa thành công sản phẩm !');
        console.log(response.data);
        // Tải lại danh sách sản phẩm sau khi xóa thành công
        $scope.getProductList();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa sản phẩm!');
        console.error('Lỗi khi xóa sản phẩm:', error);
      });
  };
});