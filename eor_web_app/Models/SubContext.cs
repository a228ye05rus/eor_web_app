using Microsoft.EntityFrameworkCore;

namespace eor_web_app.Models
{
    public class SubContext : DbContext
    {
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<FileModel> FileModels { get; set; }

        public SubContext(DbContextOptions<SubContext> options)
            : base(options) 
        { 
            //Database.EnsureDeleted(); 
            Database.EnsureCreated(); 
        }

    }
}
