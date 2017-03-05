$(document).on("click", ".page-number-element", function (e) {
    var facilityId = $("#comments-container").attr("data-id");
    var pageNumber = $(this).html();
    getSelectedPageComments(facilityId, pageNumber);
});

$(document).on("click", ".previous-page-arrow-element", function (e) {
    var facilityId = $("#comments-container").attr("data-id");
    var pageNumber = $("li.active>a").html() - 1;
    getSelectedPageComments(facilityId, pageNumber);
});

$(document).on("click", ".next-page-arrow-element", function (e) {
    var facilityId = $("#comments-container").attr("data-id");
    var pageNumber = parseInt($("li.active>a").html()) + 1;
    getSelectedPageComments(facilityId, pageNumber);
});

function getSelectedPageComments(facilityId, pageNumber) {
    $(".comments-page-body").empty();
    history.pushState({}, null, "/Facilities/FacilitiesPublic/");
    $("#comments-container").load("RedirectToGetSelectedPageComments/" + facilityId + "?pageNumber=" + pageNumber);
    $("#latest-comments-container").empty();
    history.pushState({}, null, "/Facilities/FacilitiesPublic/FacilityDetails/" + facilityId);
}
