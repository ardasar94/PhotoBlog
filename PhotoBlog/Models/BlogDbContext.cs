using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoBlog.Models
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(): base("name=ConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<BlogPost>()
            //            .HasMany<HashTag>(s => s.Tags)
            //            .WithMany(c => c.Posts)
            //            .Map(cs =>
            //            {
            //                cs.MapLeftKey("PostRefId");
            //                cs.MapRightKey("TagRefId");
            //                cs.ToTable("PostTag");
            //            });

        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
    }
}