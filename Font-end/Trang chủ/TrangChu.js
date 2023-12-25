// document.addEventListener('DOMContentLoaded', function () {
//   // Các đoạn mã để đếm ngược
//   const phầnTửNgày = document.querySelector("#days");
//   const phầnTửGiờ = document.querySelector("#hours");
//   const phầnTửPhút = document.querySelector("#minutes");
//   const phầnTửGiây = document.querySelector("#seconds");

//   const thờiGianMụcTiêu = new Date("2023-12-27T00:00:00").getTime();

//   function cậpNhậtĐồngHồ() {
//     const thờiGianHiệnTại = new Date().getTime();
//     const khoảngCáchThờiGian = thờiGianMụcTiêu - thờiGianHiệnTại;

//     if (khoảngCáchThờiGian > 0) {
//       const sốNgàyCònLại = Math.floor(khoảngCáchThờiGian / (1000 * 60 * 60 * 24));
//       const sốGiờCònLại = Math.floor((khoảngCáchThờiGian % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
//       const sốPhútCònLại = Math.floor((khoảngCáchThờiGian % (1000 * 60 * 60)) / (1000 * 60));
//       const sốGiâyCònLại = Math.floor((khoảngCáchThờiGian % (1000 * 60)) / 1000);

//       phầnTửNgày.textContent = sốNgàyCònLại;
//       phầnTửGiờ.textContent = sốGiờCònLại.toString().padStart(2, '0');
//       phầnTửPhút.textContent = sốPhútCònLại.toString().padStart(2, '0');
//       phầnTửGiây.textContent = sốGiâyCònLại.toString().padStart(2, '0');
//     } else {
//       phầnTửNgày.textContent = '00';
//       phầnTửGiờ.textContent = '00';
//       phầnTửPhút.textContent = '00';
//       phầnTửGiây.textContent = '00';
//       clearInterval(cậpNhậtMỗiGiây);
//     }
//   }

//   const cậpNhậtMỗiGiây = setInterval(cậpNhậtĐồngHồ, 1000);
//   cậpNhậtĐồngHồ();

//   // Event listener cho hình ảnh
//   var imgDanhMuc = document.getElementById('imgDanhMuc');
//   imgDanhMuc.addEventListener('click', function () {
//     window.location.href = 'http://127.0.0.1:5500/L%E1%BA%ADp%20tr%C3%ACnh%20Web%20B%C3%A1ch%20H%C3%B3a%20Xanh/HTML/Trang%20danh%20m%E1%BB%A5c/Danhmuc.html';
//   });

//   // var imgCart = document.getElementById('imgCart');
//   // imgCart.addEventListener('click', function () {
//   //   window.location.href = 'http://127.0.0.1:5500/L%E1%BA%ADp%20tr%C3%ACnh%20Web%20B%C3%A1ch%20H%C3%B3a%20Xanh/HTML/Trang%20gi%E1%BB%8F%20h%C3%A0ng/Gi%E1%BB%8F%20h%C3%A0ng.html';
//   // });


// }); 



// let slideIndex = 1;
// showSlides(slideIndex);

// // Tự động chạy slide sau mỗi 7 giây
// setInterval(function () {
//   plusSlides(1);
// }, 7000);

// // Hàm thay đổi slide
// function plusSlides(n) {
//   showSlides(slideIndex += n);
// }

// // Hàm hiển thị slide
// function showSlides(n) {
//   let i;
//   let slides = document.getElementsByClassName("slide");
//   if (n > slides.length) { slideIndex = 1 }
//   if (n < 1) { slideIndex = slides.length }
//   for (i = 0; i < slides.length; i++) {
//     slides[i].style.display = "none";
//   }
//   slides[slideIndex - 1].style.display = "block";
// }

// let productList = [];


// function addToProductList(element) {
//   const product = {};

//   // Lấy các thông tin sản phẩm từ phần tử HTML
//   const parent = element.closest('.Sanpham-tpye');
//   product.img = parent.querySelector('#product-img').src;
//   product.name = parent.querySelector('#product-name').textContent;
//   product.price = parent.querySelector('#product-price').textContent;
//   product.sales = parent.querySelector('#product-sales').textContent;
//   product.star = parent.querySelector('#product-stars').textContent;
//   product.pt = parent.querySelector('#product-pt').textContent;

//   // Thêm sản phẩm vào danh sách
//   productList.push(product);

//   console.log('Sản phẩm đã được thêm vào danh sách:', productList);
// }

// function convertToJSON() {
//   const productListJSON = JSON.stringify(productList);
//   localStorage.setItem('productListJSON', productListJSON);

//   // Đợi 
//   setTimeout(() => {
//     window.location.href = 'http://127.0.0.1:5500/L%E1%BA%ADp%20tr%C3%ACnh%20Web%20B%C3%A1ch%20H%C3%B3a%20Xanh/HTML/Trang%20gi%E1%BB%8F%20h%C3%A0ng/Gi%E1%BB%8F%20h%C3%A0ng.html';
//   }, 100);
// }

// Trong controller AngularJS
var app = angular.module('myApp', []);

app.controller('GetNewProductsController', function($scope, $http) {
  $http.get('https://localhost:7117/api/product/GetNewProductsAll?pageNumber=1&pageSize=10')
    .then(function(response) {

      $scope.products = response.data;
    })
    .catch(function(error) {
      
      console.error('Lỗi', error);
    });
});



