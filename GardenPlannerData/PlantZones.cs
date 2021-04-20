using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class PlantZones
    {
        [Key]
        public int ZoneID { get; set; }

        [Required]
        public string ZoneCode { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? Modifiedate { get; set; }
    }
}
