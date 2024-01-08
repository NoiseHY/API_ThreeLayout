var cmtModule = angular.module('cmt', []);

cmtModule.controller('cmentController', function ($scope, $http, $window) {
  $scope.cmt = {};

  var id = localStorage.getItem('userID');

  $http.get('https://localhost:7118/api/Rating/GetCommentsByMaTK?id=' + id)
    .then(function(response){
      $scope.cmt= response.data;
      console.log('ht');
    })
    .catch(function (error) {
      console.error('Lỗi khi lấy dữ liệu', error);
  });



  $scope.confirmDelete = function (cmtId) {
    if (confirm('Bạn có chắc chắn muốn xóa bình luận này không?')) {
      // debugger;
      $scope.deleteCmt(cmtId);

    }
  };

  $scope.deleteCmt = function (cmtId) {
    $http.delete('https://localhost:7118/api/Rating/Delete?id=' + cmtId)
      .then(function () {
        alert('Xóa thành công bình luận !');
        window.location.reload();
      })
      .catch(function (error) {
        alert('Đã xảy ra lỗi khi xóa bình luận!');
        console.error('Lỗi khi xóa bình luận', error);
      });
  };
})