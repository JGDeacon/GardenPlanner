using GardenPlannerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerServices
{
    public class PlantService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public IEnumerable<PlantDetails> GetPlantByType(int plantTypeID)
        {
            var query = ctx.Plants.Where(e => e.PlantTypeID == plantTypeID).Select(f => new PlantDetails
            {
                Name = e.Name,


            }
            );
        }
    }
}
