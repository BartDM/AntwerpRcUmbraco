$(document).ready(function() {
    $('.bxslider').bxSlider({
        minSlides: 5,
        maxSlides: 5,
        slideWidth: 400,
        slideMargin: 15,
        controls: false,
        auto: true,
        speed: 5000,
        randomStart: false,
        infiniteLoop: true,
        preloadImages: 'all',
        autoHover: true,
        onSliderLoad: function () {
            $('.bxslider img').hover(function () {
                var imgSrc = $(this).attr("data-img-src-color");
                $(this).attr("src", imgSrc);
                if (this.complete) {
                    $(this).trigger('load');
                }
            }, function () {
                var imgSrc = $(this).attr("data-img-src-bw");
                $(this).attr("src", imgSrc);
                if (this.complete) {
                    $(this).trigger('load');
                }
            });
        }
    });
    $('#da-slider').cslider({
        autoplay: true,
        interval: 5000,
        bgincrement:0
    });
});
