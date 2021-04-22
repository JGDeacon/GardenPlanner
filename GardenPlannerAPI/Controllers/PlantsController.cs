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
            //var userID = Guid.Parse(User.Identity.GetUserId());
            var plantService = new PlantService();
            return plantService;
        }

        ////GET all plants
        //public IHttpActionResult Get()
        //{
        //    PlantService plantService = CreatePlantService();
        //    var plants = plantService.GetAllPlants();
        //    return Ok(plants);
        //}
    }
}
