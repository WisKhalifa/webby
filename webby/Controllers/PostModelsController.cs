using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using webby.Models;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;


namespace webby.Controllers
{
    public class PostModelsController : Microsoft.AspNetCore.Mvc.Controller
    {
        PostContext context = new PostContext();

        private PostContext db;

        public PostModelsController(PostContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Index(PostModels post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return View(post);
            }
            return View("Create");
        }


        // GET: PostModels/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult createPost (PostModels pst)
        {
            context.Posts.Add(pst);
            context.SaveChanges();
            string message = "Post Created";
            return Json(new { Message = message, System.Web.Mvc.JsonRequestBehavior.AllowGet });
        }

        public JsonResult getPost (string id)
        {
            List<PostModels> posts = new List<PostModels>();
            posts = context.Posts.ToList();
            return Json(posts);
        }
        


        /* //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PostModels
        public ActionResult Index()
        {
            return View(db.PostModels.ToList());
        }

        // GET: PostModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel postModel = db.PostModels.Find(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        // POST: PostModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public IActionResult Create([Microsoft.AspNetCore.Mvc.Bind(Include = "PostId,Title,PostTime,PostContent")] PostModels postModel)
        {
            if (postModel == null)
            {
                throw new ArgumentNullException(nameof(postModel));
            }

            if (ModelState.IsValid)
            {
                db.PostModels.Add(postModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postModel);
        }
        

        // GET: PostModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel postModel = db.PostModels.Find(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        // POST: PostModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,PostTime,CreatorId,PostContent,IsPublic")] PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postModel);
        }

        // GET: PostModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel postModel = db.PostModels.Find(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        // POST: PostModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostModel postModel = db.PostModels.Find(id);
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
        } */
    }
}
