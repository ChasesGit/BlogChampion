using BlogChampion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogChampion.Data
{
    public class AppDbContext: IdentityDbContext
    {
        //Connects us to our database in our default connection (the default connection can be found in the appsettings.json) Inherits the IdentityDbContext for the use in making Roles and Users.
        //This is a constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
        //This is our post object for the database set that looks inside the Post database
        public DbSet<Post> Posts { get; set; }
    }
}
