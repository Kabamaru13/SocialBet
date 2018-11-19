using System;
using System.Collections.Generic;
using SocialBet.Helpers;
using SocialBet.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialBet.Services
{
    public interface IExtraService
    {
        IEnumerable<State> GetStates();
        State GetState(int id);
        IEnumerable<BetCategory> GetBetCategories();
        BetCategory GetBetCategory(int id);
        IEnumerable<PrizeCategory> GetPrizeCategories();
        PrizeCategory GetPrizeCategory(int id);
    }

    public class ExtraService : IExtraService
    {
        private DataContext _context;

        public ExtraService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<State> GetStates()
        {
            return _context.States;
        }

        public State GetState(int id)
        {
            return _context.States.Find(id);
        }

        public IEnumerable<BetCategory> GetBetCategories()
        {
            return _context.BetCategories;
        }

        public BetCategory GetBetCategory(int id)
        {
            return _context.BetCategories.Find(id);
        }

        public IEnumerable<PrizeCategory> GetPrizeCategories()
        {
            return _context.PrizeCategories;
        }

        public PrizeCategory GetPrizeCategory(int id)
        {
            return _context.PrizeCategories.Find(id);
        }
    }
}
