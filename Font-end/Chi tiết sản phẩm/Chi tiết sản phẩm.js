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

    $scope.addToCart = function (product) {
        var maSP = $window.localStorage.getItem('maSP');
        var maTK = $window.localStorage.getItem('userID');
        var cart = {
            MaKH: maTK,
            MaSP: maSP,
            Soluong: 1,
            Dongia: $scope.dongia
        };

        $http.post('https://localhost:7118/api/Cart/Create', cart)
            .then(function (response) {
                alert('Thêm sản phẩm vào giỏ hàng thành công !');
                // console.log(response.data); 
            })
            .catch(function (error) {
                alert('Đã xảy ra lỗi khi thêm vào giỏ hàng!');
                console.error('Lỗi khi tạo sản phẩm:', error);
            });
    };

    $scope.viewHome = function () {
        $window.location.href = '/Trang chủ/TrangChu.html';
        console.log('ht');
    };
});

app.controller('RatingController', function ($scope, $http, $window) {
    $scope.customer = {};

    var id = localStorage.getItem('userID');

    $scope.getCustomer = function () {

        if (id) {
            $http.get('https://localhost:7118/api/InfoCustomer/GetCustomerByID/' + id)
                .then(function (response) {
                    console.log("Hiển thị ");
                    $scope.customer = response.data;

                })
                .catch(function (error) {
                    console.error('Lỗi khi lấy dữ liệu', error);
                });
        } else {
            console.error('Không tìm thấy ID trong localStorage');
        }
    };

    $scope.getCustomer();

    $scope.ratingValue = 0;

    $scope.setRating = function (rating) {
        $scope.ratingValue = rating;
        console.log('Bạn đã chọn: ' + rating);
    };

    $scope.createRating = function () {
        if (!$scope.comment) {
            alert('Vui lòng nhập bình luận ');
            return;
        }

        if (!$scope.ratingValue) {
            alert('Vui lòng nhập số sao để đánh giá sản phẩm ! ');
            return;
        }

        var maSP = $window.localStorage.getItem('maSP');
        var maTK = $window.localStorage.getItem('userID');
        var BinhLuan = $scope.comment;

        var currentTime = new Date().toISOString();

        var newRating = {
            MaSP: maSP,
            MaTK: maTK,
            DanhGia: $scope.ratingValue,
            BinhLuan: BinhLuan,
            ThoiGian: currentTime
        };

        $http.post('https://localhost:7118/api/Rating/Create', newRating)
            .then(function (response) {
                console.log('Thêm bình luận thành công !', response.data);
                alert("Thêm bình luận thành công !");
                $window.location.reload();
            })
            .catch(function (error) {
                console.error('Lỗi khi thêm bình luận !', error);
                alert('Lỗi khi thêm bình luận ');
            });
    };

    $scope.getRating = function () {
        var maSP = $window.localStorage.getItem('maSP');
        $http.get('https://localhost:7118/api/Rating/GetAllRatingCmt?id=' + maSP)
            .then(function (response) {
                console.log("Hiển thị 2");
                $scope.ratingData = response.data;
            })
            .catch(function (error) {
                console.error('Lỗi', error);
            });
    }

    $scope.getRating();
})

app.controller('home', function ($window, $scope) {
    $scope.viewHome = function () {
        $window.location.href = '/Trang chủ/TrangChu.html';
        console.log('ht');
    };
})