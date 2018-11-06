using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialBet.Models;

namespace SocialBet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private async Task<IEnumerable<Bet>> GetBets()
        {
            var bets = new Bet[]{
                new Bet { Id = 1, Description = "Bet1", BetCategoryId = 1, BetDescription = "Pao - Osfp -> 1", PrizeCategoryId = 3, 
                    PrizeDescription = "2 pitogura", CreatorId = 1, RivalId = 2, ReferreeId = 3, EndDate = new DateTime(2018, 11, 9), State = 1},
                new Bet { Id = 2, Description = "Bet2", BetCategoryId = 4, BetDescription = "Speed competition", PrizeCategoryId = 1,
                    PrizeDescription = "15 eura", CreatorId = 1, RivalId = 2, ReferreeId = 3, EndDate = new DateTime(2018, 11, 1), State = 3},
                new Bet { Id = 3, Description = "Bet3", BetCategoryId = 3, BetDescription = "tha to riksw to maraki?", PrizeCategoryId = 5,
                    PrizeDescription = "1 week of service", CreatorId = 1, RivalId = 2, ReferreeId = 3, EndDate = new DateTime(2018, 11, 9), State = 2},
            };

            return bets;
        }

        /// <summary>
        /// Gets the list of bets
        /// </summary>
        /// <returns>The bets</returns>
        [HttpGet]
        public async Task<IEnumerable<Bet>> Get()
        {
            return await GetBets();
        }

        /// <summary>
        /// Gets a specific bet
        /// </summary>
        /// <param name="id">Identifier for the bet</param>
        /// <returns>The requested bet</returns>
        [HttpGet("{id}")]
        public async Task<Bet> Get(int id)
        {
            var bets = await GetBets();
            return bets.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Creates a new bet
        /// </summary>
        /// <param name="bet">The new bet</param>
        /// <returns>The saved bet</returns>
        [HttpPost]
        [Route("~/bets")]
        public void Post([FromBody] Bet bet)
        {

        }

        /// <summary>
        /// Deletes a bet
        /// </summary>
        /// <param name="id">Identifier of the bet to be deleted</param>
        /// <returns>True if the bet was deleted</returns>
        [HttpDelete]
        [Route("~/bets/{id}")]
        public void Delete(int id)
        {

        }
    }
}
