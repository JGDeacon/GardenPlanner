﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class UpdateMyPlantModel
    {
        public int MyPlantID { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }
        public int Year { get; set; }
        public DateTimeOffset DatePlanted { get; set; }



    }
}
