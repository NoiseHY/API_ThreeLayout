var accountModule = angular.module('account', []);

accountModule.factory('AuthInterceptor', function ($q, $window) {
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

accountModule.config(function ($httpProvider) {
  $httpProvider.interceptors.push('AuthInterceptor');
});

accountModule.controller('getAllaccount', function ($http, $scope, $window) {

  $scope.pageNumber = 1;
  $scope.pageSize = 10;
  $scope.noData = false;

  $http.get('https://localhost:7117/api/account/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
    .then(function (response) {
      $scope.account = response.data;
    })
    .catch(function (error) {
      console.error('Lỗi', error);
    });

  $scope.selectItem = function (selectedItem) {
    // debugger;
    $scope.accountId = selectedItem.maTK;
    $scope.accountName = selectedItem.tenTK;
    $scope.accountPassword = selectedItem.mkTK;
    $scope.accountEmail = selectedItem.email;


    console.log($scope.productId);
  };

  $http.get('https://localhost:7117/api/user_authorization/GetAll')
    .then(function (response) {
      $scope.authorizen = response.data;
    })
    .catch(function (error) {
      console.error('Lỗi', error);
    });

  $scope.createaccount = function () {

    if ($scope.selectedAuthorize == null) {
      alert('Hãy chọn  quyền');
      return;
    }

    debugger;

    var accountData = {
      tenTK: $scope.accountName,
      mkTK: $scope.accountPassword,
      email: $scope.accountEmail,
      maPQ: $scope.selectedAuthorize ? $scope.selectedAuthorize.maPQ : null
    };

    $http.post('https://localhost:7117/api/account/Create', accountData)
      .then(function () {
        alert('Thêm tài khoản thành công!');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi thêm tài khoản !');
        console.error('Lỗi khi tạo tài khoản ', error);
      });
  };

  //----------------- Update ----------------------------
  $scope.updateaccount = function () {

    if (isNaN($scope.accountId) || $scope.accountId <= 0) {
      alert('Hãy nhập Mã tài khoản  !');
      return;
    }

    if ($scope.selectedAuthorize == null) {
      alert('Hãy chọn  quyền');
      return;
    }

    var accountData = {
      maTK: $scope.accountId,
      tenTK: $scope.accountName,
      mkTK: $scope.accountPassword,
      email: $scope.accountEmail,
      maPQ: $scope.selectedAuthorize ? $scope.selectedAuthorize.maPQ : null
    };

    $http.put('https://localhost:7117/api/account/Update', accountData)
      .then(function () {
        alert('Sửa tài khoản  thành công!');

        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi sửa tài khoản !');
        console.error('Lỗi khi sửa tài khoản :', error);
      });
  };

  // ----------------- Delete ---------------------------
  $scope.confirmDelete = function (accountId) {
    if (confirm('Bạn có chắc chắn muốn xóa tài khoản  này không?')) {
      // debugger;
      $scope.deleteProduct(accountId);
      
    }
  };

  $scope.deleteProduct = function (accountId) {
    $http.delete('https://localhost:7117/api/account/Delete/' + accountId)
      .then(function () {
        alert('Xóa thành công tài khoản  !');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa tài khoản !');
        console.error('Lỗi khi xóa tài khoản :', error);
      });
  };


  $scope.buttonNext = function () {
    $scope.pageNumber++;
    debugger;
    $http.get('https://localhost:7117/api/account/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
    .then(function (response) {
      if (response.data.length === 0) {
        $scope.noData = true; 
        $scope.pageNumber--; 
      } else {
        $scope.account = response.data;
      }
      
    })
    .catch(function (error) {
      console.error('Lỗi', error);
    });

  };

  $scope.buttonPrev = function () {
    if ($scope.pageNumber > 1) {
      $scope.pageNumber--;

      $http.get('https://localhost:7117/api/account/GetAll?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize)
      .then(function (response) {
        $scope.noData = false; 
        $scope.account = response.data;
      })
      .catch(function (error) {
        console.error('Lỗi', error);
      });
    }
  };
})