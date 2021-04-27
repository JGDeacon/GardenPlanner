using GardenPlannerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GardenPlannerAPI.Controllers
{
    public class SeedDataController : ApiController
    {
        public IHttpActionResult Post()
        {
            var service = new SeedService();
            service.SeedPlants();
            return Ok("Plants Seeded");
        }
    }
}
