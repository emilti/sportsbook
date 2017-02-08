$(document).ready(function () {
    $("div[data-action='changeFacility']").click(function () {
        var id = $(this).attr("data-id");
        var childrenElements = $("div[data-context ='facility_" + id + "']").children();
        var checkClass = $(childrenElements[0]).hasClass("remove-from-favorites-button");
        if (checkClass) {
            $.post("/Facilities/FavoriteFacilities/RemoveFromFavorites", { id: id },
          function (data) {
              $(childrenElements[0]).removeClass("remove-from-favorites-button");
              $(childrenElements[0]).addClass("favorites-button");
              alertify.error("Removed From Favorites");
              // $("div[data-context = 'facility_" + id + "']").removeClass("remove-from-favorites-button");
              // $("div[data-context = 'facility_" + id + "']").addClass("favorites-button");
              $("span[data-text = 'facility_" + id + "']").text("Add To Favorites")
          })
        } else {
            $.post("/Facilities/FavoriteFacilities/AddToFavorites", { id: id },
         function (data) {
             $(childrenElements[0]).removeClass("favorites-button");
             $(childrenElements[0]).addClass("remove-from-favorites-button");
             alertify.success("Added To Favorites");
             //$("div[data-context = 'facility_" + id + "']").removeClass("favorites-button");
             //$("div[data-context = 'facility_" + id + "']").addClass("remove-from-favorites-button");
             $("span[data-text = 'facility_" + id + "']").text("Remove From Favorites")
         })
        }
    })

    $("div[data-action='changeEvent']").click(function () {
        var id = $(this).attr("data-id");
        var childrenElements = $("div[data-context ='event_" + id + "']").children();
        var checkClass = $(childrenElements[0]).hasClass("remove-from-favorites-button");
        if (checkClass) {
            $.post("/Events/FavoriteEvents/RemoveFromFavorites", { id: id },
          function (data) {
              $(childrenElements[0]).removeClass("remove-from-favorites-button");
              $(childrenElements[0]).addClass("favorites-button");

              // $("div[data-context = 'event_" + id + "']").removeClass("remove-from-favorites-button");
              // $("div[data-context = 'event_" + id + "']").addClass("favorites-button");
          })
        } else {
            $.post("/Events/FavoriteEvents/AddToFavorites", { id: id },
         function (data) {
             $(childrenElements[0]).removeClass("favorites-button");
             $(childrenElements[0]).addClass("remove-from-favorites-button");
             // $("div[data-context = 'event_" + id + "']").removeClass("favorites-button");
             // $("div[data-context = 'event_" + id + "']").addClass("remove-from-favorites-button");
         })
        }
    })
})

$(document).on('click', '.btn', function () {
    $("div[data-action='changeFacility']").click(function () {
        var id = $(this).attr("data-id");
        var checkClass = $("div[data-context ='facility_" + id + "']").hasClass("remove-from-favorites-button")
        if (checkClass) {
            $.post("/Facilities/FavoriteFacilities/RemoveFromFavorites", { id: id },
          function (data) {
              $("div[data-context = 'facility_" + id + "']").removeClass("remove-from-favorites-button");
              $("div[data-context = 'facility_" + id + "']").addClass("favorites-button");
              $("span[data-text = 'facility_" + id + "']").text("Add To Favorites")
          })
        } else {
            $.post("/Facilities/FavoriteFacilities/AddToFavorites", { id: id },
         function (data) {
             $("div[data-context = 'facility_" + id + "']").removeClass("favorites-button");
             $("div[data-context = 'facility_" + id + "']").addClass("remove-from-favorites-button");
             $("span[data-text = 'facility_" + id + "']").text("Remove From Favorites")
         })
        }
    })

    $("div[data-action='changeEvent']").click(function () {
        var id = $(this).attr("data-id");
        var checkClass = $("div[data-context = 'event_" + id + "']").hasClass("remove-from-favorites-button")
        if (checkClass) {
            $.post("/Events/FavoriteEvents/RemoveFromFavorites", { id: id },
          function (data) {
              $("div[data-context = 'event_" + id + "']").removeClass("remove-from-favorites-button");
              $("div[data-context = 'event_" + id + "']").addClass("favorites-button");
          })
        } else {
            $.post("/Events/FavoriteEvents/AddToFavorites", { id: id },
         function (data) {
             $("div[data-context = 'event_" + id + "']").removeClass("favorites-button");
             $("div[data-context = 'event_" + id + "']").addClass("remove-from-favorites-button");
         })
        }
    })
})