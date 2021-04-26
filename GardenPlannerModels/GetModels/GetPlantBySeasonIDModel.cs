using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.GetModels
{
    public class GetPlantBySeasonIDModel
    {
        [Range(1, 4, ErrorMessage = "Please enter a valid number between 1-4")]
        public int SeasonID { get; set; }
    }
}
