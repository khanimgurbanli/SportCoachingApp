using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi.Models
{
    public class GoalForCreationDTO
    {
        [Required(ErrorMessage="Name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
