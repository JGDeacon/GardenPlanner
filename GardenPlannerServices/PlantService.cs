using GardenPlannerData;
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

        private readonly Guid _userID;

        public PlantService(Guid userID)
        {
            _userID = userID;
        }

        public bool AddPlant(AddPlantModel model)
        {
            PlantCare plantCare = new PlantCare
            {
                SunExposureID = model.SunExposureID,
                WaterNeedID = model.WaterNeedID,
                Temperature = model.Temperature,
                Description = model.PlantCareDescription,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.PlantCare.Add(plantCare);

            PlantDetails plantDetails = new PlantDetails
            {
                DaysToGerminate = model.DaysToGerminate,
                DaysToHarvest = model.DaysToHarvest,
                SeedDepth = model.SeedDepth,
                IsPerennial = model.IsPerennial,
                PlantHeightMax = model.PlantHeightMax,
                PlantWidthMax = model.PlantWidthMax,
                SeedSpacing = model.SeedSpacing,
                RowSpacing = model.RowSpacing,
                RootStructureID = model.RootStructureID,
                IsDeerResistant = model.IsDeerResistant,
                IsToxicToAnimal = model.IsToxicToAnimal,
                IsToxicToHuman = model.IsToxicToHuman,
                IsMedicinal = model.IsMedicinal,
                Image = model.Image,
                Description = model.PlantDetailsDescription,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.PlantDetails.Add(plantDetails);
            ctx.SaveChanges();
            Plants newPlant = new Plants
            {
                Name = model.Name,
                ScientificName = model.ScientificName,
                ZoneID = model.ZoneID,
                SeasonID = model.SeasonID,
                PlantTypeID = model.PlantTypeID,
                PlantCareID = plantCare.PlantCareID,
                PlantDetailsID = plantDetails.PlantDetailsID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Plants.Add(newPlant);
            return ctx.SaveChanges() == 1;
        }
        public bool UpdatePlant(int plantID, UpdatePlantModel model)
        {
            PlantCare plantCare = ctx.PlantCare.Single(e => e.PlantCareID == model.PlantCareID);
            {
                plantCare.SunExposureID = model.SunExposureID;
                plantCare.WaterNeedID = model.WaterNeedID;
                plantCare.Temperature = model.Temperature;
                plantCare.Description = model.Description;
                plantCare.ModifiedDate = DateTimeOffset.UtcNow;
            }

            Plants plants = ctx.Plants.Single(e => e.PlantID == plantID);
            plants.Name = model.Name;
            plants.ScientificName = model.ScientificName;
            plants.ZoneID = model.ZoneID;
            plants.SeasonID = model.SeasonID;
            plants.PlantTypeID = model.PlantTypeID;
            plants.PlantCareID = model.PlantCareID;
            plants.PlantDetailsID = model.PlantDetailsID;
            plants.ModifiedDate = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 2;
        }
        public IEnumerable<PlantDetailsModel> GetAllPlants()
        {
            var query = ctx.Plants.Where(e => e.PlantID >= 1).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });

            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantByType(int plantTypeID)
        {
            var query = ctx.Plants.Where(e => e.PlantTypeID == plantTypeID).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantsByWidth(double min, double max)
        {
            var query = ctx.PlantDetails.Where(e => (e.PlantWidthMax >= min) && (max >= e.PlantWidthMax)).Select(f => new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                DaysToGerminate = f.DaysToGerminate,
                DaysToHarvest = f.DaysToHarvest,
                SeedDepth = f.SeedDepth,
                IsPerennial = f.IsPerennial,
                PlantHeightMax = f.PlantHeightMax,
                PlantWidthtMax = f.PlantWidthMax,
                SeedSpacing = f.SeedSpacing,
                RowSpacing = f.RowSpacing,
                IsDeerResistant = f.IsDeerResistant,
                IsToxicToAnimal = f.IsToxicToAnimal,
                IsToxicToHuman = f.IsToxicToHuman,
                IsMedicinal = f.IsMedicinal,
                Image = f.Image,
                Description = f.Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Description },
                PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).Description },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description


                },


                WaterNeeds = new WaterNeedsModel
                {

                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                }
            });
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantsBySunExposure(int sunExposureID)
        {
            var query = ctx.Plants.Where(e => e.PlantCareID == ctx.PlantCare.Single(g => g.SunExposureID == sunExposureID).PlantCareID).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantsByWaterNeed(int waterNeedID)
        {
            var query = ctx.Plants.Where(e => e.PlantCareID == ctx.PlantCare.Single(g => g.WaterNeedID == waterNeedID).PlantCareID).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });
            return query.ToList();
        }



        public IEnumerable<PlantDetailsModel> GetPlantByPlantZone(int zoneID)
        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneID).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });
            return query.ToList();
        }



        public IEnumerable<PlantDetailsModel> GetPlantsByBloomSeason(int seasonID)
        {
            var query = ctx.Plants.Where(e => e.SeasonID == seasonID).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantsByHeightMax(double plantHeightMax)
        {
            var query = ctx.PlantDetails.Where(e => e.PlantHeightMax == plantHeightMax).Select(f => new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                DaysToGerminate = f.DaysToGerminate,
                DaysToHarvest = f.DaysToHarvest,
                SeedDepth = f.SeedDepth,
                IsPerennial = f.IsPerennial,
                PlantHeightMax = f.PlantHeightMax,
                PlantWidthtMax = f.PlantWidthMax,
                SeedSpacing = f.SeedSpacing,
                RowSpacing = f.RowSpacing,
                IsDeerResistant = f.IsDeerResistant,
                IsToxicToAnimal = f.IsToxicToAnimal,
                IsToxicToHuman = f.IsToxicToHuman,
                IsMedicinal = f.IsMedicinal,
                Image = f.Image,
                Description = f.Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Description },
                PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).Description },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description

                },


                WaterNeeds = new WaterNeedsModel
                {

                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                }
            });
            return query.ToList();
        }


        public IEnumerable<PlantDetailsModel> GetPerrenialPlants()
        {
            var query = ctx.PlantDetails.Where(e => e.IsPerennial == true).Select(f => new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                DaysToGerminate = f.DaysToGerminate,
                DaysToHarvest = f.DaysToHarvest,
                SeedDepth = f.SeedDepth,
                IsPerennial = f.IsPerennial,
                PlantHeightMax = f.PlantHeightMax,
                PlantWidthtMax = f.PlantWidthMax,
                SeedSpacing = f.SeedSpacing,
                RowSpacing = f.RowSpacing,
                IsDeerResistant = f.IsDeerResistant,
                IsToxicToAnimal = f.IsToxicToAnimal,
                IsToxicToHuman = f.IsToxicToHuman,
                IsMedicinal = f.IsMedicinal,
                Image = f.Image,
                Description = f.Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Description },
                PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).Description },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description


                },


                WaterNeeds = new WaterNeedsModel
                {

                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                }
            });
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantByZoneAndSeason(int zoneId, int seasonID)
        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneId && e.SeasonID == seasonID).Select(f => new PlantDetailsModel
            {

                Name = f.Name,
                ScientificName = f.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == f.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == f.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == f.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == f.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                },

            });
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantByDaysToGerminate(int daysToGerminate)
        {
            var query = ctx.PlantDetails.Where(e => e.DaysToGerminate == daysToGerminate).Select(f => new PlantDetailsModel
            {
                Name = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                ScientificName = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                DaysToGerminate = f.DaysToGerminate,
                DaysToHarvest = f.DaysToHarvest,
                SeedDepth = f.SeedDepth,
                IsPerennial = f.IsPerennial,
                PlantHeightMax = f.PlantHeightMax,
                PlantWidthtMax = f.PlantWidthMax,
                SeedSpacing = f.SeedSpacing,
                RowSpacing = f.RowSpacing,
                IsDeerResistant = f.IsDeerResistant,
                IsToxicToAnimal = f.IsToxicToAnimal,
                IsToxicToHuman = f.IsToxicToHuman,
                IsMedicinal = f.IsMedicinal,
                Image = f.Image,
                Description = f.Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Description },
                PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).Description },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description


                },


                WaterNeeds = new WaterNeedsModel
                {

                    Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                }
            });
            return query.ToList();
        }

        //Need to check (Not sure) how it works 
        public IEnumerable<PlantDetailsModel> GetPlantByDaysToMedicianlResistanceAndToxicity()
        {
            var query =
                   ctx.PlantDetails.Where(e => e.IsMedicinal || e.IsDeerResistant || e.IsToxicToAnimal || e.IsToxicToHuman == true).Select(f => new PlantDetailsModel
                   {
                       Name = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                       ScientificName = ctx.Plants.Single(z => z.PlantID == f.PlantDetailsID).Name,
                       DaysToGerminate = f.DaysToGerminate,
                       DaysToHarvest = f.DaysToHarvest,
                       SeedDepth = f.SeedDepth,
                       IsPerennial = f.IsPerennial,
                       PlantHeightMax = f.PlantHeightMax,
                       PlantWidthtMax = f.PlantWidthMax,
                       SeedSpacing = f.SeedSpacing,
                       RowSpacing = f.RowSpacing,
                       IsDeerResistant = f.IsDeerResistant,
                       IsToxicToAnimal = f.IsToxicToAnimal,
                       IsToxicToHuman = f.IsToxicToHuman,
                       IsMedicinal = f.IsMedicinal,
                       Image = f.Image,
                       Description = f.Description,
                       PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.Single(z => z.PlantTypeID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantID).Description },
                       PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.Single(z => z.PlantCareID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).PlantCareID).Description },
                       PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.Single(z => z.SeasonID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).SeasonID).Description },
                       PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.Single(z => z.ZoneID == ctx.Plants.Single(r => r.PlantDetailsID == f.PlantDetailsID).ZoneID).Description },
                       SunExposures = new SunExposureModel
                       {
                           Name = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                           Description = ctx.SunExposures.Single(r => r.SunExposureID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description


                       },


                       WaterNeeds = new WaterNeedsModel
                       {

                           Name = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Name,
                           Description = ctx.WaterNeeds.Single(r => r.WaterNeedID == ctx.PlantCare.Single(s => s.PlantCareID == ctx.Plants.Single(z => z.PlantDetailsID == f.PlantDetailsID).PlantCareID).SunExposureID).Description
                       },
                       RootStructure = new RootStructureModel
                       {
                           Name = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Name,
                           Description = ctx.RootStructure.Single(r => r.RootStructureID == ctx.PlantDetails.Single(z => z.PlantDetailsID == f.PlantDetailsID).RootStructureID).Description
                       }
                   });

            return query.ToList();

        }

        private PlantDetailsModel BuildPlantDetailsModel(Plants plant)
        {
            PlantDetailsModel plantDetailsModel = new PlantDetailsModel
            {

                Name = plant.Name,
                ScientificName = plant.ScientificName,
                DaysToGerminate = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).DaysToGerminate,
                DaysToHarvest = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).DaysToHarvest,
                SeedDepth = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).SeedDepth,
                IsPerennial = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).IsPerennial,
                PlantHeightMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).PlantHeightMax,
                PlantWidthtMax = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).PlantWidthMax,
                SeedSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).SeedSpacing,
                RowSpacing = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).RowSpacing,
                IsDeerResistant = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).IsDeerResistant,
                IsToxicToAnimal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).IsToxicToAnimal,
                IsToxicToHuman = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).IsToxicToHuman,
                IsMedicinal = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).IsMedicinal,
                Image = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).Image,
                Description = ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).Description,
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == plant.PlantTypeID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == plant.PlantTypeID).Description },
                PlantCare = new PlantCareModel { Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == plant.PlantCareID).Description, Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == plant.PlantCareID).Temperature },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == plant.SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == plant.SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == plant.ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == plant.ZoneID).Description },

                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == plant.PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == plant.PlantCareID).SunExposureID).Description
                },
                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == plant.PlantCareID).WaterNeedID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == plant.PlantCareID).WaterNeedID).Description
                },
                RootStructure = new RootStructureModel
                {
                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plant.PlantDetailsID).RootStructureID).Description
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
                PlantWidthtMax = plantDetails.PlantWidthMax,
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
            };
            return plantDetailsModel;
        }
    }
}

