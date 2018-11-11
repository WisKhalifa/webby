using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webby.Models;

namespace webby.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

       
        

        /*public HomeController (PostContext _db)
        {
            db = _db;
        }*/

        //private PostContext db = new PostContext();

        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (PostModels post)
        {
            if (ModelState.IsValid)
            {
                
                db.PostModels.Add(post);
                db.SaveChanges();
                return RedirectToAction("PostList");
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult PostList()
        {
            PostListViewModels postListViewModel = new PostListViewModels();

            postListViewModel.Posts = db.PostModels.ToList<PostModels>();

            return View(postListViewModel);

        }


    }
}