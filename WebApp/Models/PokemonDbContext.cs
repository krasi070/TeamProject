namespace WebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PokemonDbContext : DbContext
    {
        public PokemonDbContext()
            : base("name=PokemonDbContext")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ForumPost> ForumPosts { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Pokemon> Pokemons { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ForumPost>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.ForumPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumPost>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.ForumPosts)
                .Map(m => m.ToTable("ForumPosts_Tags").MapLeftKey("ForumPostId").MapRightKey("TagId"));
        }
    }
}
