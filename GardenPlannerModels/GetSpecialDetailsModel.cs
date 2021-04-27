using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels
{
    public class GetSpecialDetailsModel
    {
        public bool IsDeerResistant { get; set; }

        public bool IsToxicToAnimal { get; set; }

        public bool IsToxicToHuman { get; set; }

        public bool IsMedicinal { get; set; }
    }
}
