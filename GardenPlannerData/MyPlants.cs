using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class MyPlants
    {
        [Key]
        public int MyPlantID { get; set; }

        //[ForeignKey(nameof(Users))]
        public int UserID { get; set; }

       
        [MaxLength(500)]
        public string Location { get; set; }

        
        [ForeignKey(nameof(Plants))]
        public int PlantID{ get; set; }
        public virtual Plants Plants { get; set; }

        public DateTimeOffset DatePlanted { get; set; }

        public string Photo { get; set; }

        
        [MaxLength(3000)]
        public string Notes { get; set; }
        public int Year { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
