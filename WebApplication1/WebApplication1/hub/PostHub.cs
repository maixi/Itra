using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WebApplication1;
using System.IO;
using WebApplication1.Filters;

namespace WebApplication1.Hubs
{
    public class PostHub : Hub
    {
        private string imgFolder = "/images/";
        private string defaultAvatar = "user.png";

        public void GetPosts(int DemId)
        {
            using (Entities db = new Entities())
            {
                var ret = (from post in db.Comments.Where(d => d.DemotivatorId == DemId).ToList()
                           orderby post.PublicationDate
                           select new
                           {
                               Message = post.CommentText,
                               PostedBy = post.AspNetUserId,
                               PostedByName = post.AspNetUser.UserName,
                               PostedByAvatar = imgFolder + defaultAvatar,
                               PostedDate = post.PublicationDate,
                               PostId = post.Id,
                           }).ToArray();
                Clients.All.loadPosts(ret, DemId);
            }
        }

        public void AddPost(Comment post, string UserId, int DemId1)
        {
            post.AspNetUserId = UserId;
            post.PublicationDate = DateTime.Now;
            post.DemotivatorId = DemId1;
            
            using (Entities db = new Entities())
            {
                db.Comments.Add(post);
                db.SaveChanges();
                var usr = db.AspNetUsers.FirstOrDefault(x => x.Id == post.AspNetUserId);
                var ret = new
                {
                    Message = post.CommentText,
                    PostedBy = post.AspNetUserId,
                    PostedByName = usr.UserName,
                    PostedByAvatar = imgFolder + defaultAvatar,
                    PostedDate = post.PublicationDate,
                    PostId = post.Id
                };

                Clients.Caller.addPost(ret);
                Clients.Others.newPost(ret, DemId1);
            }
        }


    }
}