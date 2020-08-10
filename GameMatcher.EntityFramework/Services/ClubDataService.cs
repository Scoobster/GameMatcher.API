using GameMatcher.EntityFramework.Models;
using GameMatcher.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Services
{
    public class ClubDataService
    {
        private readonly GameMatcherDataContext dbContext;

        public ClubDataService()
        {
            dbContext = new GameMatcherDataContext();
        }

        public List<Club> GetClubs()
        {
            return dbContext.Clubs
                .AsNoTracking()
                .ToList();
        }
    }
}
