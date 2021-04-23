using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
   public  class PlantDetails
   {
        [Key]
        public int PlantDetailsID { get; set; }

        [Required]
        public int DaysToGerminate { get; set; }

        public int DaysToHarvest { get; set; }

        public double SeedDepth { get; set; }

        public bool IsPerennial { get; set; }

        public double PlantHeightMax { get; set; }

        public double PlantWidthMax { get; set; }

        public double SeedSpacing { get; set; }

        public double RowSpacing { get; set; }


        [ForeignKey(nameof(RootStructure))]
        public int RootStructureID { get; set; }

        public RootStructure RootStructure { get; set; }

        public bool IsDeerResistant { get; set; }

        public bool IsToxicToAnimal { get; set; }

        public bool IsToxicToHuman { get; set; }

        public bool IsMedicinal { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Image { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
