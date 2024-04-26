using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Services.SuperHeroService;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var result = await _superHeroService.GetAllHeroes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var result = await _superHeroService.GetSingleHero(id);

            if (result is null)
            {
                return NotFound("This Hero Doesn't Exists!");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddSuperHero(SuperHero hero)
        {
            var result = await _superHeroService.AddSuperHero(hero);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(int id, SuperHero request)
        {
            var result = await _superHeroService.UpdateSuperHero(id, request);

            if (result is null)
            {
                return NotFound("This Hero Doesn't Exists!");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var result = await _superHeroService.DeleteSuperHero(id);
            if (result is null)
            {
                return NotFound("This Hero Doesn't Exists!");
            }

            return Ok(result);
        }

    }
}
