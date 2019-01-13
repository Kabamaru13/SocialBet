using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IUserService _userService;

        public BetsController(IBetService betService, IUserService userService)
        {
            _betService = betService;
            _userService = userService;
        }

        /// <summary>
        /// Gets the list of bets
        /// </summary>
        /// <returns>The bets</returns>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var bets = _betService.GetAll();
                return Ok(new ResultData() 
                { 
                    data = bets,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetGetAll, message = ex.Message }
                });
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
                return Ok(new ResultData() 
                { 
                    data = bet,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetGet, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Gets the list of bets for current user
        /// </summary>
        /// <returns>The bets</returns>
        [HttpGet]
        [Route("scope")]
        public IActionResult GetForUser(int state = 0)
        {
            try
            {
                string userId = User.Identity.Name;
                var bets = _betService.GetForUser(userId, state);
                return Ok(new ResultData() 
                { 
                    data = bets,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetScope, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Gets the list of bets for current referree
        /// </summary>
        /// <returns>The bets</returns>
        [HttpGet]
        [Route("referree")]
        public IActionResult GetForReferree(int state = 0)
        {
            try
            {
                string userId = User.Identity.Name;
                var bets = _betService.GetForReferree(userId, state);
                return Ok(new ResultData() 
                { 
                    data = bets,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetReferree, message = ex.Message }
                });
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
                if (!string.IsNullOrWhiteSpace(bet.RivalId) && !string.IsNullOrWhiteSpace(bet.ReferreeId))
                {
                    if (_userService.GetById(bet.RivalId) == null)
                    {
                        _userService.Create(new User() { Id = bet.RivalId, FirstName = "", LastName = "", Username = bet.RivalId }, bet.RivalId);
                    }

                    if (_userService.GetById(bet.ReferreeId) == null)
                    {
                        _userService.Create(new User() { Id = bet.ReferreeId, FirstName = "", LastName = "", Username = bet.ReferreeId }, bet.ReferreeId);
                    }
                }

                var newBet = _betService.Create(bet);
                _userService.UpdateStats(newBet.CreatorId, StatFunction.Creation);
                _userService.UpdateStats(newBet.RivalId, StatFunction.Creation);
                return Ok(new ResultData()
                {
                    data = newBet,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetCreate, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Update the specified bet.
        /// </summary>
        /// <returns>The updated bet</returns>
        /// <param name="id">Id of the bet</param>
        /// <param name="bet">The updated bet</param>
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Bet bet)
        {
            bet.Id = id;

            try
            {
                // save 
                var updated = _betService.Update(bet);
                return Ok(new ResultData() 
                { 
                    data = updated,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetUpdate, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Cancels the bet
        /// </summary>
        /// <returns>The bet</returns>
        /// <param name="id">Id of the bet</param>
        [HttpPut]
        [Route("Cancel")]
        public IActionResult CancelBet(int id)
        {
            var bet = _betService.GetById(id);
            string userId = User.Identity.Name;

            try
            {
                if (bet == null)
                    throw new AppException("Bet with id \"" + id + "\" not found.");

                if (!(bet.CreatorId == userId || bet.RivalId == userId))
                    throw new AppException("User \"" + userId + "\" cannot cancel the bet.");

                bet.State = 3;

                // save 
                var updated = _betService.Update(bet);
                _userService.UpdateStats(bet.CreatorId, StatFunction.Cancellation);
                _userService.UpdateStats(bet.RivalId, StatFunction.Cancellation);
                return Ok(new ResultData()
                {
                    data = updated,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetCancel, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Accepts the bet
        /// </summary>
        /// <returns>The bet</returns>
        /// <param name="id">Id of the bet</param>
        [HttpPut]
        [Route("Accept")]
        public IActionResult AcceptBet(int id)
        {
            var bet = _betService.GetById(id);
            string userId = User.Identity.Name;

            try
            {
                if (bet == null)
                    throw new AppException("Bet with id \"" + id + "\" not found.");

                if (bet.RivalId != userId)
                    throw new AppException("User \"" + userId + "\" cannot accept the bet.");

                bet.State = 2;

                // save 
                var updated = _betService.Update(bet);

                return Ok(new ResultData()
                {
                    data = updated,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetAccept, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Completes the bet
        /// </summary>
        /// <returns>The bet</returns>
        /// <param name="id">Id of the bet</param>
        /// <param name="winnerId">Winner id or empty for draw</param>
        [HttpPut]
        [Route("Complete")]
        public IActionResult CompleteBet(int id, string winnerId)
        {
            var bet = _betService.GetById(id);
            string userId = User.Identity.Name;
            var winner = _userService.GetById(winnerId);

            try
            {
                if (bet == null)
                    throw new AppException("Bet with id \"" + id + "\" not found.");

                if (bet.State != 2)
                    throw new AppException("Bet with id \"" + id + "\" cannot be completed.");

                if (bet.ReferreeId != userId)
                    throw new AppException("User \"" + userId + "\" cannot close the bet.");

                if (string.IsNullOrWhiteSpace(winnerId) || winnerId == "0") // draw
                {
                    bet.State = 4;

                    var updated = _betService.Update(bet);
                    _userService.UpdateStats(bet.CreatorId, StatFunction.Draw);
                    _userService.UpdateStats(bet.RivalId, StatFunction.Draw);
                    _userService.UpdateStats(bet.ReferreeId, StatFunction.Referee);

                    return Ok(new ResultData()
                    {
                        data = updated,
                        error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                    });
                }
                else // win
                {
                    if (winner == null)
                        throw new AppException("Winner with id \"" + winnerId + "\" not found.");

                    if (!(bet.CreatorId == winner.Id || bet.RivalId == winner.Id))
                        throw new AppException("Winner with id \"" + winner.Id + "\" is not a participant.");

                    bet.State = 4;
                    bet.WinnerId = winner.Id;

                    // save 
                    var updated = _betService.Update(bet);
                    if (bet.CreatorId == winner.Id)
                    {
                        _userService.UpdateStats(bet.CreatorId, StatFunction.Win);
                        _userService.UpdateStats(bet.RivalId, StatFunction.Loss);
                    }
                    else
                    {
                        _userService.UpdateStats(bet.CreatorId, StatFunction.Loss);
                        _userService.UpdateStats(bet.RivalId, StatFunction.Win);
                    }
                    _userService.UpdateStats(bet.ReferreeId, StatFunction.Referee);
                    _userService.UpdateStats(winner.Id, StatFunction.XP, 10);

                    return Ok(new ResultData()
                    {
                        data = updated,
                        error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                    });
                }
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetComplete, message = ex.Message }
                });
            }
        }

        /// <summary>
        /// Deletes a bet
        /// </summary>
        /// <param name="id">Identifier of the bet to be deleted</param>
        /// <returns>True if the bet was deleted</returns>
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _betService.Delete(id);
                return Ok(new ResultData() 
                { 
                    data = new { message = "Bet deleted successfully" },
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetDelete, message = ex.Message }
                });
            }
        }
    }
}
