using GameMatcher.EntityFramework.Mappings;
using GameMatcher.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Repositories
{
    public class GameMatcherDataContext: DbContext
    {
        public GameMatcherDataContext(): 
            base("Data Source=localhost;Initial Catalog=GAME_MATCHER_DEV;Integrated Security=True")
        { }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<MatchRequest> MatchRequests { get; set; }
        public DbSet<MatchConfirmed> ConfirmedMatches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClubMapping());
            modelBuilder.Configurations.Add(new PlayerMapping());
            modelBuilder.Configurations.Add(new MatchRequestMapping());
            modelBuilder.Configurations.Add(new MatchConfirmedMapping());
        }
    }
}
