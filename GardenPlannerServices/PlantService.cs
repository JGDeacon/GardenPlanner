using GardenPlannerData;
using GardenPlannerModels;
using GardenPlannerModels.GetModels;
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
            PlantCare plantCare = ctx.PlantCare.FirstOrDefault(e => e.PlantCareID == model.PlantCareID);
            //{
                plantCare.SunExposureID = model.SunExposureID;
                plantCare.WaterNeedID = model.WaterNeedID;
                plantCare.Temperature = model.Temperature;
                plantCare.Description = model.Description;
                plantCare.ModifiedDate = DateTimeOffset.UtcNow;
            //}

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
            var query = ctx.Plants.Where(e => e.PlantID >= 1).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantByType(int plantTypeID)
        {
            var query = ctx.Plants.Where(e => e.PlantTypeID == plantTypeID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantsByWidth(double min, double max) //working
        {
            List<PlantDetails> plantDetails = ctx.PlantDetails.Where(e => (e.PlantWidthMax >= min) && (max >= e.PlantWidthMax)).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantDetails item in plantDetails)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantDetailsID == item.PlantDetailsID)));
            }
            //var query = ctx.PlantDetails.Where(e => e.DaysToGerminate == daysToGerminate).ToArray().Select(f => BuildPlantDetailsModel(f));
            return plantDetailsModel.ToList();
            //var query = ctx.PlantDetails.Where(e => (e.PlantWidthMax >= min) && (max >= e.PlantWidthMax)).ToArray().Select(f => BuildPlantDetailsModel(f));
            //return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantsBySunExposure(int sunExposureID) //working
        {
            List<PlantCare> plantCareID = ctx.PlantCare.Where(e => e.SunExposureID == sunExposureID).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantCare item in plantCareID)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantCareID == item.PlantCareID)));
            }
            //var query = ctx.Plants.Where(e => e.PlantCareID == ctx.PlantCare.FirstOrDefault(g => g.SunExposureID == sunExposureID).PlantCareID).ToArray().Select(f => BuildPlantDetailsModel(f));
            
            return plantDetailsModel.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantsByWaterNeed(int waterNeedID) //working
        {
            List<PlantCare> plantCareID = ctx.PlantCare.Where(e => e.WaterNeedID == waterNeedID).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantCare item in plantCareID)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantCareID == item.PlantCareID)));
            }
            //var query = ctx.Plants.Where(e => e.PlantCareID == plantCareID.PlantCareID).ToArray().Select(f => BuildPlantDetailsModel(f));
            //var query = ctx.PlantCare.Where(e => e.PlantCareID == (ctx.PlantCare.Find(waterNeedID).PlantCareID)).Where(g => g.s => .ToArray().Select(f => BuildPlantDetailsModel(f));
            //var query = ctx.PlantCare.Where(e => e.WaterNeedID == waterNeedID).Where(r => r.PlantCareID == ctx.Plants.(ctx.PlantCare.Where(g => g.WaterNeedID == waterNeedID)).ToArray().Select(f => BuildPlantDetailsModel(f));
            return plantDetailsModel.ToList();
        }



        public IEnumerable<PlantDetailsModel> GetPlantByPlantZone(int zoneID) //working
        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }



        public IEnumerable<PlantDetailsModel> GetPlantsByBloomSeason(int seasonID) //working
        {
            var query = ctx.Plants.Where(e => e.SeasonID == seasonID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantsByHeightMax(double plantHeightMax) // working... but too specific. This should be a range
        {
            List<PlantDetails> plantDetails = ctx.PlantDetails.Where(e => e.PlantHeightMax == plantHeightMax).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();


            foreach (PlantDetails item in plantDetails)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantDetailsID == item.PlantDetailsID)));
            }
            //var query = ctx.PlantDetails.Where(e => e.DaysToGerminate == daysToGerminate).ToArray().Select(f => BuildPlantDetailsModel(f));
            return plantDetailsModel.ToList();
            //var query = ctx.PlantDetails.Where(e => e.PlantHeightMax == plantHeightMax).ToArray().Select(f => BuildPlantDetailsModel(f));
            //return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPerrenialPlants(bool selection) //working
        {
            var query = ctx.PlantDetails.Where(e => e.IsPerennial == selection).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        public IEnumerable<PlantDetailsModel> GetPlantByZoneAndSeason(int zoneId, int seasonID) //working
        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneId && e.SeasonID == seasonID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        public IEnumerable<PlantDetailsModel> GetPlantByDaysToGerminate(int minDays, int maxDays) // Working but very specific. Should be a range
        {
            List<PlantDetails> plantDetails = ctx.PlantDetails.Where(e => e.DaysToGerminate >= minDays && e.DaysToGerminate <= maxDays).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantDetails item in plantDetails)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantDetailsID == item.PlantDetailsID)));
            }
            //var query = ctx.PlantDetails.Where(e => e.DaysToGerminate == daysToGerminate).ToArray().Select(f => BuildPlantDetailsModel(f));
            return plantDetailsModel.ToList();
        }

        //Need to check (Not sure) how it works 
        //public IEnumerable<PlantDetailsModel> GetPlantByDaysToMedicianlResistanceAndToxicity() //Not referenced
        //{
        //    var query = ctx.PlantDetails.Where(e => e.IsMedicinal || e.IsDeerResistant || e.IsToxicToAnimal || e.IsToxicToHuman == true).ToArray().Select(f => BuildPlantDetailsModel(f));
        //    return query.ToList();
        //}
        public IEnumerable<PlantDetailsModel> GetPlantByMedicianlResistanceAndToxicity(GetSpecialDetailsModel model) //Not referenced
        {
            var query = ctx.PlantDetails.Where(e => (e.IsMedicinal == model.IsMedicinal) || (e.IsDeerResistant == model.IsDeerResistant) || (e.IsToxicToAnimal == model.IsToxicToAnimal) || (e.IsToxicToHuman == model.IsToxicToHuman)).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        //PlantZone
        public IEnumerable<GetPlantZonesModel> GetPlantZones()
        {
            var query = ctx.PlantZones.Where(e => e.ZoneID >= 1).Select(f => new GetPlantZonesModel
            {
                ZoneID = f.ZoneID,
                ZoneCode = f.ZoneCode,
                Description = f.Description,
                CreatedDate = f.CreatedDate,
                Modifiedate = f.Modifiedate
            });
            return query.ToList();
        }

        //Root Structure
        public IEnumerable<GetRootStructureModel> GetRootStructure()
        {
            var query = ctx.RootStructure.Where(e => e.RootStructureID >=1).Select(f => new GetRootStructureModel
            {
                RootStructureID = f.RootStructureID,
                Name = f.Name,
                Description = f.Description,
                CreatedDate = f.CreatedDate,
                ModifiedDate = f.ModifiedDate
            });
            return query.ToList();
        }

        //SunExposure
        public IEnumerable<GetSunExposureModel> GetSunExposure()
        {
            var query = ctx.SunExposures.Where(e => e.SunExposureID >= 1).Select(f => new GetSunExposureModel
            {
                SunExposureID = f.SunExposureID,
                Name = f.Name,
                Description = f.Description,
                CreatedDate = f.CreatedDate,
                ModifiedDate = f.ModifiedDate
            });
            return query.ToList();
        }

        //WaterNeeds
        public IEnumerable<GetWaterNeedsModel> GetWaterNeeds()
        {
            var query = ctx.WaterNeeds.Where(e => e.WaterNeedID >= 1).Select(f => new GetWaterNeedsModel
            {
                WaterNeedID = f.WaterNeedID,
                Name = f.Name,
                Description = f.Description,
                CreatedDate = f.CreatedDate,
                ModifiedDate = f.ModifiedDate
            });
            return query.ToList();
        }

        //Get Methods

        //Get PlantCare 
        public IEnumerable<GetPlantCareModel> GetPlantCare()
        {
            var query = ctx.PlantCare.Where(e => e.PlantCareID >= 1).ToArray().Select(e => new GetPlantCareModel
            {
                PlantCareID = e.PlantCareID,
                SunExposureID = e.SunExposureID,
                WaterNeedID = e.WaterNeedID,
                Temperature = e.Temperature,
                Description = e.Description,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate

            });
            return query.ToList();
        }

        //Get Plant Seasons
        
        public IEnumerable<GetPlantSeasonsModel> GetPlantSeasons()
        {
            var query = ctx.PlantSeasons.Where(e => e.SeasonID >= 1).Select(e => new GetPlantSeasonsModel
            {
                SeasonID = e.SeasonID,
                Name = e.Name,
                Description = e.Description,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate
               
            });
            return query.ToList();
        }

        //Get Plant Types

        public IEnumerable<GetPlantTypesModel> GetPlantTypes()
        {
            var query = ctx.PlantTypes.Where(e => e.PlantTypeID >= 1).Select(e => new GetPlantTypesModel
            {
                PlantTypeID = e.PlantTypeID,
                Name = e.Name,
                Description = e.Description,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate

            });
            return query.ToList();
          
        }

        private PlantDetailsModel BuildPlantDetailsModel(Plants plant)
        {
            PlantDetailsModel plantDetailsModel = new PlantDetailsModel
            {
                PlantID = plant.PlantID,
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
                PlantID = ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantID,
                Name = ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).Name,
                ScientificName = ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).ScientificName,
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
                PlantTypes = new PlantTypesModel { Name = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantID).Name, Description = ctx.PlantTypes.FirstOrDefault(z => z.PlantTypeID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantID).Description },
                PlantCare = new PlantCareModel { Temperature = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).Temperature, Description = ctx.PlantCare.FirstOrDefault(z => z.PlantCareID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).Description },
                PlantSeasons = new PlantSeasonsModel { Name = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).SeasonID).Name, Description = ctx.PlantSeasons.FirstOrDefault(z => z.SeasonID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).SeasonID).Description },
                PlantZones = new PlantZonesModel { ZoneCode = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).ZoneID).ZoneCode, Description = ctx.PlantZones.FirstOrDefault(z => z.ZoneID == ctx.Plants.FirstOrDefault(r => r.PlantDetailsID == plantDetails.PlantDetailsID).ZoneID).Description },
                SunExposures = new SunExposureModel
                {
                    Name = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(s => s.PlantCareID == ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.SunExposures.FirstOrDefault(r => r.SunExposureID == ctx.PlantCare.FirstOrDefault(s => s.PlantCareID == ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Description
                },


                WaterNeeds = new WaterNeedsModel
                {
                    Name = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(s => s.PlantCareID == ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Name,
                    Description = ctx.WaterNeeds.FirstOrDefault(r => r.WaterNeedID == ctx.PlantCare.FirstOrDefault(s => s.PlantCareID == ctx.Plants.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).PlantCareID).SunExposureID).Description
                },
                RootStructure = new RootStructureModel
                {
                    //Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == plantDetails.RootStructureID).Name, //First adjustment
                    //Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == plantDetails.RootStructureID).Description //First adjustment
                    
                    Name = "Name",
                    Description = "Desc"
                    
                    //Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).RootStructureID).Name,
                    //Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).RootStructureID).Description
                }
            };
            return plantDetailsModel;
        }
    }
}

