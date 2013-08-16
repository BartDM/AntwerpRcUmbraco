$(document).ready(function() {
    var sliders = $(".image-slider > .slider");

    //shiftSlider(sliders, 0);    
    $(sliders[0]).hide({ duration: 500 }, function () {
        console.log("ready with transition");
    });


});

function shiftSlider(container, nr) {
    if (nr > container.length - 1) {
        nr = 0;
    }
    var slider = container[nr];
    $(slider).animate({ color: "red" }, { duration: 500 }, function () {
        console.log("ready with transition");
    });
}

/*
            nr++;
            if (nr > container.length - 1) {
                nr = 0;
            }
            var sliderToShow = container[nr];
            $(sliderToShow).animate({ display: "block" }, { duration: 5000 }, function () {
                shiftSlider(nr++);
            });
*/