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
        //AddPlant builds a new plant based off AddPlantModel. The information is set to the properties in PlantCare and PlantDetails.
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
        // UpdatePlant takes in the plantID of the plant you would like to update and the new information you want updated. The values must be within the set
        // values of the variables (SeasonID between 1-4, etc.).
        public bool UpdatePlant(int plantID, UpdatePlantModel model)
        {
            int plantCareID = ctx.Plants.Single(e => e.PlantID == plantID).PlantCareID;
            PlantCare plantCare = ctx.PlantCare.FirstOrDefault(e => e.PlantCareID == plantCareID);


            plantCare.SunExposureID = model.SunExposureID;
            plantCare.WaterNeedID = model.WaterNeedID;
            plantCare.Temperature = model.Temperature;
            plantCare.Description = model.Description;
            plantCare.ModifiedDate = DateTimeOffset.UtcNow;



            Plants plants = ctx.Plants.Single(e => e.PlantID == plantID);
            plants.Name = model.Name;
            plants.ScientificName = model.ScientificName;
            plants.ZoneID = model.ZoneID;
            plants.SeasonID = model.SeasonID;
            plants.PlantTypeID = model.PlantTypeID;          
            plants.ModifiedDate = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 2;
        }
        // GetAllPlants will return a list of all the plants currently in the database. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class.
        public IEnumerable<PlantDetailsModel> GetAllPlants()
        {
            var query = ctx.Plants.Where(e => e.PlantID >= 1).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        // GetPlantByType will return a list of plant by the desired plantTypeID. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class. The value must be within the available range of 1-18.
        public IEnumerable<PlantDetailsModel> GetPlantByType(int plantTypeID)
        {
            var query = ctx.Plants.Where(e => e.PlantTypeID == plantTypeID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        

        //GetPlantsByWidth will return a list of plants within the desired range of PlantWidthMax. The values are in inches. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class.  The definitions for each value are available 
        // in SeedService.cs.
        public IEnumerable<PlantDetailsModel> GetPlantsByWidth(double min, double max)

        {
            List<PlantDetails> plantDetails = ctx.PlantDetails.Where(e => (e.PlantWidthMax >= min) && (max >= e.PlantWidthMax)).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantDetails item in plantDetails)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.FirstOrDefault(e => e.PlantDetailsID == item.PlantDetailsID)));
            }

            
            return plantDetailsModel.ToList();
            
        }

       
        //GetPlantsBySunExposure will return of plants that have the desired sunExposureID. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class. The value must be within the available range of 1-4. The definitions for each value are available 
        // in Seedservice.cs 
        public IEnumerable<PlantDetailsModel> GetPlantsBySunExposure(int sunExposureID)

        {
            List<PlantCare> plantCareID = ctx.PlantCare.Where(e => e.SunExposureID == sunExposureID).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantCare item in plantCareID)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantCareID == item.PlantCareID)));
            }

            
            
            return plantDetailsModel.ToList();
        }
       
        //GetPlantsByWaterNeed will return of plants that have the desired waterNeedID. The value must be within the available range of 1-4. The desired waterNeedID
        // is matched to the corresponding WaterNeedID in PlantCare. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class, and a list of PlantDetailsModel objects will be returned to user. The value must be
        // within the available range of 1-18. The definitions for each value are available 
        // in Seedservice.cs 
        public IEnumerable<PlantDetailsModel> GetPlantsByWaterNeed(int waterNeedID)

        {
            List<PlantCare> plantCareID = ctx.PlantCare.Where(e => e.WaterNeedID == waterNeedID).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantCare item in plantCareID)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantCareID == item.PlantCareID)));
            }

            return plantDetailsModel.ToList();
        }
        //GetPlantsByPlantZone will return plants that reside in the desired zone. The value must be within 1-13. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class. The definitions for each value are available 
        // in SeedService.cs

        public IEnumerable<PlantDetailsModel> GetPlantByPlantZone(int zoneID)

        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        //GetPlantsByBloomSeason will return plants that grow in the desired seasonID. The value must be within 1-4. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class. The definitions for each value are available 
        // in SeedService.cs


        public IEnumerable<PlantDetailsModel> GetPlantsByBloomSeason(int seasonID)

        {
            var query = ctx.Plants.Where(e => e.SeasonID == seasonID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

       
        //GetPlantsByHeightMax will return plants that have the desired plantHeightMax. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the Plants class. The definitions for each value are available 
        // in Configuration.cs
        public IEnumerable<PlantDetailsModel> GetPlantsByHeightMax(double plantHeightMax)

        {
            List<PlantDetails> plantDetails = ctx.PlantDetails.Where(e => e.PlantHeightMax == plantHeightMax).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();


            foreach (PlantDetails item in plantDetails)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantDetailsID == item.PlantDetailsID)));
            }

            
            return plantDetailsModel.ToList();
            
        }


        //GetPerrenialPlants will return a list of all the plants that have either a true or false IsPerrenial value, depending on the user
        //input. Each plant will be built out using the BuildPlantDetailsModel, which mainly gets data from the PlantDetails class.
        public IEnumerable<PlantDetailsModel> GetPerrenialPlants(bool selection)

        {
            var query = ctx.PlantDetails.Where(e => e.IsPerennial == selection).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

        //GetPlantByZoneAndSeason will return a list of the plants that have the desired zoneID and seasonID. Each plant will be built out using the
        //BuildPlantsModel, which mainly gets data from the Plants class. The value for zoneID must be within 1-13. The value for seasonID must be within 1-4. 
        //The definitions for these values are available in Configuration.cs
        public IEnumerable<PlantDetailsModel> GetPlantByZoneAndSeason(int zoneId, int seasonID)

        {
            var query = ctx.Plants.Where(e => e.ZoneID == zoneId && e.SeasonID == seasonID).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }

      

        //GetPlantsByDaysToGerminate will return a list of plants within the desired range of DaysToGerminate. Each plant will be built out using the
        //BuildPlantsModel, which mainly gets data from the Plants class. 
        public IEnumerable<PlantDetailsModel> GetPlantByDaysToGerminate(int minDays, int maxDays)

        {
            List<PlantDetails> plantDetails = ctx.PlantDetails.Where(e => e.DaysToGerminate >= minDays && e.DaysToGerminate <= maxDays).ToList();
            List<PlantDetailsModel> plantDetailsModel = new List<PlantDetailsModel>();

            foreach (PlantDetails item in plantDetails)
            {
                plantDetailsModel.Add(BuildPlantDetailsModel(ctx.Plants.Single(e => e.PlantDetailsID == item.PlantDetailsID)));
            }

            return plantDetailsModel.ToList();
        }

       
        //GetPlantsByMedicinal will return a list of plants that are true for either IsMedicinal, IsDeerResistant, IsToxicToAnimal, or IsToxicToHuman. Each plant will be built out using the
        // BuildPlantDetailsModel, which mainly gets data from the PlantDetails class. 
        public IEnumerable<PlantDetailsModel> GetPlantByMedicinalResistanceAndToxicity(GetSpecialDetailsModel model)

        {
            var query = ctx.PlantDetails.Where(e => (e.IsMedicinal == model.IsMedicinal) || (e.IsDeerResistant == model.IsDeerResistant) || (e.IsToxicToAnimal == model.IsToxicToAnimal) || (e.IsToxicToHuman == model.IsToxicToHuman)).ToArray().Select(f => BuildPlantDetailsModel(f));
            return query.ToList();
        }
        //  GetPlantZone will return the values for each Plant Zone, including a description of the temperatures for each zone for user knowledge.
        //  The information provided are populated using the GetPlantZonesModel
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
        //  GetRootStructure will return the values for all possible Root Structures, including a definition of the each one. The information provided
        //  are populated using the GetRootStructuresModel
        public IEnumerable<GetRootStructureModel> GetRootStructure()
        {
            var query = ctx.RootStructure.Where(e => e.RootStructureID >= 1).Select(f => new GetRootStructureModel
            {
                RootStructureID = f.RootStructureID,
                Name = f.Name,
                Description = f.Description,
                CreatedDate = f.CreatedDate,
                ModifiedDate = f.ModifiedDate
            });
            return query.ToList();
        }

        //  GetSunExposure will return list of all SunExposure options, including a definition of the each one. The information
        //  provided are populated using the GetSunExposureModel
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

        //GetWaterNeeds will return a list of all WaterNeeds options, including a definition.The information provided is populated using the GetWaterNeedsModel
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

        //GetPlantSeasons will return a list of all the PlantSeasons options. The information provided is populated using the GetPlantSeasonsModel.
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
        //GetPlantTypes will return a list of the possible PlantTypes, including additional information such as their IDs, etc. The information provided is
        //populated using the GetPlantTypesModel.
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
        //This BuildPlantDetailsModel pulls in all the information about a plant and primarily pulls data from the Plants
        //class. The properties that are not available in the Plants class are pulled from the PlantDetals class instead.
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
        //This BuildPlantDetailsModel pulls in all the information about a plant and primarily pulls data from the PlantDetails
        //class. The properties that are not available in the PlantDetails class are pulled from the Plants class instead.
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

                    Name = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).RootStructureID).Name,
                    Description = ctx.RootStructure.FirstOrDefault(r => r.RootStructureID == ctx.PlantDetails.FirstOrDefault(z => z.PlantDetailsID == plantDetails.PlantDetailsID).RootStructureID).Description
                }
            };
            return plantDetailsModel;
        }
    }
}

