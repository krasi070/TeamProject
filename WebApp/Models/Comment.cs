namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(128)]
        public string AuthorId { get; set; }

        public int ForumPostId { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
