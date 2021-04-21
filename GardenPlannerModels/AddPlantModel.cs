using GardenPlannerData;
using GardenPlannerModels.PlantDetailsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class AddPlantModel
    {
        public string Name { get; set; }

        public string ScientificName { get; set; }
        public int ZoneID { get; set; }
        public int SeasonID { get; set; }
        public int PlantTypeID { get; set; }
        public int PlantCareID { get; set; }
        public int PlantDetailsID { get; set; }
    }
}
