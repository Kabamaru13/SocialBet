using Microsoft.EntityFrameworkCore;
using SocialBet.Models;

namespace SocialBet.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<BetCategory> BetCategories { get; set; }

        public DbSet<PrizeCategory> PrizeCategories { get; set; }

        public DbSet<UserStat> UserStats { get; set; }
    }
}
