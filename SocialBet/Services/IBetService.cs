using System;
using System.Collections.Generic;
using SocialBet.Helpers;
using SocialBet.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialBet.Services
{
    public interface IBetService
    {
        IEnumerable<Bet> GetAll();
        IEnumerable<Bet> GetForUser(int userId);
        IEnumerable<Bet> GetForReferree(int userId);
        Bet GetById(int id);
        Bet Create(Bet bet);
        Bet Update(Bet bet);
        void Delete(int id);
    }

    public class BetService : IBetService
    {
        private DataContext _context;

        public BetService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Bet> GetAll()
        {
            return _context.Bets;
        }

        public IEnumerable<Bet> GetForUser(int userId)
        {
            return _context.Bets.FromSql("select * from [dbo].[Bets] where CreatorId={0} or RivalId={0}", userId);
        }

        public IEnumerable<Bet> GetForReferree(int userId)
        {
            return _context.Bets.FromSql("select * from [dbo].[Bets] where ReferreeId={0}", userId);
        }

        public Bet GetById(int id)
        {
            return _context.Bets.Find(id);
        }

        public Bet Create(Bet bet)
        {
            try
            {
                _context.Bets.Add(bet);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }

            return bet;
        }

        public Bet Update(Bet betParam)
        {
            var bet = _context.Bets.Find(betParam.Id);

            if (bet == null)
                throw new AppException("Bet not found");

            try
            {
                // update bet properties, check what can be changed later
                bet.BetCategoryId = betParam.BetCategoryId;
                bet.BetDescription = betParam.BetDescription;
                bet.CreatorId = betParam.CreatorId;
                bet.Description = betParam.Description;
                bet.EndDate = betParam.EndDate;
                bet.PrizeCategoryId = betParam.PrizeCategoryId;
                bet.PrizeDescription = betParam.PrizeDescription;
                bet.ReferreeId = betParam.ReferreeId;
                bet.RivalId = betParam.RivalId;
                bet.StartDate = betParam.StartDate;
                bet.State = betParam.State;

                _context.Bets.Update(bet);
                _context.SaveChanges();

                return bet;
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var bet = _context.Bets.Find(id);
            if (bet != null)
            {
                try
                {
                    _context.Bets.Remove(bet);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new AppException(ex.Message);
                }
            }
        }
    }
}
