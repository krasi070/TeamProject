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
    }
}