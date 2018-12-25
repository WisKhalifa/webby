using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using webby.Interfaces;
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

        //Db access
        private ApplicationDbContext db = new ApplicationDbContext();

        //Using Dependency Injection and repository pattern to access db for posts
        private readonly IPostRepository _postRepository = new PostRepository();
        public HomeController (IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        //Check if user is in admin role
        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }

        //GET: Create view
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        //POST: Create a post/add post to db
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "PostId,Title,PostContent")] PostModels post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Add(post);
                
                return RedirectToLocal("PostList");
            }
            return View("Create");
        }

        //GET: Post list view model to get all posts from db
        [HttpGet]
        public ActionResult PostList()
        {
            PostListViewModels postListViewModel = new PostListViewModels
            {
                Posts = db.PostModels.ToList<PostModels>()
            };

            return View(postListViewModel);

        }

        //GET: View post actions to edit and delete if admin
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


        //Return details of post by id
        public ActionResult Details(int? id)
        {

            return View(db.PostModels.Find(id));
        }

        //GET: Return a post by given id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.PostModels.Find(id);
            return View(model);
        }

        //GET: Return comments by given id
        [HttpGet]
        public ActionResult _Comments(int id)
        {
            var comments = this.db.Comments
                .Where(x => x.PostId == id)
                .Select(CommentViewModels.ViewModel);

            ViewBag.test = comments;


            return this.PartialView("_Comments", comments);
        }

        //POST: Add a comment to the db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentId,Name,Text,PostId")]CommentModels data)
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

        //GET: Return a comment with a given id
        [HttpGet]
        public ActionResult CreateComment(int pId)
        {
            CommentModels newCom = new CommentModels();
            newCom.PostId = pId;
            ViewBag.Foo = pId;
            return View(newCom);
        }

        //Return apartial view of comments with id
        public PartialViewResult GetComs(int pId)
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

        //Return a post to edit given an id
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

        //POST: Add an edited post into the ed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,PostContent")] PostModels postModel)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Edit(postModel);
                return RedirectToLocal("PostList");
            }
            return View(postModel);
        }

        //Delete a post from the db given an id
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

        //POST: Confirm post has been deleted from db
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _postRepository.Remove(id);
            return RedirectToLocal("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal (string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        

    }
}