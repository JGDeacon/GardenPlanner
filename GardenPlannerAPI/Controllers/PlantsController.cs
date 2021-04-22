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
        //this method will create a PlantService
        public PlantService CreatePlantService()
        {
            //var userID = Guid.Parse(User.Identity.GetUserId());
            var plantService = new PlantService();
            return plantService;
        }

        //GET By PlantTypeID
        public IHttpActionResult GetType(int plantTypeID)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantByType(plantTypeID);
            return Ok(plant);
        }

        //GET By Width
        public IHttpActionResult GetWidth(double min, double max)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantsByWidth(min, max);
            return Ok(plant);
        }

        //GET By SunExposure
        public IHttpActionResult GetSun(int sunExposureID)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantByType(sunExposureID);
            return Ok(plant);
        }

        //GET By WaterNeed
        public IHttpActionResult GetWaterNeed(int waterNeedID)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantsByWaterNeed(waterNeedID);
            return Ok(plant);
        }

        //GET By BloomSeason
        public IHttpActionResult GetBloomSeason(int seasonId)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantsByBloomSeason(seasonId);
            return Ok(plant);
        }

        //GET By HeightMax
        public IHttpActionResult GetHeightMax(double plantHeightMax)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantsByHeightMax(plantHeightMax);
            return Ok(plant);
        }

        //GET By PlantZoneID
        public IHttpActionResult GetPlantZone(int zoneID)
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPlantByPlantZone(zoneID);
            return Ok(plant);
        }

        //GET Perrenials
        public IHttpActionResult GetPerrenials()
        {
            PlantService plantService = CreatePlantService();
            var plant = plantService.GetPerrenialPlants();
            return Ok(plant);
        }

        //GET By PlantZone and Season
        //public IHttpActionResult GetPlantZoneAndSeason(int zoneId, int seasonID)
        //{
        //    PlantService plantService = CreatePlantService();

        //}

    }
}
