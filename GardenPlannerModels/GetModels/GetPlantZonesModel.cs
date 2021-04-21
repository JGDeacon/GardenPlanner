using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class GetPlantZonesModel
    {
        public int ZoneID { get; set; }
   
        public string ZoneCode { get; set; }
        
        public string Description { get; set; }
        
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? Modifiedate { get; set; }
    }
}
