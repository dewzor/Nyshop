using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ReviewController : Controller
    {
        //// GET api/wall
        //[HttpGet]
        //public IEnumerable<Review> Get(string id) // Hämtar alla wallposts som är skrivna på "id"s wall.
        //{
        //    using (var context = new StoreContext())
        //    {
        //        List<Review> reviews = new List<Review>();
        //        int idd = Int32.Parse(id);
        //        var posts = (from a in context.WALLPOST
        //                     where (a.PID == idd)
        //                     select a);
        //        foreach (var r in posts)
        //        {
        //            Review post = new Review();
        //            post.postid = r.POSTID;
        //            post.authorid = r.FID;
        //            post.post = r.POST;
        //            post.walluserid = r.PID;
        //            var poster = _usersRepository.getUserByID(post.authorid);
        //            post.authorname = poster.Firstname;
        //            temppost.Add(post);
        //        }
        //        return temppost;
        //    }
        //}
        // SET api/review
        [HttpPost]
        public void Post(Review post) // Handles submitted review and adds it to db
        {
            using (var context = new StoreContext())
            {
                Review newreview = new Review();
                newreview.ProductId = post.ProductId;
                newreview.CustomerId = post.CustomerId;
                //newreview.ProductId = Convert.ToInt32(post.productidstring); // PID är id för användaren vars wall posten postas till.
                //newreview.FID = Convert.ToInt32(post.authoridstring); // Friendid som postar posten.
                if (post.Comment.Length < 1000)
                    newreview.Comment = post.Comment;  // ..
                try
                {
                    //context.Review.Add(newreview);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



    }
}
