using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBet.Helpers;
using SocialBet.Services;

namespace SocialBet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExtrasController : ControllerBase
    {
        private IExtraService _extraService;

        public ExtrasController(IExtraService extraService)
        {
            _extraService = extraService;
        }

        [HttpGet()]
        [Route("states")]
        public IActionResult GetStates()
        {
            try
            {
                var states = _extraService.GetStates();
                return Ok(new ResultData() 
                { 
                    data = states,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.States, message = ex.Message }
                });
            }
        }

        [HttpGet()]
        [Route("betcategories")]
        public IActionResult GetBetCategories()
        {
            try
            {
                var categories = _extraService.GetBetCategories();
                return Ok(new ResultData() 
                { 
                    data = categories,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetCategories, message = ex.Message }
                });
            }
        }

        [HttpGet()]
        [Route("prizecategories")]
        public IActionResult GetPrizeCategories()
        {
            try
            {
                var categories = _extraService.GetPrizeCategories();
                return Ok(new ResultData()
                {
                    data = categories,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.PrizeCategories, message = ex.Message }
                });
            }
        }
    }
}
