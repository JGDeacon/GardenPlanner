using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class AddMyPlantModel
    {
        [MaxLength(500)]
        public string Location { get; set; }
        public int PlantID { get; set; }
        public DateTimeOffset DatePlanted { get; set; }

        [MaxLength(3000)]
        public string Notes { get; set; }
        public int Year { get; set; }
        public string Photo { get; set; }
    }
}
