using GardenPlannerData;
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
        private readonly Guid _userID;
        public MyPlantService(Guid userID)
        {
            _userID = userID;
        }
        public IEnumerable<MyPlantsModel> GetMyPlants()
        {
            var query = ctx.MyPlants.Where(e => e.UserID == _userID).Select(e => new MyPlantsModel
            {
                Location = e.Location,
                PlantName = ctx.Plants.Single(f => f.PlantID == e.PlantID).Name,
                DatePlanted = e.DatePlanted,
                Notes = e.Notes,
                Year = e.Year,
                Photo = e.Photo
            }
            );
            return query.ToArray();
        }
    }
}
