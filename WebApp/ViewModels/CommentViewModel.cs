using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class CommentViewModel
    {
        private ApplicationDbContext applicationDbContext;

        public CommentViewModel()
        {
            this.applicationDbContext = new ApplicationDbContext();
        }

        public Comment Comment { get; set; }

        public ApplicationUser GetUser()
        {
            return this.applicationDbContext.Users
                .FirstOrDefault(u => u.Id == this.Comment.AuthorId);
        }
    }
}