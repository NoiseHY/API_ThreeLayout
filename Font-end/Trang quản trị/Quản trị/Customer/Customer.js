var customerModule = angular.module('customer', []);

customerModule.factory('AuthInterceptor', function ($q, $window) {
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

customerModule.config(function ($httpProvider) {
  $httpProvider.interceptors.push('AuthInterceptor');
});

customerModule.controller('getAllCustomer', function ($http, $scope, $window) {

  $scope.pageNumber = 1;
  $scope.pageSize = 10;
  $scope.noData = false;

  $http.get('https://localhost:7117/api/customer/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
    .then(function (response) {
      $scope.customer = response.data;
    })
    .catch(function (error) {
      console.error('Lỗi', error);
    });

  $scope.selectItem = function (selectedItem) {
    // debugger;
    $scope.customerId = selectedItem.maKH;
    $scope.customerName = selectedItem.tenKH;
    $scope.customerAddress = selectedItem.diachiKH;
    $scope.customerNumber = selectedItem.sdt;
    $scope.customerDate = new Date(selectedItem.ngaysinh);


    console.log($scope.productId);
  };


  $scope.createCustomer = function () {

    var selectedDate = new Date($scope.customerDate);
    selectedDate.setDate(selectedDate.getDate() + 1);

    var customerData = {
      tenKH: $scope.customerName,
      diachiKH: $scope.customerAddress,
      sdt: $scope.customerNumber,
      ngaysinh: selectedDate
    };

    $http.post('https://localhost:7117/api/customer/Create', customerData)
      .then(function () {
        alert('Thêm khách hàng thành công!');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi thêm khách hàng !');
        console.error('Lỗi khi tạo khách hàng ', error);
      });
  };

  //----------------- Update ----------------------------
  $scope.updateCustomer = function () {

    if (isNaN($scope.customerId) || $scope.customerId <= 0) {
      alert('Hãy nhập Mã khách hàng  !');
      return;
    }

    var selectedDate = new Date($scope.customerDate);
    selectedDate.setDate(selectedDate.getDate() + 1);

    var customerData = {
      maKH: $scope.customerId,
      tenKH: $scope.customerName,
      diachiKH: $scope.customerAddress,
      sdt: $scope.customerNumber,
      ngaysinh: selectedDate
    };

    $http.put('https://localhost:7117/api/customer/Update', customerData)
      .then(function () {
        alert('Sửa khách hàng  thành công!');

        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi sửa khách hàng !');
        console.error('Lỗi khi sửa khách hàng :', error);
      });
  };

  // ----------------- Delete ---------------------------
  $scope.confirmDelete = function (customerId) {
    if (confirm('Bạn có chắc chắn muốn xóa khách hàng  này không?')) {
      // debugger;
      $scope.deleteProduct(customerId);
      
    }
  };

  $scope.deleteProduct = function (customerId) {
    $http.delete('https://localhost:7117/api/customer/Delete/' + customerId)
      .then(function () {
        alert('Xóa thành công khách hàng  !');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa khách hàng !');
        console.error('Lỗi khi xóa khách hàng :', error);
      });
  };


  $scope.buttonNext = function () {
    $scope.pageNumber++;
    debugger;
    $http.get('https://localhost:7117/api/customer/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
    .then(function (response) {
      if (response.data.length === 0) {
        $scope.noData = true; 
        $scope.pageNumber--; 
      } else {
        $scope.customer = response.data;
      }
      
    })
    .catch(function (error) {
      console.error('Lỗi', error);
    });

  };

  $scope.buttonPrev = function () {
    if ($scope.pageNumber > 1) {
      $scope.pageNumber--;

      $http.get('https://localhost:7117/api/customer/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
      .then(function (response) {
        $scope.noData = false; 
        $scope.customer = response.data;
      })
      .catch(function (error) {
        console.error('Lỗi', error);
      });
    }
  };
})