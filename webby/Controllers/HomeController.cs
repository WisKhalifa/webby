using Microsoft.AspNet.Identity;
using System.Linq;
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


        private ApplicationDbContext db = new ApplicationDbContext();


        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(PostModels post)
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

        [HttpGet]
        public ActionResult _PostDetails(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var postDetails = this.db.PostModels
                .Where(e => e.AuthorId == id)
                .Select(PostListViewModels.ViewModel)
                .FirstOrDefault();

            this.ViewBag.CanEdit = isAdmin;

            return this.PartialView("_PostDetails", postDetails);
        }


    }
}