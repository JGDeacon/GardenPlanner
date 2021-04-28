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

        //GetMyPlants method will return all the plants in the My Plant List.
        //This method uses GetPlantModel and populates the information from MyPlants class
        public IEnumerable<GetMyPlantModel> GetMyPlants() 
        {
            var query = ctx.MyPlants.Where(e => e.UserID == _userID).Select(e => new GetMyPlantModel
            {
                MyPlantID = e.MyPlantID,
                Location = e.Location,
                PlantID = e.PlantID,
                PlantName = ctx.Plants.FirstOrDefault(f => f.PlantID == e.PlantID).Name,
                DatePlanted = e.DatePlanted,
                Notes = e.Notes,
                Year = e.Year,
                Photo = e.Photo
            }
            );
            return query.ToArray();
        }

        //AddMyPlantMethod allows posting new plant to my plant based of AddMyPlantModel
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

        //AddNote allows user to add a note to a plant on MyPlant list. It takes MyPlantID and adds note to plant that matches with MyPlantID.
        public bool AddNote(AddNotesToMyPlant model)
        {
            MyPlants myPlants = ctx.MyPlants.Single(e => e.MyPlantID == model.MyPlantID);
            if (myPlants.UserID != _userID)
            {
                return false;
            }
            myPlants.Notes = model.Notes;
            return ctx.SaveChanges() == 1;
        }

        //DeleteMyPlant method takes in the myPlantID and deletes the plant that matches with that ID from MyPlant list. 
        public bool DeleteMyPlant(int myPlantID)
        {
            MyPlants myPlants = ctx.MyPlants.Single(e => e.MyPlantID == myPlantID);
            if (myPlants.UserID != _userID)
            {
                return false;
            }
            ctx.MyPlants.Remove(myPlants);
            return ctx.SaveChanges() == 1;
        }

        //UpdateMyPlant method takes in myPlantID of the plant you would like to update and new infomation that needs to be updated.
        //Populates the new updated information using UpdateMyPlantModel
        public bool UpdateMyPlant(UpdateMyPlantModel model)
        {
            MyPlants myPlants = ctx.MyPlants.Single(e => e.MyPlantID == model.MyPlantID);
            if (myPlants.UserID != _userID)
            {
                return false;
            }
            myPlants.Location = model.Location;
            myPlants.DatePlanted = model.DatePlanted;
            myPlants.Photo = model.Photo;
            myPlants.Notes = model.Notes;
            myPlants.Year = model.Year;
            myPlants.ModifiedDate = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 1;
        }
    }
}
