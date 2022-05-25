using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi.ViewModels
{
    public class ReadGoalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AthleteId { get; set; }
    }

    public class WriteGoalViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AthleteId { get; set; }
    }
}
