using Microsoft.EntityFrameworkCore;
using SocialBet.Models;

namespace SocialBet.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Bet> Bets { get; set; }
    }
}
