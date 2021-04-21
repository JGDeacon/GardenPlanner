﻿using GardenPlannerData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class PlantDetailsModel
    {
        public string Name { get; set; }

        public string ScientificName { get; set; }

        public int DaysToGerminate { get; set; }

        public int DaysToHarvest { get; set; }

        public double SeedDepth { get; set; }

        public bool IsPerennial { get; set; }

        public double PlantHeightMax { get; set; }

        public double PlantWidthtMax { get; set; }

        public double SeedSpacing { get; set; }

        public double RowSpacing { get; set; }

        public bool IsDeerResistant { get; set; }

        public bool IsToxicToAnimal { get; set; }

        public bool IsToxicToHuman { get; set; }

        public bool IsMedicinal { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public PlantTypes PlantTypes { get; set; }

        public RootStructure RootStructure { get; set; }

        public PlantCare PlantCare { get; set; }

        public PlantSeasons PlantSeasons { get; set; }

        public SunExposures SunExposures { get; set; }

        public WaterNeeds WaterNeeds { get; set; }

        public PlantZones PlantZones { get; set; }

    }
}
