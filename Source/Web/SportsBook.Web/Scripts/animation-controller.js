$(document).ready(function () {
    $('.image').each(function () {
        animationHover(this, 'bounce');
    });   
});


function animationHover(element, animation) {
    element = $(element);
    var siblings = element.siblings();
    var parentCaption = element.parent().parent();
    var parentCaptionChildren = parentCaption.children();
    element.hover(
        function () {
            for (var i = 0; i < siblings.length; i++) {
                if ($(siblings[i]).hasClass("details-caption-holder")) {
                    $(siblings[i]).css("display", "inline-block");
                    $(siblings[i]).addClass("animated " + animation);
                }
            }

            for (var i = 0; i < parentCaptionChildren.length; i++) {
                if(parentCaptionChildren.hasClass("user-rating-holder")){
                    $(parentCaptionChildren[i]).css("display", "inline-block");
                    $(parentCaptionChildren[i]).addClass("animated " + animation);
                }
            }
        },
        function () {

            // var result = underElement('.details-caption-holder', ".user-rating-holder");
            // var timer;
            // $(".details-caption-holder", ".user-rating-holder").mouseleave(function () {
            //     timer = setTimeout(setDisplayNone, 10);
            // }).mouseenter(function () {
            //     clearTimeout(timer);
            // });
            // 
            // function setDisplayNone() {
            //     $(".details-caption-holder", ".user-rating-holder").css("display", "none");
            // }

            //wait for animation to finish before removing classes
            window.setTimeout(function () {



                window.isHovering = function (selector) {
                    return $(selector).data('hover') ? true : false; //check element for hover property
                }

                for (var i = 0; i < siblings.length; i++) {
                    if ($(siblings[i]).hasClass("details-caption-holder")) {                        
                        $(siblings[i]).removeClass("animated " + animation)
                    }
                }

                for (var i = 0; i < parentCaptionChildren.length; i++) {
                    if ($(parentCaptionChildren[i]).hasClass("user-rating-holder")) {
                        $(parentCaptionChildren[i]).removeClass("animated " + animation)
                    }
                }

            }, 1000);          
        });
}


// $(".image").hover(function (event) {
//     var target = $(event.target);
//     var siblings = target.siblings();
//     for (var i = 0; i < siblings.length; i++) {
//         if ($(siblings[i]).hasClass("details-caption-holder")) {
//             $(siblings[i]).css("display", "inline-block");
//             $(siblings[i]).addClass("animated bounceIn")
//             //.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
//             //$(siblings[i]).removeClass("animated bounceIn");                    
//             //});                
//         }
//     }
// }, function (event) {
//     var target = $(event.target);
//     var siblings = target.siblings();
//     for (var i = 0; i < siblings.length; i++) {
//         if ($(siblings[i]).hasClass("details-caption-holder")) {
//             $(siblings[i]).css("display", "none");
//             $(siblings[i]).removeClass("animated bounceIn")
//             //.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
//             //$(siblings[i]).removeClass("animated bounceIn");                    
//             //});                
//         }
//     }    
// });
//},
// mouseleave: function (event) {
//    var target = $(event.target);
//    var siblings = target.siblings();
//    for (var i = 0; i < siblings.length; i++) {
//        if ($(siblings[i]).hasClass("details-caption-holder")) {
//            $(siblings[i]).removeClass("animated fadeIn");
//            $(siblings[i]).css("display", "none");
//        }
//    }
//}
// });

// $(".image").bind("animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd", function (e) {
//     $(this).removeClass('animated bounceIn');
// });


