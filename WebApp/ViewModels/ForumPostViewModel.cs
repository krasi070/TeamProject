using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ForumPostViewModel
    {
        private ApplicationDbContext applicationDbContext;

        public ForumPostViewModel()
        {
            this.applicationDbContext = new ApplicationDbContext();
        }

        public ForumPost ForumPost { get; set; }

        public ApplicationUser GetUser()
        {
            return this.applicationDbContext.Users
                .FirstOrDefault(u => u.Id == this.ForumPost.AuthorId);
        }

        public ICollection<CommentViewModel> GetCommentsAsViewModel()
        {
            return this.ForumPost.Comments
                .Select(c => new CommentViewModel() {Comment = c})
                .ToList();
        }
    }
}