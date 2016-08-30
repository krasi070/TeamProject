namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pokemon")]
    public partial class Pokemon
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string MainType { get; set; }

        [StringLength(30)]
        public string SecondaryType { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
