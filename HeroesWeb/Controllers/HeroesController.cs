using HeroesWeb.Models;
using HeroesWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroItem>>> GetHeros(string name = null)
        {
            var ret = await _heroService.GetHeros(name);

            return ret.OrderBy(x => x.EmpNo).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HeroItem>> GetHero(string id)
        {
            return await _heroService.GetHero(id);
        }

        [HttpPost]
        public async Task<ActionResult<HeroItem>> Post([FromBody] HeroItem item)
        {
            item = await _heroService.Create(item);

            if (default(HeroItem) == item)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<HeroItem>> Put([FromBody] HeroItem item)
        {
            item = await _heroService.Update(item);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _heroService.Delete(id);

            return Ok();
        }
    }

}
