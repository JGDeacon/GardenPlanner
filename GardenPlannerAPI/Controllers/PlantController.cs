using GardenPlannerModels;
using GardenPlannerModels.GetModels;
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
        private PlantService CreatePlantService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var plantService = new PlantService(userID);            
            return plantService;
        }

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
        [Route("api/UpdatePlant")]

        public IHttpActionResult Put(int plantID, UpdatePlantModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlantService();

            if (!service.UpdatePlant(plantID, model))
                return InternalServerError();

            return Ok("Plant has been updated");
        }

        [Route("api/Plants")]
        public IHttpActionResult Get() 
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
        public IHttpActionResult GetWidth(double min, double max)
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
        public IHttpActionResult GetBloomSeason(GetPlantBySeasonIDModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantsByBloomSeason(model.SeasonID);
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
        public IHttpActionResult GetPlantZone(GetPlantByZoneIDModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantByPlantZone(model.ZoneID);
            return Ok(plants);
        }
        [Route("api/Plants/Feature")]
        public IHttpActionResult GetPerrenial(bool selection) 
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPerrenialPlants(selection);
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
        public IHttpActionResult GetDaysToGerminate(int minDays, int maxDays)
        {
            PlantService plantService = CreatePlantService();
            var plants = plantService.GetPlantByDaysToGerminate(minDays,maxDays);
            return Ok(plants);
        }
        [Route("api/PlantSeasons")]
        public IHttpActionResult GetSeasons()
        {
            PlantService plantService = CreatePlantService();
            var seasons = plantService.GetPlantSeasons();
            return Ok(seasons);
        }
        [Route("api/PlantTypes")]
        public IHttpActionResult GetTypes()
        {
            PlantService plantService = CreatePlantService();
            var types = plantService.GetPlantTypes();
            return Ok(types);
        }
        [Route("api/PlantZones")]
        public IHttpActionResult GetZones()
        {
            PlantService plantService = CreatePlantService();
            var zones = plantService.GetPlantZones();
            return Ok(zones);
        }
        [Route("api/RootStructure")]
        public IHttpActionResult GetRootStructures()
        {
            PlantService plantService = CreatePlantService();
            var rootStructures = plantService.GetRootStructure();
            return Ok(rootStructures);
        }
        [Route("api/SunExposures")]
        public IHttpActionResult GetSunExposure()
        {
            PlantService plantService = CreatePlantService();
            var sunExposures = plantService.GetSunExposure();
            return Ok(sunExposures);
        }
        [Route("api/WaterNeeds")]
        public IHttpActionResult GetWaterNeeds()
        {
            PlantService plantService = CreatePlantService();
            var waterNeeds = plantService.GetWaterNeeds();
            return Ok(waterNeeds);
        }
     
        [Route("api/SpecialDetails")]
        public IHttpActionResult GetPlantByMedicianlResistanceAndToxicity(GetSpecialDetailsModel model)
        {
            PlantService plantService = CreatePlantService();
            var specialDetails = plantService.GetPlantByMedicinalResistanceAndToxicity(model);
            return Ok(specialDetails);
        }
    }
}
