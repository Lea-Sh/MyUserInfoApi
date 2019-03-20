using Microsoft.EntityFrameworkCore;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Data
{
    public class UserContext : DbContext
    {
//        public UserContext(DbContextOptions<UserContext> options)
//            : base(options)
//        {
//        }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UserInfo;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
