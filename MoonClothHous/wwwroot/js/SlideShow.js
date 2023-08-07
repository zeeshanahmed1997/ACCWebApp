const slider = document.querySelector(".slider");
let slideIndex = 0;

function showSlide(index) {
    slider.style.transform = `translateX(-${index * 100}%)`;
}

function autoSlide() {
    slideIndex = (slideIndex + 1) % 3; // Change '3' to the number of slides
    showSlide(slideIndex);
}

// Start auto-sliding every 3 seconds
setInterval(autoSlide, 3000);
