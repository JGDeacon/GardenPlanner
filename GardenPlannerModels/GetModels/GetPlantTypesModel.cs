using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class GetPlantTypesModel
    {
        public int PlantTypeID { get; set; }
      
        public string Name { get; set; }
       
        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
