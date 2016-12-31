$(document).ready(function () {
    var userRatings = $(".user-rating")
    for (var i = 0; i < userRatings.length; i++) {
        setStars(userRatings[i]);
    }

    function setStars(userRating) {
        var facilityId = $(userRating).attr("data-id");
        var ratingValue = $(userRating).attr("rating-value");
        var childrenStars = $(userRating).children();
        for (var j = 0; j < 5; j++) {
            if (j < 5 - ratingValue) {
                $(childrenStars[j]).html("&#9734;");
                $(childrenStars[j]).css("color", "black");
            } else
            {
                $(childrenStars[j]).html("&#9733;");
                $(childrenStars[j]).css("color", "aqua");
            }            
        }
    }


    $(".star").on("click", function (event) {
        var starDivHolder = $(this).parent();
        var facilityId = $(this).parent().attr("data-id");
        var clickedStar = $(event.target);
        var previousElements = clickedStar.nextAll();
        var ratingValue = previousElements.length + 1;
        $.post("/Facilities/Ratings/AddRating",
            { facilityId: facilityId, ratingValue: ratingValue }, function success() {
                starDivHolder.attr("rating-value", ratingValue);
                setStars(starDivHolder);
            })
    });
})