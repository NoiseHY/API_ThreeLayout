let slideIndex = 1;
showSlides(slideIndex);

// Tự động chạy slide sau mỗi 7 giây
setInterval(function() { 
  plusSlides(1);
}, 7000);

// Hàm thay đổi slide
function plusSlides(n) {
  showSlides(slideIndex += n);
}

// Hàm hiển thị slide
function showSlides(n) {
  let i;
  let slides = document.getElementsByClassName("slide");
  if (n > slides.length) {slideIndex = 1}
  if (n < 1) {slideIndex = slides.length}
  for (i = 0; i < slides.length; i++) {
      slides[i].style.display = "none";
  }
  slides[slideIndex-1].style.display = "block";
}

var imgDanhMuc = document.getElementById('imgDanhMuc');



imgDanhMuc.addEventListener('click', function(){
  window.location.href = 'http://127.0.0.1:5500/L%E1%BA%ADp%20tr%C3%ACnh%20Web%20B%C3%A1ch%20H%C3%B3a%20Xanh/HTML/Chi%20ti%E1%BA%BFt%20s%E1%BA%A3n%20ph%E1%BA%A9m/Chi%20ti%E1%BA%BFt%20s%E1%BA%A3n%20ph%E1%BA%A9m.html';
})