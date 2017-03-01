// $(document).ready(function () {  
    $(".submit-comment-button").on("click", function (event) {
        var id = $(this).attr("data-id"),
        commentBoxes = $(".add-comment-box"),
        commentBox,
        content;
        if (commentBoxes.length > 0) {
            commentBox = commentBoxes[0];
            content = $(commentBox).val()
        }

        $.post("/Facilities/Comments/AddComment",
           { id: id, content: content }, function success(data, textStatus, jqXHR) {
             alertify.success("You commented.")
           });
    })
// });