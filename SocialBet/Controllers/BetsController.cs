using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialBet.Helpers;
using SocialBet.Models;
using SocialBet.Services;

namespace SocialBet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private IBetService _betService;

        public BetsController(IBetService betService)
        {
            _betService = betService;
        }

        /// <summary>
        /// Gets the list of bets
        /// </summary>
        /// <returns>The bets</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var bets = _betService.GetAll();
                return Ok(new ResultData() { data = bets, error = null });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a specific bet
        /// </summary>
        /// <param name="id">Identifier for the bet</param>
        /// <returns>The requested bet</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var bet = _betService.GetById(id);
                return Ok(new ResultData() { data = bet, error = null });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new bet
        /// </summary>
        /// <param name="bet">The new bet</param>
        /// <returns>The saved bet</returns>
        [HttpPost]
        [Route("create")]
        public IActionResult Post([FromBody] Bet bet)
        {
            try
            {
                var newBet = _betService.Create(bet);
                return Ok(new ResultData(){ data = newBet, error = null});
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Bet bet)
        {
            bet.Id = id;

            try
            {
                // save 
                var updated = _betService.Update(bet);
                return Ok(new ResultData() { data = updated, error = null });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a bet
        /// </summary>
        /// <param name="id">Identifier of the bet to be deleted</param>
        /// <returns>True if the bet was deleted</returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _betService.Delete(id);
                return Ok(new ResultData() { data = "Bet deleted successfully", error = null });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }
    }
}
