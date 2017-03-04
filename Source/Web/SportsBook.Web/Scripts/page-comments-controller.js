$(".page-number-element").on("click", function () { changeCommentsPage() });
function changeCommentsPage() {
    var facilityId = $("#comments-container").attr("data-id");
    $(".comments-page-body").empty();
    var pageNumber = $(this).html();
    history.pushState({}, null, "/Facilities/FacilitiesPublic/");
    $("#comments-container").load("RedirectToGetSelectedPageComments/" + facilityId + "?pageNumber=" + pageNumber);
}

$(document).on("click", ".page-number-element", function (e) {
    var facilityId = $("#comments-container").attr("data-id");
    $(".comments-page-body").empty();
    var pageNumber = $(this).html();
    history.pushState({}, null, "/Facilities/FacilitiesPublic/");
    $("#comments-container").load("RedirectToGetSelectedPageComments/" + facilityId + "?pageNumber=" + pageNumber);
    $("#latest-comments-container").empty();
    history.pushState({}, null, "/Facilities/FacilitiesPublic/FacilityDetails/" + facilityId);
});