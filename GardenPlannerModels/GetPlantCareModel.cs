using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class GetPlantCareModel
    {
        public int PlantCareID { get; set; }
        
        public int SunExposureID { get; set; }
      
        public int WaterNeedID { get; set; }
     
        public string Temperature { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
