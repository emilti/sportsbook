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
           $(".add-comment-textarea").val("");
           handleActionCall(facilityId);
       });
})

$("#show-comments-button").on("click", function () { showComments() });
function showComments() {
    var facilityId = $("#comments-container").attr("data-id");
    $("#show-comments-button").css("display", "none");
    $("#comments-part").css("display", "inline-block");
    $("#hide-comments-button").css("display", "inline-block")
    handleActionCall(facilityId);
}

$("#hide-comments-button").on("click", function () { hideComments() });
function hideComments() {
    $("#hide-comments-button").css("display", "none")
    $("#comments-part").css("display", "none")
    $("#show-comments-button").css("display", "inline-block")
}

function handleActionCall(facilityId) {
    history.pushState({}, null, "/Facilities/FacilitiesPublic/");
    $("#comments-container").load("GetLatestComments/" + facilityId);
    history.pushState({}, null, "/Facilities/FacilitiesPublic/FacilityDetails/" + facilityId);
}