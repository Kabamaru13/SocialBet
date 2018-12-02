using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ExtrasController : ControllerBase
    {
        private IExtraService _extraService;

        public ExtrasController(IExtraService extraService)
        {
            _extraService = extraService;
        }

        [HttpGet("~/states")]
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

        [HttpGet("~/states/{id}")]
        public IActionResult GetState(int id)
        {
            try
            {
                var state = _extraService.GetState(id);
                return Ok(new ResultData() 
                { 
                    data = state,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.State, message = ex.Message }
                });
            }
        }

        [HttpGet("~/betcategories")]
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

        [HttpGet("~/betcategories/{id}")]
        public IActionResult GetBetCategory(int id)
        {
            try
            {
                var category = _extraService.GetBetCategory(id);
                return Ok(new ResultData()
                {
                    data = category,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.BetCategory, message = ex.Message }
                });
            }
        }

        [HttpGet("~/prizecategories")]
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

        [HttpGet("~/prizecategories/{id}")]
        public IActionResult GetPrizeCategory(int id)
        {
            try
            {
                var category = _extraService.GetPrizeCategory(id);
                return Ok(new ResultData()
                {
                    data = category,
                    error = new Error() { errorCode = (int)ErrorCode.NoError, message = "" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultData()
                {
                    data = new { },
                    error = new Error() { errorCode = (int)ErrorCode.PrizeCategory, message = ex.Message }
                });
            }
        }
    }
}
