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
           var addedRecentlyComments = $("#latest-comments-container").children();
           $(addedRecentlyComments[0]).load("RedirectToLatestGetCommentAction/" + facilityId);
           history.pushState({}, null, "/Facilities/FacilitiesPublic/FacilityDetails/" + facilityId);
       });
})