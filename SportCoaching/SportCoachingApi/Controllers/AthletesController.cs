using Microsoft.AspNetCore.Mvc;
using SportCoachingApi.Context;
using SportCoachingApi.Entities;
using SportCoachingApi.ViewModels;
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

        public AthletesController(SportsCoatchingContext context) => _context = context;


        [HttpGet]
        public ActionResult<IEnumerable<ReadAthleteViewModel>> GetAthlets()
        {
            List<ReadAthleteViewModel> athletesViewModels = new List<ReadAthleteViewModel>();


            var athlete = _context.Athletes.OrderBy(a => a.Name).ToList();

            foreach (Athlete athlt in athlete)
            {
                ReadAthleteViewModel readAthlete = new ReadAthleteViewModel();
                readAthlete.Id = athlt.Id;
                readAthlete.Name = athlt.Name;
                readAthlete.Age = athlt.Age;

                athletesViewModels.Add(readAthlete);
            }
            return  Ok(athletesViewModels);
        }


        [HttpGet("{Id}")]
        public ActionResult<ReadAthleteViewModel> GetAthleteById(int id)
        {
            ReadAthleteViewModel readAthlete = new ReadAthleteViewModel();

            var athletToReturn = _context.Athletes
                .Where(a => a.Id == id)
                .FirstOrDefault();


            if (athletToReturn == null) return NotFound();

            readAthlete.Id = readAthlete.Id;
            readAthlete.Name = readAthlete.Name;
            readAthlete.Age = readAthlete.Age;

             return Ok(readAthlete);
        }
    }
}
