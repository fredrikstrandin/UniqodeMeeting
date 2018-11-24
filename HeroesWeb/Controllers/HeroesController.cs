using HeroesUtils.Attributes;
using HeroesWeb.Models;
using HeroesWeb.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroesService _heroService;

        public HeroesController(IHeroesService heroService)
        {
            _heroService = heroService;
        }

        /// <summary>
        /// List Heroes.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Heroes?name=alva
        ///
        /// </remarks>
        /// <param name="name">First part of name to serch for. This can be null</param>
        /// <returns>A list of heroes</returns>
        /// <response code="200">A list of heroes</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<HeroItem>>> GetHeros(string name = null)
        {            
            var ret = await _heroService.GetHerosAsync(name);
            
            return ret.OrderBy(x => x.EmpNo).ToList();
        }

        [HttpGet("{id}")]
        [ETag("HeroesEntity", "Version")]
        public async Task<ActionResult<HeroItem>> GetHero(string id)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject)?.Value;

            //Logga vem som tittat

            return await _heroService.GetHeroAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<HeroItem>> Post([FromBody] HeroItem item)
        {
            item = await _heroService.CreateAsync(item);

            if (default(HeroItem) == item)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<HeroItem>> Put([FromBody] HeroItem item)
        {
            item = await _heroService.UpdateAsync(item);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _heroService.DeleteAsync(id);

            return Ok();
        }
    }

}
