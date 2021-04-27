using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.QuestionModels
{
    public class AddQuestionModel
    {
        public int PlantID { get; set; }
        [MaxLength(1000, ErrorMessage ="1000 char limit")]
        public string Question { get; set; }
    }
}
