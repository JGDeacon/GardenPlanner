using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class MyPlantsModel
    {
        [MaxLength(500)]
        public string Location { get; set; }
        public string PlantName { get; set; }
        public DateTimeOffset DatePlanted { get; set; }

        [MaxLength(3000)]
        public string Notes { get; set; }
        public int Year { get; set; }
        public string Photo { get; set; }
    }
}
