using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class UpdatePlantModel
    {
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public int ZoneID { get; set; }
        public int SeasonID { get; set; }
        public int PlantTypeID { get; set; }
        public int PlantCareID { get; set; }
        public int SunExposureID { get; set; }
        public int WaterNeedID { get; set; }
        public string Temperature { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }
        public int PlantDetailsID { get; set; }
    }
}
