using GardenPlannerData;
using GardenPlannerModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerServices
{
    public class SeedService
    {
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public SeedService()
        {

        }
        public void SeedPlants()
        {
            SeedSupportTables();

            List<AddPlantModel> addPlantModels = new List<AddPlantModel>();
            addPlantModels.Add(new AddPlantModel
            {
                Name = "Orchid",
                ScientificName = "Orchid",
                DaysToGerminate = 14,
                DaysToHarvest = 22,
                SeedDepth = 2.4,
                IsPerennial = false,
                PlantHeightMax = 18,
                PlantWidthMax = 30,
                SeedSpacing = 6,
                RowSpacing = 11.4,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = true,
                Image = "https://unsplash.com/photos/OsHqxjVV0HE",
                ZoneID = 6,
                SeasonID = 3,
                PlantTypeID = 3,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 5,
                Temperature = "65 - 90 degrees F",
                PlantDetailsDescription = "Dies really easy, don't touch it. - Not Accurate Info",
                PlantCareDescription = "Water every 4 days, cover plant if there is a frost warning."
            });
            addPlantModels.Add(new AddPlantModel
            {
                Name = "Clamatis",
                ScientificName = "Clamatist Orgestishin",
                DaysToGerminate = 22,
                DaysToHarvest = 39,
                SeedDepth = 4.5,
                IsPerennial = true,
                PlantHeightMax = 72,
                PlantWidthMax = 30.3,
                SeedSpacing = 12,
                RowSpacing = 14.8,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1557735039-6941f81b232a?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=675&q=80",
                ZoneID = 8,
                SeasonID = 2,
                PlantTypeID = 11,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 4,
                Temperature = "65 - 90 degrees F",
                PlantDetailsDescription = "These vines need a latice structure to climb. - Not Accurate Info",
                PlantCareDescription = "Water every 2 days, cover plant if there is a frost warning."
            });
            addPlantModels.Add(new AddPlantModel
            {
                Name = "Poison Ivy",
                ScientificName = "Poison Ivyis",
                DaysToGerminate = 4,
                DaysToHarvest = 6,
                SeedDepth = 2.4,
                IsPerennial = true,
                PlantHeightMax = 188.2,
                PlantWidthMax = 188.5,
                SeedSpacing = 1,
                RowSpacing = 1,
                IsDeerResistant = false,
                IsToxicToAnimal = true,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1507750997121-f7166b4cc320?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1352&q=80",
                ZoneID = 7,
                SeasonID = 2,
                PlantTypeID = 11,
                SunExposureID = 3,
                WaterNeedID = 3,
                RootStructureID = 7,
                Temperature = "45 - 110 degrees F",
                PlantDetailsDescription = "A Batman villain. - Not Accurate Info",
                PlantCareDescription = "If you touch one of these wash your hands and arms with Dawn Dish soap to get the plant oils off your skin."
            });
            addPlantModels.Add(new AddPlantModel
            {
                Name = "Aloe",
                ScientificName = "Aloe Vera",
                DaysToGerminate = 39,
                DaysToHarvest = 64,
                SeedDepth = 8.9,
                IsPerennial = true,
                PlantHeightMax = 74,
                PlantWidthMax = 52,
                SeedSpacing = 27,
                RowSpacing = 41.3,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = true,
                Image = "https://images.unsplash.com/photo-1556408977-209cb41649e4?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1314&q=80",
                ZoneID = 13,
                SeasonID = 3,
                PlantTypeID = 3,
                SunExposureID = 1,
                WaterNeedID = 3,
                RootStructureID = 3,
                Temperature = "90 degrees + F",
                PlantDetailsDescription = "Lives in the heat so it can heal your burns. - Not Accurate Info",
                PlantCareDescription = "Don't break the leaves at the base."
            });
            addPlantModels.Add(new AddPlantModel
            {
                Name = "Potato",
                ScientificName = "Potatoe",
                DaysToGerminate = 14,
                DaysToHarvest = 52,
                SeedDepth = 12.9,
                IsPerennial = false,
                PlantHeightMax = 2,
                PlantWidthMax = 6,
                SeedSpacing = 18,
                RowSpacing = 12.3,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1508313880080-c4bef0730395?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1267&q=80",
                ZoneID = 8,
                SeasonID = 2,
                PlantTypeID = 17,
                SunExposureID = 4,
                WaterNeedID = 3,
                RootStructureID = 5,
                Temperature = "55 degrees + F",
                PlantDetailsDescription = "Has eyes, but can't see. - Not Accurate Info",
                PlantCareDescription = "Don't eat the eyes."

            }) ;
            addPlantModels.Add(new AddPlantModel
            {
                Name = "Peruvian Lilly",
                ScientificName = "Alstoemeria",
                DaysToGerminate = 28,
                DaysToHarvest = 105,
                SeedDepth = 8.0,
                IsPerennial = true,
                PlantHeightMax = 36,
                PlantWidthMax = 24,
                SeedSpacing = 18,
                RowSpacing = 15.8,
                IsDeerResistant = true,
                IsToxicToAnimal = true,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://unsplash.com/photos/6PX8C0Pwx8A",
                ZoneID = 8,
                SeasonID = 3,
                PlantTypeID = 3,
                SunExposureID = 2,
                WaterNeedID = 3,
                RootStructureID = 1,
                Temperature = "63 - 72 degrees F",
                PlantDetailsDescription = "perennial native to South America. Terminal clusters of small, lily-like flowers top slender, upright stems growing in bushy clumps to 2-3' tall. Flowers in yellow or orange, often with spotting and streaking. Blooms in summer.",
                PlantCareDescription = "Once in bloom, fertilize weekly. Pull stems intead of cut. Use mulch to keep plant warm in colder weather. "
            });


            addPlantModels.Add(new AddPlantModel
            {
                Name = "Mint",
                ScientificName = "Mentha",
                DaysToGerminate = 15,
                DaysToHarvest = 90,
                SeedDepth = .25,
                IsPerennial = true,
                PlantHeightMax = 24,
                PlantWidthMax = 24,
                SeedSpacing = 18,
                RowSpacing = 18,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = true,
                Image = "https://unsplash.com/photos/boadZKqd1YM",
                ZoneID = 6,
                SeasonID = 3,
                PlantTypeID = 3,
                SunExposureID = 2,
                WaterNeedID = 1,
                RootStructureID = 3,
                Temperature = "65 - 90 degrees F",
                PlantDetailsDescription = "Spreads easily. Strong scented.",
                PlantCareDescription = "Water frequently. Plant in contained space, easily overtakes neighboring plants. Great for dishes, drinks, and medicinal purposes."
            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Morning Glory",
                ScientificName = "Ipomoea",
                DaysToGerminate = 21,
                DaysToHarvest = 32,
                SeedDepth = 0.5,
                IsPerennial = true,
                PlantHeightMax = 144,
                PlantWidthMax = 72,
                SeedSpacing = 6,
                RowSpacing = 6,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = true,
                Image = "https://unsplash.com/photos/CY8d-s7ACrA",
                ZoneID = 6,
                SeasonID = 3,
                PlantTypeID = 6,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 7,
                Temperature = "64 degrees F",
                PlantDetailsDescription = "Vines grow by clinging to nearby structures. Grows rapidly. Best grown contained.",
                PlantCareDescription = "Water every 4 days, cover plant if there is a frost warning."
            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Parsley",
                ScientificName = "Petroselinum crispum",
                DaysToGerminate = 30,
                DaysToHarvest = 80,
                SeedDepth = 1,
                IsPerennial = false,
                PlantHeightMax = 18,
                PlantWidthMax = 30,
                SeedSpacing = 6,
                RowSpacing = 11.4,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = true,
                Image = "https://unsplash.com/photos/WGZv8R05LSo",
                ZoneID = 9,
                SeasonID = 4,
                PlantTypeID = 4,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 2,
                Temperature = "65 - 90 degrees F",
                PlantDetailsDescription = "Used to treat high blood pressure, allergies, and inflammatory diseases. Native to the Mediterranean.",
                PlantCareDescription = "Keep soil lightly moist. Fertilize every two weeks. Grows well in containers."
            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Zinnia",
                ScientificName = "Zinnia elegans",
                DaysToGerminate = 5,
                DaysToHarvest = 70,
                SeedDepth = .25,
                IsPerennial = false,
                PlantHeightMax = 18,
                PlantWidthMax = 30,
                SeedSpacing = 18,
                RowSpacing = 18,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = false,
                IsMedicinal = true,
                Image = "https://unsplash.com/photos/P16UKYi8rAA",
                ZoneID = 7,
                SeasonID = 2,
                PlantTypeID = 1,
                SunExposureID = 1,
                WaterNeedID = 2,
                RootStructureID = 2,
                Temperature = "74 - 84 degrees F",
                PlantDetailsDescription = "Pollinator plant. Attracts butterflies and hummingbirds. Very bright flowers.",
                PlantCareDescription = "Water deeply a few times a week so soil stays 6-8 inches deep. Don't overwater or else the flowers will succumb to rot diseases (especially when using wet,clay soils)."

            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Damask Rose ",
                ScientificName = "Rosa damascena",
                DaysToGerminate = 14,
                DaysToHarvest = 95,
                SeedDepth = 2,
                IsPerennial = true,
                PlantHeightMax = 4,
                PlantWidthMax = 3,
                SeedSpacing = 36,
                RowSpacing = 42,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1607542661840-179d473a913d?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=358&q=80",
                ZoneID = 8,
                SeasonID = 3,
                PlantTypeID = 3,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 1,
                Temperature = "70 degrees + F",
                PlantDetailsDescription = "Roses are erect, climbing, or trailing shrubs, \n " +
                "the stems of which are usually copiously armed with prickles of various shapes and sizes,commonly called thorns.",
                PlantCareDescription = "Rose care is easier than you think—anyone can grow them successfully.Plant your roses in a sunny location with good drainage.Fertilize them regularly for impressive flowers. Water them evenly to keep the soil moist."
            });


            addPlantModels.Add(new AddPlantModel
            {
                Name = "Hibiscus",
                ScientificName = "Hibiscus rosa-sinensis",
                DaysToGerminate = 21,
                DaysToHarvest = 85,
                SeedDepth = 2,
                IsPerennial = true,
                PlantHeightMax = 8,
                PlantWidthMax = 5,
                SeedSpacing = 48,
                RowSpacing = 60,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1599596852967-98d70b0ee789?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=375&q=80",
                ZoneID = 8,
                SeasonID = 3,
                PlantTypeID = 3,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 1,
                Temperature = "70 degrees + F",
                PlantDetailsDescription = "Hibiscus, (genus Hibiscus), genus of numerous species of herbs, shrubs, and trees in the mallow family (Malvaceae) that are native to warm temperate and tropical regions.",
                PlantCareDescription = "When hibiscus are in their blooming stage, they require large amounts of water. \n" +
                "Your hibiscus will need daily watering in warm weather.But once the weather cools,your hibiscus needs far less water and too much water can kill it.In the winter,water your hibiscus only when the soil is dry to the touch."
            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Tomato",
                ScientificName = "Solanum lycopersicum",
                DaysToGerminate = 21,
                DaysToHarvest = 90,
                SeedDepth = 1,
                IsPerennial = false,
                PlantHeightMax = 4,
                PlantWidthMax = 3,
                SeedSpacing = 18,
                RowSpacing = 24,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1565698228480-eec300c9679b?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=350&q=80",
                ZoneID = 8,
                SeasonID = 3,
                PlantTypeID = 17,
                SunExposureID = 2,
                WaterNeedID = 2,
                RootStructureID = 1,
                Temperature = "75 degrees + F",
                PlantDetailsDescription = "Tomato, flowering plant of the nightshade family, cultivated extensively for its edible fruits. The fruits are commonly eaten raw, served as a cooked vegetable",
                PlantCareDescription = "Water in the early morning so that plants have sufficient moisture to make it through a hot day.Water generously the first few days that the tomato seedlings or transplants are in the ground." 
               
            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Jalapeno ",
                ScientificName = "Capsicum annuum 'Jalapeño",
                DaysToGerminate = 21,
                DaysToHarvest = 90,
                SeedDepth = 1,
                IsPerennial = false,
                PlantHeightMax = 3,
                PlantWidthMax = 2,
                SeedSpacing = 12,
                RowSpacing = 16,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1587742497245-52afcc1df553?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=747&q=80",
                ZoneID = 8,
                SeasonID = 3,
                PlantTypeID = 17,
                SunExposureID = 2,
                WaterNeedID = 1,
                RootStructureID = 1,
                Temperature = "70 degrees + F",
                PlantDetailsDescription = "The jalapeño pepper is a medium-sized chili pepper. Mature jalapeños are 2 to 3 inches in length and are typically picked and consumed while still green.Occasionally, they are allowed to fully ripen and turn red in color.",
                PlantCareDescription = "Keep the soil constantly moist, but not soaking wet. Jalapeno peppers love water, but you don't want to inundate the plants, or you run the risk of rotting. Water every other day or every third day.Include a good plant food product for fertilizing." 


            });

            addPlantModels.Add(new AddPlantModel
            {
                Name = "Bell Pepper ",
                ScientificName = "Capsicum annuum",
                DaysToGerminate = 21,
                DaysToHarvest = 90,
                SeedDepth = 1,
                IsPerennial = false,
                PlantHeightMax = 3,
                PlantWidthMax = 2,
                SeedSpacing = 18,
                RowSpacing = 24,
                IsDeerResistant = false,
                IsToxicToAnimal = false,
                IsToxicToHuman = true,
                IsMedicinal = false,
                Image = "https://images.unsplash.com/photo-1563576803227-0d085f32f5b2?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=750&q=80",
                ZoneID = 8,
                SeasonID = 3,
                PlantTypeID = 17,
                SunExposureID = 2,
                WaterNeedID = 1,
                RootStructureID = 1,
                Temperature = "75 degrees + F",
                PlantDetailsDescription = "Bell pepper,, also called sweet pepper or capsicum, pepper cultivar in the nightshade family(Solanaceae),grown for its thick, mild fruits.",
                PlantCareDescription = "Water carefully. Bell peppers need a deep watering, about one to two inches per week.Provide sun,Use the right fertilizer."

            });

            foreach (AddPlantModel item in addPlantModels)
            {
                AddPlant(item);
            }

        }

        private void SeedSupportTables()
        {
            List<PlantSeasons> plantSeasons = new List<PlantSeasons>();
            plantSeasons.Add(new PlantSeasons { SeasonID = 1, Name = "Winter", Description = "Coldest part of the year", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantSeasons.Add(new PlantSeasons { SeasonID = 2, Name = "Spring", Description = "Starting to get warmer", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantSeasons.Add(new PlantSeasons { SeasonID = 3, Name = "Summer", Description = "Hottest part of the year", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantSeasons.Add(new PlantSeasons { SeasonID = 4, Name = "Fall", Description = "Starting to get colder", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            ctx.PlantSeasons.AddRange(plantSeasons);

            List<PlantZones> plantZones = new List<PlantZones>();
            plantZones.Add(new PlantZones { ZoneID = 1, ZoneCode = "1", Description = "-60 to -50 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 2, ZoneCode = "2", Description = "-50 to -40 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 3, ZoneCode = "3", Description = "-40 to -30 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 4, ZoneCode = "4", Description = "-30 to -20 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 5, ZoneCode = "5", Description = "-20 to -10 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 6, ZoneCode = "6", Description = "-10 to 0 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 7, ZoneCode = "7", Description = "0 to 10 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 8, ZoneCode = "8", Description = "10 to 20 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 9, ZoneCode = "9", Description = "20 to 30 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 10, ZoneCode = "10", Description = "30 to 40 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 11, ZoneCode = "11", Description = "40 to 50 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 12, ZoneCode = "12", Description = "50 to 60 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantZones.Add(new PlantZones { ZoneID = 13, ZoneCode = "13", Description = "60 to 70 (F)", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            ctx.PlantZones.AddRange(plantZones);

            List<PlantTypes> plantTypes = new List<PlantTypes>();
            plantTypes.Add(new PlantTypes { PlantTypeID = 1, Name = "Wildflower", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 2, Name = "Thistle", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 3, Name = "Flower", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 4, Name = "Herb", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 5, Name = "Mushroom", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 6, Name = "Weed", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 7, Name = "Fern", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 8, Name = "Cattail", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 9, Name = "Reed", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 10, Name = "Bamboo", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 11, Name = "Ivy", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 12, Name = "Shrub", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 13, Name = "Moss", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 14, Name = "Grass", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 15, Name = "Palm tree", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 16, Name = "Bush", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 17, Name = "Vegetable", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            plantTypes.Add(new PlantTypes { PlantTypeID = 18, Name = "Tree", Description = "They Grow", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            ctx.PlantTypes.AddRange(plantTypes);

            List<WaterNeeds> waterNeeds = new List<WaterNeeds>();
            waterNeeds.Add(new WaterNeeds { WaterNeedID = 1, Name = "High", Description = "Plants need from 60 to 100 percent of the water needed for a grass lawn", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            waterNeeds.Add(new WaterNeeds { WaterNeedID = 2, Name = "Moderate", Description = "Plants need 30 to 60 percent of the water needed for a grass lawn", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            waterNeeds.Add(new WaterNeeds { WaterNeedID = 3, Name = "Low", Description = "Plants need 10 to 30 percent of the water needed for a grass lawn", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            waterNeeds.Add(new WaterNeeds { WaterNeedID = 4, Name = "Very Low", Description = "Plants need 10 percent or less of the water needed for a grass lawn", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            ctx.WaterNeeds.AddRange(waterNeeds);

            List<RootStructure> rootStructures = new List<RootStructure>();
            rootStructures.Add(new RootStructure { RootStructureID = 1, Name = "Fibrous Roots", Description = "Fibrous roots are found in monocot plants. They are slender, branched, and grow directly from the stem. These roots tend to grow close to the surface and spread horizontally. They are characterized by a cluster-like appearance with numerous roots together, all nearly of the same size. In the fibrous root system, the primary root is short-lived. It is replaced by numerous roots. Fibrous root system does not provide strong anchorage to the plant as they do not penetrate deep into the soil.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            rootStructures.Add(new RootStructure { RootStructureID = 2, Name = "Taproots", Description = "Taproots are found in the majority of dicot plants. This type of root is a direct elongation of the radical. Unlike fibrous roots, taproots are not branches. Instead, they are a single primary root that grows deep into the soil. A taproot gives rise to lateral branches (secondary and tertiary roots) leading to the formation of a taproot system. The branches of a taproot grow in acropetal succession which means, the longer and older ones are present at the base while newer, shorter ones are near the apex of the primary root. Taproot system provides a very strong anchorage. The reason for this strong support is that they penetrate deep into the soil. In some plants, the taproot does not grow too deep. Instead, its lateral branches grow longer horizontally along the surface. These types of roots are called feeder roots.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            rootStructures.Add(new RootStructure { RootStructureID = 3, Name = "Adventitious Roots", Description = "Adventitious roots are similar to the fibrous roots. However, they can be underground or aerial (above the ground). They can grow from any part of the plant except the radical. Usually, they grow from intermodal, stem nodes, and leaves. These roots can be thick, thin, or modified according to the species. Adventitious roots arise under stress conditions such as waterlogging after floods. Leaf cuttings and branch cuttings in plants such as rose can result in the development of adventitious roots. They also develop in cases of plant injury. They can increase the survival chances of a plant as the plant propagates itself with the assistance of adventitious roots.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            rootStructures.Add(new RootStructure { RootStructureID = 4, Name = "Creeping Roots", Description = "Creeping roots are the types of roots that do not penetrate deep into the soil. They are shallow and spread a long way horizontally from the base of the plant. Many trees have creeping roots.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            rootStructures.Add(new RootStructure { RootStructureID = 5, Name = "Tuberous Roots", Description = "Tuberous roots are very thick roots. They store significant amounts of food to feed the whole plant. They are a fleshy, enlarged, and modified storage organ. They are modified from the stem. The propagation of tuberous roots is by crown division where each crown division has several buds and sufficient storage to make a new plant.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            rootStructures.Add(new RootStructure { RootStructureID = 6, Name = "Water Roots", Description = "Water roots are the types of roots that plants in water grow. They are finer and more brittle. They have a capability to allow oxygen from the atmosphere to diffuse in which is then used by the roots for metabolism and growth. They are morphologically different from soil roots.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            rootStructures.Add(new RootStructure { RootStructureID = 7, Name = "Parasite Roots", Description = "Parasite roots are types of roots that attach themselves to the other plant and suck nutrients from it. They do not offer any benefit to the host plant. Instead, they cause serious damage, hence the name, parasite roots.", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            ctx.RootStructure.AddRange(rootStructures);

            List<SunExposures> sunExposures = new List<SunExposures>();
            sunExposures.Add(new SunExposures { SunExposureID = 1, Name = "Full Sun", Description = "Plants need at least 6 hours of direct sun daily", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            sunExposures.Add(new SunExposures { SunExposureID = 2, Name = "Part Sun", Description = "Plants thrive with between 3 and 6 hours of direct sun per day", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            sunExposures.Add(new SunExposures { SunExposureID = 3, Name = "Part Shade", Description = "Plants require between 3 and 6 hours of sun per day, but need protection from intense mid-day sun", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            sunExposures.Add(new SunExposures { SunExposureID = 4, Name = "Full Shade", Description = "Plants require less than 3 hours of direct sun per day", CreatedDate = new DateTime(2021, 1, 1, 00, 00, 00) });
            ctx.SunExposures.AddRange(sunExposures);

            List<UserRoles> userRoles = new List<UserRoles>();
            userRoles.Add(new UserRoles { Name = "Admin", Description = "All powerful" });
            userRoles.Add(new UserRoles { Name = "Moderator", Description = "Can Update Some Tables" });
            userRoles.Add(new UserRoles { Name = "End User", Description = "Can Comment, Ask questions, add to MyPlants Table" });
            ctx.UserRoles.AddRange(userRoles);

            PasswordHasher ph = new PasswordHasher();
            //PasswordHasher<ApplicationUser> passwordHasher2 = new PasswordHasher<ApplicationUser>();
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "Administrator",
                Email = "admin@plantpeople.com",
                PasswordHash = ph.HashPassword("Password1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "Moderator",
                Email = "Moderator@plantpeople.com",
                PasswordHash = ph.HashPassword("Password1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 2,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "End User",
                Email = "Moderator@plantpeople.com",
                PasswordHash = ph.HashPassword("Password1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "gardenlover",
                Email = "gardenlover@garden.com",
                PasswordHash = ph.HashPassword("Test123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "Beyonce",
                Email = "QueenB@destinyschild.com",
                PasswordHash = ph.HashPassword("Password1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "Beyonce",
                Email = "QueenB@destinyschild.com",
                PasswordHash = ph.HashPassword("Password1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "@TerryBrown",
                Email = "FirstTimeInstructor@1150.org",
                PasswordHash = ph.HashPassword("Test@1150"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            applicationUsers.Add(new ApplicationUser
            {
                UserName = "Shirisha",
                Email = "Shirisha@gmail.com",
                PasswordHash = ph.HashPassword("Password0!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleID = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                Answer = false,
                WhenToWater = false,
                WhenToPlant = false,
                PlantBloom = false,
                PlantAdded = false
            });
            foreach (ApplicationUser user in applicationUsers)
            {
                ctx.Users.Add(user);
            }
            ctx.SaveChanges();
        }

        public void AddPlant(AddPlantModel model)
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
            ctx.SaveChanges();
        }
    }
}
