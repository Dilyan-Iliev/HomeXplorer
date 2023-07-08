document.addEventListener('DOMContentLoaded', function () {
    // Get the carousel element
    var carousel = document.getElementById('carouselExampleControls');

    // Get the carousel items
    var carouselItems = carousel.querySelectorAll('.carousel-item');

    // Set the current slide index
    var currentSlide = 0;

    // Function to show the current slide
    function showSlide() {
        // Remove the 'active' class from all carousel items
        carouselItems.forEach(function (item) {
            item.classList.remove('active');
        });

        // Add the 'active' class to the current slide
        carouselItems[currentSlide].classList.add('active');
    }

    // Function to move to the next slide
    function nextSlide() {
        currentSlide++;

        // If the current slide index exceeds the number of slides, wrap to the first slide
        if (currentSlide >= carouselItems.length) {
            currentSlide = 0;
        }

        showSlide();
    }

    // Function to move to the previous slide
    function prevSlide() {
        currentSlide--;

        // If the current slide index is less than 0, wrap to the last slide
        if (currentSlide < 0) {
            currentSlide = carouselItems.length - 1;
        }

        showSlide();
    }

    // Get the next and previous buttons
    var nextButton = carousel.querySelector('.carousel-control-next');
    var prevButton = carousel.querySelector('.carousel-control-prev');

    // Add click event listeners to the next and previous buttons
    nextButton.addEventListener('click', nextSlide);
    prevButton.addEventListener('click', prevSlide);

    // Automatic slide interval in milliseconds (e.g., 3000ms = 3 seconds)
    var slideInterval = 3000;

    // Function to automatically move to the next slide
    function autoSlide() {
        nextSlide();
    }

    // Start the automatic slide timer
    var timer = setInterval(autoSlide, slideInterval);

    // Pause the automatic slide when the carousel is hovered
    carousel.addEventListener('mouseover', function () {
        clearInterval(timer);
    });

    // Resume the automatic slide when the carousel is no longer hovered
    carousel.addEventListener('mouseout', function () {
        timer = setInterval(autoSlide, slideInterval);
    });
});