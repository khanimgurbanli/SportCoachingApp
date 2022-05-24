using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SportCoachingApi.Context;
using SportCoachingApi.Entities;
using SportCoachingApi.Models;
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
        public ActionResult<IEnumerable<Goal>> GetGoalByAthleteId(int athleteId)
        {
            var athlete = _context.Athletes.Find(athleteId);
            if (athlete == null) return NotFound();

            var goals = _context.Goals
                .Where(g => g.Athlete.Id == athleteId)
                .ToList();

            return Ok(goals);
        }

        [HttpGet("{id}", Name = "GetGoal")]
        public ActionResult<Goal> GetGoalByAthleteIdAndGoalId(int athleteId, int id)
        {
            var athlete = _context.Athletes.Find(athleteId);
            if (athlete == null) return NotFound();

            var goal = _context.Goals
                .Where(g => g.Athlete.Id == athleteId && g.Id==id)
                .ToList();

            if (goal == null) return NotFound();

            return Ok(goal);
        }


        [HttpPost]
        public IActionResult CreateGoal(int athleteId, GoalForCreationDTO goal)
        {

            if (goal.Description == goal.Name) ModelState.AddModelError("Description", "Description would be different from Name");

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var athlete = AthletesDataStore.Current.Athletes.FirstOrDefault(a => a.Id == athleteId);
            if (athlete == null) return NotFound();

            var maxGoalId = AthletesDataStore.Current.Athletes.SelectMany(a => a.Goals).Max(g => g.Id);

            var finalGoal = new GoalsDTO()
            {
                Id = ++maxGoalId,
                Name = goal.Name,
                Description = goal.Description
            };

            athlete.Goals.Add(finalGoal);

            return CreatedAtRoute("GetGoal", new { athleteId, id = finalGoal.Id });
        }


        [HttpPut("{id}")]
        public IActionResult UpdateGoal(int athleteId, int id, GoalForUpdateDTO goal)
        {
            if (goal.Description == goal.Name) ModelState.AddModelError("Description", "Description would be different from Name");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var athlete = AthletesDataStore.Current.Athletes.FirstOrDefault(a => a.Id == athleteId);
            if (athlete == null) return NotFound();

            var goalFromDataStore = athlete.Goals.FirstOrDefault(g => g.Id == id);
            if (goal == null) return NotFound();

            goalFromDataStore.Name = goal.Name;
            goalFromDataStore.Description = goal.Description;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchGoal(int athleteId, int id, JsonPatchDocument<GoalForUpdateDTO> jsonPatch)
        {
            var athlete = AthletesDataStore.Current.Athletes.FirstOrDefault(a => a.Id == athleteId);
            if (athlete == null) return NotFound();

            var goalFromDataStore = athlete.Goals.FirstOrDefault(g => g.Id == id);
            if (goalFromDataStore == null) return NotFound();

            var goalToPatch = new GoalForUpdateDTO()
            {
                Name = goalFromDataStore.Name,
                Description = goalFromDataStore.Description
            };

            jsonPatch.ApplyTo(goalToPatch, ModelState);

            //------> Validation <-------
            if (!ModelState.IsValid) return BadRequest();

            //Check the same value
            if (goalToPatch.Description == goalToPatch.Name) ModelState.AddModelError("Description", "Description would be different from Name");

            if (!TryValidateModel(goalToPatch)) return BadRequest(ModelState);

            goalFromDataStore.Name = goalToPatch.Name;
            goalFromDataStore.Description = goalToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGoal(int athleteId, int id)
        {
            var athlete = AthletesDataStore.Current.Athletes.FirstOrDefault(a => a.Id == athleteId);
            if (athlete == null) return NotFound();

            var goalFromDataStore = athlete.Goals.FirstOrDefault(g => g.Id == id);
            if (goalFromDataStore == null) return NotFound();

            athlete.Goals.Remove(goalFromDataStore);
            return NoContent();
        }
    }
}
