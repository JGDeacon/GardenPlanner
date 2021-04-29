using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.GetModels
{
    public class GetPlantByZoneIDModel
    {
        [Range(1,13, ErrorMessage = "Please enter a valid number between 1-13")]
        public int ZoneID { get; set; }


    }
}
