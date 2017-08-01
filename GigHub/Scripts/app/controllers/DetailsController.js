var DetailsController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollow);
    };

    var toggleFollow = function (e) {
        button = $(e.target);
        var followeeId = button.attr("data-user-id");
        if (button.text() === "Follow") {
            followingService.follow(followeeId, done, fail);
        } else {
            followingService.unfollow(followeeId, done, fail);
        }
    };

    var done = function () {
        var text = button.text() === "Follow" ? "Following" : "Follow";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function () {
        alert("something failed");
    };


    return {
        init: init
    };

}(FollowingService);