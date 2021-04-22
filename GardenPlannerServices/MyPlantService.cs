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
        public IEnumerable<AddMyPlantModel> GetMyPlants()
        {
            var query = ctx.MyPlants.Where(e => e.UserID == _userID).Select(e => new AddMyPlantModel
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
        public bool AddMyPlant(AddMyPlantModel model)
        {
            MyPlants myPlants = new MyPlants
            {
                UserID = _userID,
                Location = model.Location,
                PlantID = model.PlantID,
                DatePlanted = model.DatePlanted,
                Photo = model.Photo,
                Notes = model.Notes,
                Year = model.Year,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.MyPlants.Add(myPlants);
            return ctx.SaveChanges() == 1;
        }
        public bool AddNote(AddNotesToMyPlant model)
        {
            MyPlants myPlants = ctx.MyPlants.Single(e => e.MyPlantID == model.MyPlantID);
            myPlants.Notes = model.Notes;
            return ctx.SaveChanges() == 1;
        }
        public bool DeleteMyPlant(int myPlantID)
        {
            MyPlants myPlants = ctx.MyPlants.Single(e => e.MyPlantID == myPlantID);
            ctx.MyPlants.Remove(myPlants);
            return ctx.SaveChanges() == 1;
        }
        public bool UpdateMyPlant(UpdateMyPlantModel model)
        {
            MyPlants myPlants = ctx.MyPlants.Single(e => e.MyPlantID == model.MyPlantID);
            myPlants.Location = model.Location;
            myPlants.PlantID = model.MyPlantID;
            myPlants.DatePlanted = model.DatePlanted;
            myPlants.Photo = model.Photo;
            myPlants.Notes = model.Notes;
            myPlants.Year = model.Year;
            myPlants.ModifiedDate = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 1;
        }
    }
}
