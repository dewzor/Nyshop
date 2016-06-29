using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ReviewController : ApiController
    {
        // GET api/review
        [HttpGet]
        public IEnumerable<Review> Get(string id) // Gets all reviews for sent product id
        {
            using (var context = new StoreContext())
            {
                List<Review> reviewlist = new List<Review>();
                int idd = Int32.Parse(id);
                var reviews = (from a in context.Reviews
                             where (a.ProductId == idd)
                             select a);
                foreach (var review in reviews)
                {
                    reviewlist.Add(review);
                    //Review post = new Review();
                    //post.CustomerId = review.CustomerId;
                    //post.authorid = review.FID;
                    //post.post = review.POST;
                    //post.walluserid = review.PID;
                    //var poster = _usersRepository.getUserByID(post.authorid);
                    //post.authorname = poster.Firstname;
                    //temppost.Add(post);
                }
                return reviewlist;
            }
        }
        // SET api/review
        [HttpPost]
        public void Post(Review post) // Handles submitted review and adds it to db
        {
            using (var context = new StoreContext())
            {
                Review newreview = new Review();
                newreview.ProductId = post.ProductId;
                newreview.CustomerId = post.CustomerId;
                newreview.Rating = post.Rating;
                newreview.Time = DateTime.Now;
                if (post.Comment.Length < 1000)
                    newreview.Comment = post.Comment;  // If comment is below 1000 chars.
                
                try
                {
                    context.Reviews.Add(newreview);
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
