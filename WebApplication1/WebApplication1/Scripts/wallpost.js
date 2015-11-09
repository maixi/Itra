﻿function getTimeAgo(varDate) {
    if (varDate) {
        return $.timeago(varDate.toString().slice(-1) == 'Z' ? varDate : varDate + 'Z');
    }
    else {
        return '';
    }
}
// Model
function Post(data, hub) {
    var self = this;
    data = data || {};
    self.PostId = data.PostId;
    self.Message = ko.observable(data.Message || "");
    self.PostedBy = "/Account/UserAccount/"+data.PostedBy || "";
    self.PostedByName = data.PostedByName || "";
    self.PostedByAvatar = data.PostedByAvatar || "";
    self.PostedDate = data.PostedDate;
    self.error = ko.observable();
    self.PostComments = ko.observableArray();
    self.NewComments = ko.observableArray();
    self.newCommentMessage = ko.observable();
    self.LikeCount = data.LikeCount;
    self.hub = hub;
    self.addComment = function () {
        self.hub.server.addComment({ "PostId": self.PostId, "Message": self.newCommentMessage() }).done(function (comment) {
            self.PostComments.push(new Comment(comment));
            self.newCommentMessage('');
        }).fail(function (err) {
            self.error(err);
        });
    }
    self.addLike = function()
    {
        var formData = new FormData;
        formData.append("CommentId", self.PostId)
        $.ajax(
            {
                async: false,
                url: '/Demotivators/AddLike',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (data)
                {
                    document.getElementById(self.PostId).textContent = data;
                }
            });
        
    }

    self.loadNewComments = function () {
        self.PostComments(self.PostComments().concat(self.NewComments()));
        self.NewComments([]);
    }
    self.toggleComment = function (item, event) {
        $(event.target).next().find('.publishComment').toggle();
    }


    if (data.PostComments) {
        var mappedPosts = $.map(data.PostComments, function (item) { return new Comment(item); });
        self.PostComments(mappedPosts);
    }

}



function Comment(data) {
    var self = this;
    data = data || {};

    // Persisted properties
    self.CommentId = data.CommentId;
    self.PostId = data.PostId;
    self.Message = ko.observable(data.Message || "");
    self.CommentedBy = data.CommentedBy || "";
    self.CommentedByAvatar = data.CommentedByAvatar || "";
    self.CommentedByName = data.CommentedByName || "";
    self.CommentedDate = getTimeAgo(data.CommentedDate);
    self.error = ko.observable();
}




function viewModel() {
    var self = this;
    self.posts = ko.observableArray();
    self.newMessage = ko.observable();
    self.error = ko.observable();

    //SignalR related
    self.newPosts = ko.observableArray();
    // Reference the proxy for the hub.  
    self.hub = $.connection.postHub;


    self.init = function () {
        self.error(null);
        var DemId = document.getElementById("DemId").value;
        self.hub.server.getPosts(DemId).fail(function (err) {
            self.error(err);
        });
    }

    self.addPost = function () {
        self.error(null);
        var DemId1 = document.getElementById("DemId").value;
        var UserId = document.getElementById("UserId").value;
        self.hub.server.addPost({ "CommentText": self.newMessage() }, UserId, DemId1).fail(function (err) {
            self.error(err);
        });
    }

    self.loadNewPosts = function () {
        self.posts(self.posts().concat(self.newPosts()));
        self.newPosts([]);
    }

    //functions called by the Hub
    self.hub.client.loadPosts = function (data, DemId) {
        var DemId1 = document.getElementById("DemId").value;
        if (DemId == DemId1) {
            var mappedPosts = $.map(data, function (item) { return new Post(item, self.hub); });
            self.posts(mappedPosts);
        }
    }

    self.hub.client.addPost = function (post) {
        self.posts.push(new Post(post, self.hub));
        self.newMessage('');
    }

    self.hub.client.newPost = function (post, DemId1) {
        var DemId2 = document.getElementById("DemId").value;
        if (DemId1 == DemId2) {
            self.newPosts.splice(-1, 0, new Post(post, self.hub));
        }
    }

    self.hub.client.error = function (err) {
        self.error(err);
    }

    self.hub.client.newComment = function (comment, postId) {
        //check in existing posts
        var posts = $.grep(self.posts(), function (item) {
            return item.PostId === postId;
        });
        if (posts.length > 0) {
            posts[0].NewComments.push(new Comment(comment));
        }
        else {
            //check in new posts (not displayed yet)
            posts = $.grep(self.newPosts(), function (item) {
                return item.PostId === postId;
            });
            if (posts.length > 0) {
                posts[0].NewComments.push(new Comment(comment));
            }
        }
    }
    return self;
};

//custom bindings
//textarea autosize
ko.bindingHandlers.jqAutoresize = {
    init: function (element, valueAccessor, aBA, vm) {
        if (!$(element).hasClass('msgTextArea')) {
            $(element).css('height', '1em');
        }
        $(element).autosize();
    }
};

var vmPost = new viewModel();
ko.applyBindings(vmPost);
$.connection.hub.start().done(function () {
    vmPost.init();
});