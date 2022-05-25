using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportCoachingApi.Context;
using SportCoachingApi.Entities;
using SportCoachingApi.Models;
using SportCoachingApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi.Controllers
{
    [ApiController]
    [Route("api/athletes/{athleteId}/goals")]
    public class GoalsController : ControllerBase
    {
        private readonly SportsCoatchingContext _context;

        public GoalsController(SportsCoatchingContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ReadGoalViewModel>> GetGoalByAthleteId(int athleteId)
        {
            List<ReadGoalViewModel> readGoals = new List<ReadGoalViewModel>();
           
            var athlete = _context.Athletes.Find(athleteId);
            if (athlete == null) return NotFound();

            var goals = _context.Goals
                .Where(g => g.Athlete.Id == athleteId)
                .ToList();

            foreach (Goal goal in goals)
            {
                ReadGoalViewModel readGoal = new ReadGoalViewModel();

                readGoal.Id = goal.Id;
                readGoal.Name = goal.Name;
                readGoal.Description = goal.Description;
                readGoal.AthleteId = goal.Athlete.Id;

                readGoals.Add(readGoal);
            }        

            return Ok(readGoals);
        }

        [HttpGet("{id}", Name = "GetGoal")]
        public ActionResult<ReadGoalViewModel> GetGoalByAthleteIdAndGoalId(int athleteId, int id)
        {
            ReadGoalViewModel readGoal = new ReadGoalViewModel();

            var athlete = _context.Athletes.Find(athleteId);

            if (athlete == null) return NotFound();

            var goal = _context.Goals
                .Where(g => g.Athlete.Id == athleteId && g.Id==id)
                .FirstOrDefault();

            if (goal == null) return NotFound();

            readGoal.Id = goal.Id;
            readGoal.Name = goal.Name;
            readGoal.Description = goal.Description;
            readGoal.AthleteId = goal.Athlete.Id;

            return Ok(readGoal);
        }


        [HttpPost]
        public IActionResult CreateGoal(int athleteId, GoalForCreationDTO goal)
        {
            Goal finalGoal = new Goal();

            if (goal.Description == goal.Name) ModelState.AddModelError("Description", "Description would be different from Name");

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var athlete = _context.Athletes.Find(athleteId);

            if (athlete == null) return NotFound();

            var maxGoalId = AthletesDataStore.Current.Athletes.SelectMany(a => a.Goals).Max(g => g.Id);

            //var finalGoal = new GoalsDTO()
            //{
            //    Id = ++maxGoalId,
            //    Name = goal.Name,
            //    Description = goal.Description
            //};

            finalGoal.Athlete = athlete;
            finalGoal.Name = goal.Name;
            finalGoal.Description = goal.Description;

            _context.Goals.Add(finalGoal);
            _context.SaveChanges();

            return CreatedAtRoute("GetGoal", new { athleteId, id = finalGoal.Id },finalGoal);
        }


        [HttpPut("{id}")]
        public ActionResult<WriteGoalViewModel> UpdateGoal(int athleteId, int id, GoalForUpdateDTO goal)
        {
            if (goal.Description == goal.Name) ModelState.AddModelError("Description", "Description would be different from Name");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var athlete = _context.Athletes.Find(athleteId);
            if (athlete == null) return NotFound();

            var goalFromDataBase= _context.Goals.FirstOrDefault(g => g.Id == id);
            if (goal == null) return NotFound();

            goalFromDataBase.Name = goal.Name;
            goalFromDataBase.Description = goal.Description;

            _context.Entry(goalFromDataBase).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult<WriteGoalViewModel> PatchGoal(int athleteId, int id, JsonPatchDocument<WriteGoalViewModel> jsonPatch)
        {
            var athlete = _context.Athletes.Find(athleteId);
            if (athlete == null) return NotFound();

            var goalFromDataBase = _context.Goals.FirstOrDefault(g => g.Id == id);
            if (goalFromDataBase == null) return NotFound();

            var goalToPatch = new WriteGoalViewModel()
            {
                Name = goalFromDataBase.Name,
                Description = goalFromDataBase.Description
            };

            jsonPatch.ApplyTo(goalToPatch, ModelState);

            //------> Validation <-------
            if (!ModelState.IsValid) return BadRequest();

            //Check the same value
            if (goalToPatch.Description == goalToPatch.Name) ModelState.AddModelError("Description", "Description would be different from Name");

            if (!TryValidateModel(goalToPatch)) return BadRequest(ModelState);

            goalFromDataBase.Name = goalToPatch.Name;
            goalFromDataBase.Description = goalToPatch.Description;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<WriteGoalViewModel> DeleteGoal(int athleteId, int id)
        {
            var athlete = _context.Athletes.Find(athleteId);

            if (athlete == null) return NotFound();

            var goalFromDataStore = _context.Goals.FirstOrDefault(g => g.Id == id);
            if (goalFromDataStore == null) return NotFound();

            _context.Goals.Remove(goalFromDataStore);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
