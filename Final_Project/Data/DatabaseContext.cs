using Final_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<FavoriteFood> FavoriteFoods { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
