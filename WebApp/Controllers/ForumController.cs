using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ForumController : Controller
    {
        private PokemonDbContext db = new PokemonDbContext();

        // GET: Forum
        public ActionResult Index(string tag = null)
        {
            if (string.IsNullOrEmpty(tag))
            {
                var recentPosts = db.ForumPosts
                .OrderByDescending(f => f.Date)
                .Take(10)
                .Select(f => new ForumPostViewModel { ForumPost = f })
                .ToList();

                return View(recentPosts);
            }

            Tag tagToSearchBy = db.Tags.FirstOrDefault(t => t.Name == tag);
            if (tagToSearchBy == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forumPosts = tagToSearchBy.ForumPosts
                .OrderByDescending(f => f.Date)
                .Select(f => new ForumPostViewModel() { ForumPost = f })
                .Take(10)
                .ToList();

            return View(forumPosts);
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ForumPost forumPost = db.ForumPosts.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }

            return View(new ForumPostViewModel() { ForumPost = forumPost});
        }

        // GET: Forum/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Body")] ForumPost forumPost, string tags)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            forumPost.AuthorId = applicationDbContext.Users
                .First(u => u.UserName == User.Identity.Name).Id;
            var forumPostTags = tags
                .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Distinct()
                .ToList();
            foreach (var tag in forumPostTags)
            {
                if (!db.Tags.Any(t => t.Name == tag))
                {
                    Tag currTag = new Tag() { Name = tag };
                    currTag.ForumPosts.Add(forumPost);
                    db.Tags.Add(currTag);
                    forumPost.Tags.Add(currTag);
                }
                else
                {
                    Tag currTag = db.Tags.FirstOrDefault(t => t.Name == tag);
                    currTag.ForumPosts.Add(forumPost);
                    forumPost.Tags.Add(currTag);
                }
            }


            if (ModelState.IsValid)
            {
                db.ForumPosts.Add(forumPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forumPost);
        }

        // GET: Forum/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPosts.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            return View(forumPost);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date,AuthorId")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = forumPost.Id});
            }
            return View(forumPost);
        }

        // GET: Forum/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPosts.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            return View(forumPost);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumPost forumPost = db.ForumPosts.Find(id);
            db.Comments.RemoveRange(forumPost.Comments);
            db.Tags.RemoveRange(forumPost.Tags);
            db.ForumPosts.Remove(forumPost);
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
