using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public ActionResult Create([Bind(Include = "PostId,Title,PostContent")] PostModels post)
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
                .Where(e => e.PostId == id)
                .Select(PostListViewModels.ViewModel)
                .FirstOrDefault();

            this.ViewBag.CanEdit = isAdmin;

            return this.PartialView("_PostDetails", postDetails);
        }

        
        public ActionResult Details (int? id)
        {
            
            return View(db.PostModels.Find(id));
        }

        [HttpGet]
        public ActionResult Details (int id)
        {
            var model = db.PostModels.Find(id);
            return View(model);
        }

        
        [HttpGet]
        public ActionResult _Comments (int id)
        {
            var comments =  this.db.Comments
                .Where(x => x.PostId == id)
                .Select(CommentViewModels.ViewModel);

            ViewBag.test = comments;
            

            return this.PartialView("_Comments",comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment ([Bind(Include = "CommentId,Name,Text,PostId")]CommentModels data)
        {
            
            
            if (ModelState.IsValid)
            {
                var _comm = new CommentModels()
                {
                    PostId = data.PostId,
                    Name = System.Web.HttpContext.Current.User.Identity.Name,
                    Text = data.Text
                };
                db.Comments.Add(_comm);
                db.SaveChanges();
            }
            return PartialView("CreateComment");
        }

        [HttpGet]
        public ActionResult CreateComment (int pId)
        {
            CommentModels newCom = new CommentModels();
            newCom.PostId = pId;
            ViewBag.Foo = pId;
            return View(newCom);
        }


        public PartialViewResult GetComs (int pId)
        {
            IQueryable<CommentViewModels> comments = db.Comments.Where(c => c.Post.PostId == pId)
                .Select(c => new CommentViewModels
                {
                    CommentId = c.CommentId,
                    Name = c.Name,
                    Text = c.Text
                }).AsQueryable();
            return PartialView("~/Views/Home/_Comments.cshtml", comments);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModels postModel = db.PostModels.Find(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,PostContent")] PostModels postModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PostList");
            }
            return View(postModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModels postModel = db.PostModels.Find(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostModels postModel = db.PostModels.Find(id);
            db.PostModels.Remove(postModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}