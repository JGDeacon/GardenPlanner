using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class PlantCare
    {
        [Key]
        public int PlantCareID { get; set; }

        [ForeignKey(nameof(SunExposure))]
        public int SunExposureID { get; set; }

        public SunExposures SunExposure { get; set; }

        [ForeignKey(nameof(WaterNeed))]
        public int WaterNeedID { get; set; }

        public WaterNeeds WaterNeed { get; set; }

        [Required]
        public string Temperature { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }



    }
}
