using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi.ViewModels
{
    public class ReadAthleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class WriteAthleteViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


}
