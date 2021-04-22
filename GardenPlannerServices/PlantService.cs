﻿using GardenPlannerData;
using GardenPlannerModels;
using GardenPlannerModels.PlantDetailsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerServices
{
    public class PlantService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public IEnumerable<PlantDetailsModel> GetPlantByType(int plantTypeID)
        {
            var query = ctx.Plants.Where(e => e.PlantTypeID == plantTypeID).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantsByWidth(double min, double max)
        {
            var query = ctx.PlantDetails.Where(e => (e.PlantWidthtMax >= min) && (max >= e.PlantWidthtMax)).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantsBySunExposure(int sunExposureID)
        {
            var query = ctx.Plants.Where(e => e.PlantCareID == ctx.PlantCare.Single(g => g.SunExposureID == sunExposureID).PlantCareID).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantsByWaterNeed(int waterNeedID)
        {
            var query = ctx.Plants.Where(e => e.PlantCareID == ctx.PlantCare.Single(g => g.WaterNeedID == waterNeedID).PlantCareID).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantsByBloomSeason(int seasonID)
        {
            var query = ctx.Plants.Where(e => e.SeasonID == seasonID).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

       

        public IEnumerable<PlantDetailsModel> GetPlantsByHeightMax(double plantHeightMax)
        {
            var query = ctx.PlantDetails.Where(e => e.PlantHeightMax == plantHeightMax).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantByPlantZone(int zoneID)
        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneID).Select(f => BuildPlantDetailsModel(f)); return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPerrenialPlants()
        {
            var query = ctx.PlantDetails.Where(e => e.IsPerennial == true).Select(f => new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).ScientificName,
                DaysToGerminate = f.DaysToGerminate,
                DaysToHarvest = f.DaysToHarvest,
                SeedDepth = f.SeedDepth,
                IsPerennial = f.IsPerennial,
                PlantHeightMax = f.PlantHeightMax,
                PlantWidthtMax = f.PlantWidthtMax,
                SeedSpacing = f.SeedSpacing,
                RowSpacing = f.RowSpacing,
                IsDeerResistant = f.IsDeerResistant,
                IsToxicToAnimal = f.IsToxicToAnimal,
                IsToxicToHuman = f.IsToxicToHuman,
                IsMedicinal = f.IsMedicinal,
                Image = f.Image,
                Description = f.Description,
                PlantTypes = new PlantTypesModel
                {
                    Name = ctx.PlantTypes.Single(r => r.PlantTypeID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantTypeID).Name,
                    Description = ctx.PlantTypes.Single(r => r.PlantTypeID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantTypeID).Description
                },
                PlantCare = new PlantCareModel
                {
                    Description = ctx.PlantCare.Single(r => r.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description,
                    Temperature = ctx.PlantCare.Single(r => r.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature
                },
                PlantSeasons = new PlantSeasonsModel
                {
                    Name = ctx.PlantSeasons.Single(r => r.SeasonID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).SeasonID).Name,
                    Description = ctx.PlantSeasons.Single(r => r.SeasonID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).SeasonID).Description
                },
                PlantZones = new PlantZonesModel
                {
                    ZoneCode = ctx.PlantZones.Single(r => r.ZoneID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode,
                    Description = ctx.PlantZones.Single(r => r.ZoneID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).ZoneID).Description

                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(z => z.RootStructureID == f.RootStructureID).Name,
                    Description = ctx.RootStructure.Single(z => z.RootStructureID == f.RootStructureID).Description
                },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).WaterNeedID).Description
                }

            });

            return query.ToList();
        }


        public IEnumerable<PlantDetailsModel> GetPlantByPlantZone(int zoneID)

        public IEnumerable<PlantDetailsModel> GetPlantByZoneAndSeason(int zoneId, int seasonID)

        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneId && e.SeasonID == seasonID).Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        private PlantDetailsModel BuildPlantDetailsModel(Plants plant)
        {
            PlantDetailsModel plantDetailsModel = new PlantDetailsModel
            {

                Name = plant.Name,
                ScientificName = plant.ScientificName,
                DaysToGerminate = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).PlantWidthtMax,
                SeedSpacing = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).Image,
                Description = ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == plant.PlantTypeID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == plant.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.Single(z => z.PlantCareID == plant.PlantCareID).Description, Temperature = ctx.PlantCare.Single(z => z.PlantCareID == plant.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == plant.SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == plant.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == plant.ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == plant.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(z => z.PlantCareID == plant.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(z => z.PlantCareID == plant.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(z => z.PlantCareID == plant.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(z => z.PlantCareID == plant.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == plant.PlantDetailsID).RootStructureID).Description
                },

            };
            return plantDetailsModel;

        }
        private PlantDetailsModel BuildPlantDetailsModel(PlantDetails plantDetails)
        {
            PlantDetailsModel plantDetailsModel = new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantID == plantDetails.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantID == plantDetails.PlantDetailsID).Name,
                DaysToGerminate = plantDetails.DaysToGerminate,
                DaysToHarvest = plantDetails.DaysToHarvest,
                SeedDepth = plantDetails.SeedDepth,
                IsPerennial = plantDetails.IsPerennial,
                PlantHeightMax = plantDetails.PlantHeightMax,
                PlantWidthtMax = plantDetails.PlantWidthtMax,
                SeedSpacing = plantDetails.SeedSpacing,
                RowSpacing = plantDetails.RowSpacing,
                IsDeerResistant = plantDetails.IsDeerResistant,
                IsToxicToAnimal = plantDetails.IsToxicToAnimal,
                IsToxicToHuman = plantDetails.IsToxicToHuman,
                IsMedicinal = plantDetails.IsMedicinal,
                Image = plantDetails.Image,
                Description = plantDetails.Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantID).Description },
                PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).Description },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == plantDetails.PlantDetailsID).ZoneID).Description },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == plantDetails.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == plantDetails.PlantDetailsID).RootStructureID).Description
                }


            });

            return query.ToList();

        }

        public IEnumerable<PlantDetailsModel> GetPlantByDaysToGerminate(int daysToGerminate)
        {
            var query = ctx.PlantDetails.Where(e => e.DaysToGerminate == daysToGerminate).Select(f => new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).ScientificName,
                DaysToGerminate = f.DaysToGerminate,
                DaysToHarvest = f.DaysToHarvest,
                SeedDepth = f.SeedDepth,
                IsPerennial = f.IsPerennial,
                PlantHeightMax = f.PlantHeightMax,
                PlantWidthtMax = f.PlantWidthtMax,
                SeedSpacing = f.SeedSpacing,
                RowSpacing = f.RowSpacing,
                IsDeerResistant = f.IsDeerResistant,
                IsToxicToAnimal = f.IsToxicToAnimal,
                IsToxicToHuman = f.IsToxicToHuman,
                IsMedicinal = f.IsMedicinal,
                Image = f.Image,
                Description = f.Description,
                PlantTypes = new PlantTypesModel
                {
                    Name = ctx.PlantTypes.Single(r => r.PlantTypeID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantTypeID).Name,
                    Description = ctx.PlantTypes.Single(r => r.PlantTypeID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantTypeID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(z => z.RootStructureID == f.RootStructureID).Name,
                    Description = ctx.RootStructure.Single(z => z.RootStructureID == f.RootStructureID).Description
                },
                PlantCare = new PlantCareModel
                {
                    Description = ctx.PlantCare.Single(r => r.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description,
                    Temperature = ctx.PlantCare.Single(r => r.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature
                },
                PlantSeasons = new PlantSeasonsModel
                {
                    Name = ctx.PlantSeasons.Single(r => r.SeasonID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).SeasonID).Name,
                    Description = ctx.PlantSeasons.Single(r => r.SeasonID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).SeasonID).Description
                },
                SunExposures =  new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(g => g.PlantCareID ==ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(g => g.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).WaterNeedID).Description
                },
                PlantZones = new PlantZonesModel
                {
                    ZoneCode = ctx.PlantZones.Single(r => r.ZoneID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode,
                    Description = ctx.PlantZones.Single(r => r.ZoneID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).ZoneID).Description

                }

            });

            return query.ToList();

        
        }

       



            };
            return plantDetailsModel;
        }


    }
}
