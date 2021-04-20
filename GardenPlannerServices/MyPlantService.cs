using GardenPlannerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerServices
{
    public class MyPlantService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public IEnumerable<MyPlant> GetMyPlants()
        {
            var query = ctx.MyPlants.Where(e => e.UserID == _userID).Select(e => new MyPlant
            {
                Location = e.Location,
                PlantName = ctx.Plants.Where(f => f.PlantID == e.PlantID).Name,
                DatePlanted = e.DatePlanted,
                Notes = e.Notes,
                Year = e.Year,
                Photo = e.Photo
            }
            );
        }
    }
}
