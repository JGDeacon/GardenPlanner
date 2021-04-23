using GardenPlannerData;
using GardenPlannerModels.PlantDetailsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class AddPlantModel
    {
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public int DaysToGerminate { get; set; }

        public int DaysToHarvest { get; set; }

        public double SeedDepth { get; set; }

        public bool IsPerennial { get; set; }

        public double PlantHeightMax { get; set; }

        public double PlantWidthMax { get; set; }

        public double SeedSpacing { get; set; }

        public double RowSpacing { get; set; }

        public bool IsDeerResistant { get; set; }

        public bool IsToxicToAnimal { get; set; }

        public bool IsToxicToHuman { get; set; }

        public bool IsMedicinal { get; set; }

        public string Image { get; set; }
        public int ZoneID { get; set; }
        public int SeasonID { get; set; }
        public int PlantTypeID { get; set; }
        public int SunExposureID { get; set; }
        public int WaterNeedID { get; set; }
        public int RootStructureID { get; set; }
        public string Temperature { get; set; }
        [MaxLength(3000)]
        public string PlantDetailsDescription { get; set; }
        public string PlantCareDescription { get; set; }
    
    }
}