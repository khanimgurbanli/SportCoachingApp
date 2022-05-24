using Microsoft.AspNetCore.Mvc;
using SportCoachingApi.Context;
using SportCoachingApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi.Controllers
{
    [ApiController]
    [Route("api/athletes")]
    public class AthletesController : ControllerBase
    {
        private readonly SportsCoatchingContext _context;

        public AthletesController(SportsCoatchingContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Athlete>> GetAthlets()
        {
            return  Ok(_context.Athletes.OrderBy(a=>a.Name).ToList());
        }


        [HttpGet("{Id}")]
        public ActionResult<Athlete> GetAthleteById(int id)
        {
            var athletToReturn = _context.Athletes
                .Where(a => a.Id == id)
                .FirstOrDefault();

            if (athletToReturn == null) return NotFound();
             return Ok(athletToReturn);
        }
    }
}
