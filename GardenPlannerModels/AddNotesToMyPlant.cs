using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class AddNotesToMyPlant
    {
        public int MyPlantID { get; set; }

        [MaxLength(3000)]
        public string Notes { get; set; }
    }
}
