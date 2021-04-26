using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.AnswerModels
{
    public class AddAnswerModel
    {
        public int QuestionID { get; set; }
        [MaxLength(3000,ErrorMessage ="3000 char limit")]
        public string Answer { get; set; }

    }
}
