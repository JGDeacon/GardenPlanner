using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class PlantCare
    {
        public int SunExposureID { get; set; }
        public int WaterNeedID { get; set; }
        public string Temperature { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }
    }
}
