using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi.Models
{
    public class AthleteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int NumberOfGoals { get { return Goals.Count(); } }
        public ICollection<GoalsDTO> Goals { get; set; } = new List<GoalsDTO>();
    }
}
