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
    public class PlantsController : ApiController
    {
        //will create a PlantService
        private PlantService CreatePlantService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var plantService = new PlantService(userID);
            return plantService;
        }

        //POST
        [Route("api/MakePlant")]
        public IHttpActionResult Post(AddPlantModel plant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlantService();

            if (!service.AddPlant(plant))
                return InternalServerError();

            return Ok("New plant has been created");
        }
        //PUT
        public IHttpActionResult Put(int plantID, AddPlantModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlantService();

            if (!service.UpdatePlant(plantID, model))
                return InternalServerError();

            return Ok("Plant has been updated");
        }

        //All the GETS
        [Route("api/Plants")]
        public IHttpActionResult Get() //missing
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetAllPlants();
            return Ok(plant);
        }
        [Route("api/Plants")]
        public IHttpActionResult GetType(int plantTypeID)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantByType(plantTypeID);
            return Ok(plant);
        }
        [Route("api/Plants/Width")]
        public IHttpActionResult GetWidth(double min, double max)//poor name
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantsByWidth(min, max);
            return Ok(plants);
        }
        [Route("api/Plants/Sun")]
        public IHttpActionResult GetSunExposure(int sunExposureID)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantsBySunExposure(sunExposureID);
            return Ok(plants);
        }
        [Route("api/Plants/Water")]
        public IHttpActionResult GetWaterNeed(int waterNeedID)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantsByWaterNeed(waterNeedID);
            return Ok(plants);
        }
        [Route("api/Plants/Bloom")]
        public IHttpActionResult GetBloomSeason(int seasonID)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantsByBloomSeason(seasonID);
            return Ok(plants);
        }
        [Route("api/Plants/Height")]
        public IHttpActionResult GetHeightMax(double plantHeightMax)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantsByHeightMax(plantHeightMax);
            return Ok(plants);
        }
        [Route("api/Plants/Zone")]
        public IHttpActionResult GetPlantZone(int zoneID)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantByPlantZone(zoneID);
            return Ok(plants);
        }
        [Route("api/Plants/Feature")]
        public IHttpActionResult GetPerrenial() //missing
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPerrenialPlants();
            return Ok(plants);
        }
        [Route("api/Plants/Feature")]
        public IHttpActionResult GetZoneAndSeason(int zoneId,int seasonID)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantByZoneAndSeason(zoneId, seasonID);
            return Ok(plants);
        }
        [Route("api/Plants/Feature")]
        public IHttpActionResult GetDaysToGerminate(int daysToGerminate)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantByDaysToGerminate(daysToGerminate);
            return Ok(plants);
        }
    }
}
