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
            } else {
                $(childrenStars[j]).html("&#9733;");
                $(childrenStars[j]).css("color", "aqua");
            }
        }        
    }

    function updateFacilityRating(starDivHolder, facilityId) {        
        var boxHolder = starDivHolder.parent().parent().parent().parent().parent().children();
        var overallRatingElements = $(boxHolder[3]).children();
        var totalRatingContainer = $(overallRatingElements).children();
        var totalRatingContainerElements = $(totalRatingContainer[1]).children();
        var totalRatingElementContainer = $(totalRatingContainerElements[0]).children();
        var spanContainer = totalRatingElementContainer[0];
        //var spanContainerElements = $(spanContainer).children();
        //var spanRating = $(spanContainerElements).html();
        $.get("/Facilities/Ratings/GetFacilityRating", { id: facilityId}, function (data) {
            $(spanContainer).text(data);
        });
    }


    $(".star-click").on("click", function (event) {
        var starDivHolder = $(this).parent();
        var facilityId = $(this).parent().attr("data-id");       
        var clickedStar = $(event.target);
        var previousElements = clickedStar.nextAll();
        var ratingValue = previousElements.length + 1;
        $.post("/Facilities/Ratings/AddRating",
            { facilityId: facilityId, ratingValue: ratingValue }, function success(data, textStatus, jqXHR) {
                starDivHolder.attr("rating-value", ratingValue);
                setStars(starDivHolder);               
                var facilityId = $(starDivHolder).attr("data-id");
                
                updateFacilityRating(starDivHolder, facilityId)
            });

        $.get("/Facilities/FacilitiesPublic/GetFacilityName", { id: facilityId }, function (data) {
            alertify.success("You rated " + data + " with " + ratingValue + " stars.");            
        });
    });
});
