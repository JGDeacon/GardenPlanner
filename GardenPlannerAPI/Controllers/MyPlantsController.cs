using GardenPlannerData;
using GardenPlannerModels;
using GardenPlannerServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GardenPlannerAPI.Controllers
{
    [Authorize]
    public class MyPlantsController : ApiController
    {
        public IHttpActionResult Get()
        {
            MyPlantService myPlantService = CreateMyPlantService();
            var myPlants = myPlantService.GetMyPlants();
            return Ok(myPlants);
        }

        public IHttpActionResult Post(AddMyPlantModel newPlant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateMyPlantService();

            if (!service.AddMyPlant(newPlant))
                return InternalServerError();

            return Ok("New plant has been added to the MyPlants");
        }

        [Route("api/Note")]

        public IHttpActionResult AddNote(AddNotesToMyPlant addNotes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            var service = CreateMyPlantService();

            if (!service.AddNote(addNotes))
                return InternalServerError();
            return Ok();


        }

        public IHttpActionResult Put(UpdateMyPlantModel plant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMyPlantService();

            if (!service.UpdateMyPlant(plant))
                return InternalServerError();

            return Ok();
            
        }

        public IHttpActionResult Delete(int myPlantID)
        {
            var service = CreateMyPlantService();

            if (!service.DeleteMyPlant(myPlantID))
                return InternalServerError();

            return Ok($"Plant with id {myPlantID} has been deleted from MyPlants");
        }

        private MyPlantService CreateMyPlantService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var myPlantService  = new MyPlantService(userID);
            return myPlantService;
        }
    }
}
