using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class Plants
    {
        [Key]
        public int PlantID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ScientificName { get; set; }

        [ForeignKey(nameof(PlantZones))]
        public int ZoneID { get; set; }

        public PlantZones PlantZones { get; set; }

        [ForeignKey(nameof(PlantSeasons))]
        public int SeasonID { get; set; }

        public PlantSeasons PlantSeasons { get; set; }

        [ForeignKey(nameof(PlantTypes))]
        public int PlantTypeID { get; set; }

        public PlantTypes PlantTypes { get; set; }

        [ForeignKey(nameof(PlantCare))]
        public int PlantCareID { get; set; }

        public PlantCare PlantCare { get; set; }

        [ForeignKey(nameof(PlantDetails))]
        public int PlantDetailsID { get; set; }

        public PlantDetails PlantDetails { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
