// $(document).ready(function () {  
$(".submit-comment-button").on("click", function (event) {
    var facilityId = $(this).attr("data-id"),
    commentBoxes = $(".add-comment-textarea"),
    commentBox,
    content;
    if (commentBoxes.length > 0) {
        commentBox = commentBoxes[0];
        content = $(commentBox).val()
    }


    $.post("/Facilities/Comments/AddComment",
       { id: facilityId, content: content }, function success(data, textStatus, jqXHR) {
           var url = '@Url.Action("GetLatestComment", "Comments", new { area= "Facilities", id="facilityId" });';
           url = url.replace("facilityId", facilityId);
           history.pushState({}, null, "/Facilities/FacilitiesPublic/");
           $("#latest-comments-container").prepend("<div></div>");
           var recentlyAddedComments = $("#latest-comments-container").children();
           $(recentlyAddedComments[0]).load("RedirectToGetLastComment/" + facilityId);
           history.pushState({}, null, "/Facilities/FacilitiesPublic/FacilityDetails/" + facilityId);
       });
})

$("#show-comments-button").on("click", function () { showComments() });
function showComments() {
    var facilityId = $("#comments-container").attr("data-id");
    $("#show-comments-button").css("display", "none");
    $("#comments-part").css("display", "inline-block");
    $("#hide-comments-button").css("display", "inline-block")
    history.pushState({}, null, "/Facilities/FacilitiesPublic/");
    $("#comments-container").load("GetLatestComments/" + facilityId);
    history.pushState({}, null, "/Facilities/FacilitiesPublic/FacilityDetails/" + facilityId);
}

$("#hide-comments-button").on("click", function () { hideComments() });
function hideComments() {
    $("#hide-comments-button").css("display", "none")
    $("#comments-part").css("display", "none")
    $("#show-comments-button").css("display", "inline-block")
}